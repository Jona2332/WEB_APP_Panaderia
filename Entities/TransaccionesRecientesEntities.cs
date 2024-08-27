namespace WEB_APP_Panaderia.Entities
{
	public class TransaccionesRecientesEntities
	{
		public long idOrden { get; set; }
		public float total { get; set; }
		public string metodoPago { get; set; } = string.Empty;
		public bool express { get; set; }
		public string estado { get; set; } = string.Empty;
	}
}
