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
    }
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
        public string tolerance { get; set; }
        public string measurementUnit { get; set; }
        [Required]
        [JsonConverter(typeof(StringEnumConverter))]
        public PhaseProductType phaseProductType { get; set; }
        [NotMapped]
        public Product product { get; set; }
    }
}