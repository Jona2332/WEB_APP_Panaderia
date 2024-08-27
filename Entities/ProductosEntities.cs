namespace WEB_APP_Panaderia.Entities
{
	public class ProductosEntities
	{
		public long idProducto { get; set; }
		public string nombre { get; set; } = string.Empty;
		public int cantidad { get; set; }
		public float precioUnitario { get; set; }
		public string descripcion { get; set; } = string.Empty;
		public string tipo { get; set; } = string.Empty;
		public long idCategoria { get; set; }
		public long idInsumo { get; set; }
		public long idProveedor { get; set; }
	}
}
