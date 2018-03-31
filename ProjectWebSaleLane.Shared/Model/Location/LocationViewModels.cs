using ProjectWebSaleLand.Shared.Model.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectWebSaleLane.Shared.Model.Location
{
    public class LocationViewModels
    {
        public List<LocationModels> ListLocation { get; set; }
        public LocationViewModels()
        {
            ListLocation = new List<LocationModels>();
        }
    }
}
