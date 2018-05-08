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
using recipeservice.Model;
using recipeservice.Services.Interfaces;
using securityfilter;

namespace recipeservice.Controllers {
    [Route ("api/[controller]")]
    public class RecipeTypesController : Controller {
        private readonly IRecipeTypeService _recipeTypeService;
        public RecipeTypesController (IRecipeTypeService recipeTypeService) {
            _recipeTypeService = recipeTypeService;
        }

        [HttpGet]
        [SecurityFilter ("recipes__allow_read")]
        [ResponseCache (CacheProfileName = "recipecache")]
        public async Task<IActionResult> Get ([FromQuery] int startat, [FromQuery] int quantity) {
            if (quantity == 0)
                quantity = 50;
            var recipeTypes = await _recipeTypeService.getRecipeTypes (startat, quantity);
            return Ok (recipeTypes);
        }

        [HttpGet ("{id}")]
        [SecurityFilter ("recipes__allow_read")]
        [ResponseCache (CacheProfileName = "recipecache")]
        public async Task<IActionResult> Get (int id) {
            var recipeType = await _recipeTypeService.getRecipeType (id);
            if (recipeType == null)
                return NotFound ();
            return Ok (recipeType);
        }

        [HttpPost]
        [SecurityFilter ("recipes__allow_update")]
        public async Task<IActionResult> Post ([FromBody] RecipeType recipeType) {
            recipeType.recipeTypeId = 0;
            if (ModelState.IsValid) {
                recipeType = await _recipeTypeService.addRecipeType (recipeType);
                return Created ($"api/phases/{ recipeType.recipeTypeId}", recipeType);
            }
            return BadRequest (ModelState);
        }

        [HttpPut ("{id}")]
        [SecurityFilter ("recipes__allow_update")]
        public async Task<IActionResult> Put (int id, [FromBody] RecipeType recipeType) {
            if (ModelState.IsValid) {
                recipeType = await _recipeTypeService.updateRecipeType (id, recipeType);
                if (recipeType != null) {
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
                var productionOrderType = await _recipeTypeService.deleteRecipeType (id);
                if (productionOrderType != null) {
                    return NoContent ();
                }
                return NotFound ();
            }
            return BadRequest (ModelState);
        }
    }
}