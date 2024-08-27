using WEB_APP_Panaderia.Entities;

namespace WEB_APP_Panaderia.Interfaces
{
	public interface IInsumosModel
	{
		public List<InsumosEntities>? ConsultaInsumos();

		public InsumosEntities? ConsultaUnRegistroInsumo(int idInsumo);

		public List<PresentacionEntities>? ConsultaPresentacion();

		public List<TipoInsumoEntities>? ConsultaTipoInsumo();

		public List<CategoriaInsumosEntities>? ConsultaCategoriaInsumo();

		public List<MarcaEntities>? ConsultaMarca();

		public void ActualizarInsumos(InsumosEntities entidad);

		public void ActualizarPresentacion(PresentacionEntities entidad);

		public void ActualizarTipoInsumo(TipoInsumoEntities entidad);

		public void ActualizarCategoriaInsumo(CategoriaInsumosEntities entidad);

		public void ActualizarMarca(MarcaEntities entidad);

		public void BorrarInsumos(int idInsumo);

		public void BorrarPresentacion(int idPresentacion);

		public void BorrarTipoInsumo(int idTipoInsumo);

		public void BorrarCategoriaInsumo(int idCategoriaInsumo);

		public void BorrarMarca(int idMarca);

		public int AgregarInsumos(InsumosEntities entidad);

		public int AgregarPresentacion(PresentacionEntities entidad);

		public int AgregarTipoInsumo(TipoInsumoEntities entidad);

		public int AgregarCategoriaInsumo(CategoriaInsumosEntities entidad);

		public int AgregarMarca(MarcaEntities entidad);

	}
}
