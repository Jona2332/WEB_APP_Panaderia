using WEB_APP_Panaderia.Entities;
using WEB_APP_Panaderia.Interfaces;

namespace WEB_APP_Panaderia.Models
{
	public class InsumosModel : IInsumosModel
	
		{
			private readonly IConfiguration _configuration;
			private readonly IHttpContextAccessor _contextAccessor;

			public InsumosModel(IConfiguration configuration, IHttpContextAccessor contextAccessor)
			{
				_configuration = configuration;
				_contextAccessor = contextAccessor;
			}


			public List<InsumosEntities>? ConsultaInsumos()
			{
				using (var client = new HttpClient())
				{
				string urlApi = _configuration.GetSection("Parametros:urlApi").Value + "/Insumos/ConsultaInsumos";
					HttpResponseMessage response = client.GetAsync(urlApi).Result;

					if (response.IsSuccessStatusCode)
					{
						var result = response.Content.ReadFromJsonAsync<List<InsumosEntities>>().Result;
						return result;
					}

					if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
						throw new Exception("Excepción Web Api: " + response.Content.ReadAsStringAsync().Result);

					return new List<InsumosEntities>();
				}
			}

		public InsumosEntities? ConsultaUnRegistroInsumo(int idInsumo)
		{
			using (var client = new HttpClient())
			{
				string urlApi = _configuration.GetSection("Parametros:urlApi").Value + $"/Insumos/ConsultaUnRegistroInsumo/{idInsumo}";
				HttpResponseMessage response = client.GetAsync(urlApi).Result;

				if (response.IsSuccessStatusCode)
				{
					var result = response.Content.ReadFromJsonAsync<InsumosEntities>().Result;
					return result;
				}

				if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
					throw new Exception("Excepción Web Api: " + response.Content.ReadAsStringAsync().Result);

				return null;
			}
		}

		public List<PresentacionEntities>? ConsultaPresentacion()
		{
			using (var client = new HttpClient())
			{
				string urlApi = _configuration.GetSection("Parametros:urlApi").Value + "/Insumos/ConsultaPresentacion";
				HttpResponseMessage response = client.GetAsync(urlApi).Result;

				if (response.IsSuccessStatusCode)
				{
					var result = response.Content.ReadFromJsonAsync<List<PresentacionEntities>>().Result;
					return result;
				}

				if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
					throw new Exception("Excepción Web Api: " + response.Content.ReadAsStringAsync().Result);

				return new List<PresentacionEntities>();
			}
		}


		public List<TipoInsumoEntities>? ConsultaTipoInsumo()
		{
			using (var client = new HttpClient())
			{
				string urlApi = _configuration.GetSection("Parametros:urlApi").Value + "/Insumos/ConsultaTipoInsumo";
				HttpResponseMessage response = client.GetAsync(urlApi).Result;

				if (response.IsSuccessStatusCode)
				{
					var result = response.Content.ReadFromJsonAsync<List<TipoInsumoEntities>>().Result;
					return result;
				}

				if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
					throw new Exception("Excepción Web Api: " + response.Content.ReadAsStringAsync().Result);

				return new List<TipoInsumoEntities>();
			}
		}

		public List<CategoriaInsumosEntities>? ConsultaCategoriaInsumo()
		{
			using (var client = new HttpClient())
			{
				string urlApi = _configuration.GetSection("Parametros:urlApi").Value + "/Insumos/ConsultaCategoriaInsumo";
				HttpResponseMessage response = client.GetAsync(urlApi).Result;

				if (response.IsSuccessStatusCode)
				{
					var result = response.Content.ReadFromJsonAsync<List<CategoriaInsumosEntities>>().Result;
					return result;
				}

				if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
					throw new Exception("Excepción Web Api: " + response.Content.ReadAsStringAsync().Result);

				return new List<CategoriaInsumosEntities>();
			}
		}

