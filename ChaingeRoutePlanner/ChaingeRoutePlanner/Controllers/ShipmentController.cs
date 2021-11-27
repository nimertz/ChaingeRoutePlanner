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
    public class ShipmentController : ControllerBase
    {
        private readonly ILogger<ShipmentController> _logger;
        private readonly IShipmentRepository _shipmentRepository;

        public ShipmentController(ILogger<ShipmentController> logger, IShipmentRepository shipmentRepository)
        {
            _logger = logger;
            _shipmentRepository = shipmentRepository;
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Shipment>> CreateShipment(ShipmentRequest sr)
        {
            Shipment shipment;
            if (sr.Pickup)
            {
                shipment = new Shipment
                {
                    Pickup = new ShipmentStep
                    {
                        Description = sr.Description,
                        Location = sr.Location,
                        Service = sr.Service,
                        TimeWindows = sr.TimeWindows == null ? null : new List<TimeWindow>(sr.TimeWindows)
                    },
                    Delivery = new ShipmentStep
                    {
                        Description = "Chainge",
                        Location = new Coordinate(12.531511187553406,55.70711874697016)
                    },
                    Amount = new List<int> {sr.Amount},
                };
            }
            else
            {
                shipment = new Shipment
                {
                    Pickup = new ShipmentStep
                    {
                        Description = "Chainge",
                        Location = new Coordinate(12.531511187553406,55.70711874697016)
                    },
                    Delivery = new ShipmentStep
                    {
                        Description = sr.Description,
                        Location = sr.Location,
                        Service = sr.Service,
                        TimeWindows = sr.TimeWindows == null ? null : new List<TimeWindow>(sr.TimeWindows)
                    },
                    Amount = new List<int> {sr.Amount},
                };
            }

            return Ok(await _shipmentRepository.AddShipmentAsync(shipment));
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Shipment>> GetShipmentById(int id)
        {
            var shipment = await _shipmentRepository.GetShipmentByIdAsync(id);
            if (shipment == null)
            {
                return NotFound();
            }

            return Ok(shipment);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Shipment>> UpdateShipment(int id,ShipmentRequest sr)
        {
            var shipment = await _shipmentRepository.GetShipmentByIdAsync(id);
            if (shipment == null)
            {
                return NotFound();
            }
            
            if (sr.Pickup)
            {
                shipment = new Shipment
                {
                    Pickup = new ShipmentStep
                    {
                        Description = sr.Description,
                        Location = sr.Location,
                        Service = sr.Service,
                        TimeWindows = sr.TimeWindows
                    },
                    Delivery = new ShipmentStep
                    {
                        Description = "Chainge",
                        Location = new Coordinate(12.5294459,55.7067838)
                    },
                    Amount = new List<int> {sr.Amount},
                };
            }
            else
            {
                shipment = new Shipment
                {
                    Pickup = new ShipmentStep
                    {
                        Description = "Chainge",
                        Location = new Coordinate(12.5294459,55.7067838)
                    },
                    Delivery = new ShipmentStep
                    {
                        Description = sr.Description,
                        Location = sr.Location,
                        Service = sr.Service,
                        TimeWindows = sr.TimeWindows
                    },
                    Amount = new List<int> {sr.Amount},
                };
            }

            return Ok(await _shipmentRepository.UpdateShipmentAsync(shipment));
        }
        
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteShipment(int id)
        {
            var shipment = await _shipmentRepository.GetShipmentByIdAsync(id);
            if (shipment == null)
            {
                return NotFound();
            }
            
            await _shipmentRepository.DeleteShipmentAsync(shipment);

            return NoContent();
        }
        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ShipmentDTO>>> GetAllShipments()
        {
            var shipments = await _shipmentRepository.GetAllShipmentsAsync();
            List<ShipmentDTO> dtos = new();
            foreach (var shipment in shipments)
            {
                dtos.Add(new ShipmentDTO
                {
                    Id = shipment.Id,
                    Amount = shipment.Amount[0],
                    Description = shipment.Pickup.Description + " -> " + shipment.Delivery.Description
                });
            }
    
            return Ok(dtos);
        }
        
        
        [HttpGet("all/details")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Shipment>>> GetAllShipmentsDetails()
        {
            return Ok(await _shipmentRepository.GetAllShipmentsAsync());
        }
        
    }
}