using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WikiORT.Context;
using WikiORT.Models;

namespace WikiORT.Controllers
{
    public class MensajesController : Controller
    {
        private readonly WikiDatabaseContext _context;

        public MensajesController(WikiDatabaseContext context)
        {
            _context = context;
        }

        // GET: Mensajes
        public async Task<IActionResult> Index()
        {
            var wikiDatabaseContext = _context.Mensajes.Include(m => m.Articulo);
            return View(await wikiDatabaseContext.ToListAsync());
        }

        // GET: Mensajes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mensaje = await _context.Mensajes
                .Include(m => m.Articulo)
                .FirstOrDefaultAsync(m => m.MensajeId == id);
            if (mensaje == null)
            {
                return NotFound();
            }

            return View(mensaje);
        }

        // GET: Mensajes/Create
        public IActionResult Create()
        {
            ViewData["ArticuloId"] = new SelectList(_context.Articulos, "ArticuloId", "ArticuloId");
            return View();
        }

        // POST: Mensajes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MensajeId,Titulo,Texto,Fecha,ArticuloId")] Mensaje mensaje)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mensaje);
                await _context.SaveChangesAsync();
                return RedirectToAction("VistaParaAutores", "Autores");
            }
            ViewData["ArticuloId"] = new SelectList(_context.Articulos, "ArticuloId", "ArticuloId", mensaje.ArticuloId);
            return View(mensaje);
        }

        // GET: Mensajes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mensaje = await _context.Mensajes.FindAsync(id);
            if (mensaje == null)
            {
                return NotFound();
            }
            ViewData["ArticuloId"] = new SelectList(_context.Articulos, "ArticuloId", "ArticuloId", mensaje.ArticuloId);
            return View(mensaje);
        }

        // POST: Mensajes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MensajeId,Titulo,Texto,Fecha,ArticuloId")] Mensaje mensaje)
        {
            if (id != mensaje.MensajeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mensaje);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MensajeExists(mensaje.MensajeId))
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
            ViewData["ArticuloId"] = new SelectList(_context.Articulos, "ArticuloId", "ArticuloId", mensaje.ArticuloId);
            return View(mensaje);
        }

        // GET: Mensajes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mensaje = await _context.Mensajes
                .Include(m => m.Articulo)
                .FirstOrDefaultAsync(m => m.MensajeId == id);
            if (mensaje == null)
            {
                return NotFound();
            }

            return View(mensaje);
        }

        // POST: Mensajes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mensaje = await _context.Mensajes.FindAsync(id);
            _context.Mensajes.Remove(mensaje);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MensajeExists(int id)
        {
            return _context.Mensajes.Any(e => e.MensajeId == id);
        }
        
    }
}
