using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using recipeservice.Data;
using recipeservice.Model;
using recipeservice.Services.Interfaces;

namespace recipeservice.Services {
    public class RecipeTypeService : IRecipeTypeService {
        private readonly ApplicationDbContext _context;

        public RecipeTypeService (ApplicationDbContext context) {
            _context = context;

        }
        public async Task<RecipeType> addRecipeType (RecipeType recipeType) {
            _context.RecipeTypes.Add (recipeType);
            await _context.SaveChangesAsync ();
            return recipeType;
        }

        public async Task<RecipeType> deleteRecipeType (int recipeTypeId) {
            var recipeType = await _context.RecipeTypes
                .Where (x => x.recipeTypeId == recipeTypeId)
                .FirstOrDefaultAsync ();
            if (recipeType != null) {
                _context.Entry (recipeType).State = EntityState.Deleted;
                await _context.SaveChangesAsync ();
            }
            return recipeType;
        }

        public async Task<RecipeType> getRecipeType (int recipeTypeId) {
            var recipeType = await _context.RecipeTypes
                .Where (x => x.recipeTypeId == recipeTypeId)
                .FirstOrDefaultAsync ();
            return recipeType;
        }

        public async Task<List<RecipeType>> getRecipeTypes (int startat, int quantity) {
            var recipeTypeIds = await _context.RecipeTypes
                .OrderBy (x => x.recipeTypeId)
                .Skip (startat).Take (quantity)
                .Select (x => x.recipeTypeId)
                .ToListAsync ();
            List<RecipeType> rOtypes = new List<RecipeType> ();
            foreach (var item in recipeTypeIds) {
                rOtypes.Add (await getRecipeType (item));
            }
            return rOtypes;
        }

        public async Task<RecipeType> updateRecipeType (int recipeTypeId, RecipeType recipeType) {
            var currentType = await _context.RecipeTypes
                .Where (x => x.recipeTypeId == recipeTypeId)
                .AsNoTracking ()
                .FirstOrDefaultAsync ();
            if (recipeTypeId != recipeType.recipeTypeId ||
                recipeType == null ||
                currentType == null) {
                return null;
            }
            
            _context.RecipeTypes.Update (recipeType);
            await _context.SaveChangesAsync ();
            return recipeType;
        }
    }
}