using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace recipeservice.Model {
    public class Recipe {
        public int recipeId { get; set; }

        [MaxLength (50)]
        public string recipeName { get; set; }

        [MaxLength (100)]
        public string recipeDescription { get; set; }

        [MaxLength (50)]
        public string recipeCode { get; set; }
        public PhaseProduct recipeProduct { get; set; }
        public int[] phasesId { get; set; }

        [Required]
        public int? recipeTypeId { get; set; }

        [NotMapped]
        public string typeDescription { get; set; }

    }
}