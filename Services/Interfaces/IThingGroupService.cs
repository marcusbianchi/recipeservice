using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using recipeservice.Model;

namespace recipeservice.Services.Interfaces
{
    public interface IThingGroupService
    {
        Task<(ThingGroup, HttpStatusCode)> getGroup(int groupId);
        Task<(List<ThingGroup>, HttpStatusCode)> getGroups(int startat, int quantity);
        Task<(List<Thing>, HttpStatusCode)> GetAttachedThings(int groupId);
    }
}
