using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSR_GasStation.Domain.Models;
using WSR_GasStation.Models;
using Microsoft.EntityFrameworkCore;
using API.Models;

namespace API.Controllers
{
   
    public class StationController : Controller
    {

        StationDbContext _db;

        public StationController(StationDbContext db)
        {
            this._db = db;
        }

        //stations/fuel?name 
        [Route("stations/")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetStationViewModel>>> Get(string fuel) 
        {
            List<GetStationViewModel> model = new List<GetStationViewModel>();   

            List<Station> needStations = new List<Station>();
            List<Station> stations = await _db.Station.Include(s=>s.StationInfos).ToListAsync();

            foreach (var station in stations) {
                foreach (var stationInfos in station.StationInfos) {
                    if (stationInfos.Name == fuel)
                        needStations.Add(station);
                    model.Add(new GetStationViewModel() { Address = station.Address, Cost = stationInfos.Price, Id = station.ID_Station });
                }
            }

            return model; 
        }

        [Route("setStation/")]
        [HttpPost]
        public async Task<ActionResult<String>> Post(int id, string address, List<StationInfo> gases) {

            Station station = new Station() { Address = address, StationInfos = gases };

            await _db.Station.AddAsync(station);
            await _db.SaveChangesAsync();

            return Ok(station); 
        }
    }
}
