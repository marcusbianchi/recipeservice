using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using recipeservice.Model;

namespace recipeservice.Services.Interfaces
{
    public interface IParametersService
    {
        Task<(Parameter, HttpStatusCode)> getParameter(int thingId);
        Task<(List<Parameter>, HttpStatusCode)> getParameterList(int[] thingId);
        Task<(List<Parameter>, HttpStatusCode)> getParameters(int startat, int quantity);


    }
}