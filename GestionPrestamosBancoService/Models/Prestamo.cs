namespace GestionPrestamosBancoService.Models
{
    public class Prestamo
    {
        public int IdPrestamo { get; set; }
        public int IdCliente { get; set; }
        public int IdMoneda { get; set; }
        public decimal MontoPrincipal { get; set; }
        public decimal TasaInteres { get; set; }
        public int PlazoMeses { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinEstimada { get; set; }
        public decimal SaldoPendiente { get; set; }
        public string EstadoPrestamo { get; set; }
    }
}
