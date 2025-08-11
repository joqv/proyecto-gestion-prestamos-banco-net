using GestionPrestamosBancoService.Data.Contrato;
using Microsoft.AspNetCore.Mvc;

namespace GestionPrestamosBancoService.Controllers
{
    public class ClientesController : Controller
    {

        private readonly ICliente clienteDB;
        public ClientesController(ICliente clienteRepo)
        {
            clienteDB = clienteRepo;
        }

        [HttpGet]
        [Route("lista")]
        public async Task<IActionResult> Listar()
        {
            return Ok(await Task.Run(() => clienteDB.Listar()));
        }
    }
}
