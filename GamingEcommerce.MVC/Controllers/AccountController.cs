using AutoMapper;
using GamingEcommerce.BLL.Services.Contracts;
using GamingEcommerce.BLL.ViewModels.GeneralViewModels;
using GamingEcommerce.BLL.ViewModels.WebsiteViewModels;
using GamingEcommerce.DAL.DataContext.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.FlowAnalysis;
using System.Drawing.Printing;
using System.Threading.Tasks;
using Controller = Microsoft.AspNetCore.Mvc.Controller;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;

namespace GamingEcommerce.MVC.Controllers
{
    
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAddressService _addressService;
        private readonly IMapper _mapper;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager, IAddressService addressService, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _addressService = addressService;
            _mapper = mapper;
        }

        [Authorize(Roles = "Client")]
        public IActionResult Dashboard()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
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

            if (!string.IsNullOrEmpty(model.ReturnUrl))
            {
                return Redirect(model.ReturnUrl);
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

            var roleResult = await _roleManager.CreateAsync(new IdentityRole { Name = "Client" });

            
            await _userManager.AddToRoleAsync(user, "Client");
            
            return RedirectToAction("Login","Account");
        }

        public async Task<IActionResult> Address()
        {
            var defaultAddress = await _addressService.GetAsync(predicate: x => x.IsDefault && !x.IsDeleted);

            var list = await _addressService.GetAllAsync(predicate: x => !x.IsDeleted && !x.IsDefault);

            var model = new AddressPageViewModel
            {
                DefaultAddress = defaultAddress,
                OtherAddresses = list
            };

            return View(model);
        }

        public IActionResult AddAddress()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddAddress(CreateAddressViewModel address)
        {
            var addresses = await _addressService.GetAllAsync(predicate: x=> !x.IsDeleted);

            if (addresses.Count == 0) address.IsDefault = true;

            var user = await _userManager.GetUserAsync(User);

            if (user == null) return BadRequest();

            address.UserId = user.Id;

            await _addressService.AddAsync(address);

            return RedirectToAction("Address", "Account");
        }

        public async Task<IActionResult> UpdateAddress(int id)
        {
            var existAddress = await _addressService.GetByIdAsync(id);

            if (existAddress == null) return BadRequest();

            var updateModel = _mapper.Map<UpdateAddressViewModel>(existAddress);

            return View(updateModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateAddress(int id, UpdateAddressViewModel address)
        {
            var current = await _addressService.GetByIdAsync(id);
            if (current == null) return NotFound();

            
            address.Id = id;
            address.UserId = current.UserId;
            address.IsDefault = current.IsDefault;

            var ok = await _addressService.UpdateAsync(address);
            if (!ok)
            {
                ModelState.AddModelError("", "Update failed");
                return View(address);
            }

            return RedirectToAction("Address", "Account");
        }

        public async Task<IActionResult> DeleteAddress(int id)
        {
            var current = await _addressService.GetByIdAsync(id);
            if (current == null) return NotFound();

            current.IsDeleted = true;

            if (current.IsDefault)
            {
                var list = await _addressService.GetAllAsync(predicate: x=> !x.IsDeleted);
                var newCurrent = list.FirstOrDefault();

                if (newCurrent != null)
                {
                  newCurrent = list.Skip(1).Take(1).FirstOrDefault();
                }
                newCurrent!.IsDefault = true;

                await _addressService.UpdateAsync(_mapper.Map<UpdateAddressViewModel>(newCurrent));
            }

            var ok = await _addressService.UpdateAsync(_mapper.Map<UpdateAddressViewModel>(current));
            if (!ok)
            {
                ModelState.AddModelError("", "Update failed");
                return RedirectToAction("Address", "Account");
            }

            return RedirectToAction("Address", "Account");
        }

        public async Task<IActionResult> MakeDefault(int id)
        {
            var currentDefault = await _addressService.GetAsync(predicate: x=> x.IsDefault && !x.IsDeleted);

            currentDefault.IsDefault = false;

            await _addressService.UpdateAsync(_mapper.Map<UpdateAddressViewModel>(currentDefault));

            var newDefault = await _addressService.GetByIdAsync(id);

            newDefault.IsDefault=true;

            await _addressService.UpdateAsync(_mapper.Map<UpdateAddressViewModel>(newDefault));

            return RedirectToAction("Address", "Account");
        }
    }
}
