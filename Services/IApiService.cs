using WEB_APP_Panaderia.Models;

namespace WEB_APP_Panaderia.Services
{
    public interface IApiService
    {
        List<Email> GetEmails();
        void SendEmail(Email email);
        void DeleteEmail(int id);
        List<Cliente> GetClientes();
    }
}
