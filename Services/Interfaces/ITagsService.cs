using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using recipeservice.Model;

namespace recipeservice.Services.Interfaces
{
    public interface ITagsService
    {
        Task<(Tag, HttpStatusCode)> getParameter(int thingId);
        Task<(List<Tag>, HttpStatusCode)> getParameterList(int[] thingId);
        Task<(List<Tag>, HttpStatusCode)> getParameters(int startat, int quantity);


    }
}