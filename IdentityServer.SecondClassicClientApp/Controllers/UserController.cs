using IdentityModel.Client;
using IdentityServer.Client2.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Diagnostics;
using System.Globalization;
using System.Security.Claims;

namespace IdentityServer.Client2.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IConfiguration _configuration;

        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            CookieInformation cookieInformation = new CookieInformation();
            var authenticationValues = await HttpContext.AuthenticateAsync();
            if (authenticationValues != null)
            {
                var properties = authenticationValues.Properties.Items;
                foreach (var property in properties)
                {
                    Debug.WriteLine($"{property.Key} - {property.Value}");
                    cookieInformation.CookieAuthenticationProperties.Add(new CookieAuthenticationProperties { Key = property.Key, Value = property.Value });
                }
            }
            foreach (var claim in User.Claims)
            {
                Debug.WriteLine($"{claim.Type} - {claim.Value}");
                cookieInformation.CookieClaims.Add(new CookieClaims { Key = claim.Type, Value = claim.Value });
            }

            #region Sample
            var authenticatedUserClaim = User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault();
            var authenticatedUserCountry = User.Claims.Where(x => x.Type == ClaimTypes.Country).FirstOrDefault();
            var authenticatedUserId = authenticatedUserClaim.Value;
            #endregion

            return View(cookieInformation);
        }

        public async Task LogOut()
        {
            await HttpContext.SignOutAsync("Cookies");
            await HttpContext.SignOutAsync("oidc");
        }

        public async Task<IActionResult> GetRefreshToken()
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                var discoveryDocument = await httpClient.GetDiscoveryDocumentAsync("https://localhost:7025");
                if (discoveryDocument.IsError)
                {
                    throw new Exception("Discovery Endpoint not found!");
                }
                var refreshToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.RefreshToken);

                RefreshTokenRequest refreshTokenRequest = new RefreshTokenRequest();
                refreshTokenRequest.ClientId = _configuration["CookieConfigurations:ClientId"];
                refreshTokenRequest.ClientSecret = _configuration["CookieConfigurations:ClientSecret"];
                refreshTokenRequest.RefreshToken = refreshToken;
                refreshTokenRequest.Address = discoveryDocument.TokenEndpoint;

                var token = await httpClient.RequestRefreshTokenAsync(refreshTokenRequest);
                if (token.IsError)
                {
                    throw new Exception("Token not found!");
                }
                var tokens = new List<AuthenticationToken>
                {
                    new AuthenticationToken{Name = OpenIdConnectParameterNames.IdToken,Value = token.IdentityToken},
                    new AuthenticationToken{Name = OpenIdConnectParameterNames.AccessToken,Value = token.AccessToken},
                    new AuthenticationToken{Name = OpenIdConnectParameterNames.RefreshToken,Value = token.RefreshToken},
                    new AuthenticationToken{Name = OpenIdConnectParameterNames.ExpiresIn,Value = DateTime.UtcNow.AddSeconds(token.ExpiresIn).ToString("o",CultureInfo.InvariantCulture)},
                };

                var authenticationResult = await HttpContext.AuthenticateAsync();
                var authenticationProperties = authenticationResult.Properties;

                authenticationProperties.StoreTokens(tokens);

                await HttpContext.SignInAsync(_configuration["CookieConfigurations:DefaultScheme"], authenticationResult.Principal, authenticationProperties);

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }

        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Admin()
        {
            return View();
        }

        [Authorize(Roles = "customer")]
        public async Task<IActionResult> Customer()
        {
            return View();
        }

        [Authorize(Roles = "customer,admin")]
        public async Task<IActionResult> Common()
        {
            return View();
        }
    }
}
