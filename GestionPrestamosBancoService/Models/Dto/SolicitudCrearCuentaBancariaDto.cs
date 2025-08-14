namespace GestionPrestamosBancoService.Models.Dto
{
    public class SolicitudCrearCuentaBancariaDto
    {
        public int IdCliente { get; set; }
        public int IdTipoCuenta { get; set; }
        public int IdMoneda { get; set; }
        public decimal SaldoInicial { get; set; }
    }
}
