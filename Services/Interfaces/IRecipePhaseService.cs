using System.Collections.Generic;
using System.Threading.Tasks;
using recipeservice.Model;

namespace recipeservice.Services.Interfaces
{
    public interface IRecipePhaseService
    {
        Task<Phase> addPhaseToRecipe(Phase phase, int recipeId);
        Task<List<Phase>> getPhasesFromRecipe(int recipeId);
        Task<Recipe> removePhaseFromRecipe(int phaseId, int recipeId);
    }
}