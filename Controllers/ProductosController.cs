using Microsoft.AspNetCore.Mvc;
using WEB_APP_Panaderia.Interfaces;
using WEB_APP_Panaderia.Models;

namespace WEB_APP_Panaderia.Controllers
{
	public class ProductosController : Controller
	{
		private readonly IProductosModel _productosModel;

		public ProductosController( IProductosModel productosModel)
		{
			_productosModel = productosModel;
		}

		[HttpGet]
		public IActionResult ConsultarProductosPorCategoria(int idCategoria)
		{
			var productos = _productosModel.ConsultarProductosPorCategoria(idCategoria);
			return Json(productos);
		}


	}
}
