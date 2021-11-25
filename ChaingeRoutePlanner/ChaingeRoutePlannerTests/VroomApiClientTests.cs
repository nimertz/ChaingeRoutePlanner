using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChaingeRoutePlanner;
using ChaingeRoutePlanner.Models.VROOM.Input;
using ChaingeRoutePlanner.VroomClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChaingeRoutePlannerTests
{
    [TestClass]
    public class VroomApiClientTests
    {
        // uncomment to test implementation (needs VROOM instance)
        [TestMethod]
        public async Task TestJobs()
        {
            EnvironmentConfig ec = new EnvironmentConfig
            {
                ApiEndpoint = "https://api.openrouteservice.org/optimization",
                ApiKey = "5b3ce3597851110001cf624890b9bd86eea74d508e1e2d272ce4c7bd"
            };
            VroomApiClient apiClient = new VroomApiClient(ec);
            Assert.IsTrue(apiClient.IsHealthy().Result);
            int id = 0;
            
            var response = await apiClient.PerformRequest(new VroomInput
            {
                Jobs = new List<Job>()
                {
                    new Job()
                    {
                        Id = id++,
                        Location = new Coordinate(151.7735849, -32.9337431)
                    },
                    new Job()
                    {
                        Id = id++,
                        Location = new Coordinate(151.7617514, -32.9351314)
                    },
                    new Job()
                    {
                        Id = id++,
                        Location = new Coordinate(151.7105484, -32.9338793)
                    }
                },
                Vehicles = new List<Vehicle>()
                {
                    new Vehicle()
                    {
                        Id = id++,
                        Start = new Coordinate(151.7005484, -32.9331793),
                        End = new Coordinate(151.7105484, -32.9338793)
                    }
                }
            });
            Assert.IsTrue(response.Code == 0);
        }
        
        [TestMethod]
        public async Task TestShipment()
        {
            EnvironmentConfig ec = new EnvironmentConfig
            {
                ApiEndpoint = "https://api.openrouteservice.org/optimization",
                ApiKey = "5b3ce3597851110001cf624890b9bd86eea74d508e1e2d272ce4c7bd"
            };
            VroomApiClient apiClient = new VroomApiClient(ec);
            Assert.IsTrue(apiClient.IsHealthy().Result);
            int id = 0;

            VroomInput vi = new VroomInput
            {
                Shipments = new List<Shipment>()
                {
                    new()
                    {
                        Id = id++,
                        Amount = new List<int> {20},
                        Delivery = new ShipmentStep
                        {
                            Id = id++,
                            Description = "delivery",
                            Location = new Coordinate(12.584928742097654, 55.63329421574227),
                        },
                        Pickup = new ShipmentStep
                        {
                            Id = id++,
                            Description = "pickup at Chainge",
                            Location = new Coordinate(12.531645327470956, 55.70688354420953)
                        }
                    }
                },
                Vehicles = new List<Vehicle>()
                {
                    new Vehicle()
                    {
                        Id = id++,
                        Start = new Coordinate(12.531613140962866, 55.70687145438041),
                        End = new Coordinate(12.531613140962866, 55.70687145438041)
                    }
                },
            };
            var response = await apiClient.PerformRequest(vi);
            
            Assert.IsTrue(response.Code == 0);
        }
    }
}