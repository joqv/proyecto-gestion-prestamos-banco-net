namespace GestionPrestamoBancoWeb.Models.Dto
{
    public class SolicitudCrearCuentaBancaria
    {
        public int IdCliente { get; set; }
        public int IdTipoCuenta { get; set; }
        public int IdMoneda { get; set; }
        public decimal SaldoInicial { get; set; }
    }
}
