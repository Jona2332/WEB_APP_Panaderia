namespace WEB_APP_Panaderia.Interfaces
{
    public interface IPdfProcessingService
    {
        Task<string> ProcessPdfAsync(string pdfPath, string supplierName);
    }
}