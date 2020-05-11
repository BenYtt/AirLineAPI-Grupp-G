using AirLineAPI.Db_Context;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirLineAPI.Services
{
    public class Repository : IRepository
    {
        protected readonly AirLineContext _context;
        protected readonly ILogger<Repository> _logger;


        public Repository(AirLineContext context, ILogger<Repository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void Add<T>(T entity) where T : class
        {
            _logger.LogInformation($"{entity.GetType() } Added");
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _logger.LogInformation($"{entity.GetType() } Deleted");
            _context.Remove(entity);
        }

        public async Task<bool> Save()
        {
            _logger.LogInformation($"Saved");
            return (await _context.SaveChangesAsync()) >= 0;
        }

        public void Update<T>(T entity) where T : class
        {
            _logger.LogInformation($"{entity.GetType() } Updated");
            _context.Update(entity);
        }
    }
}
