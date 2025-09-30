using GamingEcommerce.BLL.ViewModels.WebsiteViewModels;
using GamingEcommerce.DAL.DataContext.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;
using System.Web.Mvc;
using Controller = Microsoft.AspNetCore.Mvc.Controller;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;

namespace GamingEcommerce.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginViewModel model)
        {
            if(!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user == null)
            {
                ModelState.AddModelError("", "Username or password is incorrect!");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, true);

            if(!result.Succeeded)
            {
                ModelState.AddModelError("", "Username or password is incorrect!");
                return View(model);
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterClientViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            var user = new AppUser
            {
                UserName = model.UserName,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if(!result.Succeeded)
            {
                foreach(var item in result.Errors)
                {
                    ModelState.AddModelError(item.Code, item.Description);
                }

                return View(model);
            }


            return RedirectToAction("Login","Account");
        }
    }
}
