using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    public class PhaseService : IPhaseService
    {
        private readonly ApplicationDbContext _context;
        private readonly IProductService _productService;
        private readonly IParametersService _parametersService;

        public PhaseService(ApplicationDbContext context,
        IProductService productService,
        IParametersService parametersService)
        {
            _context = context;
            _productService = productService;
            _parametersService = parametersService;
        }

        public async Task<Phase> getPhase(int phaseId)
        {
            Phase phase = await _context.Phases
            .Include(x => x.inputProducts)
            .Include(x => x.phaseParameters)
            .Include(x => x.outputProducts)
            .Where(x => x.phaseId == phaseId).FirstOrDefaultAsync();
            if (phase != null)
            {
                if (phase.phaseParameters != null)
                {
                    int[] parametersId = phase.phaseParameters.Select(x => x.parameterId).ToArray();
                    if (parametersId.Length > 0)
                    {
                        var (parameters, statusParameter) = await _parametersService.getParameterList(parametersId);
                        if (statusParameter == HttpStatusCode.OK)
                        {
                            phase.phaseParameters.ToList()
                             .ForEach(x => x.parameter = parameters
                             .Where(y => y.parameterId == x.parameterId).FirstOrDefault());

                        }
                    }
                }

                List<int> productsId = new List<int>();
                if (phase.inputProducts != null)
                    productsId.AddRange(phase.inputProducts.Select(x => x.productId).ToList());
                if (phase.outputProducts != null)
                {
                    foreach (var item in phase.outputProducts.Select(x => x.productId))
                        if (!productsId.Contains(item))
                            productsId.Add(item);
                }
                if (productsId.Count > 0)
                {
                    var (products, statusProduct) = await _productService.getProductList(productsId.ToArray());
                    if (productsId.Count > 0)
                    {
                        if (statusProduct == HttpStatusCode.OK)
                        {
                            phase.inputProducts.ToList()
                             .ForEach(x => x.product = products
                             .Where(y => y.productId == x.productId).FirstOrDefault());
                            phase.outputProducts.ToList()
                                            .ForEach(x => x.product = products
                                            .Where(y => y.productId == x.productId).FirstOrDefault());
                        }
                    }
                }
            }

            return phase;
        }

        public async Task<List<Phase>> getPhases(int startat, int quantity)
        {
            var phases = await _context.Phases
            .Include(x => x.inputProducts)
            .Include(x => x.phaseParameters)
            .Include(x => x.outputProducts)
           .OrderBy(x => x.phaseId)
           .Skip(startat).Take(quantity)
           .ToListAsync();
            var outputase = new List<Phase>();
            foreach (var item in phases)
            {
                outputase.Add(await getPhase(item.phaseId));
            }
            return phases;
        }

        public async Task<Phase> updatePhase(int phaseId, Phase phase)
        {
            var curPhase = await _context.Phases
                .Include(x => x.inputProducts)
                .Include(x => x.phaseParameters)
                .Include(x => x.outputProducts)
               .AsNoTracking()
               .Where(x => x.phaseId == phaseId)
               .FirstOrDefaultAsync();

            phase.inputProducts = curPhase.inputProducts;
            phase.outputProducts = curPhase.outputProducts;
            phase.phaseParameters = phase.phaseParameters;

            if (phaseId != phase.phaseId)
            {
                return null;
            }
            _context.Phases.Update(phase);
            await _context.SaveChangesAsync();
            return curPhase;
        }


        public async Task<Phase> addInputProductToPhase(PhaseProduct phaseProduct, int phaseId)
        {

            var curPhase = await getPhase(phaseId);
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

        public async Task<Phase> addOutputProductToPhase(PhaseProduct phaseProduct, int phaseId)
        {

            var curPhase = await getPhase(phaseId);
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

        public async Task<Phase> addParameterToPhase(PhaseParameter phaseParameter, int phaseId)
        {
            var curPhase = await getPhase(phaseId);
            if (curPhase.outputProducts != null)
            {
                if (curPhase != null && !curPhase.phaseParameters
                .Select(x => x.parameterId).ToList().Contains(phaseParameter.parameterId))
                {
                    return await AddParameter(phaseParameter, curPhase);
                }
            }
            else
            {
                return await AddParameter(phaseParameter, curPhase);
            }
            return null;
        }


        public async Task<Phase> removeParameterFromPhase(int phaseParameterId, int phaseid)
        {
            var phaseParameter = await _context.PhaseParameters
            .Where(x => x.phaseParameterId == phaseParameterId)
            .FirstOrDefaultAsync();
            if (phaseParameter != null)
            {
                _context.Remove(phaseParameter);
                await _context.SaveChangesAsync();
                return await getPhase(phaseid);
            }
            return null;
        }

        public async Task<List<PhaseParameter>> getParameterFromPhase(int phaseId)
        {
            var phase = await _context.Phases
            .Include(x => x.phaseParameters)
            .Where(x => x.phaseId == phaseId)
            .FirstOrDefaultAsync();
            if (phase != null)
            {
                if (phase.phaseParameters != null)
                {
                    int[] parametersId = phase.phaseParameters.Select(x => x.parameterId).ToArray();
                    if (parametersId.Length > 0)
                    {
                        var (parameters, statusParameter) = await _parametersService.getParameterList(parametersId);
                        if (statusParameter == HttpStatusCode.OK)
                        {
                            phase.phaseParameters.ToList()
                             .ForEach(x => x.parameter = parameters
                             .Where(y => y.parameterId == x.parameterId).FirstOrDefault());

                        }
                    }
                }
                return phase.phaseParameters.ToList();
            }
            return null;
        }

        public async Task<Phase> addPhase(Phase phase)
        {

            phase.inputProducts = new List<PhaseProduct>();
            phase.outputProducts = new List<PhaseProduct>();
            phase.phaseParameters = new List<PhaseParameter>();
            await _context.AddAsync(phase);
            await _context.SaveChangesAsync();
            return phase;
        }

        public async Task<Phase> deletePhase(int phaseId)
        {
            var curPhase = await _context.Phases
                .Include(x => x.inputProducts)
                .Include(x => x.phaseParameters)
                .Include(x => x.outputProducts)
              .AsNoTracking()
              .Where(x => x.phaseId == phaseId)
              .FirstOrDefaultAsync();
            if (curPhase != null)
            {
                _context.Entry(curPhase).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
            }

            return curPhase;
        }

        public async Task<Phase> removeProductFromPhase(int phaseProductId, int phaseid)
        {
            var phaseProduct = await _context.PhaseProducts
           .Where(x => x.phaseProductId == phaseProductId)
           .FirstOrDefaultAsync();
            if (phaseProduct != null)
            {
                _context.Remove(phaseProduct);
                await _context.SaveChangesAsync();
                return await getPhase(phaseid);
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

            var (products, statusProduct) = await _productService.getProductList(phaseProducts.Select(x => x.productId).ToArray());
            if (products.Count > 0)
            {
                if (statusProduct == HttpStatusCode.OK)
                {
                    phaseProducts.ToList()
                     .ForEach(x => x.product = products
                     .Where(y => y.productId == x.productId).FirstOrDefault());
                }

            }
            return phaseProducts;
        }
        private async Task<Phase> AddProduct(PhaseProduct phaseProduct, Phase currentPhase, ListType type)
        {
            var (product, code) = await _productService.getProduct(phaseProduct.productId);
            if (code == HttpStatusCode.OK)
            {
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
            }
            return await getPhase(currentPhase.phaseId);
        }



        private async Task<Phase> AddParameter(PhaseParameter phaseParameter, Phase currentPhase)
        {
            var (parameter, code) = await _parametersService.getParameter(phaseParameter.parameterId);
            if (code == HttpStatusCode.OK)
            {
                if (currentPhase.phaseParameters == null)
                    currentPhase.phaseParameters = new List<PhaseParameter>();
                currentPhase.phaseParameters.Add(phaseParameter);
                await _context.SaveChangesAsync();
            }
            return await getPhase(currentPhase.phaseId);

        }

    }

}
