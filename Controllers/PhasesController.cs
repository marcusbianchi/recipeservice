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
    public class PhasesController : Controller
    {
        private readonly IPhaseService _phaseService;
        public PhasesController(IPhaseService phaseService)
        {
            _phaseService = phaseService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]int startat, [FromQuery]int quantity)
        {
            if (quantity == 0)
                quantity = 50;
            var phases = await _phaseService.getPhases(startat, quantity);
            return Ok(phases);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var phase = await _phaseService.getPhase(id);
            if (phase == null)
                return NotFound();
            return Ok(phase);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Phase phase)
        {
            phase.phaseId = 0;
            if (ModelState.IsValid)
            {
                phase = await _phaseService.addPhase(phase);
                return Created($"api/phases/{phase.phaseId}", phase);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Phase phase)
        {
            if (ModelState.IsValid)
            {
                phase = await _phaseService.updatePhase(id, phase);
                if (phase != null)
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
                var phase = await _phaseService.deletePhase(id);
                if (phase != null)
                {
                    return NoContent();
                }
                return NotFound();
            }
            return BadRequest(ModelState);
        }
    }

}