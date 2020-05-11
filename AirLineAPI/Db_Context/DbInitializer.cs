using AirLineAPI.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirLineAPI.Db_Context
{
    public class DbInitializer
    {


        public void Initialize(AirLineContext context)
        {

            context.Database.EnsureCreated();

            if (context.Flights.Any())
            {
                return;
            }

            var destinations = new Destination[]
          {
                new Destination{City = "Stockholm", Country = "Sweden"},
                new Destination{City = "Gothenburg", Country = "Sweden"},
                new Destination{City = "Oslo", Country = "Norway"},
                new Destination{City = "Stavanger", Country = "Norway"},
                new Destination{City = "Helsinki", Country = "Finland"}
          };

            foreach (var d in destinations)
            {
                context.Add(d);
            }
            context.SaveChanges();

            var flights = new Flight[]
            {
                new Flight {Manufacturer = "Boeing", Model = "737"},
                new Flight {Manufacturer = "Boeing", Model = "737 MAX"},
                new Flight {Manufacturer = "Boeing", Model = "T-43"},
                new Flight {Manufacturer = "Cessna", Model = "182"}
            };

            foreach (var f in flights)
            {
                context.Flights.Add(f);
            }
            context.SaveChanges();


            var passengers = new Passenger[]
            {
                new Passenger {Name = "Berra", PersonalNumber = 199002128812},
                new Passenger {Name = "Lars", PersonalNumber = 198810128912},
                new Passenger {Name = "Moa", PersonalNumber = 197011011010},
                new Passenger {Name = "Peter", PersonalNumber = 199005285896},
                new Passenger {Name = "Greta", PersonalNumber = 197110316689},
                new Passenger {Name = "Göran", PersonalNumber = 197405213880},
                new Passenger {Name = "Felix", PersonalNumber = 198602056726},
                new Passenger {Name = "Johanna", PersonalNumber = 197712165666},
                new Passenger {Name = "Gösta", PersonalNumber = 196807170433},
                new Passenger {Name = "Freja", PersonalNumber = 198310226678},
                new Passenger {Name = "Klara", PersonalNumber = 199207295040},
                new Passenger {Name = "Anders", PersonalNumber = 199407081570},
                new Passenger {Name = "Pernilla", PersonalNumber = 196004135080},
                new Passenger {Name = "Klas", PersonalNumber = 199402092317},
                new Passenger {Name = "Hans", PersonalNumber = 198301155761},
                new Passenger {Name = "Nils", PersonalNumber = 196711138567},
                new Passenger {Name = "Lasse", PersonalNumber = 198505291875},
                new Passenger {Name = "Ulla", PersonalNumber = 199403212146},
                new Passenger {Name = "Loe", PersonalNumber = 196310315087},
                new Passenger {Name = "Lowe", PersonalNumber = 198205313938},
                new Passenger {Name = "Stig", PersonalNumber = 199903105287, }
            };

            foreach (var p in passengers)
            {
                context.Add(p);
            }
            context.SaveChanges();

            var routes = new Route[]
           {
                new Route{Name = "GBG-STHLM", TravelTime = new TimeSpan(0,4,0,0), StartDestination = destinations.Single(x => x.City == "Stockholm"), EndDestination = destinations.Single(x => x.City == "Gothenburg")},
                new Route{Name = "STHLM-GBG", TravelTime = new TimeSpan(0,4,0,0), StartDestination = destinations.Single(x => x.City == "Gothenburg"), EndDestination = destinations.Single(x => x.City == "Stockholm")},
                new Route{Name = "OSLO-HEL", TravelTime = new TimeSpan(0,5,0,0), StartDestination = destinations.Single(x => x.City == "Oslo"), EndDestination = destinations.Single(x => x.City == "Helsinki")},
                new Route{Name = "HEL-OSLO", TravelTime = new TimeSpan(0,5,0,0), StartDestination = destinations.Single(x => x.City == "Helsinki"), EndDestination = destinations.Single(x => x.City == "Oslo")},
                new Route{Name = "STHLM-STAV", TravelTime = new TimeSpan(0,7,0,0), StartDestination = destinations.Single(x => x.City == "Stockholm"), EndDestination = destinations.Single(x => x.City == "Stavanger")},
                new Route{Name = "STAV-STHLM", TravelTime = new TimeSpan(0,7,0,0), StartDestination = destinations.Single(x => x.City == "Stavanger"), EndDestination = destinations.Single(x => x.City == "Stockholm")},
                new Route{Name = "HEL-STHLM", TravelTime = new TimeSpan(0,5,0,0), StartDestination = destinations.Single(x => x.City == "Helsinki"), EndDestination = destinations.Single(x => x.City == "Stockholm")},
                new Route{Name = "STAV-HEL", TravelTime = new TimeSpan(0,4,0,0), StartDestination = destinations.Single(x => x.City == "Stavanger"), EndDestination = destinations.Single(x => x.City == "Helsinki")}
           };

            foreach (var r in routes)
            {
                context.Add(r);
            }
            context.SaveChanges();


            var timeTables = new TimeTable[]
            {
                new TimeTable { Route = routes.Single(x => x.Name == "GBG-STHLM"), DepartureTime = new DateTime(2021, 9, 12, 13, 30, 0), Flight = flights.Single(x => x.Model == "737") },
                new TimeTable { Route = routes.Single(x => x.Name == "STHLM-GBG"), DepartureTime = new DateTime(2021, 10, 2, 12, 00, 0), Flight = flights.Single(x => x.Model == "182") },
                new TimeTable { Route = routes.Single(x => x.Name == "OSLO-HEL"), DepartureTime = new DateTime(2021, 6, 20, 15, 15, 0), Flight = flights.Single(x => x.Model == "737") },
                new TimeTable { Route = routes.Single(x => x.Name == "HEL-OSLO"), DepartureTime = new DateTime(2021, 9, 1, 16, 00, 0), Flight = flights.Single(x => x.Model == "737 MAX") },
                new TimeTable { Route = routes.Single(x => x.Name == "STAV-STHLM"), DepartureTime = new DateTime(2021, 11, 9, 16, 00, 0), Flight = flights.Single(x => x.Model == "T-43") },
            };



            foreach (var t in timeTables)
            {
                t.ArrivalTime = t.DepartureTime.Add(routes.Single(x => x.Name == "STHLM-GBG").TravelTime);
                context.Add(t);
            }
            context.SaveChanges();


            var passangerTimeTable = new PassengerTimeTable[]
           {
                 new PassengerTimeTable{Passenger = passengers.Single(x => x.PersonalNumber == 199002128812), TimeTable = timeTables[0]},
                 new PassengerTimeTable{Passenger = passengers.Single(x => x.PersonalNumber == 198810128912), TimeTable = timeTables[1]},
                 new PassengerTimeTable{Passenger = passengers.Single(x => x.PersonalNumber == 197011011010), TimeTable = timeTables[1]},
                 new PassengerTimeTable{Passenger = passengers.Single(x => x.PersonalNumber == 199005285896), TimeTable = timeTables[1]},
                 new PassengerTimeTable{Passenger = passengers.Single(x => x.PersonalNumber == 197110316689), TimeTable = timeTables[2]},
           };

            foreach (var p in passangerTimeTable)
            {
                context.Add(p);
            }
            context.SaveChanges();

        }
    }
}

