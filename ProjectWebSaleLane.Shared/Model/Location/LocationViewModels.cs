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
        public List<LocationModels> ListCate { get; set; }
        public LocationViewModels()
        {
            ListCate = new List<LocationModels>();
        }
    }
}
