using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using recipeservice.Model;

namespace recipeservice.Services.Interfaces
{
    public interface IProductService
    {
        Task<Product> getProduct(int productId);
        Task<(List<Product>, int)> getProducts(int startat, int quantity, ProductFields fieldFilter, string fieldValue, ProductFields orderField, OrderEnum order);
        Task<Product> addProduct(Product product);
        Task<Product> updateProduct(int productId, Product product);
        Task<bool> deleteProduct(int productId);
        Task<List<Product>> getProductList(int[] prolductIds);
        Task<List<Product>> GetChildrenProducts(int productId);

    }

    public enum ProductFields
    {
        Default,
        productName,
        productDescription,
        productCode,
        productGTIN
    }
}

