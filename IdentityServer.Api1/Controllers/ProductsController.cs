using IdentityServer.Api1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Api1.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        [Authorize(Policy = "ReadProduct")]
        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = new List<Product>()
            {
                new Product(){ Id = 1, Name = "Kalem", Price = 100, Stock = 10},
                new Product(){ Id = 2, Name = "Defter", Price = 101, Stock = 11},
                new Product(){ Id = 3, Name = "Silgi", Price = 102, Stock = 12},
                new Product(){ Id = 4, Name = "Boya", Price = 103, Stock = 13},
                new Product(){ Id = 5, Name = "Kitap", Price = 104, Stock = 14},
            };

            return Ok(products);
        }

        [Authorize(Policy = "UpdateOrCreate")]
        [HttpPut]
        public IActionResult UpdateProduct(int id)
        {
            return Ok($"id si {id} olan kayıt güncellenmiştir.");
        }

        [Authorize(Policy = "Create")]
        [HttpPost]
        public IActionResult CreateProduct(Product product)
        {
            return Ok(product);
        }
    }
}
