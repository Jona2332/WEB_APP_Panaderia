namespace WEB_APP_Panaderia.Entities
{
	public class OrdenProductoEntities
	{
		public long idProducto { get; set; }
		public long idCategoria { get; set; }
		public string nombre { get; set; } = string.Empty;
		public int cantidad { get; set; }
		public float precioUnitario { get; set; }
		public string descripcion { get; set; } = string.Empty;
		public string sabores { get; set; } = string.Empty;
		public string tipo { get; set; } = string.Empty;
		public float total { get; set; }
		public string nota { get; set; } = string.Empty;
	}
}
