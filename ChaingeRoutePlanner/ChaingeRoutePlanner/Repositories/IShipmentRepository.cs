using System.Collections.Generic;
using System.Threading.Tasks;
using ChaingeRoutePlanner.Models.VROOM.Input;
using Microsoft.AspNetCore.Mvc;

namespace ChaingeRoutePlanner.Repositories
{
    public interface IShipmentRepository
    {
        Task<Shipment> GetShipmentByIdAsync(int id);
        
        Task<Shipment> AddShipmentAsync(Shipment shipment);
        
        Task<Shipment> UpdateShipmentAsync(Shipment shipment);
        
        Task DeleteShipmentAsync(Shipment shipment);
        
        Task<List<Shipment>> GetAllShipmentsAsync();
    }
}