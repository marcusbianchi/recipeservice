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
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [ResponseCache(CacheProfileName = "recipecache")]
        public async Task<IActionResult> Get([FromQuery]int startat, [FromQuery]int quantity,
            [FromQuery]string fieldFilter, [FromQuery]string fieldValue,
            [FromQuery]string orderField, [FromQuery]string order)
        {
            var fieldFilterEnum = ProductFields.Default;
            Enum.TryParse(fieldFilter, true, out fieldFilterEnum);
            var orderFieldEnum = ProductFields.Default;
            Enum.TryParse(orderField, true, out orderFieldEnum);
            var orderEnumValue = OrderEnum.Ascending;
            Enum.TryParse(order, true, out orderEnumValue);
            if (quantity == 0)
                quantity = 10;
            var (products, total) = await _productService.getProducts(startat, quantity
            , fieldFilterEnum, fieldValue, orderFieldEnum, orderEnumValue);

            return Ok(new { values = products, total = total });
        }

        [HttpGet("list/")]
        [ResponseCache(CacheProfileName = "recipecache")]
        public async Task<IActionResult> GetList([FromQuery]int[] productId)
        {
            var products = await _productService.getProductList(productId);
            return Ok(products);
        }

        [HttpGet("{id}")]
        [ResponseCache(CacheProfileName = "recipecache")]
        public async Task<IActionResult> Get(int id)
        {
            var product = await _productService.getProduct(id);
            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Product product)
        {
            if (ModelState.IsValid)
            {
                product.productId = 0;
                product.parentProductsIds = new int[0];
                product.childrenProductsIds = new int[0];

                product = await _productService.addProduct(product);
                return Created($"api/products/{product.productId}", product);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Product product)
        {
            if (ModelState.IsValid)
            {
                var curTProduct = await _productService.updateProduct(id, product);
                if (curTProduct == null)
                    return NotFound();
                return NoContent();

            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _productService.deleteProduct(id);

            if (result)
                return NoContent();
            return NotFound();
        }
    }
}
