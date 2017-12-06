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

        public async Task<(List<Product>, int)> getProducts(int startat, int quantity,
        ProductFields fieldFilter, string fieldValue, ProductFields orderField, OrderEnum order)
        {
            var queryProducts = _context.Products
                      .Where(x => x.enabled == true);
            queryProducts = ApplyFilter(queryProducts, fieldFilter, fieldValue);
            queryProducts = ApplyOrder(queryProducts, orderField, order);
            var products = await queryProducts.Include(x => x.additionalInformation)
            .Skip(startat).Take(quantity).ToListAsync();
            var totalCount = await _context.Products.Where(x => x.enabled == true).CountAsync();
            return (products, totalCount);
        }

        public async Task<List<Product>> GetChildrenProducts(int productId)
        {
            var parentProduct = await _context.Products
           .Where(x => x.productId == productId).FirstOrDefaultAsync();

            if (parentProduct != null)
            {
                var things = await _context.Products.Include(x => x.additionalInformation)
                .Where(x => parentProduct.childrenProductsIds.Contains(x.productId))
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
        private IQueryable<Product> ApplyFilter(IQueryable<Product> queryProducts,
        ProductFields fieldFilter, string fieldValue)
        {
            switch (fieldFilter)
            {
                case ProductFields.productCode:
                    queryProducts = queryProducts.Where(x => x.productCode.Contains(fieldValue));
                    break;
                case ProductFields.productDescription:
                    queryProducts = queryProducts.Where(x => x.productDescription.Contains(fieldValue));
                    break;
                case ProductFields.productGTIN:
                    queryProducts = queryProducts.Where(x => x.productGTIN.Contains(fieldValue));
                    break;
                case ProductFields.productName:
                    queryProducts = queryProducts.Where(x => x.productName.Contains(fieldValue));
                    break;
                default:
                    break;
            }
            return queryProducts;
        }

        private IQueryable<Product> ApplyOrder(IQueryable<Product> queryProducts,
        ProductFields orderField, OrderEnum order)
        {
            switch (orderField)
            {
                case ProductFields.productCode:
                    if (order == OrderEnum.Ascending)
                        queryProducts = queryProducts.OrderBy(x => x.productCode);
                    else
                        queryProducts = queryProducts.OrderByDescending(x => x.productCode);
                    break;
                case ProductFields.productDescription:
                    if (order == OrderEnum.Ascending)
                        queryProducts = queryProducts.OrderBy(x => x.productDescription);
                    else
                        queryProducts = queryProducts.OrderByDescending(x => x.productDescription);
                    break;
                case ProductFields.productGTIN:
                    if (order == OrderEnum.Ascending)
                        queryProducts = queryProducts.OrderBy(x => x.productGTIN);
                    else
                        queryProducts = queryProducts.OrderByDescending(x => x.productGTIN);
                    break;
                case ProductFields.productName:
                    if (order == OrderEnum.Ascending)
                        queryProducts = queryProducts.OrderBy(x => x.productName);
                    else
                        queryProducts = queryProducts.OrderByDescending(x => x.productName);
                    break;
                default:
                    queryProducts = queryProducts.OrderBy(x => x.productId);
                    break;
            }
            return queryProducts;
        }

    }
}