		public List<MarcaEntities>? ConsultaMarca()
		{
			using (var client = new HttpClient())
			{
				string urlApi = _configuration.GetSection("Parametros:urlApi").Value + "/Insumos/ConsultaMarca";
				HttpResponseMessage response = client.GetAsync(urlApi).Result;

				if (response.IsSuccessStatusCode)
				{
					var result = response.Content.ReadFromJsonAsync<List<MarcaEntities>>().Result;
					return result;
				}

				if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
					throw new Exception("Excepción Web Api: " + response.Content.ReadAsStringAsync().Result);

				return new List<MarcaEntities>();
			}
		}

		public void ActualizarInsumos(InsumosEntities reporte)
		{
			using (var client = new HttpClient())
			{
				string urlApi = _configuration.GetSection("Parametros:urlApi").Value + "/Insumos/ActualizarInsumos";

				JsonContent body = JsonContent.Create(reporte);
				HttpResponseMessage response = client.PostAsync(urlApi, body).Result;
				if (!response.IsSuccessStatusCode)
					throw new Exception("Excepción Web Api: " + response.Content.ReadAsStringAsync().Result);
			}
		}

		public void ActualizarPresentacion(PresentacionEntities reporte)
		{
			using (var client = new HttpClient())
			{
				string urlApi = _configuration.GetSection("Parametros:urlApi").Value + "/Insumos/ActualizarPresentacion";

				JsonContent body = JsonContent.Create(reporte);
				HttpResponseMessage response = client.PostAsync(urlApi, body).Result;

			}
		}

		public void ActualizarTipoInsumo(TipoInsumoEntities reporte)
		{
			using (var client = new HttpClient())
			{
				string urlApi = _configuration.GetSection("Parametros:urlApi").Value + "/Insumos/ActualizarTipoInsumo";

				JsonContent body = JsonContent.Create(reporte);
				HttpResponseMessage response = client.PostAsync(urlApi, body).Result;

			}
		}

		public void ActualizarCategoriaInsumo(CategoriaInsumosEntities reporte)
		{
			using (var client = new HttpClient())
			{
				string urlApi = _configuration.GetSection("Parametros:urlApi").Value + "/Insumos/ActualizarCategoriaInsumo";

				JsonContent body = JsonContent.Create(reporte);
				HttpResponseMessage response = client.PostAsync(urlApi, body).Result;

			}
		}


		public void ActualizarMarca(MarcaEntities reporte)
		{
			using (var client = new HttpClient())
			{
				string urlApi = _configuration.GetSection("Parametros:urlApi").Value + "/Insumos/ActualizarMarca";

				JsonContent body = JsonContent.Create(reporte);
				HttpResponseMessage response = client.PostAsync(urlApi, body).Result;

			}
		}

		public void BorrarInsumos(int idInsumo)
		{
			using (var client = new HttpClient())
			{
				string urlApi = _configuration.GetSection("Parametros:urlApi").Value + "/Insumos/BorrarInsumos";

				JsonContent body = JsonContent.Create(idInsumo);
				HttpResponseMessage response = client.PostAsync(urlApi, body).Result;

				if (!response.IsSuccessStatusCode)
					throw new Exception("Excepción Web Api: " + response.Content.ReadAsStringAsync().Result);
			}
		}

		public void BorrarPresentacion(int idPresentacion)
		{
			using (var client = new HttpClient())
			{
				string urlApi = _configuration.GetSection("Parametros:urlApi").Value + "/Insumos/BorrarPresentacion";

				JsonContent body = JsonContent.Create(idPresentacion);
				HttpResponseMessage response = client.PostAsync(urlApi, body).Result;

				if (!response.IsSuccessStatusCode)
					throw new Exception("Excepción Web Api: " + response.Content.ReadAsStringAsync().Result);
			}
		}

		public void BorrarTipoInsumo(int idTipoInsumo)
		{
			using (var client = new HttpClient())
			{
				string urlApi = _configuration.GetSection("Parametros:urlApi").Value + "/Insumos/BorrarTipoInsumo";

				JsonContent body = JsonContent.Create(idTipoInsumo);
				HttpResponseMessage response = client.PostAsync(urlApi, body).Result;

				if (!response.IsSuccessStatusCode)
					throw new Exception("Excepción Web Api: " + response.Content.ReadAsStringAsync().Result);
			}
		}

