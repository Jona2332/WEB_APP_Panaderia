using WEB_APP_Panaderia.Entities;

namespace WEB_APP_Panaderia.Models
{
    public class BalanceGeneralViewModel
    {
        public DateTime FechaCorte { get; set; } = DateTime.Now;
        public List<BalanceGeneralEntity> Activos { get; set; }
        public List<BalanceGeneralEntity> Pasivos { get; set; }
        public List<BalanceGeneralEntity> Capital { get; set; }

        public decimal TotalActivos => Activos.Sum(a => a.SaldoActual);
        public decimal TotalPasivos => Pasivos.Sum(p => p.SaldoActual);
        public decimal TotalCapital => Capital.Sum(c => c.SaldoActual);
        public decimal TotalPasivoCapital => TotalPasivos + TotalCapital;
    }
}