using GestionPrestamoBancoWeb.Models;
using GestionPrestamoBancoWeb.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace GestionPrestamoBancoWeb.Controllers
{
    public class CuentaBancariaController : Controller
    {
        private readonly IConfiguration _config;
        public CuentaBancariaController(IConfiguration config)
        {
            _config = config;
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

        private CuentaBancaria crearCuentaBancaria(SolicitudCrearCuentaBancaria solicitud)
        {
            CuentaBancaria cuentaBancaria = null;

            using (var clienteHTTP = new HttpClient())
            {
                clienteHTTP.BaseAddress = new Uri(_config["Services:URL"]);

                StringContent contenido = new StringContent(
                    JsonConvert.SerializeObject(solicitud),
                    System.Text.Encoding.UTF8,
                    "application/json"
                    );

                var mensaje = clienteHTTP.PostAsync("CuentasBancarias/crear", contenido).Result;

                var data = mensaje.Content.ReadAsStringAsync().Result;

                cuentaBancaria = JsonConvert.DeserializeObject<CuentaBancaria>(data);
            }

            return cuentaBancaria;
        }

        public IActionResult CuentasPorCliente(int id)
        {
            var listado = listarCuentasBancariasPorCliente(id);

            var idCliente = id;

            ViewBag.idCliente = idCliente;

            return View(listado);
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

        private List<TipoCuenta> listarTipoCuenta()
        {
            var listado = new List<TipoCuenta>();
            using (var clienteHTTP = new HttpClient())
            {
                clienteHTTP.BaseAddress = new Uri(_config["Services:URL"]);

                var mensaje = clienteHTTP.GetAsync("TiposCuenta/lista").Result;

                var data = mensaje.Content.ReadAsStringAsync().Result;

                listado = JsonConvert.DeserializeObject<List<TipoCuenta>>(data);
            }

            return listado;
        }

        public IActionResult CreateCuentaBancaria(int id)
        {
            var listaCuentas = listarTipoCuenta();
            var listaMonedas = listarMonedas();
            var idCliente = id;

            ViewBag.tiposCuenta = new SelectList(listaCuentas, "IdTipoCuenta", "NombreTipo");
            ViewBag.monedas = new SelectList(listaMonedas, "IdMoneda", "Nombre");

            ViewBag.idCliente = idCliente;

            return View(new SolicitudCrearCuentaBancaria());
        }

        [HttpPost]
        public IActionResult CreateCuentaBancaria(SolicitudCrearCuentaBancaria solicitud)
        {
            var nuevaCuenta = crearCuentaBancaria(solicitud);
            return RedirectToAction("CuentasPorCliente", new {id = solicitud.IdCliente});
        }
    }
}
