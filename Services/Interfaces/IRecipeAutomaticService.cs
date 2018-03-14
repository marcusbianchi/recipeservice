using System.Threading.Tasks;
using recipeservice.Model;

namespace recipeservice.Services.Interfaces
{
    public interface IRecipeAutomaticService
    {
         Task<(bool,string)> CreateAutomaticRecipe(Recipe recipe);
    }

}