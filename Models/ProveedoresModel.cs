using WEB_APP_Panaderia.Entities;
using WEB_APP_Panaderia.Interfaces;

namespace WEB_APP_Panaderia.Models
{
    public class ProveedoresModel : IProveedoresModel
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _contextAccessor;

        public ProveedoresModel(IConfiguration configuration, IHttpContextAccessor contextAccessor)
        {
            _configuration = configuration;
            _contextAccessor = contextAccessor;
        }

        public List<ProveedoresEntities> GetAllProveedores()
        {
            using (var client = new HttpClient())
            {
                string urlApi = _configuration.GetSection("Parametros:urlApi").Value + "/Proveedores/GetAllProveedores";
                HttpResponseMessage response = client.GetAsync(urlApi).Result;

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadFromJsonAsync<List<ProveedoresEntities>>().Result;
                    return result;
                }

                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    throw new Exception("Excepción Web Api: " + response.Content.ReadAsStringAsync().Result);

                return new List<ProveedoresEntities>();
            }
        }

        public void ActualizarProveedor(ProveedoresEntities entidad)
        {
            using (var client = new HttpClient())
            {
                string urlApi = _configuration.GetSection("Parametros:urlApi").Value + "/Proveedores/ActualizarProveedor";

                JsonContent body = JsonContent.Create(entidad);
                HttpResponseMessage response = client.PostAsync(urlApi, body).Result;

                if (!response.IsSuccessStatusCode)
                    throw new Exception("Excepción Web Api: " + response.Content.ReadAsStringAsync().Result);
            }
        }

        public int RegistrarProveedores(ProveedoresEntities entidad)
        {
            using (var client = new HttpClient())
            {
                string urlApi = _configuration.GetSection("Parametros:urlApi").Value + "/Proveedores/RegistrarProveedores";

                JsonContent body = JsonContent.Create(entidad);
                HttpResponseMessage response = client.PostAsync(urlApi, body).Result;

                if (response.IsSuccessStatusCode)
                    return response.Content.ReadFromJsonAsync<int>().Result;

                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    throw new Exception("Excepción Web Api: " + response.Content.ReadAsStringAsync().Result);

                return 0;
            }
        }

        public void EliminarProveedor(int idProveedor)
        {
            using (var client = new HttpClient())
            {
                string urlApi = _configuration.GetSection("Parametros:urlApi").Value + "/Proveedores/EliminarProveedor";

                JsonContent body = JsonContent.Create(idProveedor);
                HttpResponseMessage response = client.PostAsync(urlApi, body).Result;

                if (!response.IsSuccessStatusCode)
                    throw new Exception("Excepción Web Api: " + response.Content.ReadAsStringAsync().Result);
            }
        }
        public ProveedoresEntities GetProveedorById(int idProveedor)
        {
            using (var client = new HttpClient())
            {
                string urlApi = _configuration.GetSection("Parametros:urlApi").Value + $"/Proveedores/GetProveedorById/{idProveedor}";
                HttpResponseMessage response = client.GetAsync(urlApi).Result;

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadFromJsonAsync<ProveedoresEntities>().Result;
                    return result;
                }

                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    throw new Exception("Excepción Web Api: " + response.Content.ReadAsStringAsync().Result);

                return null;
            }
        }
    }
}
