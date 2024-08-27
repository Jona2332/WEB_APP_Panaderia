using WEB_APP_Panaderia.Entities;

namespace WEB_APP_Panaderia.Models
{
	public class OrdenesPizzeriaViewModel
	{
		public IEnumerable<DetalleOrdenEntities> Ordenes { get; set; }
		public DetalleOrdenEntities Orden { get; set; }
	}
}
