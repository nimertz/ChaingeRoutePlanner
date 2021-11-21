using System.Collections.Generic;
using System.Threading.Tasks;
using ChaingeRoutePlanner.Models.Contexts;
using ChaingeRoutePlanner.Models.VROOM.Input;
using Microsoft.AspNetCore.Mvc;

namespace ChaingeRoutePlanner.Repositories
{
    public class VehicleRepository : Repository<Vehicle>, IVehicleRepository
    {
        public VehicleRepository(RoutePlanningContext routePlanningContext) : base(routePlanningContext)
        {
        }

        public Task<Vehicle> GetVehicleByIdAsync(int id)
        {
            return GetByIdAsync(id);
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

        public Task<ActionResult<IEnumerable<Vehicle>>> GetAllVehiclesAsync()
        {
            return GetAllAsync();
        }
    }
}