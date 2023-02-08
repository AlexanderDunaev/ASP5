using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WEB.Data;
using WEB.Models;

namespace WEB.Controllers
{
    [Authorize(Roles = "admin")]
    public class SoftsController : Controller
    {
        private readonly ApplicationContext _context;

        public SoftsController(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Softs.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var soft = await _context.Softs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (soft == null)
            {
                return NotFound();
            }

            return View(soft);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Version,Description")] Soft soft)
        {
            if (ModelState.IsValid)
            {
                _context.Add(soft);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(soft);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var soft = await _context.Softs.FindAsync(id);
            if (soft == null)
            {
                return NotFound();
            }
            return View(soft);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Version,Description")] Soft soft)
        {
            if (id != soft.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(soft);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SoftExists(soft.Id))
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
            return View(soft);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var soft = await _context.Softs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (soft == null)
            {
                return NotFound();
            }

            return View(soft);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var soft = await _context.Softs.FindAsync(id);
            _context.Softs.Remove(soft);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SoftExists(int id)
        {
            return _context.Softs.Any(e => e.Id == id);
        }
    }
}
