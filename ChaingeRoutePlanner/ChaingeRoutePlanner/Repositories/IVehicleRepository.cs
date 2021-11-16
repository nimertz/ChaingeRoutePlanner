using System.Threading.Tasks;
using ChaingeRoutePlanner.Models.VROOM.Input;

namespace ChaingeRoutePlanner.Repositories
{
    public interface IVehicleRepository
    {
        Task<Vehicle> GetVehicleByIdAsync(uint id);
        
        Task<Vehicle> AddVehicleAsync(Vehicle vehicle);
    }
}