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
    public class ProductService : IProductService
    {

        private IConfiguration _configuration;
        private HttpClient client = new HttpClient();
        public ProductService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<(List<Product>, HttpStatusCode)> GetChildrenProducts(int productId)
        {
            List<Product> returnProducts = null;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var builder = new UriBuilder(_configuration["productServiceEndpoint"] + "/api/products/childrenproducts/" + productId);
            string url = builder.ToString();
            var result = await client.GetAsync(url);
            switch (result.StatusCode)
            {
                case HttpStatusCode.OK:
                    returnProducts = JsonConvert.DeserializeObject<List<Product>>(await client.GetStringAsync(url));
                    return (returnProducts, HttpStatusCode.OK);
                case HttpStatusCode.NotFound:
                    return (returnProducts, HttpStatusCode.NotFound);
                case HttpStatusCode.InternalServerError:
                    return (returnProducts, HttpStatusCode.InternalServerError);
            }
            return (returnProducts, HttpStatusCode.NotFound);
        }

        public async Task<(Product, HttpStatusCode)> getProduct(int ProductId)
        {
            Product returnProduct = null;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var builder = new UriBuilder(_configuration["productServiceEndpoint"] + "/api/products/" + ProductId);
            string url = builder.ToString();
            var result = await client.GetAsync(url);
            switch (result.StatusCode)
            {
                case HttpStatusCode.OK:
                    returnProduct = JsonConvert.DeserializeObject<Product>(await client.GetStringAsync(url));
                    return (returnProduct, HttpStatusCode.OK);
                case HttpStatusCode.NotFound:
                    return (returnProduct, HttpStatusCode.NotFound);
                case HttpStatusCode.InternalServerError:
                    return (returnProduct, HttpStatusCode.InternalServerError);
            }
            return (returnProduct, HttpStatusCode.NotFound);

        }

        public async Task<(List<Product>, HttpStatusCode)> getProductList(int[] prolductIds)
        {
            List<Product> listProducts = null;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var builder = new UriBuilder(_configuration["productServiceEndpoint"] + "/api/products/list?");
            string url = builder.ToString();
            foreach (var item in prolductIds)
            {
                url += $"productid={item}&";
            }
            var result = await client.GetAsync(url);
            switch (result.StatusCode)
            {
                case HttpStatusCode.OK:
                    listProducts = JsonConvert.DeserializeObject<List<Product>>(await client.GetStringAsync(url));
                    return (listProducts, HttpStatusCode.OK);
                case HttpStatusCode.NotFound:
                    return (listProducts, HttpStatusCode.NotFound);
                case HttpStatusCode.InternalServerError:
                    return (listProducts, HttpStatusCode.InternalServerError);
            }
            return (listProducts, HttpStatusCode.NotFound);
        }

        public async Task<(List<Product>, HttpStatusCode)> getProducts(int startat, int quantity)
        {
            List<Product> returnProducts = null;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var builder = new UriBuilder(_configuration["productServiceEndpoint"] + "/api/products");
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
                    returnProducts = JsonConvert.DeserializeObject<List<Product>>(await client.GetStringAsync(url));
                    return (returnProducts, HttpStatusCode.OK);
                case HttpStatusCode.NotFound:
                    return (returnProducts, HttpStatusCode.NotFound);
                case HttpStatusCode.InternalServerError:
                    return (returnProducts, HttpStatusCode.InternalServerError);
            }
            return (returnProducts, HttpStatusCode.NotFound);
        }
    }
}