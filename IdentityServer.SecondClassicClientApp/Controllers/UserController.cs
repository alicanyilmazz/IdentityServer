﻿using IdentityModel.Client;
using IdentityServer.Client2.Models;
using IdentityServer.SharedLibrary.Extensions.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Globalization;
using System.Security.Claims;

namespace IdentityServer.Client2.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IApiResourcesHttpClient _apiResourcesHttpClient;

        public UserController(IConfiguration configuration, IApiResourcesHttpClient apiResourcesHttpClient)
        {
            _configuration = configuration;
            _apiResourcesHttpClient = apiResourcesHttpClient;
        }

        public async Task<IActionResult> Index()
        {
            UserData userData = new UserData() { IsCookieDataAvailable = false, IsImageDataAvailable = false };
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
                HttpClient httpClient = await _apiResourcesHttpClient.GetHttpClientAsync();
                var response = await httpClient.GetAsync("https://localhost:7044/api/image/getall");
                ImageDto movies = null;
                if (response != null && response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    movies = JsonConvert.DeserializeObject<ImageDto>(content);
                    if (movies is not null)
                    {
                        userData.ImageData = movies;
                        userData.IsImageDataAvailable = true;
                    }
                }
                else
                {
                    userData.IsImageDataAvailable = false;
                    //Logging
                }
            }
            catch (Exception)
            {
                userData.IsImageDataAvailable = false;
                //Logging
            }
            #endregion

            return View(userData);
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
