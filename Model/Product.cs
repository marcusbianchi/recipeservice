namespace recipeservice.Model
{
    public class Product
    {
        public int productId { get; set; }
        public string producName { get; set; }
        public string producCode { get; set; }
        public string productGTIN { get; set; }
        public int[] childrenProductsIds { get; set; }

    }
}