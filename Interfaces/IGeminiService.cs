using WEB_APP_Panaderia.Entities;

namespace WEB_APP_Panaderia.Interfaces
{
    public interface IGeminiService
    {
        Task<List<CotizacionEntity>> ExtraerDatosCotizacion(Stream archivo, string extension, string nombreProveedor);
        Task<List<CotizacionEntity>> AnalizarYRecomendarCotizaciones(List<CotizacionEntity> nuevasCotizaciones, List<CotizacionEntity> cotizacionesExistentes);
    }
}