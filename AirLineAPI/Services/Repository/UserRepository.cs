using AirLineAPI.Db_Context;
using AirLineAPI.Model;
using AirLineAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AirLineAPI.Services
{
    public class UserRepository : IUserRepository
    //:
    {
        private readonly AirLineContext _context;

        public UserRepository(AirLineContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<string>> Login(string username, string password)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            var user = await _context.Users.Where(x => x.Name == username).FirstOrDefaultAsync();
            if (user == null)
            {
                response.Success = false;
                response.Message = "User not found";
            }
            else if (!await VerifyPassword(password))
            {
                response.Success = false;
                response.Message = "Password is wrong!";
            }

            return response;
        }

        private async Task<Boolean> VerifyPassword(string password)
        {
            var passwordValidation = await _context.Users.Where(n => n.ApiKey == password).FirstOrDefaultAsync();

            if (passwordValidation == null)
            {
                return false;
            }

            return true;
        }
    }
}