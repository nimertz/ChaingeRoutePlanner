using System.Collections.Generic;

namespace ChaingeRoutePlanner.Models.Requests
{
    public class RoutePlanRequest
    {
        public RoutePlanRequest(List<int> vehicleIds, List<int> shipmentIds)
        {
            this.vehicleIds = vehicleIds;
            this.shipmentIds = shipmentIds;
        }

        public List<int> vehicleIds {get; set;}
        
        public List<int> shipmentIds {get; set;}
    }
}