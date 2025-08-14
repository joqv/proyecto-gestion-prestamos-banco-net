using GestionPrestamoBancoWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GestionPrestamoBancoWeb.Controllers
{
    public class PrestamosController : Controller
    {
        private readonly IConfiguration _config;
        public PrestamosController(IConfiguration config)
        {
            _config = config;
        }

        // Metodos privados

        private List<Prestamo> listarPrestamos()
        {
            var listado = new List<Prestamo>();
            using (var clienteHTTP = new HttpClient())
            {
                clienteHTTP.BaseAddress = new Uri(_config["Services:URL"]);

                var mensaje = clienteHTTP.GetAsync("Prestamos/lista").Result;

                var data = mensaje.Content.ReadAsStringAsync().Result;

                listado = JsonConvert.DeserializeObject<List<Prestamo>>(data);
            }

            return listado;
        }

        private List<Prestamo> listarPrestamosPorCliente(int id)
        {
            var listado = new List<Prestamo>();

            using (var clienteHTTP = new HttpClient())
            {
                clienteHTTP.BaseAddress = new Uri(_config["Services:URL"]);

                var mensaje = clienteHTTP.GetAsync($"Prestamos/lista/{id}").Result;

                var data = mensaje.Content.ReadAsStringAsync().Result;

                listado = JsonConvert.DeserializeObject<List<Prestamo>>(data);
            }

            return listado;
        }

        public IActionResult Index()
        {
            var listado = listarPrestamos();
            return View(listado);
        }

        public IActionResult PorCliente(int id)
        {
            var listado = listarPrestamosPorCliente(id);
            return View(listado);
        }
    }
}
