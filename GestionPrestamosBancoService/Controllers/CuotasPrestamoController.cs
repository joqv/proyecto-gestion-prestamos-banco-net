using GestionPrestamosBancoService.Data.Contrato;
using GestionPrestamosBancoService.Models;
using GestionPrestamosBancoService.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestionPrestamosBancoService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuotasPrestamoController : ControllerBase
    {
        private readonly ICuotaPrestamo cuotasPrestamoDB;
        public CuotasPrestamoController(ICuotaPrestamo cuotaPrestamoRepo)
        {
            cuotasPrestamoDB = cuotaPrestamoRepo;
        }

        [HttpGet]
        [Route("lista")]
        public async Task<IActionResult> Listar()
        {
            return Ok(await Task.Run(() => cuotasPrestamoDB.ListarCuotas()));
        }

        [HttpPost]
        [Route("pagarCuota")]
        public async Task<IActionResult> Registrar(SolicitudPagoCuotaPrestamoDto solicitud)
        {
            var cuotaPagada = await Task.Run(() => cuotasPrestamoDB.PagarCuotaPrestamo(solicitud));

            return Ok(cuotaPagada);
        }

        [HttpGet]
        [Route("lista/{id}")]
        public async Task<IActionResult> ListarPrestamosPorCuota(int id)
        {
            return Ok(await Task.Run(() => cuotasPrestamoDB.ObtenerCuotasPorPrestamo(id)));
        }
    }
}
