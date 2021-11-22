using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChaingeRoutePlanner.Models.Contexts;
using ChaingeRoutePlanner.Models.VROOM.Input;
using Microsoft.EntityFrameworkCore;

namespace ChaingeRoutePlanner.Repositories
{
    public class ShipmentRepository : Repository<Shipment>, IShipmentRepository
    {
        public ShipmentRepository(RoutePlanningContext routePlanningContext) : base(routePlanningContext)
        {
        }
        
        public Task<Shipment> GetShipmentByIdAsync(int id)
        {
            return RoutePlanningContext.Shipments.Include(s => s.Delivery).Include(s => s.Pickup).AsSingleQuery().FirstOrDefaultAsync(s => s.Id == id);
        }

        public Task<Shipment> AddShipmentAsync(Shipment shipment)
        {
            return AddAsync(shipment);
        }

        public Task<Shipment> UpdateShipmentAsync(Shipment shipment)
        {
            return UpdateAsync(shipment);
        }

        public Task DeleteShipmentAsync(Shipment shipment)
        {
            return DeleteAsync(shipment);
        }

        public Task<List<Shipment>> GetAllShipmentsAsync()
        {
            return RoutePlanningContext.Shipments.Include(s => s.Delivery).Include(s => s.Pickup).AsSingleQuery().ToListAsync();
        }

        public Task<List<Shipment>> GetShipmentsByIds(IEnumerable<int> shipmentIds)
        {
            return RoutePlanningContext.Shipments.Include(s => s.Delivery).Include(s => s.Pickup).AsSingleQuery().Where(s => shipmentIds.Contains(s.Id)).ToListAsync();
        }
    }
}