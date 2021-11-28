using System.Collections.Generic;
using System.Threading.Tasks;
using ChaingeRoutePlanner.Models.DTO;
using ChaingeRoutePlanner.Models.Requests;
using ChaingeRoutePlanner.Models.VROOM.Input;
using ChaingeRoutePlanner.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ChaingeRoutePlanner.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VehicleController : ControllerBase
    {
        private readonly ILogger<VehicleController> _logger;
        private readonly IVehicleRepository _vehicleRepository;

        public VehicleController(ILogger<VehicleController> logger, IVehicleRepository vehicleRepository)
        {
            _logger = logger;
            _vehicleRepository = vehicleRepository;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Vehicle>> CreateVehicle(VehicleRequest vr)
        {
            var vehicle = new Vehicle
            {
                Description = vr.Description,
                Capacity = vr.Capacity.HasValue ? new List<int> {(int) vr.Capacity} : new List<int> {180},
                TimeWindow = vr.TimeWindow
            };

            return await _vehicleRepository.AddVehicleAsync(vehicle);
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Vehicle>> GetVehicleById(int id)
        {
            var vehicle = await _vehicleRepository.GetVehicleByIdAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return vehicle;
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Vehicle>> UpdateVehicle(int id,VehicleRequest vr)
        {
            var vehicle = await _vehicleRepository.GetVehicleByIdAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }

            vehicle.Description = vr.Description;
            vehicle.Capacity = vr.Capacity.HasValue ? new List<int> {(int) vr.Capacity} : new List<int> {180};

            return await _vehicleRepository.UpdateVehicleAsync(vehicle);
        }
        
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteVehicle(int id)
        {
            var vehicle = await _vehicleRepository.GetVehicleByIdAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            
            await _vehicleRepository.DeleteVehicleAsync(vehicle);

            return NoContent();
        }
        
        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<VehicleDTO>>> GetAllVehicles()
        {
            var  v = await _vehicleRepository.GetAllVehiclesAsync();
            List<VehicleDTO> dtos = new();
            foreach (var vehicle in v)
            {
                dtos.Add(new VehicleDTO
                {
                    Id = vehicle.Id,
                    Description = vehicle.Description ?? "Chainge Bike",
                    Capacity = vehicle.Capacity != null && vehicle.Capacity.Count > 0 ? vehicle.Capacity[0] : 180
                });
            }

            return Ok(dtos);
        }
        
        [HttpGet("all/details")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Vehicle>>> GetAllVehiclesDetails()
        {
            return Ok(await _vehicleRepository.GetAllVehiclesAsync());
        }
    }
}