using AirLineAPI.Db_Context;
using AirLineAPI.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirLineAPI.Repository
{
    public class DestinationRepo: IDestinationRepository
    {
        
            private readonly AirLineContext db;
            public DestinationRepo(AirLineContext context)
            {
                this.db = context;
            }

            public async Task<DestinationView> GetDestinationById(int id)
            {
                if (db != null)
                {
                    return await (from d in db.Destinations
                                  from f in db.Flights
                                  from t in db.TimeTables
                                  where d.ID == id
                                  select new DestinationView
                                  {
                                      ID = d.ID,
                                      FlightName = f.Model,
                                      Country = d.Country,
                                      City = d.City,
                                      DepartureTime = t.DepartureTime,
                                      ArrivalTime = t.ArrivalTime
                                  }).FirstOrDefaultAsync();
                }

                return null;
            }

        }
}
