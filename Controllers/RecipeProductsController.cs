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
    [Route ("api/recipes/product")]
    public class RecipeProductsController : Controller {
        private readonly IRecipeService _recipeService;
        public RecipeProductsController (IRecipeService recipeService) {
            _recipeService = recipeService;
        }

        [HttpPost ("{recpieId}")]
        [SecurityFilter ("recipes__allow_update")]
        public async Task<IActionResult> Post (int recpieId, [FromBody] PhaseProduct phaseProduct) {
            phaseProduct.phaseProductId = 0;
            if (ModelState.IsValid) {
                var product = await _recipeService.addProductToRecipe (recpieId, phaseProduct);
                if (product != null)
                    return Ok (product);
                else
                    return NotFound ();
            }
            return BadRequest (ModelState);
        }

        [HttpDelete ("{recipeId}")]
        [SecurityFilter ("recipes__allow_update")]
        public async Task<IActionResult> Delete (int recipeId, [FromBody] PhaseProduct phaseProduct) {
            if (ModelState.IsValid) {
                var product = await _recipeService.removeProductToRecipe (recipeId, phaseProduct);
                if (product != null)
                    return Ok (product);
                else
                    return NotFound ();
            }
            return BadRequest (ModelState);
        }
    }
}