using EmlakOfisi.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmlakOfisi.API.Data
{
    public class AppRepository : IAppRepository
    {
        private DataContext _context;

        public AppRepository(DataContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public Advert GetAdvertById(int advertId) //emlakçının ilanını düzenlemesi için kullanılacak agentscontroller
        {
            var advert = _context.Adverts.FirstOrDefault(x => x.Id == advertId);
            return advert;

        }

        public List<Advert> GetAdvertsByAgent(int agentId) //Emlakçının ilanlarını listelemesi için kullanılacak agentscontroller
        {
            var adverts = _context.Adverts.Where(x=>x.AgentId==agentId).ToList();
            return adverts;
        }

        public Agent GetAgentById(int agentId)
        {
            var agent = _context.Agents.FirstOrDefault(x => x.Id == agentId);
            return agent;
        }

        public List<Agent> GetAgents() //admin sayfası için kullanılacak adminController
        {
            var agents = _context.Agents.ToList();
            return agents;
        }

        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }

        public void UpdateAdvert(int id, Advert advert) //Emlakçının ilanını değiştirmesi için kullanılacak agentcontroller
        {
            Advert oldAdvert = _context.Adverts.FirstOrDefault(x => x.Id == id);
            oldAdvert.Title = advert.Title;
            oldAdvert.Price = advert.Price;
            oldAdvert.Location = advert.Location;
            oldAdvert.NumberOfRooms = advert.NumberOfRooms;
            oldAdvert.TotalFloor = advert.TotalFloor;
            oldAdvert.PlaceFloor = advert.PlaceFloor;
            oldAdvert.IsInUse = advert.IsInUse;
            oldAdvert.IsInHousingEstate = advert.IsInHousingEstate;
            oldAdvert.IsFurnished = advert.IsFurnished;
            oldAdvert.Heating = advert.Heating;
            oldAdvert.AdvertType = advert.AdvertType;
            
            
        }

        //public void UpdateAgent(int id, Agent agent) //emlakçının bilgilerini değiştirmesi için kullanılacak agentcontroller
        //{
        //    Agent oldAgent = _context.Agents.FirstOrDefault(x => x.Id == id);
        //    oldAgent.Username = agent.Username;
        //    oldAgent.CompanyName = agent.CompanyName;
        //    oldAgent.Password = agent.Password;
        //}
    }
}
