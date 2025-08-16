namespace GestionPrestamosBancoService.Models
{
    public class CuotaPrestamo
    {
        public int IdCuota { get; set; }
        public int IdPrestamo { get; set; }
        public int NumeroCuota { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public decimal MontoTotalCuota { get; set; }
        public decimal MontoCapital { get; set; }
        public decimal MontoInteres{ get; set; }
        public decimal SaldoPendiente { get; set; }
        public decimal MontoPagado { get; set; }
        public string EstadoCuota { get; set; }
    }
}
