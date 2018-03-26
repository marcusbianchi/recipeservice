using System.ComponentModel.DataAnnotations;

namespace recipeservice.Model {
    public class RecipeType {
        public int recipeTypeId { get; set; }
        [Required]
        [MaxLength (50)]
        public string typeDescription { get; set; }
        [Required]
        [MaxLength (50)]
        public string typeScope { get; set; }
    }
}