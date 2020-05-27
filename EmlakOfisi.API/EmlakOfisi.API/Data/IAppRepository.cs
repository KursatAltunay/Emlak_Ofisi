using EmlakOfisi.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmlakOfisi.API.Data
{
    public interface IAppRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        void UpdateAdvert(int id, Advert advert);
        //void UpdateAgent(int id, Agent agent) ;
        bool SaveAll();
        List<Advert> GetAdvertsByAgent(int agentId);
        Advert GetAdvertById(int advertId);

        List<Agent> GetAgents();
        Agent GetAgentById(int agentId);
    }
}
