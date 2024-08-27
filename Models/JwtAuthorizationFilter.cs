using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WEB_APP_Panaderia.Models
{
	public class JwtAuthorizationFilter : IAuthorizationFilter
	{
		public void OnAuthorization(AuthorizationFilterContext context)
		{
			// Evita la redirección si ya estamos en la página de inicio
			if (context.HttpContext.Request.Path == "/Home/Index" || context.HttpContext.Request.Path == "/")
			{
				return;
			}

			if (context.HttpContext.Session == null)
			{
				context.Result = new RedirectToActionResult("Index", "Home", null);
				return;
			}

			var token = context.HttpContext.Session.GetString("Token");
			if (string.IsNullOrEmpty(token))
			{
				context.Result = new RedirectToActionResult("Index", "Home", null);
			}
		}
	}
}