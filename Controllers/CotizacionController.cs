using iText.Kernel.Pdf.Canvas.Parser.Listener;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using System.Text;
using WEB_APP_Panaderia.Models;
using WEB_APP_Panaderia.Interfaces;
using WEB_APP_Panaderia.Services;
using WEB_APP_Panaderia.Entities;

namespace WEB_APP_Panaderia.Controllers
{
    public class CotizacionController : Controller
    {
        private readonly ICotizacionesModel _cotizacionModel;
        private readonly IConfiguration _configuration;
        private readonly GeminiService _geminiService;

        public CotizacionController(ICotizacionesModel cotizacionModel, IConfiguration configuration, GeminiService geminiService)
        {
            _cotizacionModel = cotizacionModel;
            _configuration = configuration;
            _geminiService = geminiService;
        }
        [TypeFilter(typeof(JwtAuthorizationFilter))]
        public async Task<IActionResult> CotizacionesLista()
        {
            try
            {
                var viewModel = new ViewModelCotizacion
                {
                    Cotizaciones = _cotizacionModel.ObtenerTodasLasCotizaciones(),
                    Cotizacion = new CotizacionEntity()
                };
                if (CotizacionesLista == null)
                {
                    ViewData["Message"] = "No hay registros de cotizaciones.";
                }

                return View(viewModel);
            }
            catch (Exception ex)
            {

                return View("Error");
            }
        }

        [TypeFilter(typeof(JwtAuthorizationFilter))]
        [HttpPost]
        public IActionResult RegistrarCotizaciones(CotizacionEntity cotizacion)
        {
            try
            {
                _cotizacionModel.RegistrarCotizaciones(cotizacion);
                return RedirectToAction("CotizacionesLista");
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }

        }

        //var cotizaciones = await _cotizacionModel.ObtenerTodasLasCotizaciones();
        // return View("CotizacionesLista");






        [TypeFilter(typeof(JwtAuthorizationFilter))]
        [HttpPost]
        public async Task<IActionResult> ImportarCotizacion(IFormFile pdfFile, string nombreProveedor)
        {
            if (pdfFile == null || pdfFile.Length == 0)
            {
                ModelState.AddModelError("", "Por favor, seleccione un archivo PDF.");
                return View();
            }

            // Extract text from PDF
            string pdfText = await ExtractTextFromPdfAsync(pdfFile);

            // Analyze the text of the PDF to obtain Nombre_Producto and Precio_Por_Kg
            var (nombreProducto, precioPorKg) = AnalyzePdfText(pdfText);

            // Get all existing quotes
            var cotizacionesExistentes = _cotizacionModel.ObtenerTodasLasCotizaciones();

            // Prepare the prompt for Gemini
            string prompt = PrepareGeminiPrompt(pdfText, cotizacionesExistentes);

            // Get recommendation from Gemini
            string recomendacion = await _geminiService.GetRecommendation(prompt);

            // Create new quote
            var nuevaCotizacion = new CotizacionEntity
            {
                Nombre_Producto = nombreProducto,
                Precio_Por_Kg = precioPorKg,
                Nombre_Proveedor = nombreProveedor,
                Recomendacion = recomendacion
            };

            // Import the quote
            //await _cotizacionModel.ImportarCotizacionConPdf(nuevaCotizacion, pdfText);

            // Update existing recommendations if necessary
            await UpdateExistingRecommendations(cotizacionesExistentes, nuevaCotizacion);

            return RedirectToAction("Index");
        }

