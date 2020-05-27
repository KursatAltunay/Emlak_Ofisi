using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmlakOfisi.API.Models
{
    public class Advert
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Price { get; set; }
        public string Location { get; set; }       
        public string AdvertType { get; set; }
        public int AgentId { get; set; }
        public string? Heating { get; set; }
        public string? NumberOfRooms { get; set; }
        public int? TotalFloor { get; set; }
        public int? PlaceFloor { get; set; }
        public bool? IsInHousingEstate { get; set; }
        public bool? IsFurnished { get; set; }
        public bool? IsInUse { get; set; }
    }
}
