namespace WEB_APP_Panaderia.Entities
{
    public class UsuariosEntities
    {

        public long idUsuario { get; set; }
        public string nombre { get; set; } = string.Empty;
        public string correo { get; set; } = string.Empty;
        public string contrasenna { get; set; } = string.Empty;
        public int idRol { get; set; } = 2;
        public byte estado { get; set; } = 1;
        public string Token { get; set; } = string.Empty;
        public string descripcion { get; set; } = string.Empty;



    }
}
