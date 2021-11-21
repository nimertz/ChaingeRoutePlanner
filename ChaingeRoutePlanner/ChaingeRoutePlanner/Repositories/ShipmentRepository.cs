using System.Collections.Generic;
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
            return GetByIdAsync(id);
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
            return RoutePlanningContext.Shipments.Include(s => s.Delivery).Include(s => s.Pickup).ToListAsync();
        }
    }
}