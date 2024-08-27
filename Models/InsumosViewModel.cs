using WEB_APP_Panaderia.Entities;

namespace WEB_APP_Panaderia.Models
{
	public class InsumosViewModel
	{
		public IEnumerable<InsumosEntities>? Insumos { get; set; }
		public InsumosEntities Insumo { get; set; }
	}
}
