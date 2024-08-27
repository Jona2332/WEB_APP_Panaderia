using WEB_APP_Panaderia.Interfaces;

namespace WEB_APP_Panaderia.Models
{
    public class PdfProcessingService : IPdfProcessingService
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _contextAccessor;

        public PdfProcessingService(IConfiguration configuration, IHttpContextAccessor contextAccessor)
        {

            _configuration = configuration;
            _contextAccessor = contextAccessor;
        }
        public async Task<string> ProcessPdfAsync(string pdfPath, string supplierName)
        {
            using (var client = new HttpClient())
            {
                string urlApi = _configuration.GetSection("Parametros1:urlApi1").Value + $"ProcessPdf?pdfPath={pdfPath}&supplierName={supplierName}";

                var content = new MultipartFormDataContent();
                content.Add(new StreamContent(File.OpenRead(pdfPath)), "pdfFile", Path.GetFileName(pdfPath));
                content.Add(new StringContent(supplierName), "supplierName");

                HttpResponseMessage response = await client.PostAsync(urlApi, content);

                if (!response.IsSuccessStatusCode)
                    throw new Exception("Excepción Web Api: " + await response.Content.ReadAsStringAsync());

                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}