using iText.Kernel.Pdf;
using iText.Layout.Element;
using iText.Layout.Properties;
using WEB_APP_Panaderia.Entities;
using WEB_APP_Panaderia.Interfaces;
using iText.Layout;
using iText.IO.Image;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace WEB_APP_Panaderia.Models
{
	public class RegistroDesechosModel : IRegistroDesechosModel
	{
		private readonly IConfiguration _configuration;
		private readonly IHttpContextAccessor _contextAccessor;

		public RegistroDesechosModel(IConfiguration configuration, IHttpContextAccessor contextAccessor)
		{
			_configuration = configuration;
			_contextAccessor = contextAccessor;
		}

		public List<RegistroDesechosEntities>? ConsultarRegistroDesechos() 
		{
			using (var client = new HttpClient()) 
			{
				string urlApi = _configuration.GetSection("Parametros:urlApi").Value + "/RegistroDesechos/ConsultarRegistroDesechos";
				HttpResponseMessage response = client.GetAsync(urlApi).Result;

				if (response.IsSuccessStatusCode) 
				{
					var result = response.Content.ReadFromJsonAsync<List<RegistroDesechosEntities>>().Result;
					return result;
				}

				if(response.StatusCode == System.Net.HttpStatusCode.BadRequest)
				
					throw new Exception("Excepción Web Api: " + response.Content.ReadAsStringAsync().Result);
				return new List<RegistroDesechosEntities>();
			}
		
		}

		public void ActualizarRegistroDesecho(RegistroDesechosEntities reporte)
		{
			using (var client = new HttpClient())
			{
				string urlApi = _configuration.GetSection("Parametros:urlApi").Value + "/RegistroDesechos/ActualizarRegistroDesechos";

				JsonContent body = JsonContent.Create(reporte);
				HttpResponseMessage response = client.PostAsync(urlApi, body).Result;

			}
		}

		public RegistroDesechosEntities? ConsultarUnRegistroDesecho(int id)
		{
			using (var client = new HttpClient())
			{
				string urlApi = _configuration.GetSection("Parametros:urlApi").Value + $"/RegistroDesechos/ConsultarUnRegistroDesecho/{id}";
				HttpResponseMessage response = client.GetAsync(urlApi).Result;

				if (response.IsSuccessStatusCode)
				{
					var result = response.Content.ReadFromJsonAsync<RegistroDesechosEntities>().Result;
					return result;
				}

				if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
					throw new Exception("Excepción Web Api: " + response.Content.ReadAsStringAsync().Result);

				return null;
			}
		}


		public List<CategoriaDesechoEntities> ConsultarCategoriaDesecho()
		{
			using (var client = new HttpClient())
			{
				string urlApi = _configuration.GetSection("Parametros:urlApi").Value + "/RegistroDesechos/ConsultarCategoriaDesecho";
				HttpResponseMessage response = client.GetAsync(urlApi).Result;

				if (response.IsSuccessStatusCode)
				{
					var result = response.Content.ReadFromJsonAsync<List<CategoriaDesechoEntities>>().Result;
					return result;
				}

				if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)

					throw new Exception("Excepción Web Api: " + response.Content.ReadAsStringAsync().Result);
				return new List<CategoriaDesechoEntities>();
			}

		}

		public List<CategoriaDesechoTratamientoEntities> ConsultarCategoriaDesechoTratamiento()
		{
			using (var client = new HttpClient())
			{
				string urlApi = _configuration.GetSection("Parametros:urlApi").Value + "/RegistroDesechos/ConsultarCategoriaDesechoTratamiento";
				HttpResponseMessage response = client.GetAsync(urlApi).Result;

				if (response.IsSuccessStatusCode)
				{
					var result = response.Content.ReadFromJsonAsync<List<CategoriaDesechoTratamientoEntities>>().Result;
					return result;
				}

				if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)

					throw new Exception("Excepción Web Api: " + response.Content.ReadAsStringAsync().Result);
				return new List<CategoriaDesechoTratamientoEntities>();
			}

		}

		public List<DesechoDisposicionFinalEntities> ConsultarDesechoDisposicionFinal()
		{
			using (var client = new HttpClient())
			{
				string urlApi = _configuration.GetSection("Parametros:urlApi").Value + "/RegistroDesechos/ConsultarDesechoDisposicionFinal";
				HttpResponseMessage response = client.GetAsync(urlApi).Result;

				if (response.IsSuccessStatusCode)
				{
					var result = response.Content.ReadFromJsonAsync<List<DesechoDisposicionFinalEntities>>().Result;
					return result;
				}

				if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)

					throw new Exception("Excepción Web Api: " + response.Content.ReadAsStringAsync().Result);
				return new List<DesechoDisposicionFinalEntities>();
			}

		}

		public List<DesechoTransporteEntities> ConsultarDesechoTransporte()
		{
			using (var client = new HttpClient())
			{
				string urlApi = _configuration.GetSection("Parametros:urlApi").Value + "/RegistroDesechos/ConsultarDesechoTransporte";
				HttpResponseMessage response = client.GetAsync(urlApi).Result;

				if (response.IsSuccessStatusCode)
				{
					var result = response.Content.ReadFromJsonAsync<List<DesechoTransporteEntities>>().Result;
					return result;
				}

				if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)

					throw new Exception("Excepción Web Api: " + response.Content.ReadAsStringAsync().Result);
				return new List<DesechoTransporteEntities>();
			}

		}

		public int AgregarRegistroDesecho(RegistroDesechosEntities reporte)
		{
			using (var client = new HttpClient())
			{
				string urlApi = _configuration.GetSection("Parametros:urlApi").Value + "/RegistroDesechos/AgregarRegistroDesechos";

				JsonContent body = JsonContent.Create(reporte);
				HttpResponseMessage response = client.PostAsync(urlApi, body).Result;

				if (response.IsSuccessStatusCode)
					return response.Content.ReadFromJsonAsync<int>().Result;

				if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
					throw new Exception("Excepción Web Api: " + response.Content.ReadAsStringAsync().Result);

				return 0;
			}
		}

		public void EliminarRegistroDesechos(int idEvento)
		{
			using (var client = new HttpClient())
			{
				string urlApi = _configuration.GetSection("Parametros:urlApi").Value + "/RegistroDesechos/EliminarRegistroDesechos";

				JsonContent body = JsonContent.Create(idEvento);
				HttpResponseMessage response = client.PostAsync(urlApi, body).Result;

				if (!response.IsSuccessStatusCode)
					throw new Exception("Excepción Web Api: " + response.Content.ReadAsStringAsync().Result);
			}
		}


		public byte[] GenerarPdfRegistroDesechos(List<RegistroDesechosEntities> registros)
		{
			//List<RegistroDesechosEntities> registros = ConsultarRegistroDesechos();

			using (var stream = new MemoryStream())
			{
				var writer = new PdfWriter(stream);
				var pdf = new PdfDocument(writer);
				var document = new Document(pdf);

				// Agregar el título
				var title = new Paragraph("Reporte de Desechos")
					.SetTextAlignment(TextAlignment.CENTER)
					.SetFontSize(20)
					.SetBold()
					.SetMarginBottom(20);
				document.Add(title);

				// Agregar el logo
				//var logoPath = "/assets/img/logo-pizzeria-panaderia.png"; // Ruta al archivo
				//var logo = new Image(ImageDataFactory.Create(logoPath))
				//	.SetHorizontalAlignment(HorizontalAlignment.CENTER)
				//	.SetMarginBottom(20);
				//document.Add(logo);

				Table table = new Table(new float[] { 1, 2, 2, 2, 2, 1.5f, 1.5f });
				table.SetWidth(UnitValue.CreatePercentValue(100));

				table.AddHeaderCell("ID Evento");
				table.AddHeaderCell("Tratamiento Residuo");
				table.AddHeaderCell("Disposición Final");
				table.AddHeaderCell("Transporte");
				table.AddHeaderCell("Fecha Revisión");
				table.AddHeaderCell("Categoría");
				table.AddHeaderCell("Usuario");

				foreach (var item in registros)
				{
					table.AddCell(item.idEvento.ToString());
					table.AddCell(item.tratamientoResiduo);
					table.AddCell(item.disposicionFinal);
					table.AddCell(item.transporte);
					table.AddCell(item.fechaRevision.ToString("dd/MM/yyyy"));
					table.AddCell(item.categoria);
					table.AddCell(item.usuario);
				}

				document.Add(table);
				document.Close();
				writer.Close();

				return stream.ToArray();
			}
		}

		public byte[] GenerarExcelRegistroDesechos(List<RegistroDesechosEntities> registros)
		{
			ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
			using (var package = new ExcelPackage())
			{
				var worksheet = package.Workbook.Worksheets.Add("RegistroDesechos");

				// Agregar el título
				worksheet.Cells["A1"].Value = "Reporte de Desechos";
				worksheet.Cells["A1"].Style.Font.Size = 20;
				worksheet.Cells["A1"].Style.Font.Bold = true;
				worksheet.Cells["A1:H1"].Merge = true;
				worksheet.Cells["A1:H1"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

				//// Agregar el logo
				//var logoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "logo.png"); // Ruta a tu archivo de logo
				//var logo = worksheet.Drawings.AddPicture("Logo", new FileInfo(logoPath));
				//logo.SetPosition(1, 0, 8, 0); // Ajusta la posición según sea necesario

				// Crear encabezados
				worksheet.Cells["A3"].Value = "ID Evento";
				worksheet.Cells["B3"].Value = "Tratamiento Residuo";
				worksheet.Cells["C3"].Value = "Disposición Final";
				worksheet.Cells["D3"].Value = "Transporte";
				worksheet.Cells["E3"].Value = "Fecha Revisión";
				worksheet.Cells["F3"].Value = "Categoría";
				worksheet.Cells["G3"].Value = "Usuario";

				using (var range = worksheet.Cells["A3:G3"])
				{
					range.Style.Font.Bold = true;
					range.Style.Fill.PatternType = ExcelFillStyle.Solid;
					range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
				}

				// Agregar datos
				for (int i = 0; i < registros.Count; i++)
				{
					var row = i + 4;
					var registro = registros[i];

					worksheet.Cells[row, 1].Value = registro.idEvento;
					worksheet.Cells[row, 2].Value = registro.tratamientoResiduo;
					worksheet.Cells[row, 3].Value = registro.disposicionFinal;
					worksheet.Cells[row, 4].Value = registro.transporte;
					worksheet.Cells[row, 5].Value = registro.fechaRevision.ToString("dd/MM/yyyy");
					worksheet.Cells[row, 6].Value = registro.categoria;
					worksheet.Cells[row, 7].Value = registro.usuario;
				}

				// Auto-ajustar el tamaño de las columnas
				worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

				return package.GetAsByteArray();
			}
		}


	}
}
