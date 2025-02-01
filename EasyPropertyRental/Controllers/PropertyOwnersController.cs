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
    public class PropertyOwnersController : Controller
    {
        private readonly PropertyRentalDbContext _context;

        public PropertyOwnersController(PropertyRentalDbContext context)
        {
            _context = context;
        }

        // GET: PropertyOwners
        public async Task<IActionResult> Index()
        {
            return View(await _context.PropertyOwners.ToListAsync());
        }

        // GET: PropertyOwners/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertyOwner = await _context.PropertyOwners
                .FirstOrDefaultAsync(m => m.PoId == id);
            if (propertyOwner == null)
            {
                return NotFound();
            }

            return View(propertyOwner);
        }

        // GET: PropertyOwners/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PropertyOwners/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PoId,FirstName,LastName,Email,Password,CreatedAt")] PropertyOwner propertyOwner)
        {
            if (ModelState.IsValid)
            {
                _context.Add(propertyOwner);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(propertyOwner);
        }

        // GET: PropertyOwners/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertyOwner = await _context.PropertyOwners.FindAsync(id);
            if (propertyOwner == null)
            {
                return NotFound();
            }
            return View(propertyOwner);
        }

        // POST: PropertyOwners/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PoId,FirstName,LastName,Email,Password,CreatedAt")] PropertyOwner propertyOwner)
        {
            if (id != propertyOwner.PoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(propertyOwner);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PropertyOwnerExists(propertyOwner.PoId))
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
            return View(propertyOwner);
        }

        // GET: PropertyOwners/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var propertyOwner = await _context.PropertyOwners
                .FirstOrDefaultAsync(m => m.PoId == id);
            if (propertyOwner == null)
            {
                return NotFound();
            }

            return View(propertyOwner);
        }

        // POST: PropertyOwners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var propertyOwner = await _context.PropertyOwners.FindAsync(id);
            if (propertyOwner != null)
            {
                _context.PropertyOwners.Remove(propertyOwner);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PropertyOwnerExists(int id)
        {
            return _context.PropertyOwners.Any(e => e.PoId == id);
        }
    }
}
