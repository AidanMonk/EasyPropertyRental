using EasyPropertyRental.Models.ViewModels;
using EasyPropertyRental.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EasyPropertyRental.Controllers
{
    public class PropertyManagerAccountController : Controller
    {
        private readonly PropertyRentalDbContext _dbContext;

        public PropertyManagerAccountController(PropertyRentalDbContext dbContext)
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
                var user = _dbContext.PropertyManagers.Where(x => x.Email == model.Email && x.Password == model.Password).FirstOrDefault();
                if (user != null)
                {
                    //success, create cookie
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.Name, user.FirstName),
                        new Claim(ClaimTypes.Role, "PropertyManager"),
                        new Claim("pm_id", user.PmId.ToString())
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

        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Registration(RegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                PropertyManager manager = new PropertyManager();
                manager.FirstName = model.FirstName;
                manager.LastName = model.LastName;
                manager.Email = model.Email;
                manager.Password = model.Password;

                try
                {
                    _dbContext.Add(manager);
                    _dbContext.SaveChanges();

                    ModelState.Clear();
                    ViewBag.Message = $"{manager.FirstName} {manager.LastName} registered successfully";

                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Please enter a unique email or password");
                    return View(model);
                }
                return View();
            }

            return View(model);
        }

        [Authorize]
        public IActionResult Home()
        {
            ViewBag.Name = HttpContext.User.Identity.Name;
            return View();

        }

/*        public async Task<IActionResult> ManageBuildings()
        {

            int.TryParse(User.Claims.FirstOrDefault(c => c.Type == "pm_id")?.Value, out int userId);

            var buildings = await _dbContext.Buildings
                .Where(b => b.PmId != null && b.PmId == userId) // Filter for buildings where the PM matches the logged-in user
                .Select(b => new BuildingViewModel
                {
                    BuildingId = b.BuildingId,
                    Name = b.Name,
                    Address = b.Address,
                    PropertyManagerName = b.Pm != null ? $"{b.Pm.FirstName} {b.Pm.LastName}" : "N/A"
                })
                .ToListAsync();

            return View(buildings);
        }*/
    }
}
