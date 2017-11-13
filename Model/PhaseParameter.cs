using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace recipeservice.Model
{
    public class PhaseParameter
    {
        public int phaseParameterId { get; set; }
        [Required]
        public int parameterId { get; set; }
        [Required]
        [MaxLength(50)]
        public string setupValue { get; set; }
        [Required]
        [MaxLength(50)]
        public string measurementUnit { get; set; }
        [MaxLength(50)]
        public string minValue { get; set; }
        [MaxLength(50)]
        public string maxValue { get; set; }
        [NotMapped]
        public Parameter parameter { get; set; }


    }
}