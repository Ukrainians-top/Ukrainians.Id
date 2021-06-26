using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using UkrainiansId.Web.Models;

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
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var claims = result.Principal.Claims;
            return LocalRedirect("/");
        }

        [NonAction]
        private TestUser GetUser(IEnumerable<Claim> claims)
        {
            return new TestUser
            {
                Id = claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value,
                Email = claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value,
                FullName = claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value,
                Firstname = claims.FirstOrDefault(x => x.Type == ClaimTypes.GivenName).Value,
                Lastname = claims.FirstOrDefault(x => x.Type == ClaimTypes.Surname).Value,
            };
        }
    }
}