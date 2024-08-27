using Microsoft.AspNetCore.Mvc.Filters;

namespace WEB_APP_Panaderia.Interfaces
{
	public interface IAuthorizationFilter : IFilterMetadata
	{
		void OnAuthorization(AuthorizationFilterContext context);
	}
}
