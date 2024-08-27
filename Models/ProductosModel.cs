using Microsoft.Extensions.Configuration;
using WEB_APP_Panaderia.Entities;
using WEB_APP_Panaderia.Interfaces;

namespace WEB_APP_Panaderia.Models
{
	public class ProductosModel : IProductosModel
	{
		private readonly IConfiguration _configuration;
		private readonly IHttpContextAccessor _contextAccessor;

		public ProductosModel(IConfiguration configuration, IHttpContextAccessor contextAccessor)
		{
			_configuration = configuration;
			_contextAccessor = contextAccessor;
		}


		public List<ProductosEntities> ConsultarProductosPorCategoria(int idCategoria)
		{
			using (var client = new HttpClient())
			{
				string urlApi = _configuration.GetSection("Parametros:urlApi").Value + "/Productos/ConsultarProductosPorCategoria?idCategoria=" + idCategoria;
				HttpResponseMessage response = client.GetAsync(urlApi).Result;

				if (response.IsSuccessStatusCode)
				{
					var result = response.Content.ReadFromJsonAsync<List<ProductosEntities>>().Result;
					return result;
				}

				if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
					throw new Exception("Excepción Web Api: " + response.Content.ReadAsStringAsync().Result);

				return new List<ProductosEntities>();
			}
		}


	}
}
