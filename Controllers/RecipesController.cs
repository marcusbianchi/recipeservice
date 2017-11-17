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
        public async Task<IActionResult> Get([FromQuery]int startat, [FromQuery]int quantity)
        {
            if (quantity == 0)
                quantity = 50;
            var recipes = await _recipeService.getRecipes(startat, quantity);
            return Ok(recipes);
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