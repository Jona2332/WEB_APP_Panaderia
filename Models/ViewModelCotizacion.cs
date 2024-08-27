using WEB_APP_Panaderia.Entities;

namespace WEB_APP_Panaderia.Models
{
    public class ViewModelCotizacion
    {
        public IEnumerable<CotizacionEntity>? Cotizaciones { get; set; }
        public CotizacionEntity? Cotizacion { get; set; }
        public IFormFile File { get; set; }
    }
}