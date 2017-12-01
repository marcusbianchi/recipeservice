using System;
using System.Linq;
using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using recipeservice.Data;
using recipeservice.Model;
using recipeservice.Services.Interfaces;

namespace recipeservice.Services
{
    enum ListType
    {
        input,
        output

    }
    public class PhaseProductService : IPhaseProductService
    {
        private readonly ApplicationDbContext _context;
        private readonly IProductService _productService;
        private readonly IPhaseService _phaseService;

        public PhaseProductService(ApplicationDbContext context,
       IProductService productService,
       IPhaseService phaseService)
        {
            _context = context;
            _productService = productService;
            _phaseService = phaseService;
        }
        public async Task<PhaseProduct> addProductToPhase(PhaseProduct phaseProduct, int phaseId)
        {

            var curPhase = await _phaseService.getPhase(phaseId);
            if (curPhase.phaseProducts != null)
            {
                if (curPhase != null && !curPhase.phaseProducts
                .Select(x => x.productId).ToList().Contains(phaseProduct.productId))
                {
                    return await AddProduct(phaseProduct, curPhase, ListType.input);
                }
            }
            else
            {
                return await AddProduct(phaseProduct, curPhase, ListType.input);
            }
            return null;
        }


        public async Task<Phase> removeProductFromPhase(int phaseProductId, int phaseid)
        {
            var phaseProduct = await _context.PhaseProducts
           .Where(x => x.phaseProductId == phaseProductId).FirstOrDefaultAsync();
            if (phaseProduct != null)
            {
                _context.Remove(phaseProduct);
                await _context.SaveChangesAsync();
                return await _phaseService.getPhase(phaseid);
            }
            return null;
        }

        public async Task<List<PhaseProduct>> getProductsFromPhase(int phaseId)
        {
            return await getProductsFromPhase(phaseId, ListType.output);
        }

        private async Task<List<PhaseProduct>> getProductsFromPhase(int phaseId, ListType type)
        {
            List<PhaseProduct> phaseProducts = null;
            Phase phase = null;

            phase = await _context.Phases.Include(x => x.phaseProducts)
            .Where(x => x.phaseId == phaseId).FirstOrDefaultAsync();
            phaseProducts = phase.phaseProducts.ToList();


            var products = await _productService.getProductList(phaseProducts.Select(x => x.productId).ToArray());
            if (products.Count > 0)
            {
                phaseProducts.ToList()
                 .ForEach(x => x.product = products
                 .Where(y => y.productId == x.productId).FirstOrDefault());
            }
            return phaseProducts;
        }
        private async Task<PhaseProduct> AddProduct(PhaseProduct phaseProduct, Phase currentPhase, ListType type)
        {
            var product = await _productService.getProduct(phaseProduct.productId);

            if (currentPhase.phaseProducts == null)
                currentPhase.phaseProducts = new List<PhaseProduct>();
            currentPhase.phaseProducts.Add(phaseProduct);
            await _context.SaveChangesAsync();

            return phaseProduct;
        }


    }
}