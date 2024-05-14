using Encriptacion.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Encriptacion.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
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

        [HttpPost]
        public IActionResult Cifrar(string mensaje, int clave)
        {
            string mensajeCifrado = CifrarMensaje(mensaje, clave);
            ViewBag.MensajeCifrado = mensajeCifrado;
            ViewBag.Clave = clave;
            return View("Index");
        }

        [HttpPost]
        public IActionResult Descifrar(string mensajeCifrado, int clave)
        {
            string mensajeDescifrado = DescifrarMensaje(mensajeCifrado, clave);
            ViewBag.MensajeDescifrado = mensajeDescifrado;
            return View("Index");
        }

        private string CifrarMensaje(string mensaje, int clave)
        {
            string mensajeCifrado = "";

            foreach (char caracter in mensaje)
            {
                if (char.IsLetter(caracter))
                {
                    char letraCifrada = (char)(((char.ToUpper(caracter) - 'A' + clave) % 27) + 'A');
                    mensajeCifrado += letraCifrada;
                }
                else
                {
                    mensajeCifrado += caracter;
                }
            }

            return mensajeCifrado;
        }

        private string DescifrarMensaje(string mensajeCifrado, int clave)
        {
            string mensajeDescifrado = "";

            foreach (char caracter in mensajeCifrado)
            {
                if (char.IsLetter(caracter))
                {
                    char letraDescifrada = (char)(((char.ToUpper(caracter) - 'A' - clave + 27) % 27) + 'A');
                    mensajeDescifrado += letraDescifrada;
                }
                else
                {
                    mensajeDescifrado += caracter;
                }
            }

            return mensajeDescifrado;
        }
    }
}
