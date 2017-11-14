using System.Collections.Generic;
using System.Threading.Tasks;
using recipeservice.Model;

namespace recipeservice.Services.Interfaces
{
    public interface IPhaseProductService
    {
        Task<List<PhaseProduct>> getInputProductsFromPhase(int phaseid);
        Task<List<PhaseProduct>> getOutputProductsFromPhase(int phaseid);
        Task<Phase> removeProductFromPhase(int phaseProductId, int phaseid);
        Task<PhaseProduct> addInputProductToPhase(PhaseProduct phaseProduct, int phaseId);
        Task<PhaseProduct> addOutputProductToPhase(PhaseProduct phaseProduct, int phaseId);
    }
}