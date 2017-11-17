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
        public async Task<PhaseProduct> addInputProductToPhase(PhaseProduct phaseProduct, int phaseId)
        {

            var curPhase = await _phaseService.getPhase(phaseId);
            if (curPhase.outputProducts != null)
            {
                if (curPhase != null && !curPhase.inputProducts
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

        public async Task<PhaseProduct> addOutputProductToPhase(PhaseProduct phaseProduct, int phaseId)
        {

            var curPhase = await _phaseService.getPhase(phaseId);
            if (curPhase.outputProducts != null)
            {
                if (curPhase != null && !curPhase.outputProducts
                .Select(x => x.productId).ToList().Contains(phaseProduct.productId))
                {
                    return await AddProduct(phaseProduct, curPhase, ListType.output);
                }
            }
            else
            {
                return await AddProduct(phaseProduct, curPhase, ListType.output);
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

        public async Task<List<PhaseProduct>> getOutputProductsFromPhase(int phaseId)
        {
            return await getProductsFromPhase(phaseId, ListType.output);
        }
        public async Task<List<PhaseProduct>> getInputProductsFromPhase(int phaseId)
        {
            return await getProductsFromPhase(phaseId, ListType.input);
        }

        private async Task<List<PhaseProduct>> getProductsFromPhase(int phaseId, ListType type)
        {
            List<PhaseProduct> phaseProducts = null;
            Phase phase = null;
            if (type == ListType.input)
            {
                phase = await _context.Phases.Include(x => x.inputProducts)
                .Where(x => x.phaseId == phaseId).FirstOrDefaultAsync();
                phaseProducts = phase.inputProducts.ToList();
            }
            else if (type == ListType.output)
            {
                phase = await _context.Phases.Include(x => x.outputProducts)
                .Where(x => x.phaseId == phaseId).FirstOrDefaultAsync();
                phaseProducts = phase.outputProducts.ToList();
            }

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

            if (type == ListType.input)
            {
                if (currentPhase.inputProducts == null)
                    currentPhase.inputProducts = new List<PhaseProduct>();
                currentPhase.inputProducts.Add(phaseProduct);
            }
            else if (type == ListType.output)
            {
                if (currentPhase.outputProducts == null)
                    currentPhase.outputProducts = new List<PhaseProduct>();
                currentPhase.outputProducts.Add(phaseProduct);
            }
            await _context.SaveChangesAsync();

            return phaseProduct;
        }


    }
}