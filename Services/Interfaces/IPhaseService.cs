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
       

    }
}


