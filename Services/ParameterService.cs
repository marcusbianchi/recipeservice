using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using recipeservice.Model;
using recipeservice.Services.Interfaces;

namespace recipeservice.Services
{
    public class ParameterService : IParametersService
    {
        private IConfiguration _configuration;
        private HttpClient client = new HttpClient();
        public ParameterService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<(Parameter, HttpStatusCode)> getParameter(int thingId)
        {
            Parameter returnParameter = null;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var builder = new UriBuilder(_configuration["thingServiceEndpoint"] + "/api/parameters/" + thingId);
            string url = builder.ToString();
            var result = await client.GetAsync(url);
            switch (result.StatusCode)
            {
                case HttpStatusCode.OK:
                    returnParameter = JsonConvert.DeserializeObject<Parameter>(await client.GetStringAsync(url));
                    return (returnParameter, HttpStatusCode.OK);
                case HttpStatusCode.NotFound:
                    return (returnParameter, HttpStatusCode.NotFound);
                case HttpStatusCode.InternalServerError:
                    return (returnParameter, HttpStatusCode.InternalServerError);
            }
            return (returnParameter, HttpStatusCode.NotFound);

        }

        public async Task<(List<Parameter>, HttpStatusCode)> getParameterList(int[] parameterids)
        {
            List<Parameter> listParameters = null;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var builder = new UriBuilder(_configuration["thingServiceEndpoint"] + "/api/parameters/list?");
            string url = builder.ToString();
            foreach (var item in parameterids)
            {
                url += $"parameterid={item}&";
            }
            var result = await client.GetAsync(url);
            switch (result.StatusCode)
            {
                case HttpStatusCode.OK:
                    listParameters = JsonConvert.DeserializeObject<List<Parameter>>(await client.GetStringAsync(url));
                    return (listParameters, HttpStatusCode.OK);
                case HttpStatusCode.NotFound:
                    return (listParameters, HttpStatusCode.NotFound);
                case HttpStatusCode.InternalServerError:
                    return (listParameters, HttpStatusCode.InternalServerError);
            }
            return (listParameters, HttpStatusCode.NotFound);
        }

        public async Task<(List<Parameter>, HttpStatusCode)> getParameters(int startat, int quantity)
        {
            List<Parameter> returnParameters = null;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var builder = new UriBuilder(_configuration["thingServiceEndpoint"] + "/api/parameters");
            var query = HttpUtility.ParseQueryString(builder.Query);
            if (startat != 0)
                query["startat"] = startat.ToString();
            if (quantity != 0)
                query["quantity"] = quantity.ToString();
            builder.Query = query.ToString();
            string url = builder.ToString();
            var result = await client.GetAsync(url);
            switch (result.StatusCode)
            {
                case HttpStatusCode.OK:
                    returnParameters = JsonConvert.DeserializeObject<List<Parameter>>(await client.GetStringAsync(url));
                    return (returnParameters, HttpStatusCode.OK);
                case HttpStatusCode.NotFound:
                    return (returnParameters, HttpStatusCode.NotFound);
                case HttpStatusCode.InternalServerError:
                    return (returnParameters, HttpStatusCode.InternalServerError);
            }
            return (returnParameters, HttpStatusCode.NotFound);
        }
    }
}