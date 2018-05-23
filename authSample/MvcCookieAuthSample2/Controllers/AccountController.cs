using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Exceptionless;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MvcCookieAuthSample.Data;
using MvcCookieAuthSample.ViewModels;

namespace MvcCookieAuthSample.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        private IActionResult RedirectToLocal(string retureUrl)
        {
            if (Url.IsLocalUrl(retureUrl))
            {
                return Redirect(retureUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
        [HttpGet]
        public IActionResult Login(string returnUrl=null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            return View();
        }
        [HttpPost]
        //[AllowAnonymous]
        public async Task<IActionResult> Login(RegisterViewModel register,string returnUrl=null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            var user = await _userManager.FindByEmailAsync(register.Email);
            if (user == null)
            {
                
            }
            await _signInManager.SignInAsync(user, new AuthenticationProperties() {IsPersistent = true});
            return RedirectToLocal(returnUrl);
        }
        [HttpPost]
        //[AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel register,string returnUrl=null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            var identityUser =new ApplicationUser()
            {
                Email = register.Email,
                NormalizedEmail = register.Email,
                UserName = register.Email,
            };

            ExceptionlessClient.Default.SubmitLog("test");

            var identityResult = await _userManager.CreateAsync(identityUser, register.Password);

            if (identityResult.Succeeded)
            {
                await _signInManager.SignInAsync(identityUser, new AuthenticationProperties() {IsPersistent = true});
                return RedirectToLocal(returnUrl);
            }

            return View();
        }
        [HttpGet]
        //[AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult MakeLogin()
        {
            var claims=new List<Claim>()
            {
                new Claim(ClaimTypes.Name,"我零0七"),
                new Claim(ClaimTypes.Role,"admin")
            };

            var claimIdentity=new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimIdentity));
            return Ok();
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}