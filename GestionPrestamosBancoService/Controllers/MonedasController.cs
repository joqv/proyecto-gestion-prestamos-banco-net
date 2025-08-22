using GestionPrestamosBancoService.Data.Contrato;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionPrestamosBancoService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonedasController : ControllerBase
    {
        private readonly IMoneda monedaDB;
        public MonedasController(IMoneda monedaRepo)
        {
            monedaDB = monedaRepo;
        }

        [HttpGet]
        [Route("lista")]
        public async Task<IActionResult> Listar()
        {
            return Ok(await Task.Run(() => monedaDB.ListarMonedas()));
        }
    }
}
