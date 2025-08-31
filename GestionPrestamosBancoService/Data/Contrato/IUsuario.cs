using GestionPrestamosBancoService.Models;

namespace GestionPrestamosBancoService.Data.Contrato
{
    public interface IUsuario
    {
        Usuario Login(string username, string password);

        Usuario ObtenerPorUsername(string username);
    }
}
