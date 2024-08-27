namespace WEB_APP_Panaderia.Entities
{
	public class InsumosEntities
	{
		public long idInsumo { get; set; }
		public string nombre { get; set; } = string.Empty;
		public string tipo { get; set; } = string.Empty;
		public string marca { get; set; } = string.Empty;
		public string categoria { get; set; } = string.Empty;
		public string presentacion { get; set; } = string.Empty;
		public long idCategoriaInsumos { get; set; }
		public long idTipoInsumo { get; set; }
		public long idMarca { get; set; }
		public long idPresentacion { get; set; }


	}
}
