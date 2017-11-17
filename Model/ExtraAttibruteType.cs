using System.ComponentModel.DataAnnotations;

namespace recipeservice.Model
{
    public class ExtraAttibruteType
    {
        public int extraAttibruteTypeId { get; set; }
        [Required]
        [MaxLength(50)]
        public string extraAttibruteTypeName { get; set; }
    }
}