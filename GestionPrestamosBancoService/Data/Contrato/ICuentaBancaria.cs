using GestionPrestamosBancoService.Models;

namespace GestionPrestamosBancoService.Data.Contrato
{
    public interface ICuentaBancaria
    {
        List<CuentaBancaria> Listar();

        CuentaBancaria Registrar(CuentaBancaria cuentaBancaria);
    }
}