        [TypeFilter(typeof(JwtAuthorizationFilter))]
        private async Task<string> ExtractTextFromPdfAsync(IFormFile pdfFile)
        {
            using (var stream = new MemoryStream())
            {
                await pdfFile.CopyToAsync(stream);
                stream.Position = 0;

                using (var reader = new PdfReader(stream))
                using (var document = new PdfDocument(reader))
                {
                    var strategy = new SimpleTextExtractionStrategy();
                    var text = new StringBuilder();

                    for (int i = 1; i <= document.GetNumberOfPages(); i++)
                    {
                        var page = document.GetPage(i);
                        text.Append(PdfTextExtractor.GetTextFromPage(page, strategy));
                    }

                    return text.ToString();
                }
            }
        }
        [TypeFilter(typeof(JwtAuthorizationFilter))]
        private (string nombreProducto, decimal precioPorKg) AnalyzePdfText(string pdfText)
        {
            // Implement the analysis of the PDF text to extract Nombre_Producto and Precio_Por_Kg
            // This is a simplified example
            var match = Regex.Match(pdfText, @"Producto:\s*(.+)\s*Precio:\s*(\d+(?:\.\d{1,2})?)");
            if (match.Success)
            {
                return (match.Groups[1].Value.Trim(), decimal.Parse(match.Groups[2].Value));
            }
            return ("Producto Desconocido", 0m);
        }

        private string PrepareGeminiPrompt(string pdfText, List<CotizacionEntity> cotizacionesExistentes)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Analice los datos extraídos del PDF que son estos:");
            sb.AppendLine(pdfText);
            sb.AppendLine("\nCompare con los siguientes productos existentes:");
            foreach (var cotizacion in cotizacionesExistentes)
            {
                sb.AppendLine($"{cotizacion.Nombre_Producto}: {cotizacion.Precio_Por_Kg:C} por kg");
            }
            sb.AppendLine("\nGenere una recomendación en base al precio y nombre de producto, si es recomendable adquirir alguno de los productos de la cotización importada o si caso contrario lo existente en el sistema es mejor que lo que estamos importando en el PDF.");
            return sb.ToString();
        }

        [TypeFilter(typeof(JwtAuthorizationFilter))]
        private async Task UpdateExistingRecommendations(List<CotizacionEntity> existingCotizaciones, CotizacionEntity newCotizacion)
        {
            foreach (var cotizacion in existingCotizaciones)
            {
                if (cotizacion.Nombre_Producto == newCotizacion.Nombre_Producto)
                {
                    string prompt = $"Compare estos dos productos:\n" +
                                    $"1. {cotizacion.Nombre_Producto} a {cotizacion.Precio_Por_Kg:C} por kg del proveedor {cotizacion.Nombre_Proveedor}\n" +
                                    $"2. {newCotizacion.Nombre_Producto} a {newCotizacion.Precio_Por_Kg:C} por kg del proveedor {newCotizacion.Nombre_Proveedor}\n" +
                                    $"¿Cuál es más recomendable y por qué?";

                    string nuevaRecomendacion = await _geminiService.GetRecommendation(prompt);

                    cotizacion.Recomendacion = nuevaRecomendacion;
                    await _cotizacionModel.ActualizarCotizacion(cotizacion);
                }
            }
        }
        [TypeFilter(typeof(JwtAuthorizationFilter))]
        [HttpGet]
        public async Task<IActionResult> ObtenerCotizacion(int id)
        {
            var cotizacion = await _cotizacionModel.ObtenerCotizacionPorId(id);
            if (cotizacion == null)
            {
                return NotFound();
            }
            return Json(cotizacion);
        }
        [TypeFilter(typeof(JwtAuthorizationFilter))]
        [HttpPost]
        public async Task<IActionResult> ActualizarCotizacion(CotizacionEntity cotizacion)
        {

            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .Select(x => new { x.Key, x.Value.Errors })
                    .ToArray();

                foreach (var error in errors)
                {
                    Console.WriteLine($"Key: {error.Key}, Errors: {string.Join(", ", error.Errors.Select(e => e.ErrorMessage))}");
                }
            }
            if (ModelState.IsValid)
            {
                await _cotizacionModel.ActualizarCotizacion(cotizacion);
                return Ok();
            }
            return BadRequest(ModelState);
        }
        [TypeFilter(typeof(JwtAuthorizationFilter))]
        [HttpPost]
        public async Task<IActionResult> EliminarCotizacion(int id)
        {
            await _cotizacionModel.EliminarCotizacion(id);
            return Ok();
        }
    }
}