using WEB_APP_Panaderia.Entities;

namespace WEB_APP_Panaderia.Interfaces
{
    public interface IContabilidadModel
    {
        CuentaContableEntity InsertarCuentaContable(CuentaContableEntity cuenta);
        void ActualizarCuentaContable(CuentaContableEntity cuenta);
        void EliminarCuentaContable(int idCuenta);
        List<CuentaContableEntity> ConsultarCuentasContables();
        void GenerarAsientoVenta(int idVenta);
        List<EstadoResultadosEntity> GenerarEstadoResultados(DateTime fechaInicio, DateTime fechaFin);
        List<BalanceGeneralEntity> GenerarBalanceGeneral(DateTime fechaCorte);
    }
}