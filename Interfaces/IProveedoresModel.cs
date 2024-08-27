using WEB_APP_Panaderia.Entities;

namespace WEB_APP_Panaderia.Interfaces
{
	public interface IProveedoresModel
	{

        public int RegistrarProveedores(ProveedoresEntities entidad);
        public List<ProveedoresEntities> GetAllProveedores();
        public void ActualizarProveedor(ProveedoresEntities entidad);
        public void EliminarProveedor(int id);
        public ProveedoresEntities GetProveedorById(int idProveedor);
    }


}
