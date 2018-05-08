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
using securityfilter;

namespace recipeservice.Controllers {
    [Route ("api/[controller]")]
    public class RecipesController : Controller {
        private readonly IRecipeService _recipeService;
        private readonly IRecipeAutomaticService _recipeAutomaticService;
        public RecipesController (IRecipeService recipeService, IRecipeAutomaticService recipeAutomaticService) {
            _recipeService = recipeService;
            _recipeAutomaticService = recipeAutomaticService;
        }

        [HttpGet]
        [SecurityFilter ("recipes__allow_read")]
        [ResponseCache (CacheProfileName = "recipecache")]
        public async Task<IActionResult> Get ([FromQuery] int startat, [FromQuery] int quantity, [FromQuery] string fieldFilter, [FromQuery] string fieldValue, [FromQuery] string orderField, [FromQuery] string order) {
            List<string> fields = new List<string> ();
            fields.Add (fieldFilter + "," + fieldValue);

            var orderFieldEnum = RecipeFields.Default;
            Enum.TryParse (orderField, true, out orderFieldEnum);
            var orderEnumValue = OrderEnum.Ascending;
            Enum.TryParse (order, true, out orderEnumValue);
            if (quantity == 0)
                quantity = 50;
            var (recipes, total) = await _recipeService.getRecipes (startat, quantity, fields, orderFieldEnum, orderEnumValue);

            return Ok (new { values = recipes, total = total });
        }

        [HttpGet ("v2")]
        [SecurityFilter ("recipes__allow_read")]
        [ResponseCache (CacheProfileName = "recipecache")]
        public async Task<IActionResult> Get ([FromQuery] int startat, [FromQuery] int quantity, [FromQuery] List<string> filters, [FromQuery] string orderField, [FromQuery] string order) {
            var orderFieldEnum = RecipeFields.Default;
            Enum.TryParse (orderField, true, out orderFieldEnum);
            var orderEnumValue = OrderEnum.Ascending;
            Enum.TryParse (order, true, out orderEnumValue);
            if (quantity == 0)
                quantity = 50;
            var (recipes, total) = await _recipeService.getRecipes (startat, quantity, filters, orderFieldEnum, orderEnumValue);

            return Ok (new { values = recipes, total = total });
        }

        [HttpGet ("{id}")]
        [SecurityFilter ("recipes__allow_read")]
        [ResponseCache (CacheProfileName = "recipecache")]
        public async Task<IActionResult> Get (int id) {
            var phase = await _recipeService.getRecipe (id);
            if (phase == null)
                return NotFound ();
            return Ok (phase);
        }

        [HttpGet ("recipeCode/{recipeCode}")]
        [SecurityFilter ("recipes__allow_read")]
        [ResponseCache (CacheProfileName = "recipecache")]
        public async Task<IActionResult> Get (string recipeCode) {
            var phase = await _recipeService.getRecipeCode (recipeCode);
            if (phase == null)
                return NotFound ();
            return Ok (phase);
        }

        [HttpPost]
        [SecurityFilter ("recipes__allow_update")]
        public async Task<IActionResult> Post ([FromBody] Recipe recipe) {
            recipe.recipeId = 0;
            if (ModelState.IsValid) {
                recipe = await _recipeService.addRecipe (recipe);

                if (recipe != null) {
                    var (returnAutomatic, stringErro) = await _recipeAutomaticService.CreateAutomaticRecipe (recipe);

                    if (!returnAutomatic) {
                        await _recipeService.deleteRecipe (recipe.recipeId);
                        Console.WriteLine ("Erro in automatic create recipe - " + stringErro);
                        return StatusCode (500, stringErro);
                    }

                    return Created ($"api/phases/{recipe.recipeId}", recipe);
                }

                Console.WriteLine ("Erro create recipe ");
                return StatusCode (500, "Erro create recipe ");

            }
            return BadRequest (ModelState);
        }

        [HttpPut ("{id}")]
        [SecurityFilter ("recipes__allow_update")]
        public async Task<IActionResult> Put (int id, [FromBody] Recipe recipe) {
            if (ModelState.IsValid) {
                recipe = await _recipeService.updateRecipe (id, recipe);
                if (recipe != null) {
                    return NoContent ();
                }
                return NotFound ();
            }
            return BadRequest (ModelState);
        }

        [HttpDelete ("{id}")]
        [SecurityFilter ("recipes__allow_update")]
        public async Task<IActionResult> Delete (int id) {
            if (ModelState.IsValid) {
                var recipe = await _recipeService.deleteRecipe (id);
                if (recipe != null) {
                    return NoContent ();
                }
                return NotFound ();
            }
            return BadRequest (ModelState);
        }
    }
}