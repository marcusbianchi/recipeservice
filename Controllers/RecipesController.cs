using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using recipeservice.Model;
using recipeservice.Services.Interfaces;

namespace recipeservice.Controllers
{
    [Route("api/[controller]")]
    public class RecipesController : Controller
    {
        private readonly IRecipeService _recipeService;
        public RecipesController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }


        [HttpGet]
        [ResponseCache(CacheProfileName = "recipecache")]
        public async Task<IActionResult> Get([FromQuery]int startat, [FromQuery]int quantity,
            [FromQuery]string fieldFilter, [FromQuery]string fieldValue,
            [FromQuery]string orderField, [FromQuery]string order)
        {
            var fieldFilterEnum = RecipeFields.Default;
            Enum.TryParse(fieldFilter, true, out fieldFilterEnum);
            var orderFieldEnum = RecipeFields.Default;
            Enum.TryParse(orderField, true, out orderFieldEnum);
            var orderEnumValue = OrderEnum.Ascending;
            Enum.TryParse(order, true, out orderEnumValue);
            if (quantity == 0)
                quantity = 50;
            var (recipes, total) = await _recipeService.getRecipes(startat, quantity
                    , fieldFilterEnum, fieldValue, orderFieldEnum, orderEnumValue);

            return Ok(new { values = recipes, total = total });
        }

        [HttpGet("{id}")]
        [ResponseCache(CacheProfileName = "recipecache")]
        public async Task<IActionResult> Get(int id)
        {
            var phase = await _recipeService.getRecipe(id);
            if (phase == null)
                return NotFound();
            return Ok(phase);
        }

        [HttpGet("recipeCode/{recipeCode}")]
        [ResponseCache(CacheProfileName = "recipecache")]
        public async Task<IActionResult> Get(string recipeCode)
        {
            var phase = await _recipeService.getRecipeCode(recipeCode);
            if (phase == null)
                return NotFound();
            return Ok(phase);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Recipe recipe)
        {
            recipe.recipeId = 0;
            if (ModelState.IsValid)
            {
                recipe = await _recipeService.addRecipe(recipe);
                return Created($"api/phases/{recipe.recipeId}", recipe);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Recipe recipe)
        {
            if (ModelState.IsValid)
            {
                recipe = await _recipeService.updateRecipe(id, recipe);
                if (recipe != null)
                {
                    return NoContent();
                }
                return NotFound();
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (ModelState.IsValid)
            {
                var recipe = await _recipeService.deleteRecipe(id);
                if (recipe != null)
                {
                    return NoContent();
                }
                return NotFound();
            }
            return BadRequest(ModelState);
        }
    }
}