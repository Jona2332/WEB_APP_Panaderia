using WEB_APP_Panaderia.Entities;

namespace WEB_APP_Panaderia.Interfaces
{
	public interface IMetricasModel
	{
		public ConteoVentasEntities? ConsultarConteoVentasPorSemana();
		public ConteoVentasEntities? ConsultarConteoVentasPorDia();
		public List<TotalVentasProductoEntities>? ConsultarTotalVentasProductoPorDia();
		public List<TransaccionesRecientesEntities>? ConsultarTransaccionesRecientes();
		public GananciasEntities? ConsultarGanancias();
		public List<RestanteLotesEntities>? ConsultarRestanteLotes();
	}
}