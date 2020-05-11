using AirLineAPI.Db_Context;
using AirLineAPI.Dto;
using AirLineAPI.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace AirLineAPI.Services
{
    public class TimeTableRepo: ITimeTableRepository
    {
        private readonly AirLineContext db;
        public TimeTableRepo(AirLineContext context)
        {
            this.db = context;
        }

        public async Task<TimeTableView> GetTimeTableById(int id)
        {
            if (db != null)
            {
                return await (from p in db.TimeTables
                              from r in db.Routes
                              from f in db.Flights
                              where p.ID == id
                              select new TimeTableView
                              {
                                 ID= p.ID,
                                 FlightName= f.Manufacturer,
                                 RouteName= r.Name,
                                 DepartureTime=p.DepartureTime,
                                 ArrivalTime=p.ArrivalTime
                              }).FirstOrDefaultAsync();
            }

            return null;
        }
    }
}
