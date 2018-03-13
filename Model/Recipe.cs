using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace recipeservice.Model
{
    public class Recipe
    {
        public int recipeId { get; set; }
        [MaxLength(50)]
        public string recipeName { get; set; }
        [MaxLength(100)]
        public string recipeDescription { get; set; }
        [MaxLength(50)]
        public string recipeCode { get; set; }
        public PhaseProduct recipeProduct { get; set; }
        public int[] phasesId { get; set; }
    }
}
