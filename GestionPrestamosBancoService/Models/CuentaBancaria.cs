namespace GestionPrestamosBancoService.Models
{
    public class CuentaBancaria
    {
        public int IdCuenta { get; set; }
        public int IdCliente { get; set; }
        public int IdTipoCuenta { get; set; }
        public int IdMoneda { get; set; }
        public string NumeroCuenta { get; set; }
        public decimal Saldo { get; set; }
        public DateTime FechaApertura { get; set; }
    }
}
