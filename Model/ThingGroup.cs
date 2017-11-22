using System.Collections.Generic;

namespace recipeservice.Model
{
    public class ThingGroup
    {
        public int thingGroupId { get; set; }
        public string groupName { get; set; }
        public string groupCode { get; set; }
        public ICollection<Tag> tags { get; set; }
    }
}