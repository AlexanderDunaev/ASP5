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
    public class ExecutorsController : Controller
    {
        private readonly ApplicationContext _context;

        public ExecutorsController(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Executors.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var executor = await _context.Executors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (executor == null)
            {
                return NotFound();
            }

            return View(executor);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SurName,Name,Patronymic")] Executor executor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(executor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(executor);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var executor = await _context.Executors.FindAsync(id);
            if (executor == null)
            {
                return NotFound();
            }
            return View(executor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SurName,Name,Patronymic")] Executor executor)
        {
            if (id != executor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(executor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExecutorExists(executor.Id))
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
            return View(executor);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var executor = await _context.Executors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (executor == null)
            {
                return NotFound();
            }

            return View(executor);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var executor = await _context.Executors.FindAsync(id);
            _context.Executors.Remove(executor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExecutorExists(int id)
        {
            return _context.Executors.Any(e => e.Id == id);
        }
    }
}
