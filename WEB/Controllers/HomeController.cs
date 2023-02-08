using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WEB.Data;
using WEB.Models;

namespace WEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationContext _context;

        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(ApplicationContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var applicationContext = _context.Repairs.Include(r => r.Equipment).Include(r => r.Executor).Include(r => r.Soft).Include(r => r.Status);
            return View(await applicationContext.ToListAsync());
        }

        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repair = await _context.Repairs
                .Include(r => r.Equipment)
                .Include(r => r.Executor)
                .Include(r => r.Soft)
                .Include(r => r.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (repair == null)
            {
                return NotFound();
            }

            return View(repair);
        }

        [Authorize]
        public IActionResult Create()
        {
            ViewData["EquipmentId"] = new SelectList(_context.Equipments, "Id", "Name");
            ViewData["ExecutorId"] = new SelectList(_context.Executors, "Id", "SurName");
            ViewData["SoftId"] = new SelectList(_context.Softs, "Id", "Name");
            ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "Name");
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DescriptionProblem,StatusId,EquipmentId,SoftId,ExecutorId")] Repair repair)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = await _userManager.FindByEmailAsync(User.Identity.Name);

                repair.RepairDate = DateTime.Now;
                repair.RepairTime = DateTime.Now;
                repair.RepairChangeDate = DateTime.Now;
                repair.RepairChangeTime = DateTime.Now;
                repair.RepairUser = user.UserName;
                _context.Add(repair);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EquipmentId"] = new SelectList(_context.Equipments, "Id", "Name", repair.EquipmentId);
            ViewData["ExecutorId"] = new SelectList(_context.Executors, "Id", "SurName", repair.ExecutorId);
            ViewData["SoftId"] = new SelectList(_context.Softs, "Id", "Name", repair.SoftId);
            ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "Name", repair.StatusId);
            return View(repair);
        }

        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            
            if (id == null)
            {
                return NotFound();
            }

            var repair = await _context.Repairs.FindAsync(id);
                       

            if (repair == null)
            {
                return NotFound();
            }

            if ((User.Identity.Name == repair.RepairUser)||(User.IsInRole("admin")))
            {
                ViewData["EquipmentId"] = new SelectList(_context.Equipments, "Id", "Name", repair.EquipmentId);
                ViewData["ExecutorId"] = new SelectList(_context.Executors, "Id", "SurName", repair.ExecutorId);
                ViewData["SoftId"] = new SelectList(_context.Softs, "Id", "Name", repair.SoftId);
                ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "Name", repair.StatusId);
                return View(repair);
            }
            else
            {
                return
                RedirectToAction("Index");
            }
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RepairDate,RepairTime,RepairChangeDate,RepairChangeTime,DescriptionProblem,StatusId,EquipmentId,SoftId,ExecutorId,RepairUser")] Repair repair)
        {
            if (id != repair.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    repair.RepairChangeDate = DateTime.Now;
                    repair.RepairChangeTime = DateTime.Now;
                    _context.Update(repair);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RepairExists(repair.Id))
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
            ViewData["EquipmentId"] = new SelectList(_context.Equipments, "Id", "InventoryNumber", repair.EquipmentId);
            ViewData["ExecutorId"] = new SelectList(_context.Executors, "Id", "SurName", repair.ExecutorId);
            ViewData["SoftId"] = new SelectList(_context.Softs, "Id", "Name", repair.SoftId);
            ViewData["StatusId"] = new SelectList(_context.Statuses, "Id", "Name", repair.StatusId);
            return View(repair);
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repair = await _context.Repairs
                .Include(r => r.Equipment)
                .Include(r => r.Executor)
                .Include(r => r.Soft)
                .Include(r => r.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (repair == null)
            {
                return NotFound();
            }

            return View(repair);
        }

        [Authorize(Roles = "admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var repair = await _context.Repairs.FindAsync(id);
            _context.Repairs.Remove(repair);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RepairExists(int id)
        {
            return _context.Repairs.Any(e => e.Id == id);
        }
    }
}
