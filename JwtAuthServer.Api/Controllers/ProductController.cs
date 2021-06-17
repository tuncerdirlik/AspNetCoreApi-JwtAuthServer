using JwtAuthServer.Core.Dtos;
using JwtAuthServer.Core.Model;
using JwtAuthServer.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtAuthServer.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : CustomBaseController
    {
        private readonly IServiceGeneric<Product, ProductDto> productService;

        public ProductController(IServiceGeneric<Product, ProductDto> productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = await productService.GetAllAsync();
            return ActionResultInstance(products);
        }

        [HttpPost]
        public async Task<IActionResult> SaveProduct(ProductDto productDto)
        {
            var product = await productService.AddAsync(productDto);
            return ActionResultInstance(product);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(ProductDto productDto)
        {
            var result = await productService.Update(productDto.Id, productDto);
            return ActionResultInstance(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProdut(int id)
        {
            var result = await productService.Remove(id);
            return ActionResultInstance(result);
        }
    }
}
