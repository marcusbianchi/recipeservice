namespace recipeservice.Model
{
    public class Tag
    {
        public int tagId { get; set; }
        public string tagName { get; set; }
        public string tagDescription { get; set; }
        public int thingGroupId { get; set; }
        public ThingGroup thingGroup { get; set; }

    }
}