using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace recipeservice.Model
{
    public class Product
    {
        public int productId { get; set; }
        public int[] parentProductsIds { get; set; }
        [Required]
        [MaxLength(50)]
        public string productName { get; set; }
        [MaxLength(100)]
        public string productDescription { get; set; }
        [MaxLength(50)]
        public string productCode { get; set; }
        [MaxLength(50)]
        public string productGTIN { get; set; }
        public int[] childrenProductsIds { get; set; }
        [DefaultValue(true)]
        public bool enabled { get; set; }
        public ICollection<AdditionalInformation> additionalInformation { get; set; }
    }
}