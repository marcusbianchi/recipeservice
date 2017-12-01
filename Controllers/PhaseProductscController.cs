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
    [Route("api/phases/products/")]
    public class PhaseProductscController : Controller
    {
        private readonly IPhaseProductService _phaseProductService;
        public PhaseProductscController(IPhaseProductService phaseProductService)
        {
            _phaseProductService = phaseProductService;
        }

        [HttpGet("{phaseId}")]
        public async Task<IActionResult> Get(int phaseId)
        {
            return Ok(await _phaseProductService.getProductsFromPhase(phaseId));
        }


        [HttpPost("{phaseId}")]
        public async Task<IActionResult> Post(int phaseId, [FromBody]PhaseProduct PhaseProduct)
        {
            if (ModelState.IsValid)
            {
                PhaseProduct.phaseProductId = 0;
                var phase = await _phaseProductService.addProductToPhase(PhaseProduct, phaseId);
                if (phase != null)
                    return Ok(phase);
                return NotFound();
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{phaseId}")]
        public async Task<IActionResult> Delete(int phaseId, [FromBody]PhaseProduct PhaseProduct)
        {
            Phase phase = null;
            if (ModelState.IsValid)
            {
                phase = await _phaseProductService.removeProductFromPhase(PhaseProduct.phaseProductId, phaseId);
                if (phase != null)
                    return Ok(phase);
                return NotFound();
            }
            return BadRequest(ModelState);
        }
    }

}