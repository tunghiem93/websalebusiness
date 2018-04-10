using ProjectWebSaleLand.Shared.Model.Location;
using ProjectWebSaleLand.Shared.Model.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectWebSaleLane.Shared.Model.Product
{
    public class ProductDetailViewModels
    {
        public ProductModels Product { get; set; }
        public List<ProductModels> ListProduct { get; set; }
        public List<LocationModels> ListLocation { get; set; }

        public string KeyWord { get; set; }
    }
}
