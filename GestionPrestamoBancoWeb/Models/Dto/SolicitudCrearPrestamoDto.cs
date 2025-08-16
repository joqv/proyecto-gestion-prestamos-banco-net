namespace GestionPrestamoBancoWeb.Models.Dto
{
    public class SolicitudCrearPrestamoDto
    {
        public int IdCliente { get; set; }
        public int IdMoneda { get; set; }
        public decimal MontoPrincipal { get; set; }
        public decimal TasaInteres { get; set; }
        public int PlazoMeses { get; set; }
    }
}
