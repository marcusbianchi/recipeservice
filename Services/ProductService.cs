using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using recipeservice.Data;
using recipeservice.Model;
using recipeservice.Services.Interfaces;

namespace recipeservice.Services
{
    public class ProductService : IProductService
    {
        private IConfiguration _configuration;
        private readonly ApplicationDbContext _context;
        public ProductService(IConfiguration configuration, ApplicationDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public async Task<Product> getProduct(int ProductId)
        {
            var product = await _context.Products
             .Include(x => x.additionalInformation)
             .OrderBy(x => x.productId)
             .Where(x => x.productId == ProductId)
             .FirstOrDefaultAsync();
            if (product == null)
                return null;
            return product;
        }

        public async Task<List<Product>> getProductList(int[] productIds)
        {
            var products = await _context.Products
             .Include(x => x.additionalInformation)
             .Where(x => productIds.Contains(x.productId))
             .ToListAsync();
            return products;
        }

        public async Task<List<Product>> getProducts(int startat, int quantity)
        {
            if (quantity == 0)
                quantity = 50;
            var products = await _context.Products
                      .Include(x => x.additionalInformation)
                      .Where(x => x.enabled == true)
                      .OrderBy(x => x.productId)
                      .Skip(startat)
                      .Take(quantity)
                      .ToListAsync();
            return products;
        }

        public async Task<List<Product>> GetChildrenProducts(int productId)
        {
            var parentThing = await _context.Products
           .Where(x => x.productId == productId).FirstOrDefaultAsync();

            if (parentThing != null)
            {
                var things = await _context.Products.Include(x => x.additionalInformation)
                .Where(x => parentThing.childrenProductsIds.Contains(x.productId))
                .ToListAsync();

                return things;
            }
            return null;
        }

        public async Task<Product> addProduct(Product product)
        {
            product.childrenProductsIds = new int[0];
            product.productId = 0;
            await _context.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> updateProduct(int productId, Product product)
        {
            var curTProduct = await _context.Products
                            .AsNoTracking()
                            .Where(x => x.productId == productId)
                            .FirstOrDefaultAsync();

            product.childrenProductsIds = curTProduct.childrenProductsIds;
            product.parentProductsIds = product.parentProductsIds;
            if (productId != product.productId)
            {
                return null;
            }
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<bool> deleteProduct(int productId)
        {
            var product = await _context.Products
                        .Where(x => x.productId == productId)
                        .FirstOrDefaultAsync();

            if (product != null)
            {
                product.enabled = false;
                _context.Entry(product).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }


    }
}