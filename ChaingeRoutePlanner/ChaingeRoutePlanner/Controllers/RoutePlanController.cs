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
    public class RoutePlanController : ControllerBase
    {
        private readonly ILogger<RoutePlanController> _logger;
        private IVroomApiClient _vroomApiClient;
        private IVehicleRepository _vehicleRepository;
        private IShipmentRepository _shipmentRepository;
        private IRoutePlanRepository _routePlanRepository;


        public RoutePlanController(ILogger<RoutePlanController> logger, EnvironmentConfig config, IVehicleRepository vehicleRepository, IShipmentRepository shipmentRepository,IRoutePlanRepository routePlanRepository)
        {
            _logger = logger;
            _vroomApiClient = new VroomApiClient(config);
            _vehicleRepository = vehicleRepository;
            _shipmentRepository = shipmentRepository;
            _routePlanRepository = routePlanRepository;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("[action]")]
        public async Task<VroomOutput> PostVroomInput(VroomInput vi)
        {
            var response = await _vroomApiClient.PerformRequest(vi);

            return response;
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<VroomOutput>> GetRoutePlan(RoutePlanRequest rpq)
        {
            List<Vehicle> vehicles = await _vehicleRepository.GetVehiclesByIds(rpq.vehicleIds);
            List<Shipment> shipments = await _shipmentRepository.GetShipmentsByIds(rpq.shipmentIds);
            VroomInput vi = new()
            {
                Jobs = new List<Job>(),
                Shipments = shipments,
                Vehicles = vehicles
            };
            
            //TODO figure out how to avoid having to do this hacky solution in regards to time_windows being empty instead of null
            foreach (var shipment in shipments)
            {
                if(shipment.Pickup.TimeWindows != null && shipment.Pickup.TimeWindows.Count == 0)
                {
                    shipment.Pickup.TimeWindows = null;
                }
                
                if(shipment.Delivery.TimeWindows != null && shipment.Delivery.TimeWindows.Count == 0)
                {
                    shipment.Delivery.TimeWindows = null;
                }
            }

            var response = await _vroomApiClient.PerformRequest(vi);

            if (response.Code == 0)
            {
                await _routePlanRepository.AddVroomOutputAsync(response);
            }
            else
            {
                if(response.Error != null)
                    _logger.LogError($"Error code {response.Error} returned from Vroom API");
                else
                    _logger.LogInformation($"Error code {response.Code} returned from Vroom API");
            }

            return Ok(response);
        }
        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VroomOutput>> GetRoutePlanById(int id)
        {
            var response = await _routePlanRepository.GetVroomOutputByIdAsync(id);

            if (response == null)
            {
                return NotFound("No route plan found with id :" + id);
            }

            return response;
        }
        
        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult<List<VroomOutput>>> GetAllRoutePlans()
        {
            var response = await _routePlanRepository.GetAllVroomOutputsAsync();

            if (response == null)
            {
                return NoContent();
            }

            return Ok(response);
        }
    }
}
