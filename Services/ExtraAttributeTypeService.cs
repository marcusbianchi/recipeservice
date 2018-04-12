using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using recipeservice.Data;
using recipeservice.Model;
using recipeservice.Services.Interfaces;


namespace recipeservice.Services
{
    public class ExtraAttributeTypeService : IExtraAttributeTypeService
    {
        private IConfiguration _configuration;
        private readonly ApplicationDbContext _context;
        public ExtraAttributeTypeService(IConfiguration configuration, ApplicationDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }
        public async Task<ExtraAttibruteType> addExtraAttibruteType(ExtraAttibruteType extraAttibruteType)
        {
            extraAttibruteType.extraAttibruteTypeId = 0;
            await _context.AddAsync(extraAttibruteType);
            await _context.SaveChangesAsync();
            return extraAttibruteType;
        }

        public async Task<bool> deleteExtraAttibruteType(int extraAttibruteTypeId)
        {
            var extraAttibruteType = await _context.ExtraAttibruteTypes
                                   .Where(x => x.extraAttibruteTypeId == extraAttibruteTypeId)
                                   .FirstOrDefaultAsync();

            if (extraAttibruteType != null)
            {
                _context.Entry(extraAttibruteType).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<ExtraAttibruteType> getExtraAttibruteType(int extraAttibruteTypeId)
        {
            var extraAttibruteType = await _context.ExtraAttibruteTypes
                                             .Where(x => x.extraAttibruteTypeId == extraAttibruteTypeId)
                                             .FirstOrDefaultAsync();
            if (extraAttibruteType != null)
            {
                return extraAttibruteType;
            }
            return null;
        }

        public async Task<List<ExtraAttibruteType>> getExtraAttibruteTypes()
        {
            return await _context.ExtraAttibruteTypes.ToListAsync();
        }

        public async Task<ExtraAttibruteType> updateExtraAttibruteType(int extraAttibruteTypeId, ExtraAttibruteType extraAttibruteType)
        {
            var curExtraAttibruteType = await _context.ExtraAttibruteTypes
                                                        .Where(x => x.extraAttibruteTypeId == extraAttibruteTypeId)
                                                        .FirstOrDefaultAsync();
            if (extraAttibruteType != null)
            {
                curExtraAttibruteType.extraAttibruteTypeName = extraAttibruteType.extraAttibruteTypeName;
                _context.ExtraAttibruteTypes.Update(curExtraAttibruteType);
                _context.Entry(curExtraAttibruteType).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return extraAttibruteType;
            }
            return null;
        }
    }
}