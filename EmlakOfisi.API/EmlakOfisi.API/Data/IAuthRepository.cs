using EmlakOfisi.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmlakOfisi.API.Data
{
   public interface IAuthRepository
    {
        Task<Agent> Register(Agent agent, string password);
        Task<Agent> Login(string userName, string password);
        Task<bool> UserExists(string userName);
    }
}
