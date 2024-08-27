using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WEB_APP_Panaderia.Entities;
using WEB_APP_Panaderia.Models;

namespace WEB_APP_Panaderia.Interfaces
{
	public interface IRegistroDesechosModel
	{
		public List<RegistroDesechosEntities> ConsultarRegistroDesechos();
		public int AgregarRegistroDesecho(RegistroDesechosEntities reporte);
		public void ActualizarRegistroDesecho(RegistroDesechosEntities reporte);
		public RegistroDesechosEntities? ConsultarUnRegistroDesecho(int id);
		public byte[] GenerarPdfRegistroDesechos(List<RegistroDesechosEntities> registros);
		public byte[] GenerarExcelRegistroDesechos(List<RegistroDesechosEntities> registros);
		public List<CategoriaDesechoEntities> ConsultarCategoriaDesecho();
		public List<CategoriaDesechoTratamientoEntities> ConsultarCategoriaDesechoTratamiento();
		public List<DesechoDisposicionFinalEntities> ConsultarDesechoDisposicionFinal();
		public List<DesechoTransporteEntities> ConsultarDesechoTransporte();
        public void EliminarRegistroDesechos(int idEvento);
	}
}
