using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OfficeOpenXml.Export.HtmlExport.StyleCollectors.StyleContracts;
using System.Net;
using System.Web.Mvc;
using WEB_APP_Panaderia.Entities;
using WEB_APP_Panaderia.Interfaces;
using WEB_APP_Panaderia.Models;

namespace WEB_APP_Panaderia.Controllers
{
	public class OrdenPDVController : Microsoft.AspNetCore.Mvc.Controller
	{
		private readonly IOrdenPDVModel _ordenModel;
        private readonly IConfiguration _configuration;

        public OrdenPDVController(IOrdenPDVModel ordenModel, IConfiguration configuration)
        {
            _ordenModel = ordenModel;
            _configuration = configuration;
        }



        [Microsoft.AspNetCore.Mvc.HttpPost]
        public Microsoft.AspNetCore.Mvc.ActionResult CompletarOrden([FromBody] OrdenEntities orden)
        {
            try
            {
                if (orden.metodoPago == "Tarjeta")
                {
					//TempData["OrdenTemp"] = orden;
					HttpContext.Session.SetString("OrdenTemp", JsonConvert.SerializeObject(orden));
					var stripeSessionResult = CreateStripeSession(orden);
                    return Json(new { success = true, requiresStripe = true, sessionId = stripeSessionResult.Item1, stripeUrl = stripeSessionResult.Item2 });
                }

                var result = _ordenModel.CompletarOrden(orden);
                if (result != null)
                {
                    return Json(new { success = true, idOrden = result.idOrden, nombre = result.ClienteOrden.nombre });
                }
                else
                {
                    return Json(new { success = false, message = "No se pudo completar la orden." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        private Tuple<string, string> CreateStripeSession(OrdenEntities orden)
        {
            using (var client = new WebClient())
            {
				//string urlApi = _configuration.GetSection("Parametros:urlApi").Value + "/Usuarios/GetAllUsers";
				string urlApi = _configuration.GetSection("Parametros:urlApi").Value + "/Stripe/CreateCheckoutSession";
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                string jsonOrder = JsonConvert.SerializeObject(orden);
                string response = client.UploadString(urlApi, jsonOrder);
                dynamic result = JsonConvert.DeserializeObject<dynamic>(response);
                return Tuple.Create(result.sessionId.ToString(), result.url.ToString());
            }
        }

		[Microsoft.AspNetCore.Mvc.HttpGet]
		public Microsoft.AspNetCore.Mvc.ActionResult CheckStripePaymentStatus(string sessionId)
		{
			using (var client = new WebClient())
			{
				string urlApi = _configuration.GetSection("Parametros:urlApi").Value + "/Stripe/CheckPaymentStatus?sessionId=" + sessionId;

				try
				{
					string response = client.DownloadString(urlApi);
					dynamic result = JsonConvert.DeserializeObject<dynamic>(response);

					if (result.isPaid == true)
					{
						var ordenJson = HttpContext.Session.GetString("OrdenTemp");
						var orden = JsonConvert.DeserializeObject<OrdenEntities>(ordenJson);

						if (orden != null)
						{
							var orderResult = _ordenModel.CompletarOrden(orden);

							if (orderResult != null)
							{
								return Json(new { success = true, isPaid = true, idOrden = orderResult.idOrden, nombre = orderResult.ClienteOrden.nombre });
							}
						}
						return Json(new { success = true, isPaid = true, message = "Pago completado pero no se pudo recuperar la orden para completar." });
					}
					else
					{
						return Json(new { isPaid = false, message = "Pago no completado o no se pudo verificar el estado del pago." });
					}
				}
				catch (Exception ex)
				{
					return Json(new { isPaid = false, message = "Error al verificar el estado del pago: " + ex.Message });
				}
			}
		}

		public IActionResult OrdenesPizzeria()
		{
			try
			{
				var viewModel = new OrdenesPizzeriaViewModel
				{
					Ordenes = _ordenModel.OrdenesPizzeria(),
					Orden = new DetalleOrdenEntities()
				};
				if (viewModel.Ordenes == null || viewModel.Ordenes.Count() == 0)
				{
					ViewData["Message"] = "No hay ordenes pendientes, ¡Buen trabajo!.";
				}

				return View(viewModel);
			}
			catch (Exception ex)
			{
				return View("Error");
			}
		}

		[System.Web.Mvc.HttpGet]
		public IActionResult ConsultarDetalleOrdenPDV(int id)
		{
			var detalleOrden = _ordenModel.ConsultarDetalleOrden(id);
			if (detalleOrden == null)
			{
				return NotFound(new { message = "No se encontraron detalles para la orden." });
			}
			return Json(detalleOrden);
		}

		[System.Web.Mvc.HttpGet]
		public IActionResult ConsultarEstadosOrden()
		{
			return Json(_ordenModel.ConsultarEstadosOrden());
		}

		[System.Web.Mvc.HttpPost]
		public IActionResult ActualizarEstadoOrden([FromBody] DetalleOrdenEntities orden)
		{
			try
			{
				_ordenModel.ActualizarEstadoOrden(orden);
				return Json(new { success = true });
			}
			catch (Exception ex)
			{
				return Json(new { success = false, message = ex.Message });
			}
		}

	}
}
