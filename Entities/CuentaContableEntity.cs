namespace WEB_APP_Panaderia.Entities
{
    public class CuentaContableEntity
    {
        public int IdCuenta { get; set; }
        public string CodigoCuenta { get; set; }
        public string NombreCuenta { get; set; }
        public string TipoCuenta { get; set; }
        public decimal SaldoActual { get; set; }
        public int? CuentaPadre { get; set; }
    }
}