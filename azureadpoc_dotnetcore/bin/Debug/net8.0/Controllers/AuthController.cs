using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using azureadpoc_dotnetcore.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Identity.Web.UI.Areas.MicrosoftIdentity.Controllers;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Options;

namespace azureadpoc_dotnetcore.Controllers
{
    public class AuthController : Controller
    {
        private readonly IOptions<OpenIdConnectOptions> _openIdConnectOptions;
        private readonly ILogger<AuthController> _logger;
        public AuthController(ILogger<AuthController> logger, IOptions<OpenIdConnectOptions> openIdConnectOptions)
        {
            _logger = logger;
            _openIdConnectOptions = openIdConnectOptions;
        }

        [HttpGet]
        [Route("signout")]
        public async Task<IActionResult> SignOut()
        {
            // Clear the session
            //HttpContext.Session.Clear();

            // Sign out of the authentication scheme
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignOutAsync("OpenIdConnect");
            _logger.LogInformation("Signout was called");
            var callbackUrl = Url.Action("Index", "Home", values: null, protocol: Request.Scheme);
            //var signOutUrl = $"https://tonykachappillyb2c.b2clogin.com/tonykachappillyb2c.onmicrosoft.com/B2C_1A_signup_signin/oauth2/v2.0/logout?post_logout_redirect_uri={callbackUrl}";
            //var signOutUrl = $"{_openIdConnectOptions.Value.Authority}/oauth2/v2.0/logout?post_logout_redirect_uri={callbackUrl}";
            return Redirect(callbackUrl);
        }
    }
}