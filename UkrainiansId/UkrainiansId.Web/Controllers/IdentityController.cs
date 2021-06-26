using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.MicrosoftAccount;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
namespace UkrainiansId.Web.Controllers
{
    [AllowAnonymous, Route("identity")]
    public class IdentityController : Controller
    {
        [HttpGet("login")]
        public IActionResult Login() => View();

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return LocalRedirect("/");
        }




        [HttpGet("google-login")]
        public IActionResult GoogleLogin()
        {
            var props = new AuthenticationProperties { RedirectUri = Url.Action("GoogleResponse") };
            return Challenge(props, GoogleDefaults.AuthenticationScheme);
        }

        [Route("google-response")]
        public async Task<IActionResult> GoogleResponse()
        {
            await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return LocalRedirect("/");
        }



        [HttpGet("facebook-login")]
        public IActionResult FacebookLogin()
        {
            var props = new AuthenticationProperties { RedirectUri = Url.Action("FacebookResponse") };
            return Challenge(props, FacebookDefaults.AuthenticationScheme);
        }

        [HttpGet("facebook-response")]
        public async Task<IActionResult> FacebookResponse()
        {
            await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return LocalRedirect("/");
        }



        [HttpGet("microsoft-login")]
        public IActionResult MicrosoftLogin()
        {
            var props = new AuthenticationProperties { RedirectUri = Url.Action("MicrosoftResponse") };
            return Challenge(props, MicrosoftAccountDefaults.AuthenticationScheme);
        }

        [HttpGet("microsoft-response")]
        public async Task<IActionResult> MicrosoftResponse()
        {
            await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return LocalRedirect("/");
        }
    }
}