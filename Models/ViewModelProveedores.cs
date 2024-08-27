using WEB_APP_Panaderia.Entities;

namespace WEB_APP_Panaderia.Models
{
	public class ViewModelProveedores
	{
		public IEnumerable<ProveedoresEntities>? Proveedores { get; set; }
		public ProveedoresEntities? Proveedor { get; set; }
	}
}
