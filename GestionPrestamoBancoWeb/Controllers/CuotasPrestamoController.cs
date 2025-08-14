using GestionPrestamoBancoWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GestionPrestamoBancoWeb.Controllers
{
    public class CuotasPrestamoController : Controller
    {
        private readonly IConfiguration _config;
        public CuotasPrestamoController(IConfiguration config)
        {
            _config = config;
        }


        private List<CuotaPrestamo> listarCuotasPorPrestamo(int id)
        {
            var listado = new List<CuotaPrestamo>();
            using (var clienteHTTP = new HttpClient())
            {
                clienteHTTP.BaseAddress = new Uri(_config["Services:URL"]);

                var mensaje = clienteHTTP.GetAsync($"CuotasPrestamo/lista/{id}").Result;

                var data = mensaje.Content.ReadAsStringAsync().Result;

                listado = JsonConvert.DeserializeObject<List<CuotaPrestamo>>(data);
            }

            return listado;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult PorPrestamo(int id)
        {
            var listado = listarCuotasPorPrestamo(id);
            return View(listado);
        }
    }
}