		public void BorrarCategoriaInsumo(int idCategoriaInsumo)
		{
			using (var client = new HttpClient())
			{
				string urlApi = _configuration.GetSection("Parametros:urlApi").Value + "/Insumos/BorrarCategoriaInsumo";

				JsonContent body = JsonContent.Create(idCategoriaInsumo);
				HttpResponseMessage response = client.PostAsync(urlApi, body).Result;

				if (!response.IsSuccessStatusCode)
					throw new Exception("Excepción Web Api: " + response.Content.ReadAsStringAsync().Result);
			}
		}

		public void BorrarMarca(int idMarca)
		{
			using (var client = new HttpClient())
			{
				string urlApi = _configuration.GetSection("Parametros:urlApi").Value + "/Insumos/BorrarMarca";

				JsonContent body = JsonContent.Create(idMarca);
				HttpResponseMessage response = client.PostAsync(urlApi, body).Result;

				if (!response.IsSuccessStatusCode)
					throw new Exception("Excepción Web Api: " + response.Content.ReadAsStringAsync().Result);
			}
		}

		public int AgregarInsumos(InsumosEntities reporte)
		{
			using (var client = new HttpClient())
			{
				string urlApi = _configuration.GetSection("Parametros:urlApi").Value + "/Insumos/AgregarInsumos";

				JsonContent body = JsonContent.Create(reporte);
				HttpResponseMessage response = client.PostAsync(urlApi, body).Result;

				if (response.IsSuccessStatusCode)
					return response.Content.ReadFromJsonAsync<int>().Result;

				if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
					throw new Exception("Excepción Web Api: " + response.Content.ReadAsStringAsync().Result);

				return 0;
			}
		}



		public int AgregarPresentacion(PresentacionEntities reporte)
		{
			using (var client = new HttpClient())
			{
				string urlApi = _configuration.GetSection("Parametros:urlApi").Value + "/Insumos/AgregarPresentacion";

				JsonContent body = JsonContent.Create(reporte);
				HttpResponseMessage response = client.PostAsync(urlApi, body).Result;

				if (response.IsSuccessStatusCode)
					return response.Content.ReadFromJsonAsync<int>().Result;

				if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
					throw new Exception("Excepción Web Api: " + response.Content.ReadAsStringAsync().Result);

				return 0;
			}
		}



		public int AgregarTipoInsumo(TipoInsumoEntities reporte)
		{
			using (var client = new HttpClient())
			{
				string urlApi = _configuration.GetSection("Parametros:urlApi").Value + "/Insumos/AgregarTipoInsumo";

				JsonContent body = JsonContent.Create(reporte);
				HttpResponseMessage response = client.PostAsync(urlApi, body).Result;

				if (response.IsSuccessStatusCode)
					return response.Content.ReadFromJsonAsync<int>().Result;

				if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
					throw new Exception("Excepción Web Api: " + response.Content.ReadAsStringAsync().Result);

				return 0;
			}
		}

		public int AgregarCategoriaInsumo(CategoriaInsumosEntities reporte)
		{
			using (var client = new HttpClient())
			{
				string urlApi = _configuration.GetSection("Parametros:urlApi").Value + "/Insumos/AgregarCategoriaInsumo";

				JsonContent body = JsonContent.Create(reporte);
				HttpResponseMessage response = client.PostAsync(urlApi, body).Result;

				if (response.IsSuccessStatusCode)
					return response.Content.ReadFromJsonAsync<int>().Result;

				if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
					throw new Exception("Excepción Web Api: " + response.Content.ReadAsStringAsync().Result);

				return 0;
			}
		}


		public int AgregarMarca(MarcaEntities reporte)
		{
			using (var client = new HttpClient())
			{
				string urlApi = _configuration.GetSection("Parametros:urlApi").Value + "/Insumos/AgregarMarca";

				JsonContent body = JsonContent.Create(reporte);
				HttpResponseMessage response = client.PostAsync(urlApi, body).Result;

				if (response.IsSuccessStatusCode)
					return response.Content.ReadFromJsonAsync<int>().Result;

				if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
					throw new Exception("Excepción Web Api: " + response.Content.ReadAsStringAsync().Result);

				return 0;
			}
		}




	}
}

