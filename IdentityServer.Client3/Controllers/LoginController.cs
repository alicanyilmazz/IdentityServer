using IdentityModel.Client;
using IdentityServer.Client3.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Globalization;
using System.Security.Claims;

namespace IdentityServer.Client3.Controllers
{
    public class LoginController : Controller
    {
        private readonly IConfiguration _configuration;

        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel loginViewModel)
        {
            var client = new HttpClient();
            var baseAuthenticationServerUrl = _configuration["AuthenticationServer:BaseUrl"];
            var discoveryDocument = await client.GetDiscoveryDocumentAsync(baseAuthenticationServerUrl);

            if (discoveryDocument.IsError)
            {
                //Error Catch And Log
            }
            var passwordTokenRequest = new PasswordTokenRequest();
            passwordTokenRequest.Address = discoveryDocument.TokenEndpoint;
            passwordTokenRequest.UserName = loginViewModel.Email;
            passwordTokenRequest.Password = loginViewModel.Password;
            passwordTokenRequest.ClientId = _configuration["CookieConfigurations:ClientId"];
            passwordTokenRequest.ClientSecret = _configuration["CookieConfigurations:ClientSecret"];
            passwordTokenRequest.GrantType = "password";
            var token = await client.RequestPasswordTokenAsync(passwordTokenRequest);
            if (token is null || (token is not null && token.IsError))
            {
                ModelState.AddModelError("", "Email or Password is wrong!");
                return View();
                //Error Catch And Log
            }
            var userInfoRequest = new UserInfoRequest();
            userInfoRequest.Token = token.AccessToken;
            userInfoRequest.Address = discoveryDocument.UserInfoEndpoint;
            var userInfo = await client.GetUserInfoAsync(userInfoRequest);
            if (userInfo is null || (userInfo is not null && userInfo.IsError))
            {
                //Error Catch And Log
            }
            var scheme = _configuration["CookieConfigurations:DefaultScheme"];
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(userInfo.Claims, scheme,"name","role");
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            var authenticationProperties = new AuthenticationProperties();
            authenticationProperties.StoreTokens(new List<AuthenticationToken>
            {
                new AuthenticationToken{Name = OpenIdConnectParameterNames.IdToken,Value = token.IdentityToken},
                new AuthenticationToken{Name = OpenIdConnectParameterNames.AccessToken,Value = token.AccessToken},
                new AuthenticationToken{Name = OpenIdConnectParameterNames.RefreshToken,Value = token.RefreshToken},
                new AuthenticationToken{Name = OpenIdConnectParameterNames.ExpiresIn,Value = DateTime.UtcNow.AddSeconds(token.ExpiresIn).ToString("o",CultureInfo.InvariantCulture)},
            });

            await HttpContext.SignInAsync(scheme, claimsPrincipal,authenticationProperties);
            return RedirectToAction("Index","User");
        }
    }
}
