using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EasyPropertyRental.Models;

namespace EasyPropertyRental.Controllers
{
    public class PropertyManagersController : Controller
    {
        private readonly PropertyRentalDbContext _context;

        public PropertyManagersController(PropertyRentalDbContext context)
        {
            _context = context;
        }

        // GET: PropertyManagers
        public async Task<IActionResult> Index()
        {
            //get id of the logged in owner
            int.TryParse(User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value, out int userPoId);
            var propertyRentalDbContext = _context.PropertyManagers
                .Include(p => p.Po)
                .Where(p => p.PoId == userPoId);

            return View(await propertyRentalDbContext.ToListAsync());
        }

        // GET: PropertyManagers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertyManager = await _context.PropertyManagers
                .Include(p => p.Po)
                .FirstOrDefaultAsync(m => m.PmId == id);
            if (propertyManager == null)
            {
                return NotFound();
            }

            return View(propertyManager);
        }

        // GET: PropertyManagers/Create
        public IActionResult Create()
        {
            ViewData["PoId"] = new SelectList(_context.PropertyOwners, "PoId", "PoId");

            var poId = User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value;
            ViewBag.CurrentId = poId;

            return View();
        }

        // POST: PropertyManagers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PmId,FirstName,LastName,Email,Phone,Password,PoId")] PropertyManager propertyManager)
        {
            if (ModelState.IsValid)
            {
                _context.Add(propertyManager);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["PoId"] = new SelectList(_context.PropertyOwners, "PoId", "PoId", propertyManager.PoId);
            return View(propertyManager);
        }

        // GET: PropertyManagers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertyManager = await _context.PropertyManagers.FindAsync(id);
            if (propertyManager == null)
            {
                return NotFound();
            }
            ViewData["PoId"] = new SelectList(_context.PropertyOwners, "PoId", "PoId", propertyManager.PoId);
            return View(propertyManager);
        }

        // POST: PropertyManagers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PmId,FirstName,LastName,Email,Phone,Password,PoId")] PropertyManager propertyManager)
        {
            if (id != propertyManager.PmId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(propertyManager);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PropertyManagerExists(propertyManager.PmId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["PoId"] = new SelectList(_context.PropertyOwners, "PoId", "PoId", propertyManager.PoId);
            return View(propertyManager);
        }

        // GET: PropertyManagers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertyManager = await _context.PropertyManagers
                .Include(p => p.Po)
                .FirstOrDefaultAsync(m => m.PmId == id);
            if (propertyManager == null)
            {
                return NotFound();
            }

            return View(propertyManager);
        }

        // POST: PropertyManagers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var propertyManager = await _context.PropertyManagers.FindAsync(id);
            if (propertyManager != null)
            {
                _context.PropertyManagers.Remove(propertyManager);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool PropertyManagerExists(int id)
        {
            return _context.PropertyManagers.Any(e => e.PmId == id);
        }
    }
}
