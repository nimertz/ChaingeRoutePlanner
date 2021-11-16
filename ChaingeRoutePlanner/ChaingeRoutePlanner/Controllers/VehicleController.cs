using System.Collections.Generic;
using System.Threading.Tasks;
using ChaingeRoutePlanner.Models.Requests;
using ChaingeRoutePlanner.Models.VROOM.Input;
using ChaingeRoutePlanner.Models.VROOM.Output;
using ChaingeRoutePlanner.Repositories;
using ChaingeRoutePlanner.VroomClient;
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
                Capacity = vr.Capacity.HasValue ? new List<int> {(int) vr.Capacity} : new List<int> {180}
            };

            return await _vehicleRepository.AddVehicleAsync(vehicle);
        }
    }
}