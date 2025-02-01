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
    public class OwnerBuildingsController : Controller
    {
        private readonly PropertyRentalDbContext _context;

        public OwnerBuildingsController(PropertyRentalDbContext context)
        {
            _context = context;

            if (_context == null)
            {
                throw new InvalidOperationException("The DbContext is not properly initialized.");
            }
        }

        // GET: Buildings
        public async Task<IActionResult> Index()
        {

            int.TryParse(User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value, out int userPoId);
            var allBuildings = await _context.Buildings
                .Include(b => b.Pm)
                .ToListAsync();

            var viewModel = allBuildings.Select(b => new BuildingViewModel
            {
                BuildingId = b.BuildingId,
                Name = b.Name,
                Address = b.Address,
                PropertyManagerName = b.Pm != null
        ? $"{b.Pm.FirstName} {b.Pm.LastName}"
        : "No Manager"
            }).ToList();

            return View(viewModel);
        }

        // GET: Buildings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var building = await _context.Buildings
                .Include(b => b.Pm)
                .FirstOrDefaultAsync(m => m.BuildingId == id);
            if (building == null)
            {
                return NotFound();
            }

            return View(building);
        }

        // GET: Buildings/Create
        public IActionResult Create()
        {
            ViewData["PmId"] = new SelectList(_context.PropertyManagers, "PmId", "PmId");
            return View();
        }

        // POST: Buildings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BuildingId,Name,Address,PmId")] Building building)
        {
            if (ModelState.IsValid)
            {
                _context.Add(building);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["PmId"] = new SelectList(_context.PropertyManagers, "PmId", "PmId", building.PmId);
            return View(building);
        }

        // GET: Buildings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var building = await _context.Buildings.FindAsync(id);
            if (building == null)
            {
                return NotFound();
            }

            int.TryParse(User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value, out int userPoId);
            var propertyManagers = await _context.PropertyManagers
                .Where(pm => pm.PoId == userPoId)
                .Select(pm => new
                {
                    pm.PmId,
                    FullName = $"{pm.FirstName} {pm.LastName}"
                })
                .ToListAsync();

            // Create a SelectList with the full name
            ViewBag.PmId = new SelectList(propertyManagers, "PmId", "FullName", building.PmId);
            return View(building);
        }

        // POST: Buildings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BuildingId,Name,Address,PmId")] Building building)
        {
            if (id != building.BuildingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(building);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BuildingExists(building.BuildingId))
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
            ViewData["PmId"] = new SelectList(_context.PropertyManagers, "PmId", "PmId", building.PmId);
            return View(building);
        }

        public async Task<IActionResult> EditManager(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var building = await _context.Buildings.FindAsync(id);
            if (building == null)
            {
                return NotFound();
            }

            int.TryParse(User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value, out int userPoId);
            var propertyManagers = await _context.PropertyManagers
                .Where(pm => pm.PoId == userPoId)
                .Select(pm => new
                {
                    pm.PmId,
                    FullName = $"{pm.FirstName} {pm.LastName}"
                })
                .ToListAsync();

            //
            ViewBag.Name = building.Name;
            ViewBag.Address = building.Address;

            // Create a SelectList with the full name
            ViewBag.PmId = new SelectList(propertyManagers, "PmId", "FullName", building.PmId);
            return View(building);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditManager(int id, [Bind("BuildingId,Name,Address,PmId")] Building building)
        {
            if (id != building.BuildingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(building);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BuildingExists(building.BuildingId))
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
            ViewData["PmId"] = new SelectList(_context.PropertyManagers, "PmId", "PmId", building.PmId);
            return View(building);
        }


        // GET: Buildings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var building = await _context.Buildings
                .Include(b => b.Pm)
                .FirstOrDefaultAsync(m => m.BuildingId == id);
            if (building == null)
            {
                return NotFound();
            }

            return View(building);
        }

        // POST: Buildings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var building = await _context.Buildings.FindAsync(id);
            if (building != null)
            {
                _context.Buildings.Remove(building);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool BuildingExists(int id)
        {
            return _context.Buildings.Any(e => e.BuildingId == id);
        }
    }
}
