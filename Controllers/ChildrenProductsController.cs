using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using recipeservice.Data;
using recipeservice.Model;
using recipeservice.Services.Interfaces;

namespace recipeservice.Controllers
{
    [Route("api/products/childrenproducts/")]
    public class ChildrenProductsController : Controller
    {
        private readonly IProductService _productService;

        public ChildrenProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("{parentId}")]
        [ResponseCache(CacheProfileName = "recipecache")]
        public async Task<IActionResult> Get(int parentId)
        {
            var products = await _productService.GetChildrenProducts(parentId);
            if (products == null)
                return NotFound();
            return Ok(products);
        }

        [HttpPost("{parentId}")]
        public async Task<IActionResult> Post(int parentId, [FromBody]Product product)
        {
            if (ModelState.IsValid)
            {
                var outproduct = await _productService.addChildrenProducts(parentId, product);
                if (outproduct == null)
                    return NotFound();
                return Ok(outproduct);
            }
            return BadRequest(ModelState);

        }

        [HttpDelete("{parentId}")]
        public async Task<IActionResult> Delete(int parentId, [FromBody]Product product)
        {
            if (ModelState.IsValid)
            {
                var outproduct = await _productService.removeChildrenProducts(parentId, product);
                if (outproduct == null)
                    return NotFound();
                return Ok(outproduct);
            }
            return BadRequest(ModelState);

        }
    }
}