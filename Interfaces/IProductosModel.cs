using WEB_APP_Panaderia.Entities;

namespace WEB_APP_Panaderia.Interfaces
{
	public interface IProductosModel
	{
		public List<ProductosEntities> ConsultarProductosPorCategoria(int idCategoria);
	}
}
