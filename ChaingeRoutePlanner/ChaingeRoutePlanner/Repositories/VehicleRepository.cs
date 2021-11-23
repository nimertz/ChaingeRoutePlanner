using System.Collections.Generic;
using System.Linq;
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

        public async Task<List<Vehicle>> GetAllVehiclesAsync()
        {
            return GetAllAsync().Result.Value.ToList();
        }

        public async Task<List<Vehicle>> GetVehiclesByIds(IEnumerable<int> vehicleIds)
        {
            return await RoutePlanningContext.Vehicles.Where(v => vehicleIds.Contains(v.Id)).ToListAsync();
        }
    }
}