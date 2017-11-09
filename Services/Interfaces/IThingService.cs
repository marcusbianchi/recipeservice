using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using recipeservice.Model;

namespace recipeservice.Services.Interfaces
{
    public interface IThingService
    {
        Task<(Thing, HttpStatusCode)> getThing(int thingId);
    }
}