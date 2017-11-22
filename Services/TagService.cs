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
    public class TagService : ITagsService
    {
        private IConfiguration _configuration;
        private HttpClient client = new HttpClient();
        public TagService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<(Tag, HttpStatusCode)> getParameter(int tagId)
        {
            Tag returnTag = null;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var builder = new UriBuilder(_configuration["thingServiceEndpoint"] + "/api/tags/" + tagId);
            string url = builder.ToString();
            var result = await client.GetAsync(url);
            switch (result.StatusCode)
            {
                case HttpStatusCode.OK:
                    returnTag = JsonConvert.DeserializeObject<Tag>(await client.GetStringAsync(url));
                    return (returnTag, HttpStatusCode.OK);
                case HttpStatusCode.NotFound:
                    return (returnTag, HttpStatusCode.NotFound);
                case HttpStatusCode.InternalServerError:
                    return (returnTag, HttpStatusCode.InternalServerError);
            }
            return (returnTag, HttpStatusCode.NotFound);

        }

        public async Task<(List<Tag>, HttpStatusCode)> getParameterList(int[] parameterids)
        {
            List<Tag> listTags = null;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var builder = new UriBuilder(_configuration["thingServiceEndpoint"] + "/api/tags/list?");
            string url = builder.ToString();
            foreach (var item in parameterids)
            {
                url += $"tagid={item}&";
            }
            var result = await client.GetAsync(url);
            switch (result.StatusCode)
            {
                case HttpStatusCode.OK:
                    listTags = JsonConvert.DeserializeObject<List<Tag>>(await client.GetStringAsync(url));
                    return (listTags, HttpStatusCode.OK);
                case HttpStatusCode.NotFound:
                    return (listTags, HttpStatusCode.NotFound);
                case HttpStatusCode.InternalServerError:
                    return (listTags, HttpStatusCode.InternalServerError);
            }
            return (listTags, HttpStatusCode.NotFound);
        }

        public async Task<(List<Tag>, HttpStatusCode)> getParameters(int startat, int quantity)
        {
            List<Tag> returnTag = null;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var builder = new UriBuilder(_configuration["thingServiceEndpoint"] + "/api/tags");
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
                    returnTag = JsonConvert.DeserializeObject<List<Tag>>(await client.GetStringAsync(url));
                    return (returnTag, HttpStatusCode.OK);
                case HttpStatusCode.NotFound:
                    return (returnTag, HttpStatusCode.NotFound);
                case HttpStatusCode.InternalServerError:
                    return (returnTag, HttpStatusCode.InternalServerError);
            }
            return (returnTag, HttpStatusCode.NotFound);
        }
    }
}