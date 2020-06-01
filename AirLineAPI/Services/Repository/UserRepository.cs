using AirLineAPI.Db_Context;
using AirLineAPI.Model;
using AirLineAPI.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace AirLineAPI.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly AirLineContext _context;
        private readonly IConfiguration _configuration;

        public UserRepository(AirLineContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<ServiceResponse<string>> Login(string name, string apiKey)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            User user = await _context.Users.FirstOrDefaultAsync(x => x.Name.ToLower().Equals(name.ToLower()));
            if (user == null)
            {
                response.Success = false;
                response.Message = "User not found";
            }
            else if (!VerifyPassword(name, apiKey))
            {
                response.Success = false;
                response.Message = "Password is wrong!";
            }

            return response;
        }
    }
}