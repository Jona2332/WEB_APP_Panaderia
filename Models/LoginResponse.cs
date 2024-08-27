using WEB_APP_Panaderia.Entities;

namespace WEB_APP_Panaderia.Models
{
	public class LoginResponse
	{
		public string Token { get; set; }
		public UsuariosEntities Usuario { get; set; }
	}
}
