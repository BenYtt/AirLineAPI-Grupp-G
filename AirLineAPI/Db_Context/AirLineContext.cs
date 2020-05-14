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
        public AirLineContext()
        {

        }
        private IConfiguration _configuration;
        public AirLineContext(IConfiguration config, DbContextOptions<AirLineContext> options): base(options)
        {
            this._configuration = config;
        }

        public virtual DbSet<Destination> Destinations { get; set; }
        public virtual DbSet<Flight> Flights { get; set; }
        public virtual DbSet<Passenger> Passengers { get; set; }
        public virtual DbSet<Route> Routes { get; set; }
        public virtual DbSet<TimeTable> TimeTables { get; set; }
        public virtual DbSet<PassengerTimeTable> PassengerTimeTables { get; set; }

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

