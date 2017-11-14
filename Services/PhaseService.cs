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
        public async Task<Phase> addPhase(Phase phase)
        {

            phase.inputProducts = new List<PhaseProduct>();
            phase.outputProducts = new List<PhaseProduct>();
            phase.phaseParameters = new List<PhaseParameter>();
            phase.sucessorPhasesIds = new int[0];
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
    }

}
