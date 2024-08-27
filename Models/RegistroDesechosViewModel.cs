using WEB_APP_Panaderia.Entities;

namespace WEB_APP_Panaderia.Models
{
	public class RegistroDesechosViewModel
	{
		public IEnumerable<RegistroDesechosEntities> Reportes { get; set; }
		public RegistroDesechosEntities Reporte { get; set; }

	}
}
