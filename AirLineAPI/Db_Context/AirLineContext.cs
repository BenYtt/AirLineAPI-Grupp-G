using AirLineAPI.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirLineAPI.Db_Context
{
    public class AirLineContext : DbContext
    {

        private IConfiguration _configuration;
        public AirLineContext(IConfiguration config, DbContextOptions<AirLineContext> options): base(options)
        {
            this._configuration = config;
        }

        public DbSet<Destination> Destinations { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<TimeTable> TimeTables { get; set; }
        public DbSet<PassengerTimeTable> PassengerTimeTables { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PassengerTimeTable>().HasKey(pt => new { pt.PassengerId, pt.TimeTableId });
            modelBuilder.Entity<PassengerTimeTable>().HasOne(pt => pt.Passenger).WithMany(p => p.PassengerTimeTables).HasForeignKey(pt => pt.PassengerId);
            modelBuilder.Entity<PassengerTimeTable>().HasOne(pt => pt.TimeTable).WithMany(t => t.PassengerTimeTables).HasForeignKey(pt => pt.TimeTableId);

            modelBuilder.Entity<TimeTable>().Property(t => t.DepartureTime).HasColumnType("SMALLDATETIME");
            modelBuilder.Entity<TimeTable>().Property(t => t.ArrivalTime).HasColumnType("SMALLDATETIME");

        }
    }
}

