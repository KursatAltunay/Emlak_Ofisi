using EmlakOfisi.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmlakOfisi.API.Data
{
    public class AuthRepository : IAuthRepository
    {
        private DataContext _context;
        public AuthRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<Agent> Login(string userName, string password)
        {
            var agent = await _context.Agents.FirstOrDefaultAsync(x => x.Username == userName);
            if (agent == null)
            {
                return null;
            }

            if (!VerifyPasswordHash(password, agent.PasswordHash, agent.PasswordSalt))
            {
                return null;
            }
            return agent;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i])
                    {
                        return false;
                    }
                }

                return true;

            }
        }

        public async Task<Agent> Register(Agent agent, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            agent.PasswordHash = passwordHash;
            agent.PasswordSalt = passwordSalt;

            await _context.Agents.AddAsync(agent);
            await _context.SaveChangesAsync();

            return agent;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            }
        }

        public async Task<bool> UserExists(string userName)
        {
            if (await _context.Agents.AnyAsync(x => x.Username == userName))
            {
                return true;
            }

            return false;
        }
    }
}
