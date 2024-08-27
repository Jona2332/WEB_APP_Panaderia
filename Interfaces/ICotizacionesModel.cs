using WEB_APP_Panaderia.Entities;

namespace WEB_APP_Panaderia.Interfaces
{
    public interface ICotizacionesModel
    {
        List<CotizacionEntity> ObtenerTodasLasCotizaciones();
        Task<int> ImportarCotizacion(CotizacionEntity cotizacion);
        Task<CotizacionEntity> ObtenerCotizacionPorId(int id);
        Task ActualizarCotizacion(CotizacionEntity cotizacion);
        Task EliminarCotizacion(int id);
        Task ActualizarRecomendaciones(List<CotizacionEntity> cotizaciones);
        public int RegistrarCotizaciones(CotizacionEntity entidad);
        //Task RegistrarCotizaciones(CotizacionEntity entidad);
    }
}