using WEB_APP_Panaderia.Entities;
using WEB_APP_Panaderia.Interfaces;

namespace WEB_APP_Panaderia.Models
{
    public class ContabilidadModel : IContabilidadModel
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _contextAccessor;

        public ContabilidadModel(IConfiguration configuration, IHttpContextAccessor contextAccessor)
        {
            _configuration = configuration;
            _contextAccessor = contextAccessor;
        }

        public CuentaContableEntity InsertarCuentaContable(CuentaContableEntity cuenta)
        {
            using (var client = new HttpClient())
            {
                string urlApi = _configuration.GetSection("Parametros:urlApi").Value + "/Contabilidad/cuentas";
                JsonContent body = JsonContent.Create(cuenta);
                HttpResponseMessage response = client.PostAsync(urlApi, body).Result;

                if (response.IsSuccessStatusCode)
                    return response.Content.ReadFromJsonAsync<CuentaContableEntity>().Result;

                throw new Exception("Excepción Web Api: " + response.Content.ReadAsStringAsync().Result);
            }
        }

        public void ActualizarCuentaContable(CuentaContableEntity cuenta)
        {
            using (var client = new HttpClient())
            {
                string urlApi = _configuration.GetSection("Parametros:urlApi").Value + "/Contabilidad/cuentas";
                JsonContent body = JsonContent.Create(cuenta);
                HttpResponseMessage response = client.PutAsync(urlApi, body).Result;

                if (!response.IsSuccessStatusCode)
                    throw new Exception("Excepción Web Api: " + response.Content.ReadAsStringAsync().Result);
            }
        }

        public void EliminarCuentaContable(int idCuenta)
        {
            using (var client = new HttpClient())
            {
                string urlApi = _configuration.GetSection("Parametros:urlApi").Value + $"/Contabilidad/cuentas/{idCuenta}";
                HttpResponseMessage response = client.DeleteAsync(urlApi).Result;

                if (!response.IsSuccessStatusCode)
                    throw new Exception("Excepción Web Api: " + response.Content.ReadAsStringAsync().Result);
            }
        }

        public List<CuentaContableEntity> ConsultarCuentasContables()
        {
            using (var client = new HttpClient())
            {
                string urlApi = _configuration.GetSection("Parametros:urlApi").Value + "/Contabilidad/cuentas";
                HttpResponseMessage response = client.GetAsync(urlApi).Result;

                if (response.IsSuccessStatusCode)
                    return response.Content.ReadFromJsonAsync<List<CuentaContableEntity>>().Result;

                throw new Exception("Excepción Web Api: " + response.Content.ReadAsStringAsync().Result);
            }
        }

        public void GenerarAsientoVenta(int idVenta)
        {
            using (var client = new HttpClient())
            {
                string urlApi = _configuration.GetSection("Parametros:urlApi").Value + $"/Contabilidad/asientos/venta/{idVenta}";
                HttpResponseMessage response = client.PostAsync(urlApi, null).Result;

                if (!response.IsSuccessStatusCode)
                    throw new Exception("Excepción Web Api: " + response.Content.ReadAsStringAsync().Result);
            }
        }

        public List<EstadoResultadosEntity> GenerarEstadoResultados(DateTime fechaInicio, DateTime fechaFin)
        {
            using (var client = new HttpClient())
            {
                string urlApi = _configuration.GetSection("Parametros:urlApi").Value + $"/Contabilidad/estado-resultados?fechaInicio={fechaInicio:yyyy-MM-dd}&fechaFin={fechaFin:yyyy-MM-dd}";
                HttpResponseMessage response = client.GetAsync(urlApi).Result;

                if (response.IsSuccessStatusCode)
                    return response.Content.ReadFromJsonAsync<List<EstadoResultadosEntity>>().Result;

                throw new Exception("Excepción Web Api: " + response.Content.ReadAsStringAsync().Result);
            }
        }

        public List<BalanceGeneralEntity> GenerarBalanceGeneral(DateTime fechaCorte)
        {
            using (var client = new HttpClient())
            {
                string urlApi = _configuration.GetSection("Parametros:urlApi").Value + $"/Contabilidad/balance-general?fechaCorte={fechaCorte:yyyy-MM-dd}";
                HttpResponseMessage response = client.GetAsync(urlApi).Result;

                if (response.IsSuccessStatusCode)
                    return response.Content.ReadFromJsonAsync<List<BalanceGeneralEntity>>().Result;

                throw new Exception("Excepción Web Api: " + response.Content.ReadAsStringAsync().Result);
            }
        }
    }
}