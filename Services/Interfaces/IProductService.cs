using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using recipeservice.Model;

namespace recipeservice.Services.Interfaces
{
    public interface IProductService
    {
        Task<(Product, HttpStatusCode)> getProduct(int productId);
        Task<(List<Product>, HttpStatusCode)> getProducts(int startat, int quantity);
        Task<(List<Product>, HttpStatusCode)> GetChildrenProducts(int groupId);
        Task<(List<Product>, HttpStatusCode)> getProductList(int[] prolductIds);

    }
}
