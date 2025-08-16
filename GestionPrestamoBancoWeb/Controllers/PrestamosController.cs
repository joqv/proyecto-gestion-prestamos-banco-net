using GestionPrestamoBancoWeb.Models;
using GestionPrestamoBancoWeb.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        private Prestamo obtenerPrestamoPorID(int id)
        {
            Prestamo prestamo = null;

            using (var clienteHTTP = new HttpClient())
            {
                clienteHTTP.BaseAddress = new Uri(_config["Services:URL"]);

                var mensaje = clienteHTTP.GetAsync($"Prestamos/{id}").Result;

                var data = mensaje.Content.ReadAsStringAsync().Result;

                prestamo = JsonConvert.DeserializeObject<Prestamo>(data);
            }

            return prestamo;
        }

        private Prestamo crearPrestamo(SolicitudCrearPrestamoDto solicitud)
        {
            Prestamo nuevoPrestamo = null;

            using (var clienteHTTP = new HttpClient())
            {
                clienteHTTP.BaseAddress = new Uri(_config["Services:URL"]);

                StringContent contenido = new StringContent(
                    JsonConvert.SerializeObject(solicitud),
                    System.Text.Encoding.UTF8,
                    "application/json"
                    );

                var mensaje = clienteHTTP.PostAsync("Prestamos/registro", contenido).Result;

                var data = mensaje.Content.ReadAsStringAsync().Result;

                nuevoPrestamo = JsonConvert.DeserializeObject<Prestamo>(data);
            }

            return nuevoPrestamo;
        }

        private List<Moneda> listarMonedas()
        {
            var listado = new List<Moneda>();
            using (var clienteHTTP = new HttpClient())
            {
                clienteHTTP.BaseAddress = new Uri(_config["Services:URL"]);

                var mensaje = clienteHTTP.GetAsync("Monedas/lista").Result;

                var data = mensaje.Content.ReadAsStringAsync().Result;

                listado = JsonConvert.DeserializeObject<List<Moneda>>(data);
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
            ViewBag.idCliente = id;

            return View(listado);
        }


        public IActionResult CreatePrestamo(int id)
        {
            var listaMonedas = listarMonedas();
            var idCliente = id;

            ViewBag.monedas = new SelectList(listaMonedas, "IdMoneda", "Nombre");

            ViewBag.idCliente = idCliente;

            return View(new SolicitudCrearPrestamoDto());
        }

        [HttpPost]
        public IActionResult CreatePrestamo(SolicitudCrearPrestamoDto solicitud)
        {
            var nuevoPrestamo = crearPrestamo(solicitud);

            return RedirectToAction("PorCliente", new { id = solicitud.IdCliente });
        }
    }
}
