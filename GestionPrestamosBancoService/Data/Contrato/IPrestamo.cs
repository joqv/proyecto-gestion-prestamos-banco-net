using GestionPrestamosBancoService.Models;
using GestionPrestamosBancoService.Models.Dto;

namespace GestionPrestamosBancoService.Data.Contrato
{
    public interface IPrestamo
    {
        List<Prestamo> Listar();
        Prestamo AgregarPrestamo(SolicitudPrestamoDto solicitud);
        List<Prestamo> ObtenerPrestamosPorCliente(int id);
    }
}
