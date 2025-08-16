using GestionPrestamosBancoService.Data.Contrato;
using GestionPrestamosBancoService.Models;
using GestionPrestamosBancoService.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GestionPrestamosBancoService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrestamosController : ControllerBase
    {
        private readonly IPrestamo prestamoDB;

        public PrestamosController(IPrestamo prestamoRepo)
        {
            prestamoDB = prestamoRepo;
        }

        [HttpGet]
        [Route("lista")]
        public async Task<IActionResult> Listar()
        {
            return Ok(await Task.Run(() => prestamoDB.Listar()));
        }

        [HttpPost]
        [Route("registro")]
        public async Task<IActionResult> Registrar(SolicitudPrestamoDto solicitud)
        {
            var nuevoPrestamo = await Task.Run(() => prestamoDB.AgregarPrestamo(solicitud));

            return Ok(nuevoPrestamo);
        }

        [HttpGet]
        [Route("lista/{id}")]
        public async Task<IActionResult> ListarPrestamosPorCliente(int id)
        {
            return Ok(await Task.Run(() => prestamoDB.ObtenerPrestamosPorCliente(id)));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> ObtenerPrestamoPorID(int id)
        {
            return Ok(await Task.Run(() => prestamoDB.ObtenerPrestamoPorId(id)));
        }
    }
}
