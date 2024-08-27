using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WEB_APP_Panaderia.Interfaces;

namespace WEB_APP_Panaderia.Controllers
{

	public class UsuariosRolesController : Controller
	{
		private readonly IUsuariosRolesModel _usuariosRolesModel;

		public UsuariosRolesController(IUsuariosRolesModel usuariosRolesModel)
		{
			_usuariosRolesModel = usuariosRolesModel;

		}

		[HttpGet]
		public IActionResult ConsultarUsuariosRoles()
		{
			return Json(_usuariosRolesModel.ConsultarUsuariosRoles());
		}
	}
}
