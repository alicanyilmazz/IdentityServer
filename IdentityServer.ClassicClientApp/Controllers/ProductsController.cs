using IdentityModel.Client;
using IdentityServer.Client1.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace IdentityServer.Client1.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IConfiguration _configuration;

        public ProductsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            HttpClient client = new HttpClient();
            var discoveryDocument = await client.GetDiscoveryDocumentAsync("https://localhost:7025");
            if (discoveryDocument.IsError)
            {
                //Logging
            }

            ClientCredentialsTokenRequest clientCredentialsTokenRequest = new ClientCredentialsTokenRequest();
            clientCredentialsTokenRequest.ClientId = _configuration["Client:ClientId"];
            clientCredentialsTokenRequest.ClientSecret = _configuration["Client:ClientSecret"];
            clientCredentialsTokenRequest.Address = discoveryDocument.TokenEndpoint;
            var token = await client.RequestClientCredentialsTokenAsync(clientCredentialsTokenRequest);

            if (token.IsError)
            {
                //Logging
            }

            client.SetBearerToken(token.AccessToken);

            var response = await client.GetAsync("https://localhost:7208/api/product/getproducts");
            ProductDto products = null;
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                products = JsonConvert.DeserializeObject<ProductDto>(content);
            }
            else
            {
                //Logging
            }

            return View(products);
        }
    }
}
