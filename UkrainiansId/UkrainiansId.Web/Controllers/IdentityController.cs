using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.MicrosoftAccount;
using Microsoft.AspNetCore.Authentication.Twitter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
namespace UkrainiansId.Web.Controllers
{
    [AllowAnonymous, Route("identity")]
    public class IdentityController : Controller
    {
        private readonly IAuthenticationSchemeProvider _schemeProvider;
        public IdentityController(IAuthenticationSchemeProvider schemeProvider)
        {
            _schemeProvider = schemeProvider;
        }

        [HttpGet("login")]
        public async Task<IActionResult> Login()
        {
            if (User.Identity.IsAuthenticated)
                return LocalRedirect("~/");
            ViewBag.Providers = (await _schemeProvider.GetAllSchemesAsync()).Where(x=> !string.IsNullOrEmpty(x.DisplayName)).OrderBy(x=>x.DisplayName).ToList();
            return View();
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return LocalRedirect("/");
        }

        public IActionResult SignIn(string provider)
        {
            if (User.Identity.IsAuthenticated)
                return LocalRedirect("~/");
            return Challenge(new AuthenticationProperties { RedirectUri = Url.Action("SignInResponse") }, provider);
        }

        [HttpGet("SignInResponse")]
        public async Task<IActionResult> SignInResponse()
        {
            await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return LocalRedirect("/");
        }
    }
}