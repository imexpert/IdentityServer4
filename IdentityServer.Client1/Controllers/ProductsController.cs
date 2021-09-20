using IdentityModel.Client;
using IdentityServer.Client1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace IdentityServer.Client1.Controllers
{
    public class ProductsController : Controller
    {
        private IConfiguration _configuration;

        public ProductsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            List<Product> product = new List<Product>();

            TokenResponse tokenResponse = null;

            HttpClient httpClient = new HttpClient();
            var discoveryEndpoint = await httpClient.GetDiscoveryDocumentAsync("http://localhost:5000");

            if (!discoveryEndpoint.IsError)
            {
                ClientCredentialsTokenRequest request = new ClientCredentialsTokenRequest();
                request.ClientId = _configuration["Client:ClientId"];
                request.ClientSecret = _configuration["Client:ClientSecret"];
                request.Address = discoveryEndpoint.TokenEndpoint;

                tokenResponse = await httpClient.RequestClientCredentialsTokenAsync(request);

                if (!tokenResponse.IsError)
                {
                    httpClient.SetBearerToken(tokenResponse.AccessToken);
                }
            }

            var response = await httpClient.GetAsync("http://localhost:5004/api/products/getproducts");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                product = JsonConvert.DeserializeObject<List<Product>>(content);
            }
            return View(product);
        }
    }
}
