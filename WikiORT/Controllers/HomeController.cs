using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WikiORT.Context;
using WikiORT.Models;

namespace WikiORT.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly WikiDatabaseContext _context;


        public HomeController(ILogger<HomeController> logger, WikiDatabaseContext context)
        {
            _context = context;
            _logger = logger;
        }


        public IActionResult Index()
        {
            return View();

        }

        public IActionResult Index2()
        {
            ViewBag.msg = "Usuario no registrado";
            return View();

        }

        public IActionResult Privacy()
        {
            return View();
        }

       /* public IActionResult cerrarSesion()
        {
            HttpContext.Session.SetString("AutorId", "0");
            HttpContext.Session.SetString("Nombre", "");
            HttpContext.Session.SetString("Apellido", "");

            return RedirectToAction("Index");
        }*/

        public IActionResult Logueo(String email, String password)
        {
            var queryAutor = _context.Autores.Where(a => a.Email.Equals(email) && a.Password.Equals(password)).FirstOrDefault();     
            var queryAdministrador = _context.Administradores.Where(a => a.Email.Equals(email) && a.Password.Equals(password)).FirstOrDefault();
            var a = RedirectToAction("Index");

            if (queryAdministrador == null && queryAutor == null) {

               a= RedirectToAction("Index2");
            } 


            if ((queryAdministrador != null && queryAutor == null) || (queryAdministrador != null && queryAutor != null))
            {
                HttpContext.Session.SetString("AdministradorId", queryAdministrador.AdministradorId.ToString());
                HttpContext.Session.SetString("Nombre", queryAdministrador.Nombre);
                HttpContext.Session.SetString("Apellido", queryAdministrador.Nombre);
                HttpContext.Session.SetString("Admin", true.ToString());
       
                a = RedirectToAction("VistaParaAdministradores", "Administradores");
            }
            else if (queryAutor != null && queryAdministrador == null)
            {
                HttpContext.Session.SetString("AutorId", queryAutor.AutorId.ToString());
                HttpContext.Session.SetString("Nombre", queryAutor.Nombre);
                HttpContext.Session.SetString("Apellido", queryAutor.Apellido);
                HttpContext.Session.SetString("Admin", false.ToString());

                a = RedirectToAction("VistaParaAutores", "Autores");
            }
            return a;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult EditarPerfil()
        {
            return View();
        }

    }
}
