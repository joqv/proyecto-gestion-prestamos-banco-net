using GestionPrestamoBancoWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GestionPrestamoBancoWeb.Controllers
{
    public class ClientesController : Controller
    {
        private readonly IConfiguration _config;
        public ClientesController(IConfiguration config)
        {
            _config = config;
        }

        // Metodos privados

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

        private Cliente registrarCliente(Cliente cliente)
        {
            Cliente nuevoCliente = null;

            using (var clienteHTTP = new HttpClient())
            {
                clienteHTTP.BaseAddress = new Uri(_config["Services:URL"]);

                StringContent contenido = new StringContent(
                    JsonConvert.SerializeObject(cliente),
                    System.Text.Encoding.UTF8,
                    "application/json"
                    );

                var mensaje = clienteHTTP.PostAsync("Clientes/registro", contenido).Result;

                var data = mensaje.Content.ReadAsStringAsync().Result;

                nuevoCliente = JsonConvert.DeserializeObject<Cliente>(data);
            }

            return nuevoCliente;
        }


        // Actions Results

        public IActionResult Index()
        {
            var listado = listarClientes();
            return View(listado);
        }

        public IActionResult Create()
        {
            return View(new Cliente());
        }

        [HttpPost]
        public IActionResult Create(Cliente cliente)
        {
            registrarCliente(cliente);
            return RedirectToAction("Index");
        }
    }
}
