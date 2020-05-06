using AirLineAPI.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirLineAPI.Db_Context
{
    public class AirLineContext : DbContext
    {
       
            public AirLineContext(DbContextOptions<AirLineContext> options)
                : base(options)
            {
            }

            public DbSet<Destination> Destinations { get; set; }
            public DbSet<Flight> Flights { get; set; }
            public DbSet<Passenger> Passengers { get; set; }
            public DbSet<Route> Routes { get; set; }
            public DbSet<TimeTable> TimeTables { get; set; }

        
    }
}

