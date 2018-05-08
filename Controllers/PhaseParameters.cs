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
    [Route ("api/phases/parameters/")]
    public class PhaseParametersController : Controller {
        private readonly IPhaseParameterService _phaseParameterService;
        public PhaseParametersController (IPhaseParameterService phaseParameterService) {
            _phaseParameterService = phaseParameterService;
        }

        [HttpGet ("{phaseId}")]
        [SecurityFilter ("recipes__allow_read")]
        public async Task<IActionResult> Get (int phaseId) {
            return Ok (await _phaseParameterService.getParameterFromPhase (phaseId));
        }

        [HttpPut ("{phaseParameterId}")]
        [SecurityFilter ("recipes__allow_update")]
        public async Task<IActionResult> Put (int phaseParameterId, [FromBody] PhaseParameter phaseParameter) {
            if (ModelState.IsValid) {
                var parameter = await _phaseParameterService.updateParameterToPhase (phaseParameter, phaseParameterId);
                if (parameter != null)
                    return Ok (parameter);
                return NotFound ();
            }
            return BadRequest (ModelState);
        }

        [HttpPost ("{phaseId}")]
        [SecurityFilter ("recipes__allow_update")]
        public async Task<IActionResult> Post (int phaseId, [FromBody] PhaseParameter phaseParameter) {
            phaseParameter.phaseParameterId = 0;
            if (ModelState.IsValid) {
                var phase = await _phaseParameterService.addParameterToPhase (phaseParameter, phaseId);
                if (phase != null)
                    return Ok (phase);
                return NotFound ();
            }
            return BadRequest (ModelState);
        }

        [HttpDelete ("{phaseId}")]
        [SecurityFilter ("recipes__allow_update")]
        public async Task<IActionResult> Delete (int phaseId, [FromBody] PhaseParameter phaseParameter) {
            Phase phase = null;
            if (ModelState.IsValid) {
                phase = await _phaseParameterService.removeParameterFromPhase (phaseParameter.phaseParameterId, phaseId);
                if (phase != null)
                    return Ok (phase);
                return NotFound ();
            }
            return BadRequest (ModelState);
        }
    }

}