using EasyPropertyRental.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EasyPropertyRental.Controllers
{
    public class ApartmentsController : Controller
    {
        private readonly PropertyRentalDbContext _context;

        public ApartmentsController(PropertyRentalDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(decimal? minRent, decimal? maxRent, int? minBedrooms, int? maxBedrooms, int? minBathrooms, int? maxBathrooms)
        {
            var apartments = _context.Apartments.AsQueryable();

            // Filter by rent
            if (minRent.HasValue)
            {
                apartments = apartments.Where(a => a.Rent >= minRent.Value);
            }

            if (maxRent.HasValue)
            {
                apartments = apartments.Where(a => a.Rent <= maxRent.Value);
            }

            // Filter by bedrooms
            if (minBedrooms.HasValue)
            {
                apartments = apartments.Where(a => a.Bedrooms >= minBedrooms.Value);
            }

            if (maxBedrooms.HasValue)
            {
                apartments = apartments.Where(a => a.Bedrooms <= maxBedrooms.Value);
            }

            // Filter by bathrooms
            if (minBathrooms.HasValue)
            {
                apartments = apartments.Where(a => a.Bathrooms >= minBathrooms.Value);
            }

            if (maxBathrooms.HasValue)
            {
                apartments = apartments.Where(a => a.Bathrooms <= maxBathrooms.Value);
            }

            // Pass the filtered apartments to the view
            ViewData["MinRent"] = minRent;
            ViewData["MaxRent"] = maxRent;
            ViewData["MinBedrooms"] = minBedrooms;
            ViewData["MaxBedrooms"] = maxBedrooms;
            ViewData["MinBathrooms"] = minBathrooms;
            ViewData["MaxBathrooms"] = maxBathrooms;

            return View(apartments.ToList());
        }

        // GET: ApartmentsController/Details/5
        public IActionResult Details(int id)
        {
            // Get the apartment with its related building and tenant information
            var apartment = _context.Apartments
                .Where(a => a.ApartmentId == id)
                .Include(a => a.Building)    // Include the related Building
                .Include(a => a.Tenants)     // Include related Tenants
                .FirstOrDefault();

            if (apartment == null)
            {
                return NotFound();
            }

            return View(apartment); // Pass the apartment to the view
        }
    }
}
