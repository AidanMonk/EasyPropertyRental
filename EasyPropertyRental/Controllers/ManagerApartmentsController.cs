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
    public class ManagerApartmentsController : Controller
    {
        private readonly PropertyRentalDbContext _context;

        public ManagerApartmentsController(PropertyRentalDbContext context)
        {
            _context = context;
        }

        // GET: Apartments
        public async Task<IActionResult> Index()
        {
            int.TryParse(User.Claims.FirstOrDefault(c => c.Type == "pm_id")?.Value, out int userPmId);

            var propertyRentalDbContext = _context.Apartments
                .Include(a => a.Building)
                .Include(a => a.Tenants)
                .Where(a => a.Building.PmId == userPmId)
                .ToListAsync();

            return View(await propertyRentalDbContext);
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
 
            if (int.TryParse(User.Claims.FirstOrDefault(c => c.Type == "pm_id")?.Value, out int userPmId))
            {
                // Filter buildings where the PmId matches the user's pm_id
                var filteredBuildings = _context.Buildings
                    .Where(b => b.PmId == userPmId)
                    .ToList();

                ViewData["BuildingId"] = new SelectList(filteredBuildings, "BuildingId", "Name");
            }
            else
            {
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
                return RedirectToAction(nameof(Index));
            }
            ViewData["BuildingId"] = new SelectList(_context.Buildings, "BuildingId", "name", apartment.BuildingId);
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


        public async Task<IActionResult> AssignTenant(int id)
        {
            var apartment = await _context.Apartments
                .Include(a => a.Tenants)
                .Include(a => a.Building)
                .FirstOrDefaultAsync(a => a.ApartmentId == id);

            if (apartment == null)
            {
                return NotFound();
            }

                int.TryParse(User.Claims.FirstOrDefault(c => c.Type == "pm_id")?.Value, out int pmId);

                var tenants = await _context.Tenants
                    .Where(t => t.ApartmentId == null)  // Only tenants not currently assigned
                    .Where(t => t.PmId == pmId)  // Filter tenants under the PMs of the owner
                    .Select(t => new SelectListItem
                    {
                        Value = t.TenantId.ToString(),
                        Text = $"{t.FirstName} {t.LastName}"
                    }).ToListAsync();

                var model = new AssignTenantViewModel
                {
                    ApartmentId = apartment.ApartmentId,
                    ApartmentInfo = $"{apartment.Building?.Name} - {apartment.UnitNumber}",
                    TenantList = tenants
                };

                return View(model);
        }

        // POST: AssignTenant
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignTenant(AssignTenantViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var tenant = await _context.Tenants.FindAsync(model.SelectedTenantId);

            if (tenant == null)
            {
                return NotFound();
            }
            //set the apartment as unavailable

            var apartment = await _context.Apartments.FindAsync(model.ApartmentId);
            apartment.IsAvailable = false;
            _context.Apartments.Update(apartment);

            tenant.ApartmentId = model.ApartmentId; // Assign the tenant to the apartment
            _context.Update(tenant);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
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
