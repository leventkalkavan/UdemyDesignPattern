using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApp.Observer.Models;
using WebApp.Observer.Observer;

namespace WebApp.Observer.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserObserverSubject _userObserverSubject;
        
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, UserObserverSubject userObserverSubject)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userObserverSubject = userObserverSubject;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email,string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null) return View();

            var signInResult = await _signInManager.PasswordSignInAsync(user, password, true, false);
            
            if(!signInResult.Succeeded)
            {
                return View();
            }

            return RedirectToAction(nameof(HomeController.Index), "Home");

        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        public IActionResult SignUp()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> SignUp(UserCreateViewModel userCreateViewModel)
        {
            var appUser = new AppUser()
            {
                UserName = userCreateViewModel.UserName,
                Email = userCreateViewModel.EMail
            };
            var identityRes = await _userManager.CreateAsync(appUser,userCreateViewModel.Password);
            if (identityRes.Succeeded)
            {
                _userObserverSubject.NotifyObservers(appUser);
                ViewBag.message = $"sayin {appUser.UserName}, uyelik kaydiniz olusturuldu";
            }
            else
            {
                ViewBag.message = identityRes.Errors.ToList().First().Description;
            }
            return View();
        }
    }
}