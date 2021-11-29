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
        public DbSet<VroomOutput> VroomOutputs { get; set; }

        public RoutePlanningContext(DbContextOptions<RoutePlanningContext> options) : base(options)
        {

        }
    }
}