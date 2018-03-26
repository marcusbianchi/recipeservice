using System.Collections.Generic;
using System.Threading.Tasks;
using recipeservice.Model;

namespace recipeservice.Services.Interfaces
{
  public interface IRecipeTypeService
    {
        Task<List<RecipeType>> getRecipeTypes(int startat, int quantity);
        Task<RecipeType> getRecipeType(int recipeTypeId);
        Task<RecipeType> addRecipeType(RecipeType recipeType);
        Task<RecipeType> updateRecipeType(int recipeTypeId, RecipeType recipeType);
        Task<RecipeType> deleteRecipeType(int recipeTypeId);
    }
}