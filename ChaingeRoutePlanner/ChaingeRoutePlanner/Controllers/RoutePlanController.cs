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


        public RoutePlanController(ILogger<RoutePlanController> logger, EnvironmentConfig config, IVehicleRepository vehicleRepository, IShipmentRepository shipmentRepository)
        {
            _logger = logger;
            _vroomApiClient = new VroomApiClient(config);
            _vehicleRepository = vehicleRepository;
            _shipmentRepository = shipmentRepository;
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
        public async Task<VroomOutput> GetRoutePlan(RoutePlanRequest rpq)
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

            return response;
        }
        
        
    }
}
