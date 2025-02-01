using EasyPropertyRental.Models;
using EasyPropertyRental.Models.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace EasyPropertyRental.Controllers
{
    public class TenantAccountController : Controller
    {
        private readonly PropertyRentalDbContext _dbContext;

        public TenantAccountController(PropertyRentalDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Login()
        {
            var loginViewModel = new LoginViewModel();
            ModelState.Clear();
            return View(loginViewModel);
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {

            if (ModelState.IsValid)
            {
                //check if the entered info matches an entry in the database
                var user = _dbContext.Tenants.Where(x => x.Email == model.Email && x.Password == model.Password).FirstOrDefault();
                if (user != null)
                {
                    //success, create cookie
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.Name, user.FirstName),
                        new Claim(ClaimTypes.Role, "Tenant"),
                        new Claim("t_id", user.TenantId.ToString()),
                        new Claim("t_apartment_id", user.ApartmentId.ToString())
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                    return RedirectToAction("Home");
                }
                else
                {
                    ModelState.AddModelError("", "email or password is not correct");
                }
            }
            return View(model);
        }

        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        public IActionResult Home()
        {
            ViewBag.Name = HttpContext.User.Identity.Name;
            return View();
        }

        // GET: Tenants
        public async Task<IActionResult> Index()
        {
            int.TryParse(User.Claims.FirstOrDefault(c => c.Type == "pm_id")?.Value, out int pmId);

            // Filter tenants based on the pm_id (Property Manager ID)
            var propertyRentalDbContext = _dbContext.Tenants
                .Where(t => t.PmId == pmId)
                .Include(t => t.Apartment);
            return View(await propertyRentalDbContext.ToListAsync());
        }

        // GET: Tenants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tenant = await _dbContext.Tenants
                .Include(t => t.Apartment)
                .FirstOrDefaultAsync(m => m.TenantId == id);
            if (tenant == null)
            {
                return NotFound();
            }

            return View(tenant);
        }

        // GET: Tenants/Create
        public IActionResult Create()
        {
            int.TryParse(User.Claims.FirstOrDefault(c => c.Type == "pm_id")?.Value, out int userId);

            ViewBag.NullValue = null;
            ViewBag.ManagerId = userId;
            ViewData["ApartmentId"] = new SelectList(_dbContext.Apartments, "ApartmentId", "ApartmentId");
            return View();
        }

        // POST: Tenants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Tenant tenant)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Tenants.Add(tenant);
                await _dbContext.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            int.TryParse(User.Claims.FirstOrDefault(c => c.Type == "pm_id")?.Value, out int userId);

            var apartments = await _dbContext.Apartments
                .Include(a => a.Building)    // Ensure the Building is loaded as well
                .Include(a => a.Tenants)     // Make sure Tenants are included here
                .Where(a => a.Building.PmId == userId)
                .ToListAsync();

            ViewBag.ManagerId = tenant.PmId;  // Make sure to set ViewBag again for a failed post
            ViewBag.ApartmentId = new SelectList(apartments, "ApartmentId", "ApartmentName", tenant.ApartmentId);
            return View(tenant);
        }

        // GET: Tenants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tenant = await _dbContext.Tenants.FindAsync(id);
            if (tenant == null)
            {
                return NotFound();
            }

            int.TryParse(User.Claims.FirstOrDefault(c => c.Type == "Id")?.Value, out int userPoId);

            ViewBag.ManagerId = userPoId;
            ViewData["ApartmentId"] = new SelectList(_dbContext.Apartments, "ApartmentId", "ApartmentId", tenant.ApartmentId);
            return View(tenant);
        }

        // POST: Tenants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TenantId,FirstName,LastName,Email,Phone,Password,ApartmentId,MoveInDate,MoveOutDate")] Tenant tenant)
        {
            if (id != tenant.TenantId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _dbContext.Update(tenant);
                    await _dbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TenantExists(tenant.TenantId))
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
            ViewData["ApartmentId"] = new SelectList(_dbContext.Apartments, "ApartmentId", "ApartmentId", tenant.ApartmentId);
            return View(tenant);
        }

        // GET: Tenants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tenant = await _dbContext.Tenants
                .Include(t => t.Apartment)
                .FirstOrDefaultAsync(m => m.TenantId == id);
            if (tenant == null)
            {
                return NotFound();
            }

            return View(tenant);
        }

        // POST: Tenants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tenant = await _dbContext.Tenants.FindAsync(id);
            if (tenant != null)
            {
                _dbContext.Tenants.Remove(tenant);
            }

            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TenantExists(int id)
        {
            return _dbContext.Tenants.Any(e => e.TenantId == id);
        }

        public async Task<IActionResult> ApartmentDetails()
        {
            int.TryParse(User.Claims.FirstOrDefault(c => c.Type == "t_apartment_id")?.Value, out int tenantApartmentId);

            // Fetch apartment data from the database
            var apartment = await _dbContext.Apartments
                .Include(a => a.Building)
                .FirstOrDefaultAsync(a => a.ApartmentId == tenantApartmentId);

            if (apartment == null)
            {
                ViewBag.Message = "No apartment found for the given tenant.";
                return View(); 
            }

            return View(apartment);
        }
    }
}
