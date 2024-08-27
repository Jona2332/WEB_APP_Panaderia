using WEB_APP_Panaderia.Entities;
using WEB_APP_Panaderia.Interfaces;

namespace WEB_APP_Panaderia.Models
{
	public class SaboresPizzaModel : ISaboresPizzaModel
	{
		private readonly IConfiguration _configuration;
		private readonly IHttpContextAccessor _contextAccessor;

		public SaboresPizzaModel(IConfiguration configuration, IHttpContextAccessor contextAccessor)
		{
			_configuration = configuration;
			_contextAccessor = contextAccessor;
		}


		public List<SaboresPizzaEntities> ConsultarSaboresPizza()
		{
			using (var client = new HttpClient())
			{
				string urlApi = _configuration.GetSection("Parametros:urlApi").Value + "/SaboresPizza/ConsultarSaboresPizza";
				HttpResponseMessage response = client.GetAsync(urlApi).Result;

				if (response.IsSuccessStatusCode)
				{
					var result = response.Content.ReadFromJsonAsync<List<SaboresPizzaEntities>>().Result;
					return result;
				}

				if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
					throw new Exception("Excepción Web Api: " + response.Content.ReadAsStringAsync().Result);

				return new List<SaboresPizzaEntities>();
			}
		}
	
	}
}
