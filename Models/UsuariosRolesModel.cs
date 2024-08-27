using WEB_APP_Panaderia.Entities;
using WEB_APP_Panaderia.Interfaces;

namespace WEB_APP_Panaderia.Models
{
	public class UsuariosRolesModel : IUsuariosRolesModel
	{
		private readonly IConfiguration _configuration;
		private readonly IHttpContextAccessor _contextAccessor;

		public UsuariosRolesModel(IConfiguration configuration, IHttpContextAccessor contextAccessor)
		{
			_configuration = configuration;
			_contextAccessor = contextAccessor;
		}

		public List<UsuariosRolesEntities>? ConsultarUsuariosRoles()
		{
			using (var client = new HttpClient())
			{
				string urlApi = _configuration.GetSection("Parametros:urlApi").Value + "/UsuariosRoles/ConsultarUsuariosRoles";

				HttpResponseMessage response = client.GetAsync(urlApi).Result;

				if (response.IsSuccessStatusCode)
					return response.Content.ReadFromJsonAsync<List<UsuariosRolesEntities>>().Result;

				if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
					throw new Exception("Excepción Web Api: " + response.Content.ReadAsStringAsync().Result);

				return new List<UsuariosRolesEntities>();
			}
		}

	}
}
