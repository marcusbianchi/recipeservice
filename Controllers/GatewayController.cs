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
using recipeservice.Services.Interfaces;

namespace recipeservice.Controllers
{
    [Route("")]
    public class GatewayController : Controller
    {
        private HttpClient client = new HttpClient();
        private IConfiguration _configuration;
        private IThingService _thingService;
        private IThingGroupService _thingGroupService;
        private IProductService _productService;
        private ITagsService _tagsService;

        public GatewayController(IConfiguration configuration,
         IThingService thingService,
         IThingGroupService thingGroupService,
         IProductService productService,
         ITagsService tagsService
         )
        {
            _configuration = configuration;
            _thingService = thingService;
            _thingGroupService = thingGroupService;
            _productService = productService;
            _tagsService = tagsService;
        }

        [HttpGet("gateway/thinggroups/")]
        [Produces("application/json")]
        public async Task<IActionResult> GetGroups([FromQuery]int startat, [FromQuery]int quantity)
        {

            var (thingGroups, resultCode) = await _thingGroupService.getGroups(startat, quantity);
            switch (resultCode)
            {
                case HttpStatusCode.OK:
                    return Ok(thingGroups);
                case HttpStatusCode.NotFound:
                    return NotFound();
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpGet("gateway/thinggroups/{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> GetGroup(int id)
        {
            var (thingGroup, resultCode) = await _thingGroupService.getGroup(id);
            switch (resultCode)
            {
                case HttpStatusCode.OK:
                    return Ok(thingGroup);
                case HttpStatusCode.NotFound:
                    return NotFound();
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpGet("gateway/thinggroups/attachedthings/{groupid}")]
        [Produces("application/json")]
        public async Task<IActionResult> GetAttachedThings(int groupid)
        {
            var (things, resultCode) = await _thingGroupService.GetAttachedThings(groupid);
            switch (resultCode)
            {
                case HttpStatusCode.OK:
                    return Ok(things);
                case HttpStatusCode.NotFound:
                    return NotFound();
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpGet("gateway/things/{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> GetThing(int id)
        {
            var (thing, resultCode) = await _thingService.getThing(id);
            switch (resultCode)
            {
                case HttpStatusCode.OK:
                    return Ok(thing);
                case HttpStatusCode.NotFound:
                    return NotFound();
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpGet("gateway/tags/{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> GetParameter(int id)
        {
            var (tag, resultCode) = await _tagsService.getParameter(id);
            switch (resultCode)
            {
                case HttpStatusCode.OK:
                    return Ok(tag);
                case HttpStatusCode.NotFound:
                    return NotFound();
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        [HttpGet("gateway/tags/")]
        [Produces("application/json")]
        public async Task<IActionResult> GetParameters([FromQuery]int startat, [FromQuery]int quantity,[FromQuery]string fieldFilter,
        [FromQuery]string fieldValue,[FromQuery]string orderField,[FromQuery] string order)
        {

            var (tags, resultCode) = await _tagsService.getParameters(startat, quantity,fieldFilter,
        fieldValue,orderField,order);
            switch (resultCode)
            {
                case HttpStatusCode.OK:
                    return Ok(tags);
                case HttpStatusCode.NotFound:
                    return NotFound();
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }



    }
}
