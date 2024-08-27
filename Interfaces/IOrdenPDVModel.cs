using WEB_APP_Panaderia.Entities;

namespace WEB_APP_Panaderia.Interfaces
{
	public interface IOrdenPDVModel
	{
		public OrdenEntities? CompletarOrden(OrdenEntities orden);

		public List<DetalleOrdenEntities> OrdenesPizzeria();

		public List<DetalleOrdenEntities>? ConsultarDetalleOrden(int id);

		public List<EstadoOrdenesEntities> ConsultarEstadosOrden();

		public DetalleOrdenEntities? ActualizarEstadoOrden(DetalleOrdenEntities orden);
	}
}
