using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Angular2App.ViewModels;
using Angular2App.Models;
using Angular2App.Services;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Authentication;

namespace Angular2App.Controllers
{   
    [RequireHttps]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger _logger;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,ILoggerFactory loggerFactory)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = loggerFactory.CreateLogger<AccountController>();
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Register([FromBody]RegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser { Email = model.Email, UserName = model.Email, Year=model.Year};
                // добавляем пользователя
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // установка куки
                    //await _signInManager.SignInAsync(user, false);
                    return Json("ok",new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return Json("global",new JsonSerializerSettings{ContractResolver = new CamelCasePropertyNamesContractResolver()});
        }

        /*
        [HttpGet("[action]")]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }
        
        [HttpPost("[action]")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromBody]LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = 
                    await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    // проверяем, принадлежит ли URL приложению
                    if (!String.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }
            return Json("globalLog",new JsonSerializerSettings{ContractResolver = new CamelCasePropertyNamesContractResolver()});
        }*/
        
        [HttpPost("[action]")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            // удаляем аутентификационные куки
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


        /*private List<Person> people = new List<Person>
        {
            new Person {Login="admin@gmail.com", Password="12345", Role = "admin" },
            new Person { Login="qwerty", Password="55555", Role = "user" }
        };
 */
        public IActionResult Login(string returnUrl = "/")
        {
            return new ChallengeResult("Auth0", new AuthenticationProperties() { RedirectUri = returnUrl });
        }

        [Authorize]
        public async Task Logout()
        {
            await HttpContext.Authentication.SignOutAsync("Auth0", new AuthenticationProperties
            {
                // Indicate here where Auth0 should redirect the user after a logout.
                // Note that the resulting absolute Uri must be whitelisted in the 
                // **Allowed Logout URLs** settings for the client.
                RedirectUri = Url.Action("Index", "Home")
            });
            await HttpContext.Authentication.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        [HttpPost("[action]")]
        public IActionResult getProfile()
        {
            _logger.LogInformation("KEK");
            return Json("profile",new JsonSerializerSettings{ContractResolver = new CamelCasePropertyNamesContractResolver()});
        }

        /// <summary>
        /// This is just a helper action to enable you to easily see all claims related to a user. It helps when debugging your
        /// application to see the in claims populated from the Auth0 ID Token
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet("[action]")]
        public IActionResult Claims()
        {
            return View();
        }

        [HttpGet("[action]")]
        public string Ping()
        {
            return "Pong";
        }


        [Authorize]
        [HttpGet("[action]")]
        public string PingSecured()
        {
            return "All good. You only get this message if you are authenticated.";
        }

        [Authorize(Roles = "admin")]
        [HttpGet("[action]")]
         public IActionResult PingAdmin()
        {
            _logger.LogInformation("LOL");
            return Json("LOL2");
        }
    }
}