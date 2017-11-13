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
    [Route("api/phases/parameters/")]
    public class PhaseParametersController : Controller
    {
        private readonly IPhaseService _phaseService;
        public PhaseParametersController(IPhaseService phaseService)
        {
            _phaseService = phaseService;
        }

        [HttpGet("{phaseId}")]
        public async Task<IActionResult> Get(int phaseId)
        {
            return Ok(await _phaseService.getParameterFromPhase(phaseId));
        }

        [HttpPost("{phaseId}")]
        public async Task<IActionResult> Post(int phaseId, [FromBody]PhaseParameter phaseParameter)
        {
            phaseParameter.phaseParameterId = 0;
            if (ModelState.IsValid)
            {
                var phase = await _phaseService.addParameterToPhase(phaseParameter, phaseId);
                if (phase != null)
                    return Created($"api/phases/{phase.phaseId}", phase);
                return NotFound();
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{phaseId}")]
        public async Task<IActionResult> Delete(int phaseId, [FromBody]PhaseParameter phaseParameter)
        {
            Phase phase = null;
            if (ModelState.IsValid)
            {
                phase = await _phaseService.removeParameterFromPhase(phaseParameter.phaseParameterId, phaseId);
                if (phase != null)
                    return Ok(phase);
                return NotFound();
            }
            return BadRequest(ModelState);
        }
    }

}