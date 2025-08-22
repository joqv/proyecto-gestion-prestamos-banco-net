using GestionPrestamosBancoService.Data.Contrato;
using GestionPrestamosBancoService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionPrestamosBancoService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiposCuentaController : ControllerBase
    {
        private readonly ITipoCuenta tipoCuentaDB;

        public TiposCuentaController(ITipoCuenta tipoCuentaRepo)
        {
            tipoCuentaDB = tipoCuentaRepo;
        }

        [HttpGet]
        [Route("lista")]
        public async Task<IActionResult> Listar()
        {
            return Ok(await Task.Run(() => tipoCuentaDB.ListarTipoCuenta()));
        }
    }
}
