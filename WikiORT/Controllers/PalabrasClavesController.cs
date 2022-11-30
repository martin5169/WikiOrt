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
    public class PalabrasClavesController : Controller
    {
        private readonly WikiDatabaseContext _context;

        public PalabrasClavesController(WikiDatabaseContext context)
        {
            _context = context;
        }

        // GET: PalabrasClaves
        public async Task<IActionResult> Index()
        {
            return View(await _context.PalabrasClaves.ToListAsync());
        }

        // GET: PalabrasClaves/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var palabraClave = await _context.PalabrasClaves
                .FirstOrDefaultAsync(m => m.PalabraClaveId == id);
            if (palabraClave == null)
            {
                return NotFound();
            }

            return View(palabraClave);
        }

        // GET: PalabrasClaves/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PalabrasClaves/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PalabraClaveId,Palabra")] PalabraClave palabraClave)
        {
            if (ModelState.IsValid)
            {
                _context.Add(palabraClave);
                await _context.SaveChangesAsync();
                return RedirectToAction("VistaParaAdministradores", "Administradores");
            }
            return View(palabraClave);
        }

        // GET: PalabrasClaves/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var palabraClave = await _context.PalabrasClaves.FindAsync(id);
            if (palabraClave == null)
            {
                return NotFound();
            }
            return View(palabraClave);
        }

        // POST: PalabrasClaves/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PalabraClaveId,Palabra")] PalabraClave palabraClave)
        {
            if (id != palabraClave.PalabraClaveId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(palabraClave);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PalabraClaveExists(palabraClave.PalabraClaveId))
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
            return View(palabraClave);
        }

        // GET: PalabrasClaves/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var palabraClave = await _context.PalabrasClaves
                .FirstOrDefaultAsync(m => m.PalabraClaveId == id);
            if (palabraClave == null)
            {
                return NotFound();
            }

            return View(palabraClave);
        }

        // POST: PalabrasClaves/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var palabraClave = await _context.PalabrasClaves.FindAsync(id);
            _context.PalabrasClaves.Remove(palabraClave);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PalabraClaveExists(int id)
        {
            return _context.PalabrasClaves.Any(e => e.PalabraClaveId == id);
        }
    }
}
