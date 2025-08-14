using GestionPrestamosBancoService.Data.Contrato;
using GestionPrestamosBancoService.Models;
using Microsoft.AspNetCore.Mvc;

namespace GestionPrestamosBancoService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
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

        [HttpPost]
        public async Task<IActionResult> Registrar(Cliente cliente)
        {
            return Ok(await Task.Run(() => clienteDB.RegistrarCliente(cliente)));
        }
    }
}
