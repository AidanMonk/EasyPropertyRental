using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EasyPropertyRental.Models;
using EasyPropertyRental.Models.ViewModels;

namespace EasyPropertyRental.Controllers
{
    public class OwnerApartmentsController : Controller
    {
        private readonly PropertyRentalDbContext _context;

        public OwnerApartmentsController(PropertyRentalDbContext context)
        {
            _context = context;
        }

        // GET: Apartments
        public async Task<IActionResult> Index()
        {
            var apartments = await _context.Apartments
                .Include(a => a.Building) // Eagerly load Building navigation property
                .Include(a => a.Tenants) // Eagerly load Tenants navigation property
                .ToListAsync();

            var viewModel = apartments.Select(a => new ApartmentWithTenantViewModel
            {
                ApartmentId = a.ApartmentId,
                UnitNumber = a.UnitNumber,
                Floor = a.Floor,
                Bedrooms = a.Bedrooms,
                Bathrooms = a.Bathrooms,
                Rent = a.Rent,
                BuildingName = a.Building?.Name ?? "Unknown Building", // Handle null Building
                TenantNames = a.Tenants?.Select(t => $"{t.FirstName} {t.LastName}").ToList() ?? new List<string>() // Handle null Tenants
            }).ToList();

            return View(viewModel);
        }

        // GET: Apartments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apartment = await _context.Apartments
                .Include(a => a.Building)
                .FirstOrDefaultAsync(m => m.ApartmentId == id);
            if (apartment == null)
            {
                return NotFound();
            }

            return View(apartment);
        }

        // GET: Apartments/Create
        public IActionResult Create()
        {
            if (int.TryParse(User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value, out int userPoId))
            {
                var pmIds = _context.PropertyManagers
                    .Where(pm => pm.PoId == userPoId)
                    .Select(pm => pm.PmId)
                    .ToList();

                var filteredBuildings = _context.Buildings
                    .Where(b => pmIds.Contains((int)b.PmId))
                    .ToList();

                ViewData["BuildingId"] = new SelectList(filteredBuildings, "BuildingId", "Name");
            }
            else
            {
                // Handle case where po_id is missing or invalid
                ViewData["BuildingId"] = new SelectList(Enumerable.Empty<object>(), "BuildingId", "Name");
            }

            return View();
        }

        // POST: Apartments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ApartmentId,BuildingId,UnitNumber,Floor,Bedrooms,Bathrooms,Rent,IsAvailable")] Apartment apartment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(apartment);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["BuildingId"] = new SelectList(_context.Buildings, "BuildingId", "BuildingId", apartment.BuildingId);
            return View(apartment);
        }

        // GET: Apartments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apartment = await _context.Apartments.FindAsync(id);
            if (apartment == null)
            {
                return NotFound();
            }
            ViewData["BuildingId"] = new SelectList(_context.Buildings, "BuildingId", "BuildingId", apartment.BuildingId);
            return View(apartment);
        }

        // POST: Apartments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ApartmentId,BuildingId,UnitNumber,Floor,Bedrooms,Bathrooms,Rent,IsAvailable")] Apartment apartment)
        {
            if (id != apartment.ApartmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(apartment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApartmentExists(apartment.ApartmentId))
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
            ViewData["BuildingId"] = new SelectList(_context.Buildings, "BuildingId", "BuildingId", apartment.BuildingId);
            return View(apartment);
        }


        // GET: Apartments/Delete/5
        public async Task<IActionResult> Delete(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var apartment = await _context.Apartments
                    .Include(a => a.Building)
                    .FirstOrDefaultAsync(m => m.ApartmentId == id);
                if (apartment == null)
                {
                    return NotFound();
                }

                return View(apartment);
            }

            // POST: Apartments/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                var apartment = await _context.Apartments.FindAsync(id);
                if (apartment != null)
                {
                    _context.Apartments.Remove(apartment);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            private bool ApartmentExists(int id)
            {
                return _context.Apartments.Any(e => e.ApartmentId == id);
            }
        }
}
