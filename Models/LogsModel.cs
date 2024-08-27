using WEB_APP_Panaderia.Entities;
using WEB_APP_Panaderia.Interfaces;
namespace WEB_APP_Panaderia.Models
{
	public class LogsModel: ILogsModel
	{
		private readonly IConfiguration _configuration;

		public LogsModel(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public void RegistrarBitacora(LogsEntities entidad)
		{
			using (var client = new HttpClient())
			{
				string urlApi = _configuration.GetSection("Parametros:urlApi").Value + "/Logs/RegistrarBitacora";

				//Serializar convertir un objeto a json
				JsonContent body = JsonContent.Create(entidad);
				HttpResponseMessage response = client.PostAsync(urlApi, body).Result;


			}

		}
	
	}
}
