using Microsoft.AspNetCore.Mvc;
using WEB_APP_Panaderia.Interfaces;

namespace WEB_APP_Panaderia.Controllers
{
	public class SaboresPizzaController : Controller
	{
		private readonly ISaboresPizzaModel _saboresPizzaModel;

		public SaboresPizzaController(ISaboresPizzaModel saboresPizzaModel)
		{
			_saboresPizzaModel = saboresPizzaModel;
		}

		[HttpGet]
		public IActionResult ConsultarSaboresPizza()
		{
			var sabores = _saboresPizzaModel.ConsultarSaboresPizza();
			return Json(sabores);
		}
	}
}
