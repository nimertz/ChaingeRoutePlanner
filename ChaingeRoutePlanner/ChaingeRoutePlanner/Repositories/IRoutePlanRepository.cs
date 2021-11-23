using System.Collections.Generic;
using System.Threading.Tasks;
using ChaingeRoutePlanner.Models.VROOM.Input;
using ChaingeRoutePlanner.Models.VROOM.Output;
using Microsoft.AspNetCore.Mvc;

namespace ChaingeRoutePlanner.Repositories
{
    public interface IRoutePlanRepository
    {
        Task<VroomOutput> GetVroomOutputByIdAsync(int id);
        
        Task<VroomOutput> AddVroomOutputAsync(VroomOutput vroomOutput);
        
        Task<VroomOutput> UpdateVroomOutputAsync(VroomOutput vroomOutput);
        
        Task DeleteVroomOutputAsync(VroomOutput vroomOutput);
        
        Task<ActionResult<IEnumerable<VroomOutput>>> GetAllVroomOutputsAsync();

        Task<List<VroomOutput>> GetVroomOutputsByIds(IEnumerable<int> vroomOutputIds);
    }
}