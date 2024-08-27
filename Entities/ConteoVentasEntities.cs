namespace WEB_APP_Panaderia.Entities
{
	public class ConteoVentasEntities
	{
		public long anio { get; set; }
		public long semana { get; set; }
		public long numeroVentas { get; set; }
		public float montoTotalVentas { get; set; }
		public string fecha { get; set; } = string.Empty;
	}
}
