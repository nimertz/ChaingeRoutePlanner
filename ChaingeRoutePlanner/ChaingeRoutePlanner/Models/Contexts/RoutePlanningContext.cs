using System;
using System.Collections.Generic;
using ChaingeRoutePlanner.Models.VROOM.Input;
using ChaingeRoutePlanner.Models.VROOM.Output;
using Microsoft.EntityFrameworkCore;

namespace ChaingeRoutePlanner.Models.Contexts
{
    public class RoutePlanningContext : DbContext
    {
        public DbSet<Route> Routes { get; set; }
        public DbSet<Break> Breaks { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<ShipmentStep> ShipmentSteps { get; set; }
        
        public RoutePlanningContext(DbContextOptions<RoutePlanningContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            for (uint i = 1; i <= 36; i++)
            {
                modelBuilder.Entity<Vehicle>().HasData(new Vehicle
                {
                    Id = i,
                    Description = i + " Electrical Bike",
                });
            }

            var sh1Pick = new ShipmentStep
            {
                Id = 1,
                Description = "Chainge",
                Location = new Coordinate(55.7067808,12.5305402),
                TimeWindows = new List<TimeWindow>
                    {new(new DateTime(2021, 6, 19, 7, 0, 0), new DateTime(2021, 6, 19, 9, 0, 0))}
            };
            
            var sh1Del = new ShipmentStep
            {
                Id = 2,
                Description = "Kaffe Bueno",
                Location = new Coordinate(55.741485,12.4774352),
                TimeWindows = new List<TimeWindow>
                    {new(new DateTime(2021, 6, 19, 7, 0, 0), new DateTime(2021, 6, 19, 9, 0, 0))}
            };

            var ship = new Shipment
            {
                Id = 1,
                PickupId = sh1Pick.Id,
                DeliveryId = sh1Del.Id,
                Amount = new List<int> {5},
            };

            //modelBuilder.Entity<ShipmentStep>().OwnsOne(ss => ss.Location).HasData(sh1Pick,sh1Del);
            /*modelBuilder.Entity<Shipment>()
                .HasData(new Shipment
                {
                    Id = 1,
                    PickupId = sh1Pick.Id,
                    DeliveryId = sh1Del.Id,
                    Amount = new List<int>{5},
                });*/

        }
        
    
    }
}