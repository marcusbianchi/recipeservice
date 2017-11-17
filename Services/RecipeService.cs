using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using recipeservice.Data;
using recipeservice.Model;
using recipeservice.Services.Interfaces;

namespace recipeservice.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly ApplicationDbContext _context;
        private readonly IProductService _productService;
        private readonly IPhaseService _phaseService;

        public RecipeService(ApplicationDbContext context,
       IProductService productService,
       IPhaseService phaseService)
        {
            _context = context;
            _productService = productService;
            _phaseService = phaseService;
        }

        public async Task<List<Recipe>> getRecipes(int startat, int quantity)
        {
            var recipesId = await _context.Recipes
                     .OrderBy(x => x.recipeId)
                     .Skip(startat).Take(quantity)
                     .Select(x => x.recipeId)
                     .ToListAsync();
            List<Recipe> recipes = new List<Recipe>();
            foreach (var item in recipesId)
            {
                var recipe = await getRecipe(item);
                if (recipe != null)
                    recipes.Add(recipe);
            }

            return recipes;
        }

        public async Task<Recipe> getRecipe(int recipeId)
        {
            var recipe = await _context.Recipes
                     .OrderBy(x => x.recipeId)
                     .Include(x => x.recipeProduct)
                     .Where(x => x.recipeId == recipeId)
                     .FirstOrDefaultAsync();
            if (recipe != null)
            {
                if (recipe.recipeProduct != null)
                {
                    var product = await _productService.getProduct(recipe.recipeProduct.productId);
                    if (product != null)
                    {
                        recipe.recipeProduct.product = product;
                    }
                }
            }
            return recipe;
        }

        public async Task<Recipe> addRecipe(Recipe recipe)
        {
            recipe.recipeProduct = null;
            recipe.phasesId = new int[0];
            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();
            return recipe;
        }

        public async Task<Recipe> updateRecipe(int recipeId, Recipe recipe)
        {
            var curRecipe = await _context.Recipes
                    .OrderBy(x => x.recipeId)
                    .Include(x => x.recipeProduct)
                    .Where(x => x.recipeId == recipeId).AsNoTracking()
                    .FirstOrDefaultAsync();


            if (recipeId != recipe.recipeId && curRecipe == null)
            {
                return null;
            }

            recipe.phasesId = curRecipe.phasesId;
            recipe.recipeProduct = curRecipe.recipeProduct;

            _context.Recipes.Update(recipe);
            await _context.SaveChangesAsync();
            return recipe;
        }

        public async Task<Recipe> deleteRecipe(int recipeId)
        {
            var curRecipe = await _context.Recipes
                    .OrderBy(x => x.recipeId)
                    .Include(x => x.recipeProduct)
                    .Where(x => x.recipeId == recipeId)
                    .FirstOrDefaultAsync();


            if (curRecipe != null)
            {
                _context.Entry(curRecipe).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
            }
            return curRecipe;
        }

        public async Task<PhaseProduct> addProductToRecipe(int recipeId, PhaseProduct phaseProduct)
        {
            var curRecipe = await _context.Recipes
                   .OrderBy(x => x.recipeId)
                   .Where(x => x.recipeId == recipeId)
                   .FirstOrDefaultAsync();
            if (curRecipe == null)
            {
                return null;
            }
            var product = await _productService.getProduct(phaseProduct.productId);
            curRecipe.recipeProduct = phaseProduct;
            _context.Recipes.Update(curRecipe);
            await _context.SaveChangesAsync();
            return phaseProduct;
        }

        public async Task<Recipe> removeProductToRecipe(int recipeId, PhaseProduct phaseProduct)
        {
            var curRecipe = await _context.Recipes
                   .OrderBy(x => x.recipeId)
                   .Where(x => x.recipeId == recipeId)
                   .FirstOrDefaultAsync();
            if (curRecipe == null)
            {
                return null;
            }
            if (curRecipe.recipeProduct.productId != phaseProduct.productId)
            {
                return null;
            }
            curRecipe.recipeProduct = null;
            _context.Recipes.Update(curRecipe);
            await _context.SaveChangesAsync();
            return await getRecipe(recipeId);
        }


    }
}