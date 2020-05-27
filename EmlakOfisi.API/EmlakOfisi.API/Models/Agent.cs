using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmlakOfisi.API.Models
{
    public class Agent
    {
        public Agent()
        {
            Adverts = new List<Advert>();
        }
        public int Id { get; set; }
        public string Username { get; set; }
        public string CompanyName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }

        public List<Advert> Adverts { get; set; }
    }
}
