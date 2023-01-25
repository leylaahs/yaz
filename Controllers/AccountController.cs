using eBusiness.Models;
using eBusiness.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace eBusiness.Controllers
{
    public class AccountController : Controller
    {
        UserManager<AppUser> _userManager { get; }
        SignInManager<AppUser> _signInManager { get; }

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
        public async Task<IActionResult> Login(LoginUserVM userVM)
        {
            if(!ModelState.IsValid) return View();
            var user = await _userManager.FindByNameAsync(userVM.UsernameorEmail);
            if(user == null)
            {
                user = await _userManager.FindByEmailAsync(userVM.UsernameorEmail);
                if (user == null)
                {
                    ModelState.AddModelError("", "ur username or password invalid");
                    return View();
                }
            }
            var result = await _signInManager.PasswordSignInAsync(user, userVM.Password, userVM.IsPersistance, true);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "ur username or password invalid");
                return View();
            }
            return RedirectToAction("Index","Home");
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserVM userVM)
        {
            if(!ModelState.IsValid) return View();
            AppUser user = new AppUser
            {
                Name = userVM.Name,
                Email = userVM.Email,
                Surname = userVM.Surname,
                UserName = userVM.Username
            };
            var result = await _userManager.CreateAsync(user,userVM.Password);
            if (!result.Succeeded) 
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", "ur username or password invalid");
                    return View();
                }
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
