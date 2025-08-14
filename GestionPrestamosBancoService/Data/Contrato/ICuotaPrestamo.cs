using GestionPrestamosBancoService.Models;
using GestionPrestamosBancoService.Models.Dto;

namespace GestionPrestamosBancoService.Data.Contrato
{
    public interface ICuotaPrestamo
    {
        List<CuotaPrestamo> ListarCuotas();
        CuotaPrestamo PagarCuotaPrestamo(SolicitudPagoCuotaPrestamoDto solicitud);
        CuotaPrestamo ObtenerCuotaPorID(int id);
    }
}
