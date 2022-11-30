using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WikiORT.Context;
using WikiORT.Models;

namespace WikiORT.Controllers
{
    public class AdministradoresController : Controller
    {
        private readonly WikiDatabaseContext _context;

        public AdministradoresController(WikiDatabaseContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> VistaParaAdministradores()
        {
            var wikiDatabaseContext = _context.Articulos.Include(a => a.Categoria);
            return View(await wikiDatabaseContext.ToListAsync());
        }

        // GET: Administradores
        public async Task<IActionResult> Index()
        {
            if (Login() && HttpContext.Session.GetString("Admin").Equals(true.ToString()))
            {
                return View(await _context.Administradores.ToListAsync());
            }
            else
            {
                return RedirectToAction("Index", "Administradores");
            }

        }

        // GET: Administradores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (Login() && devolverSessionId(HttpContext.Session.GetString("Email")).Equals(id.ToString()) || HttpContext.Session.GetString("Admin").Equals(true.ToString()))
            {
                if (id == null || _context.Administradores == null)
                {
                    return NotFound();
                }

                var cliente = await _context.Administradores
                    .FirstOrDefaultAsync(m => m.AdministradorId == id);
                if (cliente == null)
                {
                    return NotFound();
                }

                return View(cliente);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }

        public String devolverSessionId(String email)
        {
            String AdministradorId = _context.Administradores.First(v => v.Email.Equals(email)).AdministradorId.ToString();
            if (Login())
            {
                AdministradorId = HttpContext.Session.GetString("AdministradorId").ToString();
            }

            return AdministradorId;
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UsuarioId,Nombre,Apellido,FechaAlta,Email,Password")] Administrador administrador)
        {
            if (ModelState.IsValid)
            {
                if (!yaExiste(administrador.Email))
                {
                    HttpContext.Session.SetString("Nombre", administrador.Nombre);
                    HttpContext.Session.SetString("Apellido", administrador.Apellido);
                    HttpContext.Session.SetString("Email", administrador.Email);
                    HttpContext.Session.SetString("Admin", true.ToString());
                    _context.Add(administrador);

                    await _context.SaveChangesAsync();
                    HttpContext.Session.SetString("AdministradorId", devolverSessionId(administrador.Email));
                    //return RedirectToAction("VistaParaAdministradores", "Administradores");
                    return RedirectToAction("Home","Index") ;
                }
                else
                {
                    ViewBag.Msg = "Ya se encuentra registrado ese mail";
                    return View("Create");
                }

            }
            return RedirectToAction("VistaParaAdministradores", "Administradores");
        }
        public Boolean yaExiste(String email)
        {
            Boolean existe = false;
            var queryAdministrador = _context.Administradores.Where(e => e.Email.Equals(email)).FirstOrDefault();
            var queryAutor = _context.Autores.Where(e => e.Email.Equals(email)).FirstOrDefault();
            if (queryAdministrador != null || queryAutor != null)
            {
                existe = true;
            }

            return existe;
        }
        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (Login())
            {
                if (id == null || _context.Administradores == null)
                {
                    return NotFound();
                }

                var Administrador = await _context.Administradores.FindAsync(id);
                if (Administrador == null)
                {
                    return NotFound();
                }
                return View(Administrador);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UsuarioId,Nombre,Apellido,FechaAlta,Email,Password")] Administrador administrador)
        {
            if (Login())
            {
                if (id != administrador.AdministradorId)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(administrador);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!AdministradorExists(administrador.AdministradorId))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
                return View(administrador);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }

        // GET: Administradores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (Login())
            {
                if (id == null || _context.Administradores == null)
                {
                    return NotFound();
                }

                var administrador = await _context.Administradores
                    .FirstOrDefaultAsync(m => m.AdministradorId == id);
                if (administrador == null)
                {
                    return NotFound();
                }

                return View(administrador);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }

        // POST: Administradores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (Login() && HttpContext.Session.GetString("Admin").Equals(true.ToString()))
            {
                if (_context.Administradores == null)
                {
                    return Problem("Entity set 'WikiDatabaseContext.Administradores'  is null.");
                }

                var administrador = await _context.Administradores.FindAsync(id);
                if (administrador != null)
                {
                    _context.Administradores.Remove(administrador);

                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else if (Login() && HttpContext.Session.GetString("Admin").Equals(false.ToString()))
            {
                if (_context.Administradores == null)
                {
                    return Problem("Entity set 'WikiDatabaseContext.Administradores'  is null.");
                }

                var administrador = await _context.Administradores.FindAsync(id);
                if (administrador != null)
                {
                    _context.Administradores.Remove(administrador);
                    HttpContext.Session.SetString("AdministradorId", "0");
                    HttpContext.Session.SetString("Nombre", "");
                    HttpContext.Session.SetString("Apellido", "");
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));



            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }


        public bool Login()
        {
            bool l;
            if (HttpContext.Session.GetString("AdministradorId") != null && HttpContext.Session.GetString("Admin") == true.ToString())
            {
                l = true;
            }
            else
            {
                l = false;
            }
            return l;
        }
        private bool AdministradorExists(int id)
        {
            return _context.Administradores.Any(e => e.AdministradorId == id);
        }
    }
}
