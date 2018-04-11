using System.Collections.Generic;
using System.Threading.Tasks;
using recipeservice.Model;

namespace recipeservice.Services.Interfaces
{
    public interface IRecipeService
    {
        Task<(List<Recipe>, int)> getRecipes(int startat, int quantity,List<string> fields,
             RecipeFields orderField, OrderEnum order);
        Task<Recipe> getRecipe(int recipeId);
        Task<Recipe> getRecipeCode(string code);
        Task<Recipe> addRecipe(Recipe recipe);
        Task<Recipe> updateRecipe(int recipeId, Recipe recipe);
        Task<Recipe> deleteRecipe(int recipeId);
        Task<PhaseProduct> addProductToRecipe(int recipeId, PhaseProduct phaseProduct);
        Task<Recipe> removeProductToRecipe(int recipeId, PhaseProduct phaseProduct);
    }
    public enum RecipeFields
    {
        Default,
        recipeName,
        recipeCode,
        recipeTypeId,
    }
}