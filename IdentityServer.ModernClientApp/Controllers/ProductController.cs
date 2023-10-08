using IdentityServer.API1.Core.Dtos;
using IdentityServer.API1.Core.Entities;
using IdentityServer.API1.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.API1.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : CustomBaseController
    {
        private readonly IService<Product, ProductDto> _productService;

        public ProductController(IService<Product, ProductDto> productService)
        {
            _productService = productService;
        }

        [Authorize(Policy = "ReadProduct")]
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            return ActionResultInstance(await _productService.GetAllAsync());
        }

        [Authorize(Policy = "UpdateOrCreateProduct")]
        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductDto productDto)
        {
            return ActionResultInstance(await _productService.AddAsync(productDto));
        }
    }
}
