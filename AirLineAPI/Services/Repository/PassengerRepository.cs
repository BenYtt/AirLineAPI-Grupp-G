﻿using AirLineAPI.Db_Context;
using AirLineAPI.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace AirLineAPI.Services
{
    public class PassengerRepository : Repository, IPassengerRepository
    {
        public PassengerRepository(AirLineContext context, ILogger<Repository> logger) : base(context, logger)
        {
        }

        public async Task<Passenger[]> GetPassengers(bool includeTimeTable = false)
        {
            _logger.LogInformation($"Getting passengers");
            IQueryable<Passenger> query = _context.Passengers;
            if (includeTimeTable)
            {
                query = query.Include(x => x.PassengerTimeTables);
            }
            return await query.ToArrayAsync();
        }

        public async Task<Passenger> GetPassengerById(int id, bool includeTimeTable = false)
        {
            _logger.LogInformation($"Getting passenger by id {id}");
            IQueryable<Passenger> query = _context.Passengers;
            if (includeTimeTable)
            {
                query = query.Include(t => t.PassengerTimeTables);
            }
            query = query.Where(x => x.Id == id);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<Passenger> GetPassengerByName(string name)
        {
            _logger.LogInformation($"Getting passenger by name {name}");
            IQueryable<Passenger> query = _context.Passengers;
            query = query.Where(x => x.Name == name);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<Passenger> GetPassengerByIdentificationNumber(long identitificationNm)
        {
            _logger.LogInformation($"Getting passenger by identification number {identitificationNm}");
            IQueryable<Passenger> query = _context.Passengers.Where(a => a.IdentificationNumber == identitificationNm);
            return await query.FirstOrDefaultAsync();
        }
    }
}