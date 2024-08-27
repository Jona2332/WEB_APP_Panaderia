using Microsoft.AspNetCore.Mvc;
using WEB_APP_Panaderia.Entities;
using WEB_APP_Panaderia.Interfaces;
using WEB_APP_Panaderia.Models;

namespace WEB_APP_Panaderia.Controllers
{
	public class RegistroDesechosController : Controller
	{

		private readonly IRegistroDesechosModel _registroDesechosModel;

		public RegistroDesechosController(IRegistroDesechosModel bitacoraModel)
		{
			_registroDesechosModel = bitacoraModel;
		}

		public IActionResult RegistroDesechos()
		{
			try
			{
				var viewModel = new RegistroDesechosViewModel
				{
					Reportes = _registroDesechosModel.ConsultarRegistroDesechos(),
					Reporte = new RegistroDesechosEntities()
				};
				if (viewModel.Reportes == null || viewModel.Reportes.Count() == 0)
				{
					ViewData["Message"] = "No hay registros de desechos.";
				}

				return View(viewModel);
			}
			catch (Exception ex)
			{
				return View("Error");
			}
		}

		public IActionResult AgregarRegistroDesecho(RegistroDesechosEntities reporte)
		{
			try
			{
				_registroDesechosModel.AgregarRegistroDesecho(reporte);
				return RedirectToAction("RegistroDesechos");
			}
			catch (Exception ex)
			{
				return Json(new { success = false, message = ex.Message });
			}

		}

		[HttpPost]
		public IActionResult ActualizarRegistroDesecho(RegistroDesechosEntities reporte)
		{
			try
			{
				_registroDesechosModel.ActualizarRegistroDesecho(reporte);
				return RedirectToAction("RegistroDesechos");
			}
			catch (Exception ex)
			{
				return Json(new { success = false, message = ex.Message });
			}
		}

		[HttpPost]
		public IActionResult EliminarRegistroDesechos(int idEvento)
		{
			try
			{
				_registroDesechosModel.EliminarRegistroDesechos(idEvento);
				return Json(new { success = true });
			}
			catch (Exception ex)
			{
				return Json(new { success = false, message = ex.Message });
			}
		}



		[HttpGet]
		public IActionResult ConsultarUnRegistroDesecho(int id)
		{
			var reporte = _registroDesechosModel.ConsultarUnRegistroDesecho(id);
			return Json(reporte);
		}



		[HttpGet]
		public IActionResult ConsultarCategoriaDesecho()
		{
			return Json(_registroDesechosModel.ConsultarCategoriaDesecho());
		}

		[HttpGet]
		public IActionResult ConsultarCategoriaDesechoTratamiento()
		{
			return Json(_registroDesechosModel.ConsultarCategoriaDesechoTratamiento());
		}

		[HttpGet]
		public IActionResult ConsultarDesechoDisposicionFinal()
		{
			return Json(_registroDesechosModel.ConsultarDesechoDisposicionFinal());
		}

		[HttpGet]
		public IActionResult ConsultarDesechoTransporte()
		{
			return Json(_registroDesechosModel.ConsultarDesechoTransporte());
		}


		public IActionResult RegistroDesechosPdf()
		{

			var resultado = _registroDesechosModel.ConsultarRegistroDesechos();

			if (resultado == null || resultado.Count == 0)
			{
				return NotFound("No se encontraron registros.");
			}

			var pdfBytes = _registroDesechosModel.GenerarPdfRegistroDesechos(resultado);

			return File(pdfBytes, "application/pdf", "ReporteDesechos.pdf");
		}

		public IActionResult RegistroDesechosExcel()
		{
			var resultado = _registroDesechosModel.ConsultarRegistroDesechos();

			if (resultado == null || resultado.Count == 0)
			{
				return NotFound("No se encontraron registros.");
			}

			var excelBytes = _registroDesechosModel.GenerarExcelRegistroDesechos(resultado);

			return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "RegistroDesechos.xlsx");
		}




	}

}

