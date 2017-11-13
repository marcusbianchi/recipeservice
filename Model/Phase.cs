using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace recipeservice.Model
{
    public class Phase
    {
        public int phaseId { get; set; }
        [Required]
        [MaxLength(50)]
        public string phaseName { get; set; }
        [MaxLength(100)]
        public string phaseCode { get; set; }
        public int predecessorPhaseId { get; set; }
        public int[] sucessorPhasesIds { get; set; }
        public ICollection<PhaseProduct> inputProducts { get; set; }
        public ICollection<PhaseProduct> outputProducts { get; set; }
        public ICollection<PhaseParameter> phaseParameters { get; set; }
    }
}