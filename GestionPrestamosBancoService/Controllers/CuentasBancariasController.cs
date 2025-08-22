using GestionPrestamosBancoService.Data.Contrato;
using GestionPrestamosBancoService.Models;
using GestionPrestamosBancoService.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionPrestamosBancoService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentasBancariasController : ControllerBase
    {
        private readonly ICuentaBancaria cuentaBancariaDB;
        public CuentasBancariasController(ICuentaBancaria cuentaBancariaRepo)
        {
            cuentaBancariaDB = cuentaBancariaRepo;
        }

        [HttpGet]
        [Route("lista")]
        public async Task<IActionResult> Listar()
        {
            return Ok(await Task.Run(() => cuentaBancariaDB.Listar()));
        }

        [HttpPost]
        [Route("crear")]
        public async Task<IActionResult> Registrar(SolicitudCrearCuentaBancariaDto solicitud)
        {
            var nuevaCuenta = await Task.Run(() => cuentaBancariaDB.CrearCuentaBancaria(solicitud));

            return Ok(nuevaCuenta);
        }

        [HttpGet]
        [Route("lista/{id}")]
        public async Task<IActionResult> ListarCuentasBancariasPorCliente(int id)
        {
            return Ok(await Task.Run(() => cuentaBancariaDB.ObtenerCuentasBancariasPorCliente(id)));
        }
    }
}
