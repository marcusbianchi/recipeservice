using System;
using System.Linq;
using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using recipeservice.Data;
using recipeservice.Model;
using recipeservice.Services.Interfaces;

namespace recipeservice.Services
{
    public class RecipePhaseService : IRecipePhaseService
    {
        private readonly IPhaseService _phaseService;
        private readonly IRecipeService _recipeService;
        private readonly ApplicationDbContext _context;

        public RecipePhaseService(ApplicationDbContext context,
        IPhaseService phaseService,
        IRecipeService recipeService)
        {
            _context = context;
            _phaseService = phaseService;
            _recipeService = recipeService;
        }

        public async Task<Phase> addPhaseToRecipe(Phase phase, int recipeId)
        {
            var currentRecipe = await _recipeService.getRecipe(recipeId);
            if (currentRecipe.phasesId == null)
                currentRecipe.phasesId = new int[0];

            var currentphase = await _phaseService.getPhase(phase.phaseId);
            if (currentphase != null)
            {
                await addPhase(currentphase, currentRecipe);
                return phase;
            }

            return null;
        }

        public async Task<List<Phase>> getPhasesFromRecipe(int recipeId)
        {
            List<Phase> returnPhases = new List<Phase>();
            var currentRecipe = await _recipeService.getRecipe(recipeId);

            if (currentRecipe != null && currentRecipe.phasesId != null)
            {
                foreach (var item in currentRecipe.phasesId)
                {
                    returnPhases.Add(await _phaseService.getPhase(item));
                }
                return returnPhases;
            }
            return null;
        }
        public async Task<Recipe> removePhaseFromRecipe(int phaseId, int recipeId)
        {
            var currentRecipe = await _recipeService.getRecipe(recipeId);

            if (currentRecipe != null && currentRecipe.phasesId != null)
            {
                if (currentRecipe.phasesId.Contains(phaseId))
                    currentRecipe.phasesId = currentRecipe.phasesId.Where(val => val != phaseId).ToArray();
                await _context.SaveChangesAsync();
                return currentRecipe;
            }
            return null;
        }

        private async Task<Phase> addPhase(Phase phase, Recipe currentRecipe)
        {
            if (!currentRecipe.phasesId.Contains(phase.phaseId))
                currentRecipe.phasesId = currentRecipe.phasesId.Append(phase.phaseId).ToArray();
            await _context.SaveChangesAsync();
            return phase;
        }

    }
}