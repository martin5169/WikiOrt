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
    public class AutoresController : Controller
    {
        private readonly WikiDatabaseContext _context;

        public AutoresController(WikiDatabaseContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> VistaParaAutores()
        {
            var wikiDatabaseContext = _context.Articulos.Include(a => a.Categoria);
            return View(await wikiDatabaseContext.ToListAsync());
        }

        // GET: Autores
        public async Task<IActionResult> Index()
        {
            return View(await _context.Autores.ToListAsync());
        }

        // GET: Autores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autor = await _context.Autores
                .FirstOrDefaultAsync(m => m.AutorId == id);
            if (autor == null)
            {
                return NotFound();
            }

            return View(autor);
        }

        // GET: Autores/Create
        public IActionResult Create()
        {
            return View();
        }


        //Aca va el editar

        private IActionResult RedirectToAction(Task<IActionResult> task)
        {
            throw new NotImplementedException();
        }

        public Boolean yaExiste(String email)
        {
            Boolean existe = false;
            var queryAutor = _context.Autores.Where(e => e.Email.Equals(email)).FirstOrDefault();
            var queryAdministrador = _context.Administradores.Where(e => e.Email.Equals(email)).FirstOrDefault();
            if (queryAutor != null || queryAdministrador != null)
            {
                existe = true;
            }

            return existe;
        }

        public async Task<IActionResult> EditarMiPerfil()
        {

            if (Login())
            {
                var AutorId = int.Parse(devolverSessionId());
                if (_context.Autores == null)
                {
                    return NotFound();
                }

                var autor = await _context.Autores.FindAsync(AutorId);

                if (autor == null)
                {
                    return NotFound();
                }
                return View(autor);
            }
            else
            {
                RedirectToAction("Edit");
            }
            return RedirectToAction("VistaParaAutores","Autores");

        }


        public String devolverSessionId()
        {
            return HttpContext.Session.GetString("AutorId").ToString();

        }

        // POST: Autores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AutorId,Nombre,Apellido,FechaAlta,Email,Password")] Autor autor)
        {
            if (ModelState.IsValid)
            {
                if (!yaExiste(autor.Email))
                {
                    HttpContext.Session.SetString("Nombre", autor.Nombre);
                    HttpContext.Session.SetString("Apellido", autor.Nombre);
                    HttpContext.Session.SetString("Email", autor.Email);
                    HttpContext.Session.SetString("Admin", false.ToString());
                    _context.Add(autor);
                    await _context.SaveChangesAsync();
                    HttpContext.Session.SetString("AutorId", devolverSessionId());
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Msg = "Ya se encuentra registrado ese mail";
                    return View("Create");
                }
            }
            return View(autor);
        }

        // GET: Autores/Edit/5
        public async Task<IActionResult> Edit()
        {
            var id = devolverSessionId();
            if (id == null)
            {
                return NotFound();
            }

            var autor = await _context.Autores.FindAsync(id);
            if (autor == null)
            {
                return NotFound();
            }
            return View(autor);
        }

        // POST: Autores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("AutorId,Nombre,Apellido,FechaAlta,Email,Password")] Autor autor)
        {
            var id = int.Parse(devolverSessionId());

            if (id != autor.AutorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(autor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AutorExists(autor.AutorId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("VistaParaAutores","Autores");
            }
            return View(autor);
        }

        // GET: Autores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autor = await _context.Autores
                .FirstOrDefaultAsync(m => m.AutorId == id);
            if (autor == null)
            {
                return NotFound();
            }

            return View(autor);
        }

        // POST: Autores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (Login() && HttpContext.Session.GetString("Admin").Equals(true.ToString()))
            {
                if (_context.Autores == null)
                {
                    return Problem("Entity set 'WikiDatabaseContext.Autores'  is null.");
                }
                var autor = await _context.Autores.FindAsync(id);
                if (autor != null)
                {
                    _context.Autores.Remove(autor);

                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else if (Login() && HttpContext.Session.GetString("Admin").Equals(false.ToString()))
            {
                if (_context.Autores == null)
                {
                    return Problem("Entity set 'WikiDatabaseContext.Autores'  is null.");
                }
                var autor = await _context.Autores.FindAsync(id);
                if (autor != null)
                {
                    _context.Autores.Remove(autor);
                    _context.Autores.Remove(autor);
                    HttpContext.Session.SetString("AutorId", "0");
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

        private bool AutorExists(int id)
        {
            return _context.Autores.Any(e => e.AutorId == id);
        }

        public bool Login()
        {
            bool l;
            if (HttpContext.Session.GetString("AutorId") != null || HttpContext.Session.GetString("AdministradorId") != null)
            {
                l = true;
            }
            else
            {
                l = false;
            }
            return l;
        }
    }
}

