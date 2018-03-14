using System;
using Microsoft.Extensions.Configuration;
using recipeservice.Services.Interfaces;
using recipeservice.Model;
using System.Net.Http.Headers;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using System.Net;
using System.Threading.Tasks;

namespace recipeservice.Services
{
    public class RecipeAutomaticService : IRecipeAutomaticService
    {
        private readonly IConfiguration _configuration;
        private HttpClient client = new HttpClient();
        public RecipeAutomaticService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<(bool,string)> CreateAutomaticRecipe(Recipe recipe)
        {
            if(!Convert.ToBoolean(_configuration["RecipeCreateAutomatic"]))
                return (true,string.Empty);

            return ( await PostRecipeAutomaticCreate(recipe));
        }

        private async Task<(bool,string)> PostRecipeAutomaticCreate(Recipe recipe)
        {
            try
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpContent contentPost = new StringContent(JsonConvert.SerializeObject(recipe).ToString(), Encoding.UTF8,"application/json");
                var builder = new UriBuilder(_configuration["RecipeCreateAutomaticUrl"]);
                string url = builder.ToString();
                var result = await client.PostAsync(url,contentPost);
                
                switch (result.StatusCode)
                {
                    case HttpStatusCode.OK:                    
                        return (true,string.Empty);
                    case HttpStatusCode.Created:
                        return (true,string.Empty);
                    case HttpStatusCode.InternalServerError:
                        return (false,result.ToString());
                }
                return (false,result.ToString());
            }
            catch (Exception ex)
            {
                return (false,ex.ToString());
            }
        }
    }
}