using Microsoft.AspNetCore.Mvc;
using WEB_APP_Panaderia.Entities;
using WEB_APP_Panaderia.Interfaces;
using WEB_APP_Panaderia.Models;

namespace WEB_APP_Panaderia.Controllers
{
	public class InsumosController : Controller
	{
		private readonly IInsumosModel _InsumosModel;

		public InsumosController(IInsumosModel InsumosModel)
		{
			_InsumosModel = InsumosModel;
		}
		[HttpGet]
        public IActionResult ConsultaInsumos()
        {
            try
            {
                var viewModel = new InsumosViewModel
                {
                    Insumos = _InsumosModel.ConsultaInsumos(),
                    Insumo = new InsumosEntities()

                };
                if (viewModel.Insumos == null || viewModel.Insumos.Count() == 0)
                {
                    ViewData["Message"] = "No hay registros de Insumos.";
                }

                return View(viewModel);
            }
            catch (Exception ex)
            {

                return View("Error");
            }
        }

		[HttpGet]
		public IActionResult ConsultaUnRegistroInsumo(int idInsumo)
		{
			var reporte = _InsumosModel.ConsultaUnRegistroInsumo(idInsumo);
			return Json(reporte);
		}


		[HttpGet]
		public IActionResult ConsultaPresentacion()
		{
			var reporte = _InsumosModel.ConsultaPresentacion();

			return Json(reporte);
		}


		[HttpGet]
		public IActionResult ConsultaTipoInsumo()
		{
			return Json(_InsumosModel.ConsultaTipoInsumo());
		}

		[HttpGet]
		public IActionResult ConsultaCategoriaInsumo()
		{
			return Json(_InsumosModel.ConsultaCategoriaInsumo());
		}

		[HttpGet]
		public IActionResult ConsultaMarca()
		{
			return Json(_InsumosModel.ConsultaMarca());
		}


		public IActionResult AgregarInsumos(InsumosEntities reporte)
		{
			try
			{
				_InsumosModel.AgregarInsumos(reporte);
				return RedirectToAction("ConsultaInsumos");
			}
			catch (Exception ex)
			{
				return Json(new { success = false, message = ex.Message });
			}

		}

		public IActionResult AgregarPresentacion(PresentacionEntities reporte)
		{
			try
			{
				_InsumosModel.AgregarPresentacion(reporte);
				return RedirectToAction("Insumos");
			}
			catch (Exception ex)
			{
				return Json(new { success = false, message = ex.Message });
			}

		}
		public IActionResult AgregarTipoInsumo(TipoInsumoEntities reporte)
		{
			try
			{
				_InsumosModel.AgregarTipoInsumo(reporte);
				return RedirectToAction("Insumos");
			}
			catch (Exception ex)
			{
				return Json(new { success = false, message = ex.Message });
			}

		}
		public IActionResult AgregarCategoriaInsumo(CategoriaInsumosEntities reporte)
		{
			try
			{
				_InsumosModel.AgregarCategoriaInsumo(reporte);
				return RedirectToAction("Insumos");
			}
			catch (Exception ex)
			{
				return Json(new { success = false, message = ex.Message });
			}

		}
		public IActionResult AgregarMarca(MarcaEntities reporte)
		{
			try
			{
				_InsumosModel.AgregarMarca(reporte);
				return RedirectToAction("Insumos");
			}
			catch (Exception ex)
			{
				return Json(new { success = false, message = ex.Message });
			}

		}

		[HttpPost]
		public IActionResult ActualizarInsumos(InsumosEntities reporte)
		{
			try
			{
				_InsumosModel.ActualizarInsumos(reporte);
				return RedirectToAction("ConsultaInsumos");
			}
			catch (Exception ex)
			{
				return Json(new { success = false, message = ex.Message });
			}
		}

		[HttpPost]
		public IActionResult ActualizarPresentacion(PresentacionEntities reporte)
		{
			try
			{
				_InsumosModel.ActualizarPresentacion(reporte);
				return RedirectToAction("Insumos");
			}
			catch (Exception ex)
			{
				return Json(new { success = false, message = ex.Message });
			}
		}

		[HttpPost]
		public IActionResult ActualizarTipoInsumo(TipoInsumoEntities reporte)
		{
			try
			{
				_InsumosModel.ActualizarTipoInsumo(reporte);
				return RedirectToAction("Insumos");
			}
			catch (Exception ex)
			{
				return Json(new { success = false, message = ex.Message });
			}
		}

		[HttpPost]
		public IActionResult ActualizarCategoriaInsumo(CategoriaInsumosEntities reporte)
		{
			try
			{
				_InsumosModel.ActualizarCategoriaInsumo(reporte);
				return RedirectToAction("Insumos");
			}
			catch (Exception ex)
			{
				return Json(new { success = false, message = ex.Message });
			}
		}

		[HttpPost]
		public IActionResult ActualizarMarca(MarcaEntities reporte)
		{
			try
			{
				_InsumosModel.ActualizarMarca(reporte);
				return RedirectToAction("Insumos");
			}
			catch (Exception ex)
			{
				return Json(new { success = false, message = ex.Message });
			}
		}

		[HttpPost]
		public IActionResult BorrarInsumos(int idInsumo)
		{
			try
			{
				_InsumosModel.BorrarInsumos(idInsumo);
				return Json(new { success = true });
			}
			catch (Exception ex)
			{
				return Json(new { success = false, message = ex.Message });
			}
		}

		[HttpPost]
		public IActionResult BorrarPresentacion(int idPresentacion)
		{
			try
			{
				_InsumosModel.BorrarPresentacion(idPresentacion);
				return Json(new { success = true });
			}
			catch (Exception ex)
			{
				return Json(new { success = false, message = ex.Message });
			}
		}

		[HttpPost]
		public IActionResult BorrarTipoInsumo(int idTipoInsumo)
		{
			try
			{
				_InsumosModel.BorrarTipoInsumo(idTipoInsumo);
				return Json(new { success = true });
			}
			catch (Exception ex)
			{
				return Json(new { success = false, message = ex.Message });
			}
		}

		[HttpPost]
		public IActionResult BorrarCategoriaInsumo(int idCategoriaInsumo)
		{
			try
			{
				_InsumosModel.BorrarCategoriaInsumo(idCategoriaInsumo);
				return Json(new { success = true });
			}
			catch (Exception ex)
			{
				return Json(new { success = false, message = ex.Message });
			}
		}

		[HttpPost]
		public IActionResult BorrarMarca(int idMarca)
		{
			try
			{
				_InsumosModel.BorrarCategoriaInsumo(idMarca);
				return Json(new { success = true });
			}
			catch (Exception ex)
			{
				return Json(new { success = false, message = ex.Message });
			}
		}



	}
}
