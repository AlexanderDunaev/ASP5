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
    public class AccountingsController : Controller
    {
        private readonly ApplicationContext _context;

        public AccountingsController(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var applicationContext = _context.Accountings.Include(a => a.Department).Include(a => a.Equipment);
            return View(await applicationContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accounting = await _context.Accountings
                .Include(a => a.Department)
                .Include(a => a.Equipment)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (accounting == null)
            {
                return NotFound();
            }

            return View(accounting);
        }

        public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name");
            ViewData["EquipmentId"] = new SelectList(_context.Equipments, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AccountingDate,DepartmentId,EquipmentId")] Accounting accounting)
        {
            if (ModelState.IsValid)
            {
                _context.Add(accounting);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", accounting.DepartmentId);
            ViewData["EquipmentId"] = new SelectList(_context.Equipments, "Id", "Name", accounting.EquipmentId);
            return View(accounting);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accounting = await _context.Accountings.FindAsync(id);
            if (accounting == null)
            {
                return NotFound();
            }
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", accounting.DepartmentId);
            ViewData["EquipmentId"] = new SelectList(_context.Equipments, "Id", "Name", accounting.EquipmentId);
            return View(accounting);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AccountingDate,DepartmentId,EquipmentId")] Accounting accounting)
        {
            if (id != accounting.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(accounting);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountingExists(accounting.Id))
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
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name", accounting.DepartmentId);
            ViewData["EquipmentId"] = new SelectList(_context.Equipments, "Id", "Name", accounting.EquipmentId);
            return View(accounting);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accounting = await _context.Accountings
                .Include(a => a.Department)
                .Include(a => a.Equipment)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (accounting == null)
            {
                return NotFound();
            }

            return View(accounting);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var accounting = await _context.Accountings.FindAsync(id);
            _context.Accountings.Remove(accounting);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountingExists(int id)
        {
            return _context.Accountings.Any(e => e.Id == id);
        }
    }
}
