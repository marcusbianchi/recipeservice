using System.Collections.Generic;
using System.Threading.Tasks;
using recipeservice.Model;

namespace recipeservice.Services.Interfaces
{
    public interface IPhaseParameterService
    {
        Task<PhaseParameter> addParameterToPhase(PhaseParameter phaseParameter, int phaseId);
        Task<PhaseParameter> updateParameterToPhase(PhaseParameter phaseParameter, int phaseParameterId);
        Task<List<PhaseParameter>> getParameterFromPhase(int phaseId);
        Task<Phase> removeParameterFromPhase(int phaseParameterId, int phaseid);
    }
}