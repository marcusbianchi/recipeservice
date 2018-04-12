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
        private readonly ITagsService _tagService;

        public PhaseService(ApplicationDbContext context,
        IProductService productService,
        ITagsService tagService)
        {
            _context = context;
            _productService = productService;
            _tagService = tagService;
        }

        public async Task<Phase> getPhase(int phaseId)
        {
            Phase phase = await _context.Phases
            .Include(x => x.phaseProducts)
            .Include(x => x.phaseParameters).AsNoTracking()
            .Where(x => x.phaseId == phaseId).FirstOrDefaultAsync();
            if (phase != null)
            {
                if (phase.phaseParameters != null)
                {
                    int[] parametersId = phase.phaseParameters.Select(x => x.tagId).ToArray();
                    if (parametersId.Length > 0)
                    {
                        var (tags, statusParameter) = await _tagService.getParameterList(parametersId);
                        if (statusParameter == HttpStatusCode.OK)
                        {
                            phase.phaseParameters.ToList()
                             .ForEach(x => x.tag = tags
                             .Where(y => y.tagId == x.tagId).FirstOrDefault());

                        }
                    }
                }

                List<int> productsId = new List<int>();
                if (phase.phaseProducts != null)
                    productsId.AddRange(phase.phaseProducts.Select(x => x.productId).ToList());
                if (productsId.Count > 0)
                {
                    var products = await _productService.getProductList(productsId.ToArray());
                    if (productsId.Count > 0)
                    {
                        phase.phaseProducts.ToList()
                        .ForEach(x => x.product = products
                        .Where(y => y.productId == x.productId).FirstOrDefault());
                    }
                }
            }

            return phase;
        }

        public async Task<List<Phase>> getPhases(int startat, int quantity)
        {
            var phases = await _context.Phases
            .Include(x => x.phaseProducts)
            .Include(x => x.phaseParameters)
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
                .Include(x => x.phaseProducts)
                .Include(x => x.phaseParameters)
               .AsNoTracking()
               .Where(x => x.phaseId == phaseId)
               .FirstOrDefaultAsync();


            if (phaseId != phase.phaseId && curPhase == null)
            {
                return null;
            }
            phase.phaseProducts = curPhase.phaseProducts;
            phase.phaseParameters = phase.phaseParameters;

            _context.Phases.Update(phase);
            await _context.SaveChangesAsync();
            return curPhase;
        }
        public async Task<Phase> addPhase(Phase phase)
        {

            phase.phaseProducts = new List<PhaseProduct>();
            phase.phaseParameters = new List<PhaseParameter>();
            phase.sucessorPhasesIds = new int[0];
            await _context.AddAsync(phase);
            await _context.SaveChangesAsync();
            return phase;
        }

        public async Task<Phase> deletePhase(int phaseId)
        {
            var curPhase = await _context.Phases
                .Include(x => x.phaseProducts)
                .Include(x => x.phaseParameters)
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
