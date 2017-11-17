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
    [Route("api/recipes/phases/")]
    public class RecipePhaseController : Controller
    {
        private readonly IRecipePhaseService _recipePhaseService;
        public RecipePhaseController(IRecipePhaseService recipePhaseService)
        {
            _recipePhaseService = recipePhaseService;
        }

        [HttpGet("{recipeId}")]
        public async Task<IActionResult> Get(int recipeId)
        {
            return Ok(await _recipePhaseService.getPhasesFromRecipe(recipeId));
        }

        [HttpPost("{recipeId}")]
        public async Task<IActionResult> Post(int recipeId, [FromBody]Phase phase)
        {
            if (ModelState.IsValid)
            {
                var recipe = await _recipePhaseService.addPhaseToRecipe(phase, recipeId);
                if (recipe != null)
                    return Ok(recipe);
                return NotFound();
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{recipeId}")]
        public async Task<IActionResult> Delete(int recipeId, [FromBody]Phase phase)
        {
            if (ModelState.IsValid)
            {
                var recipe = await _recipePhaseService.removePhaseFromRecipe(phase.phaseId, recipeId);
                if (recipe != null)
                    return Ok(recipe);
                return NotFound();
            }
            return BadRequest(ModelState);
        }
    }
}