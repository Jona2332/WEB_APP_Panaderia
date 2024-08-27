using Newtonsoft.Json;
using WEB_APP_Panaderia.Entities;

namespace WEB_APP_Panaderia.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _contextAccessor;

        public ApiService(IConfiguration configuration, IHttpContextAccessor contextAccessor)
        {

            _configuration = configuration;
            _contextAccessor = contextAccessor;
        }

        public async Task<List<CotizacionEntity>> GetAllCotizaciones()
        {
            var response = await _httpClient.GetAsync("api/cotizacion");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<CotizacionEntity>>(content);
        }

        public async Task<string> ProcessPdf(string pdfContent, string providerName)
        {
            var request = new
            {
                PdfContent = pdfContent,
                ProviderName = providerName
            };
            var response = await _httpClient.PostAsJsonAsync("api/cotizacion/process", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
