using AirLineAPI.Db_Context;
using AirLineAPI.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirLineAPI.Services
{
    class RouteRepo : IRouteRepository
    {
        private AirLineContext db;
        public RouteRepo(AirLineContext contex)
        {
            this.db = contex;
        }
        public async Task<RouteView> GetRouteById(int id)
        {
            if (db!=null)
            {
                return await (from r in db.Routes
                             where r.ID == id
                             select new RouteView
                             {
                                 ID = r.ID,
                                 Name = r.Name,
                                 StartDestination = r.StartDestination,
                                 EndDestination = r.EndDestination,
                                 TravelTime = r.TravelTime,
                             }).FirstOrDefaultAsync();
            }
            return null;
        }
    }
}
