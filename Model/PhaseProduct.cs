using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace recipeservice.Model
{
    public class PhaseProduct
    {
        public int phaseProductId { get; set; }
        [Required]
        public int productId { get; set; }
        [Required]
        [MaxLength(50)]
        public string value { get; set; }
        [Required]
        [MaxLength(50)]
        public string measurementUnit { get; set; }
        [NotMapped]
        public Product product { get; set; }
    }
}