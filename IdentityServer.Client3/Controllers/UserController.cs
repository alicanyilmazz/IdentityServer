using IdentityModel.Client;
using IdentityServer.Client3.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Security.Claims;
using System.Text.Json.Serialization;

namespace IdentityServer.Client3.Controllers
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
            UserData userData = new UserData() { IsCookieDataAvailable = false, IsMovieDataAvailable = false };
            #region Cookie Processing
            try
            {
                CookieInformationDto cookieInformation = new CookieInformationDto();
                var authenticationValues = await HttpContext.AuthenticateAsync();
                if (authenticationValues != null)
                {
                    var properties = authenticationValues.Properties.Items;
                    foreach (var property in properties)
                    {
                        cookieInformation.CookieAuthenticationProperties.Add(new CookieAuthenticationProperties { Key = property.Key, Value = property.Value });
                    }
                }
                foreach (var claim in User.Claims)
                {
                    cookieInformation.CookieClaims.Add(new CookieClaims { Key = claim.Type, Value = claim.Value });
                }
                userData.CookieInformation = cookieInformation;
                userData.IsCookieDataAvailable = true;
            }
            catch (Exception)
            {
                //Log
            }
            #endregion

            #region Sample
            //var authenticatedUserClaim = User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault();
            //var authenticatedUserCountry = User.Claims.Where(x => x.Type == ClaimTypes.Country).FirstOrDefault();
            //var authenticatedUserId = authenticatedUserClaim.Value;
            #endregion

            #region API Data Fetch
            try
            {
                HttpClient httpClient = new HttpClient();
                var accessToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);
                httpClient.SetBearerToken(accessToken);
                var response = await httpClient.GetAsync("https://localhost:7044/api/movie/getmovies");
                MovieDto movies = null;
                if (response != null && response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    movies = JsonConvert.DeserializeObject<MovieDto>(content);
                    if (movies is not null)
                    {
                        userData.MovieData = movies;
                        userData.IsMovieDataAvailable = true;
                    }
                }
                else
                {
                    userData.IsMovieDataAvailable = false;
                    //Logging
                }
            }
            catch (Exception)
            {
                userData.IsMovieDataAvailable = false;
                //Logging
            }
            #endregion

            return View(userData);
        }

        [HttpPost]
        public async Task<IActionResult> Create(MovieViewModel movie, IFormFile file)
        {
            if (ModelState.IsValid && file != null)
            {
                using (var client = new HttpClient())
                {
                    var form = new MultipartFormDataContent
                    {
                        { new StringContent(movie.Name), "Name" },
                        { new StringContent(movie.ReleaseDate.ToString()), "ReleaseDate" }
                    };
                    using (var memoryStream = new MemoryStream())
                    {
                        await file.CopyToAsync(memoryStream);
                        form.Add(new ByteArrayContent(memoryStream.ToArray()), "file", file.FileName);
                    }
                    HttpClient httpClient = new HttpClient();
                    var accessToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);
                    httpClient.SetBearerToken(accessToken);
                    var response = await httpClient.PostAsync("https://localhost:7044/api/movie/getmovies", form);
                    if (response != null && response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var res = JsonConvert.DeserializeObject<NoDataContent<object>>(content);
                    }
                }
            }
            return View();
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
