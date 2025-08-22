namespace GestionPrestamosBancoService.Models.Dto
{
    public class SolicitudPagoCuotaPrestamoDto
    {
        public int IdCuota { get; set; }
        public int IdCuentaOrigen { get; set; }
        public decimal MontoAPagar { get; set; }
    }
}
