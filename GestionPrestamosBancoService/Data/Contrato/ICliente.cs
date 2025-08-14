using GestionPrestamosBancoService.Models;

namespace GestionPrestamosBancoService.Data.Contrato
{
    public interface ICliente
    {
        List<Cliente> Listar();

        Cliente RegistrarCliente(Cliente cliente);
    }
}
