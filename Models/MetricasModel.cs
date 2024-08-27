using WEB_APP_Panaderia.Entities;
using WEB_APP_Panaderia.Interfaces;

namespace WEB_APP_Panaderia.Models
{
	public class MetricasModel : IMetricasModel
	{
		private readonly IConfiguration _configuration;
		private readonly IHttpContextAccessor _contextAccessor;

		public MetricasModel(IConfiguration configuration, IHttpContextAccessor contextAccessor)
		{
			_configuration = configuration;
			_contextAccessor = contextAccessor;
		}

		public ConteoVentasEntities? ConsultarConteoVentasPorSemana()
		{
			using (var client = new HttpClient())
			{
				string urlApi = _configuration.GetSection("Parametros:urlApi").Value + "/Metricas/ConsultarConteoVentasPorSemana";
				HttpResponseMessage response = client.GetAsync(urlApi).Result;

				if (response.IsSuccessStatusCode)
				{
					var result = response.Content.ReadFromJsonAsync<ConteoVentasEntities>().Result;
					return result;
				}

				if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
					throw new Exception("Excepción Web Api: " + response.Content.ReadAsStringAsync().Result);

				return null;
			}
		}

		public ConteoVentasEntities? ConsultarConteoVentasPorDia()
		{
			using (var client = new HttpClient())
			{
				string urlApi = _configuration.GetSection("Parametros:urlApi").Value + "/Metricas/ConsultarConteoVentasPorDia";
				HttpResponseMessage response = client.GetAsync(urlApi).Result;

				if (response.IsSuccessStatusCode)
				{
					var result = response.Content.ReadFromJsonAsync<ConteoVentasEntities>().Result;
					return result;
				}

				if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
					throw new Exception("Excepción Web Api: " + response.Content.ReadAsStringAsync().Result);

				return null;
			}
		}

		public List<TotalVentasProductoEntities>? ConsultarTotalVentasProductoPorDia()
		{
			using (var client = new HttpClient())
			{
				string urlApi = _configuration.GetSection("Parametros:urlApi").Value + "/Metricas/ConsultarTotalVentasProductoPorDia";
				HttpResponseMessage response = client.GetAsync(urlApi).Result;

				if (response.IsSuccessStatusCode)
				{
					var result = response.Content.ReadFromJsonAsync<List<TotalVentasProductoEntities>>().Result;
					return result;
				}

				if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
					throw new Exception("Excepción Web Api: " + response.Content.ReadAsStringAsync().Result);

				return null;
			}
		}

		public List<TransaccionesRecientesEntities>? ConsultarTransaccionesRecientes()
		{
			using (var client = new HttpClient())
			{
				string urlApi = _configuration.GetSection("Parametros:urlApi").Value + "/Metricas/ConsultarTransaccionesRecientes";
				HttpResponseMessage response = client.GetAsync(urlApi).Result;

				if (response.IsSuccessStatusCode)
				{
					var result = response.Content.ReadFromJsonAsync<List<TransaccionesRecientesEntities>>().Result;
					return result;
				}

				if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
					throw new Exception("Excepción Web Api: " + response.Content.ReadAsStringAsync().Result);

				return null;
			}
		}

		public GananciasEntities? ConsultarGanancias()
		{
			using (var client = new HttpClient())
			{
				string urlApi = _configuration.GetSection("Parametros:urlApi").Value + "/Metricas/ConsultarGanancias";
				HttpResponseMessage response = client.GetAsync(urlApi).Result;

				if (response.IsSuccessStatusCode)
				{
					var result = response.Content.ReadFromJsonAsync<GananciasEntities>().Result;
					return result;
				}

				if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
					throw new Exception("Excepción Web Api: " + response.Content.ReadAsStringAsync().Result);

				return null;
			}
		}

		public List<RestanteLotesEntities>? ConsultarRestanteLotes()
		{
			using (var client = new HttpClient())
			{
				string urlApi = _configuration.GetSection("Parametros:urlApi").Value + "/Metricas/ConsultarRestanteLotes";
				HttpResponseMessage response = client.GetAsync(urlApi).Result;

				if (response.IsSuccessStatusCode)
				{
					var result = response.Content.ReadFromJsonAsync<List<RestanteLotesEntities>>().Result;
					return result;
				}

				if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
					throw new Exception("Excepción Web Api: " + response.Content.ReadAsStringAsync().Result);

				return null;
			}
		}

	}
}