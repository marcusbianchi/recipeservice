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
        private readonly IParametersService _parametersService;
        private readonly IPhaseService _phaseService;

        public PhaseParameterService(ApplicationDbContext context,
       IParametersService parametersService,
       IPhaseService phaseService)
        {
            _context = context;
            _parametersService = parametersService;
            _phaseService = phaseService;
        }

        private async Task<PhaseParameter> AddParameter(PhaseParameter phaseParameter, Phase currentPhase)
        {
            var (parameter, code) = await _parametersService.getParameter(phaseParameter.parameterId);
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

    }
}