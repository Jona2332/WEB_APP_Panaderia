using Microsoft.AspNetCore.Mvc;
using WEB_APP_Panaderia.Interfaces;

namespace WEB_APP_Panaderia.Controllers
{
	public class MetricasController : Controller
	{
		private readonly IMetricasModel _metricasModel;

		public MetricasController(IMetricasModel metricasModel)
		{
			_metricasModel = metricasModel;
		}

		[HttpGet]
		public IActionResult ConsultarConteoVentasPorSemana()
		{
			return Json(_metricasModel.ConsultarConteoVentasPorSemana());
		}

		[HttpGet]
		public IActionResult ConsultarConteoVentasPorDia()
		{
			return Json(_metricasModel.ConsultarConteoVentasPorDia());
		}

		[HttpGet]
		public IActionResult ConsultarTotalVentasProductoPorDia()
		{
			return Json(_metricasModel.ConsultarTotalVentasProductoPorDia());
		}

		[HttpGet]
		public IActionResult ConsultarTransaccionesRecientes()
		{
			return Json(_metricasModel.ConsultarTransaccionesRecientes());
		}

		[HttpGet]
		public IActionResult ConsultarGanancias()
		{
			return Json(_metricasModel.ConsultarGanancias());
		}

		[HttpGet]
		public IActionResult ConsultarRestanteLotes()
		{
			return Json(_metricasModel.ConsultarRestanteLotes());
		}

	}
}