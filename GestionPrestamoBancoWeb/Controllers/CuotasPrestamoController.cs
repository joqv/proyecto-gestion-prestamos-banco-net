using GestionPrestamoBancoWeb.Models;
using GestionPrestamoBancoWeb.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        private CuotaPrestamo pagarCuotaPrestamo(SolicitudPagoCuotaPrestamoDto solicitud)
        {
            CuotaPrestamo cuotaPagada = null;

            using (var clienteHTTP = new HttpClient())
            {
                clienteHTTP.BaseAddress = new Uri(_config["Services:URL"]);

                StringContent contenido = new StringContent(
                    JsonConvert.SerializeObject(solicitud),
                    System.Text.Encoding.UTF8,
                    "application/json"
                    );

                var mensaje = clienteHTTP.PostAsync("CuotasPrestamo/pagarCuota", contenido).Result;

                var data = mensaje.Content.ReadAsStringAsync().Result;

                cuotaPagada = JsonConvert.DeserializeObject<CuotaPrestamo>(data);
            }

            return cuotaPagada;
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

        private CuotaPrestamo obtenerCuotaPorID(int id)
        {
            CuotaPrestamo cuota = null;

            using (var clienteHTTP = new HttpClient())
            {
                clienteHTTP.BaseAddress = new Uri(_config["Services:URL"]);

                var mensaje = clienteHTTP.GetAsync($"CuotasPrestamo/{id}").Result;

                var data = mensaje.Content.ReadAsStringAsync().Result;

                cuota = JsonConvert.DeserializeObject<CuotaPrestamo>(data);
            }

            return cuota;
        }

        private List<CuentaBancaria> listarCuentasBancariasPorCliente(int id)
        {
            var listado = new List<CuentaBancaria>();

            using (var clienteHTTP = new HttpClient())
            {
                clienteHTTP.BaseAddress = new Uri(_config["Services:URL"]);

                var mensaje = clienteHTTP.GetAsync($"CuentasBancarias/lista/{id}").Result;

                var data = mensaje.Content.ReadAsStringAsync().Result;

                listado = JsonConvert.DeserializeObject<List<CuentaBancaria>>(data);
            }

            return listado;
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
            return View();
        }

        public IActionResult PorPrestamo(int id)
        {
            var prestamo = obtenerPrestamoPorID(id);
            var listado = listarCuotasPorPrestamo(id);

            ViewBag.IdCliente = prestamo.IdCliente;

            if (prestamo != null)
            {
                var cuentas = listarCuentasBancariasPorCliente(prestamo.IdCliente);
                ViewBag.Cuentas = cuentas;
                ViewBag.Prestamo = prestamo;
            }
                
            return View(listado);
        }

        public IActionResult PagarCuota(int idPrestamo, int idCuota)
        {
            var prestamo = obtenerPrestamoPorID(idPrestamo);
            var cuota = obtenerCuotaPorID(idCuota);
            var listaCuentas = listarCuentasBancariasPorCliente(prestamo.IdCliente);
            var listaMonedas = listarMonedas();

            var selectCuentas = listaCuentas.Select(c =>
            {
                var moneda = listaMonedas.FirstOrDefault(m => m.IdMoneda == c.IdMoneda);
                var codigo = moneda.Codigo;
                var simbolo = moneda.Simbolo;

                return new SelectListItem
                {
                    Value = c.IdCuenta.ToString(),
                    Text = $"N°: {c.NumeroCuenta} - {codigo} - {simbolo} {c.Saldo}"
                };
            }).ToList();

            //ViewBag.listaCuentasCliente = new SelectList(listaCuentas, "IdCuenta", "NumeroCuenta");
            ViewBag.listaCuentasCliente = new SelectList(selectCuentas, "Value", "Text");
            ViewBag.Cuota = cuota;

            return View(new SolicitudPagoCuotaPrestamoDto());
        }


        [HttpPost]
        public IActionResult PagarCuota(SolicitudPagoCuotaPrestamoDto solicitud)
        {
            var cuota = pagarCuotaPrestamo(solicitud);
            return RedirectToAction("PorPrestamo", new { id =  cuota.IdPrestamo});
        }
    }
}
