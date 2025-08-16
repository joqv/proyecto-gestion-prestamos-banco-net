using System.ComponentModel.DataAnnotations;

namespace GestionPrestamoBancoWeb.Models
{
    public class Prestamo
    {
        public int IdPrestamo { get; set; }
        public int IdCliente { get; set; }
        public int IdMoneda { get; set; }
        public decimal MontoPrincipal { get; set; }
        public decimal TasaInteres { get; set; }
        public int PlazoMeses { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaInicio { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaFinEstimada { get; set; }

        public decimal SaldoPendiente { get; set; }
        public string EstadoPrestamo { get; set; }
    }
}
