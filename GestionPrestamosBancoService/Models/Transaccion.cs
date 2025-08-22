namespace GestionPrestamosBancoService.Models
{
    public class Transaccion
    {
        public int IdTransaccion { get; set; }
        public int IdCuentaOrigen { get; set; }
        public int IdCuentaDestino { get; set; }
        public string TipoTransaccion { get; set; }
        public decimal Monto { get; set; }
        public int IdMoneda { get; set; }
        public DateTime FechaHoraTransaccion { get; set; }
        public string Descripcion { get; set; }
    }
}
