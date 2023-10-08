using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;

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

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
            }
            else
            {
                //Logging
            }

            return View();
        }
    }
}
