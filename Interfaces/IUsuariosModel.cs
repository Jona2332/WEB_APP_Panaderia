using WEB_APP_Panaderia.Entities;

namespace WEB_APP_Panaderia.Interfaces
{
    public interface IUsuariosModel
    {
        public UsuariosEntities? ValidarCredenciales(UsuariosEntities entidad);

        public int RegistrarUsuarios(UsuariosEntities entidad);

        public void RecuperarContrasenna(UsuariosEntities entidad);

        public int BuscarExisteCorreo(string Correo);
        public List<UsuariosEntities> GetAllUsers();
        public void ActualizarUsuario(UsuariosEntities entidad);
        public UsuariosEntities GetUsuarioById(int idUsuario);
        void DesactivarUsuario(int idUsuario);



    }
}
