using WEB_APP_Panaderia.Entities;

namespace WEB_APP_Panaderia.Models
{
	public class ViewModel
	{
        public IEnumerable<UsuariosEntities>? Usuarios { get; set; }
        public UsuariosEntities? Usuario { get; set; }
        public IEnumerable<UsuariosRolesEntities>? Roles { get; set; }
    }
}
