using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace recipeservice.Model
{
    public enum PhaseProductType
    {
        scrap,
        finished,
        semi_finished,
        contaminent,
        base_product        
    }
    public class PhaseProduct
    {
        public int phaseProductId { get; set; }
        [Required]
        public int productId { get; set; }
        [Required]
        public double minValue { get; set; }
        [Required]
        public double maxValue { get; set; }
        public string measurementUnit { get; set; }
        [Required]
        [JsonConverter(typeof(StringEnumConverter))]
        public PhaseProductType phaseProductType { get; set; }
        [NotMapped]
        public Product product { get; set; }
    }
}
