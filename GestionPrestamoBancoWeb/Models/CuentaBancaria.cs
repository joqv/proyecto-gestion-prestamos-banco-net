using System.ComponentModel.DataAnnotations;

namespace GestionPrestamoBancoWeb.Models
{
    public class CuentaBancaria
    {
        public int IdCuenta { get; set; }
        public int IdCliente { get; set; }
        public int IdTipoCuenta { get; set; }
        public int IdMoneda { get; set; }
        public string NumeroCuenta { get; set; }
        public decimal Saldo { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaApertura { get; set; }
    }
}
