using System.Threading.Tasks;
using ChaingeRoutePlanner.Models.Contexts;
using ChaingeRoutePlanner.Models.VROOM.Input;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChaingeRoutePlanner.Repositories
{
    public class VehicleRepository : Repository<Vehicle>, IVehicleRepository
    {
        public VehicleRepository(RoutePlanningContext routePlanningContext) : base(routePlanningContext)
        {
        }

        public Task<Vehicle> GetVehicleByIdAsync(uint id)
        {
            return GetAll().FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<Vehicle> AddVehicleAsync(Vehicle vehicle)
        {
            return AddAsync(vehicle);
        }

        public Task<Vehicle> UpdateVehicleAsync(Vehicle vehicle)
        {
            return UpdateAsync(vehicle);
        }

        public Task DeleteVehicleAsync(Vehicle vehicle)
        {
            return DeleteAsync(vehicle);
        }
    }
}