using System.Collections.Generic;
using System.Threading.Tasks;
using recipeservice.Model;

namespace recipeservice.Services.Interfaces
{
    public interface IExtraAttributeTypeService
    {
        Task<List<ExtraAttibruteType>> getExtraAttibruteTypes();
        Task<ExtraAttibruteType> getExtraAttibruteType(int extraAttibruteTypeId);
        Task<ExtraAttibruteType> addExtraAttibruteType(ExtraAttibruteType extraAttibruteType);
        Task<ExtraAttibruteType> updateExtraAttibruteType(int extraAttibruteTypeId, ExtraAttibruteType extraAttibruteType);
        Task<bool> deleteExtraAttibruteType(int extraAttibruteTypeId);
    }
}