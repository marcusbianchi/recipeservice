using System.Collections.Generic;
using System.Threading.Tasks;
using recipeservice.Model;

namespace recipeservice.Services.Interfaces
{
    public interface IPhaseProductService
    {
        Task<List<PhaseProduct>> getProductsFromPhase(int phaseid);
        Task<Phase> removeProductFromPhase(int phaseProductId, int phaseid);
        Task<PhaseProduct> addProductToPhase(PhaseProduct phaseProduct, int phaseId);
    }
}