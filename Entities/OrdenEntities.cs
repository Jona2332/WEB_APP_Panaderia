namespace WEB_APP_Panaderia.Entities
{
	public class OrdenEntities
	{
		public long idOrden { get; set; }
		public List<OrdenProductoEntities> OrdenProductos { get; set; } = new List<OrdenProductoEntities>();
		public float montoTotal { get; set; }
		public string metodoPago { get; set; } = string.Empty;
		public bool express { get; set; }
		public ClienteOrdenEntities ClienteOrden { get; set; } = new ClienteOrdenEntities();
	}
}
