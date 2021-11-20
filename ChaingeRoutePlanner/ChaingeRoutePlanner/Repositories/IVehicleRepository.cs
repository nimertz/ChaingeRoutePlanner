using System.Threading.Tasks;
using ChaingeRoutePlanner.Models.VROOM.Input;
using Microsoft.AspNetCore.Mvc;

namespace ChaingeRoutePlanner.Repositories
{
    public interface IVehicleRepository
    {
        Task<Vehicle> GetVehicleByIdAsync(uint id);
        
        Task<Vehicle> AddVehicleAsync(Vehicle vehicle);
        
        Task<Vehicle> UpdateVehicleAsync(Vehicle vehicle);
        Task DeleteVehicleAsync(Vehicle vehicle);
    }
}