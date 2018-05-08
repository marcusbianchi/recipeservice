using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using recipeservice.Data;
using recipeservice.Model;
using recipeservice.Services.Interfaces;
using securityfilter;

namespace recipeservice.Controllers {
    [Route ("api/[controller]")]
    public class ExtraAttributeTypesController : Controller {
        private readonly IExtraAttributeTypeService _extraAttributeTypeService;

        public ExtraAttributeTypesController (IExtraAttributeTypeService extraAttributeTypeService) {
            _extraAttributeTypeService = extraAttributeTypeService;
        }

        [HttpGet]
        [SecurityFilter ("products__allow_read")]
        [ResponseCache (CacheProfileName = "recipecache")]
        public async Task<IActionResult> Get () {

            var extraAttributeType = await _extraAttributeTypeService.getExtraAttibruteTypes ();
            return Ok (extraAttributeType);
        }

        [HttpGet ("{id}")]
        [SecurityFilter ("products__allow_read")]
        [ResponseCache (CacheProfileName = "recipecache")]
        public async Task<IActionResult> Get (int id) {
            var extraAttributeType = await _extraAttributeTypeService.getExtraAttibruteType (id);
            if (extraAttributeType == null)
                return NotFound ();

            return Ok (extraAttributeType);
        }

        [HttpPost]
        [SecurityFilter ("products__allow_update")]
        public async Task<IActionResult> Post ([FromBody] ExtraAttibruteType extraAttributeType) {
            if (ModelState.IsValid) {
                extraAttributeType = await _extraAttributeTypeService.addExtraAttibruteType (extraAttributeType);
                return Created ($"api/products/{extraAttributeType.extraAttibruteTypeId}", extraAttributeType);
            }
            return BadRequest (ModelState);
        }

        [HttpPut ("{id}")]
        [SecurityFilter ("products__allow_update")]
        public async Task<IActionResult> Put (int id, [FromBody] ExtraAttibruteType extraAttributeType) {
            if (ModelState.IsValid) {
                extraAttributeType = await _extraAttributeTypeService.updateExtraAttibruteType (id, extraAttributeType);
                if (extraAttributeType == null)
                    return NotFound ();
                return NoContent ();

            }
            return BadRequest (ModelState);
        }

        [HttpDelete ("{id}")]
        [SecurityFilter ("products__allow_update")]
        public async Task<IActionResult> Delete (int id) {
            var result = await _extraAttributeTypeService.deleteExtraAttibruteType (id);
            if (result)
                return NoContent ();
            return NotFound ();
        }
    }
}