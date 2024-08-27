namespace WEB_APP_Panaderia.Entities
{
	public class RegistroDesechosEntities
	{
		public long idEvento { get; set; }
		public long idCategoriaTratamientoResiduo { get; set; }
		public long idDisposicionFinal { get; set; }
		public long idDesechoTransporte { get; set; }
		public string tratamientoResiduo { get; set; } = string.Empty;
		public string disposicionFinal { get; set; } = string.Empty;
		public string transporte { get; set; } = string.Empty;
		public DateTime fechaRevision { get; set; }
		public long idCategoria { get; set; }
		public string categoria { get; set; } = string.Empty;
		public long idUsuario { get; set; }
		public string usuario { get; set; } = string.Empty;
	}
}
