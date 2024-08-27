using Newtonsoft.Json;
using System.Text;
using WEB_APP_Panaderia.Entities;
using WEB_APP_Panaderia.Interfaces;

namespace WEB_APP_Panaderia.Models
{
    public class CotizacionModel : ICotizacionesModel
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public CotizacionModel(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }
        public int RegistrarCotizaciones(CotizacionEntity entidad)
        {
            using (var client = new HttpClient())
            {
                string urlApi = _configuration.GetSection("Parametros:urlApi").Value + "/Cotizacion/RegistrarCotizaciones";

                JsonContent body = JsonContent.Create(entidad);
                HttpResponseMessage response = client.PostAsync(urlApi, body).Result;

                if (response.IsSuccessStatusCode)
                    return response.Content.ReadFromJsonAsync<int>().Result;

                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    throw new Exception("Excepción Web Api: " + response.Content.ReadAsStringAsync().Result);

                return 0;
            }
        }


        public List<CotizacionEntity> ObtenerTodasLasCotizaciones()
        {
            using (var client = new HttpClient())
            {
                string urlApi = _configuration.GetSection("Parametros:urlApi").Value + "/Cotizacion";

                HttpResponseMessage response = client.GetAsync(urlApi).Result;

                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadFromJsonAsync<List<CotizacionEntity>>().Result;
                    return result;
                }

                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    throw new Exception("Excepción Web Api: " + response.Content.ReadAsStringAsync().Result);

                return new List<CotizacionEntity>();
            }
        }


        public async Task<int> ImportarCotizacion(CotizacionEntity cotizacion)
        {
            string urlApi = _configuration["ApiSettings:BaseUrl"] + "/api/Cotizacion/ImportarCotizacion";
            var content = new StringContent(JsonConvert.SerializeObject(cotizacion), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync(urlApi, content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<int>(responseContent);
            }

            throw new Exception("Error al importar la cotización: " + response.ReasonPhrase);
        }

        public async Task<CotizacionEntity> ObtenerCotizacionPorId(int id)
        {
            using (var client = new HttpClient())
            {
                string urlApi = _configuration.GetSection("Parametros:urlApi").Value + $"/Cotizacion/{id}";
                HttpResponseMessage response = client.GetAsync(urlApi).Result;

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<CotizacionEntity>(content);
                }

                throw new Exception("Error al obtener la cotización: " + response.ReasonPhrase);
            }
        }

        public async Task ActualizarCotizacion(CotizacionEntity cotizacion)
        {
            using (var client = new HttpClient())
            {
                string urlApi = _configuration.GetSection("Parametros:urlApi").Value + $"/Cotizacion";

                var content = new StringContent(JsonConvert.SerializeObject(cotizacion), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PutAsync(urlApi, content);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Error al actualizar la cotización: " + response.ReasonPhrase);
                }
            }
        }

        public async Task EliminarCotizacion(int id)
        {
            using (var client = new HttpClient())
            {
                string urlApi = _configuration.GetSection("Parametros:urlApi").Value + $"/Cotizacion/{id}";


                HttpResponseMessage response = await _httpClient.DeleteAsync(urlApi);

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Error al eliminar la cotización: " + response.ReasonPhrase);
                }
            }
        }

        public async Task ActualizarRecomendaciones(List<CotizacionEntity> cotizaciones)
        {
            foreach (var cotizacion in cotizaciones)
            {
                await ActualizarCotizacion(cotizacion);
            }
        }
    }
}