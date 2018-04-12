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
    public class PhaseParameterService : IPhaseParameterService
    {
        private readonly ApplicationDbContext _context;
        private readonly ITagsService _tagService;
        private readonly IPhaseService _phaseService;

        public PhaseParameterService(ApplicationDbContext context,
       ITagsService tagService,
       IPhaseService phaseService)
        {
            _context = context;
            _tagService = tagService;
            _phaseService = phaseService;
        }

        private async Task<PhaseParameter> AddParameter(PhaseParameter phaseParameter, Phase currentPhase)
        {
            var (tag, code) = await _tagService.getParameter(phaseParameter.tagId);
            if (code == HttpStatusCode.OK)
            {
                if (currentPhase.phaseParameters == null)
                    currentPhase.phaseParameters = new List<PhaseParameter>();
                currentPhase.phaseParameters.Add(phaseParameter);
                await _context.SaveChangesAsync();
            }
            return phaseParameter;

        }

        public async Task<PhaseParameter> addParameterToPhase(PhaseParameter phaseParameter, int phaseId)
        {
            var curPhase = await _phaseService.getPhase(phaseId);
            if (curPhase.phaseParameters != null)
            {
                if (curPhase != null && !curPhase.phaseParameters
                .Select(x => x.tagId).ToList().Contains(phaseParameter.tagId))
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


        public async Task<PhaseParameter> updateParameterToPhase(PhaseParameter phaseParameter, int phaseParameterId)
        {
            var phaseParameterDb = await _context.PhaseParameters
                     .Where(x => x.phaseParameterId == phaseParameterId)
                     .FirstOrDefaultAsync();

            if (phaseParameterId != phaseParameterDb.phaseParameterId && phaseParameterDb == null)
            {
                return null;
            }

            _context.PhaseParameters.Update(phaseParameter);
            await _context.SaveChangesAsync();
            return phaseParameter;
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
                return await _phaseService.getPhase(phaseid);
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
                return phase.phaseParameters.ToList();
            }
            return null;
        }

    }
}