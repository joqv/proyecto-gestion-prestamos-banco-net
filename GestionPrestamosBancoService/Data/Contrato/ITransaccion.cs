using GestionPrestamosBancoService.Models;

namespace GestionPrestamosBancoService.Data.Contrato
{
    public interface ITransaccion
    {
        List<Transaccion> Listar();
    }
}
