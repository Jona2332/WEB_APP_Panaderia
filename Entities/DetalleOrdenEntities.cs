namespace WEB_APP_Panaderia.Entities
{
	public class DetalleOrdenEntities
	{
		public long idOrden { get; set; }
		public long idDetalleOrden { get; set; }
		public long idProducto { get; set; }
		public long idCategoria { get; set; }
		public string nombreProducto { get; set; } = string.Empty;
		public float precio { get; set; }
		public string tipo { get; set; } = string.Empty;
		public string descripcion { get; set; } = string.Empty;
		public string sabores { get; set; } = string.Empty;
		public long cantidad { get; set; }
		public float total { get; set; }
		public string nota { get; set; } = string.Empty;
		public float montoTotal { get; set; }
		public string metodoPago { get; set; } = string.Empty;
		public bool express { get; set; }
		public long idEstadoOrden { get; set; }
		public string estado { get; set; } = string.Empty;
		public long idCliente { get; set; }
		public string nombreCliente { get; set; } = string.Empty;
		public string correo { get; set; } = string.Empty;
		public string telefono { get; set; } = string.Empty;
		public string direccion { get; set; } = string.Empty;
	}
}
