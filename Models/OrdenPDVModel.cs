using System.Collections.Generic;
using WEB_APP_Panaderia.Entities;
using WEB_APP_Panaderia.Interfaces;

namespace WEB_APP_Panaderia.Models
{
	public class OrdenPDVModel : IOrdenPDVModel
	{
		private readonly IConfiguration _configuration;
		private readonly IHttpContextAccessor _contextAccessor;

		public OrdenPDVModel(IConfiguration configuration, IHttpContextAccessor contextAccessor)
		{
			_configuration = configuration;
			_contextAccessor = contextAccessor;
		}

		public OrdenEntities? CompletarOrden(OrdenEntities orden)
		{
			using (var client = new HttpClient())
			{
				string urlApi = _configuration.GetSection("Parametros:urlApi").Value + "/OrdenPDV/CompletarOrden";

				JsonContent body = JsonContent.Create(orden);
				HttpResponseMessage response = client.PostAsync(urlApi, body).Result;

				if (response.IsSuccessStatusCode)
					return response.Content.ReadFromJsonAsync<OrdenEntities>().Result;

				if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
					throw new Exception("Excepción Web Api: " + response.Content.ReadAsStringAsync().Result);

				return null;
			}
		}

		public List<DetalleOrdenEntities>? OrdenesPizzeria()
		{
			using (var client = new HttpClient())
			{
				string urlApi = _configuration.GetSection("Parametros:urlApi").Value + "/OrdenPDV/ConsultarOrdenesPDV";
				HttpResponseMessage response = client.GetAsync(urlApi).Result;

				if (response.IsSuccessStatusCode)
				{
					var result = response.Content.ReadFromJsonAsync<List<DetalleOrdenEntities>>().Result;
					return result;
				}
				
				if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
					throw new Exception("Excepción Web Api: " + response.Content.ReadAsStringAsync().Result);
					return new List<DetalleOrdenEntities>();

			}
		}

		public List<DetalleOrdenEntities>? ConsultarDetalleOrden(int id)
		{
			using (var client = new HttpClient())
			{
				string urlApi = _configuration.GetSection("Parametros:urlApi").Value + "/OrdenPDV/ConsultarDetalleOrdenPDV?idOrden=" + id;
				HttpResponseMessage response = client.GetAsync(urlApi).Result;

				if (response.IsSuccessStatusCode)
				{
					var result = response.Content.ReadFromJsonAsync<List<DetalleOrdenEntities>>().Result;
					return result;
				}

				if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
					throw new Exception("Excepción Web Api: " + response.Content.ReadAsStringAsync().Result);
				return new List<DetalleOrdenEntities>();

			}
		}

		public List<EstadoOrdenesEntities> ConsultarEstadosOrden()
		{
			using (var client = new HttpClient())
			{
				string urlApi = _configuration.GetSection("Parametros:urlApi").Value + "/OrdenPDV/ConsultarEstadosOrden";
				HttpResponseMessage response = client.GetAsync(urlApi).Result;

				if (response.IsSuccessStatusCode)
				{
					var result = response.Content.ReadFromJsonAsync<List<EstadoOrdenesEntities>>().Result;
					return result;
				}

				if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)

					throw new Exception("Excepción Web Api: " + response.Content.ReadAsStringAsync().Result);
				return new List<EstadoOrdenesEntities>();
			}

		}

		public DetalleOrdenEntities? ActualizarEstadoOrden(DetalleOrdenEntities orden)
		{
			using (var client = new HttpClient())
			{
				string urlApi = _configuration.GetSection("Parametros:urlApi").Value + "/OrdenPDV/ActualizarEstadoOrden";

				JsonContent body = JsonContent.Create(orden);
				HttpResponseMessage response = client.PostAsync(urlApi, body).Result;

				if (!response.IsSuccessStatusCode)
					throw new Exception("Excepción Web Api: " + response.Content.ReadAsStringAsync().Result);

				return null;
			}
		}


	}
}
