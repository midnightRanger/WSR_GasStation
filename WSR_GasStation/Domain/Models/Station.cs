using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSR_GasStation.Domain.Models
{
    public class Station
    {
        public int ID_Station { get; set; }
        public string Address { get; set; }

        public List<StationInfo> StationInfos { get; set; } = new();
    }
}
