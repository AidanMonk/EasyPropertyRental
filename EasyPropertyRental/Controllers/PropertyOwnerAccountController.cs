using EasyPropertyRental.Models;
using EasyPropertyRental.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace EasyPropertyRental.Controllers
{
    public class PropertyOwnerAccountController : Controller
    {
        private readonly PropertyRentalDbContext _dbContext;

        public PropertyOwnerAccountController(PropertyRentalDbContext dbContext)
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
                var user = _dbContext.PropertyOwners.Where(x => x.Email == model.Email && x.Password == model.Password).FirstOrDefault();
                if(user != null)
                {
                    //success, create cookie
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.Name, user.FirstName),
                        new Claim(ClaimTypes.Role, "PropertyOwner"),
                        new Claim("Id", user.PoId.ToString())
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
                PropertyOwner owner = new PropertyOwner();
                owner.FirstName = model.FirstName;
                owner.LastName = model.LastName;
                owner.Email = model.Email;
                owner.Password = model.Password;

                try
                {
                    _dbContext.Add(owner);
                    _dbContext.SaveChanges();

                    ModelState.Clear();
                    ViewBag.Message = $"{owner.FirstName} {owner.LastName} registered successfully";

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
    }

}
