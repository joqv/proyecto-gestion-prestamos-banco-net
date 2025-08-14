using GestionPrestamosBancoService.Models;
using GestionPrestamosBancoService.Models.Dto;

namespace GestionPrestamosBancoService.Data.Contrato
{
    public interface ICuentaBancaria
    {
        List<CuentaBancaria> Listar();
        CuentaBancaria CrearCuentaBancaria(SolicitudCrearCuentaBancariaDto solicitud);
        CuentaBancaria ObtenerCuentaBancariaPorID(int id);
    }
}
