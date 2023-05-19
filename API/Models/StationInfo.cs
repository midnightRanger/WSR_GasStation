using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSR_GasStation.Domain.Models
{
    public class StationInfo
    {
        public int ID_StationInfo { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int AmountOfFuel { get; set; }
        
        public int StationId { get; set; }
        public Station Station { get; set; }
    }
}
