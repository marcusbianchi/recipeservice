using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace recipeservice.Model
{
    public class PhaseParameter
    {
        public int phaseParameterId { get; set; }
        [Required]
        public int tagId { get; set; }
        [Required]
        [MaxLength(50)]
        public string setupValue { get; set; }
        [Required]
        [MaxLength(50)]
        public string measurementUnit { get; set; }
        [NotMapped]
        public Tag tag { get; set; }
    }
}