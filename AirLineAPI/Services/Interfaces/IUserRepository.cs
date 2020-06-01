using AirLineAPI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirLineAPI.Services.Interfaces
{
    public interface IUserRepository
    {
        Task<ServiceResponse<string>> Login(string username, string password);
    }
}