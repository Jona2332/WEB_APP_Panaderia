namespace WEB_APP_Panaderia.Entities
{
    public class CotizacionEntity
    {
        public int Id_Producto { get; set; }
        public string? Nombre { get; set; }
        public decimal Precio { get; set; }
        public string? Proveedor { get; set; }
        public string Nombre_Producto { get; set; } = string.Empty;
        public decimal Precio_Por_Kg { get; set; }
        public string Nombre_Proveedor { get; set; } = string.Empty;
        public string? Recomendacion { get; set; } = string.Empty;

    }
}