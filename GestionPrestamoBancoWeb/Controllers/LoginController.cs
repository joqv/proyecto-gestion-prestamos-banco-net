using GestionPrestamoBancoWeb.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace GestionPrestamoBancoWeb.Controllers
{
    public class LoginController : Controller
    {
        private readonly IConfiguration _config;

        
        public LoginController(IConfiguration config)
        {
            _config = config;
        }
        [HttpGet]
        public IActionResult LoginUsuario()
        {
            return View(new Login());
        }

        [HttpPost]
        public IActionResult LoginUsuario(Login model)
        {
            if (!ModelState.IsValid)
                return View(model);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_config["Services:URL"]);

                var content = new StringContent(JsonConvert.SerializeObject(model),
                                                Encoding.UTF8, "application/json");

                var response = client.PostAsync("Login/login", content).Result;
                if (!response.IsSuccessStatusCode)
                {
                    ViewBag.Error = "Usuario o contraseña incorrectos";
                    return View(model);
                }

                var data = response.Content.ReadAsStringAsync().Result;
                dynamic result = JsonConvert.DeserializeObject(data);

                HttpContext.Session.SetString("JWToken", (string)result.token);
                HttpContext.Session.SetString("Username", (string)result.username);

                return RedirectToAction("Index", "Home");
            }
        }

    }
}
