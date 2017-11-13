using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using recipeservice.Model;

namespace recipeservice.Services.Interfaces
{
    public interface IPhaseService
    {
        Task<Phase> getPhase(int phaseId);
        Task<List<Phase>> getPhases(int startat, int quantity);
        Task<Phase> addPhase(Phase phase);
        Task<Phase> updatePhase(int phaseId, Phase phase);
        Task<Phase> deletePhase(int phaseId);
        Task<Phase> addInputProductToPhase(PhaseProduct phaseProduct, int phaseId);
        Task<Phase> addOutputProductToPhase(PhaseProduct phaseProduct, int phaseId);
        Task<Phase> addParameterToPhase(PhaseParameter phaseParameter, int phaseId);
        Task<List<PhaseParameter>> getParameterFromPhase(int phaseId);
        Task<Phase> removeParameterFromPhase(int phaseParameterId, int phaseid);
        Task<Phase> removeProductFromPhase(int phaseProductId, int phaseid);
        Task<List<PhaseProduct>> getInputProductsFromPhase(int phaseid);
        Task<List<PhaseProduct>> getOutputProductsFromPhase(int phaseid);

    }
}


