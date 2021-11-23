using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChaingeRoutePlanner.Models.Contexts;
using ChaingeRoutePlanner.Models.VROOM.Output;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChaingeRoutePlanner.Repositories
{
    public class RoutePlanRepository : Repository<VroomOutput>, IRoutePlanRepository
    {
        public RoutePlanRepository(RoutePlanningContext routePlanningContext) : base(routePlanningContext)
        {
        }

        public Task<VroomOutput> GetVroomOutputByIdAsync(int id)
        {
            return GetByIdAsync(id);
        }

        public Task<VroomOutput> AddVroomOutputAsync(VroomOutput vroomOutput)
        {
            return AddAsync(vroomOutput);
        }

        public Task<VroomOutput> UpdateVroomOutputAsync(VroomOutput vroomOutput)
        {
            return UpdateAsync(vroomOutput);
        }

        public Task DeleteVroomOutputAsync(VroomOutput vroomOutput)
        {
            return DeleteAsync(vroomOutput);
        }

        public Task<ActionResult<IEnumerable<VroomOutput>>> GetAllVroomOutputsAsync()
        {
            return GetAllAsync();
        }

        public async Task<List<VroomOutput>> GetVroomOutputsByIds(IEnumerable<int> vroomOutputIds)
        {
            return await RoutePlanningContext.VroomOutputs.Where(v => vroomOutputIds.Contains(v.Id)).ToListAsync();
        }
    }
}