using GestionPrestamoBancoWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace GestionPrestamoBancoWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _config;

        public HomeController(ILogger<HomeController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        private List<Cliente> listarClientes()
        {
            var listado = new List<Cliente>();
            using (var clienteHTTP = new HttpClient())
            {
                clienteHTTP.BaseAddress = new Uri(_config["Services:URL"]);

                var mensaje = clienteHTTP.GetAsync("Clientes/lista").Result;

                var data = mensaje.Content.ReadAsStringAsync().Result;

                listado = JsonConvert.DeserializeObject<List<Cliente>>(data);
            }

            return listado;
        }

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

        private List<CuentaBancaria> listarCuentasBancarias()
        {
            var listado = new List<CuentaBancaria>();

            using (var clienteHTTP = new HttpClient())
            {
                clienteHTTP.BaseAddress = new Uri(_config["Services:URL"]);

                var mensaje = clienteHTTP.GetAsync($"CuentasBancarias/lista").Result;

                var data = mensaje.Content.ReadAsStringAsync().Result;

                listado = JsonConvert.DeserializeObject<List<CuentaBancaria>>(data);
            }

            return listado;
        }

        public IActionResult Index()
        {
            var clientes = listarClientes();
            var prestamos = listarPrestamos();
            var cuentasBancarias = listarCuentasBancarias();

            ViewBag.cantidadClientes = clientes.Count();
            ViewBag.cantidadPrestamos = prestamos.Count();
            ViewBag.cantidadCuentas = cuentasBancarias.Count();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
