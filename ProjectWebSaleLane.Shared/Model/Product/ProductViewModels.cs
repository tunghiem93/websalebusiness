using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ProjectWebSaleLand.Shared.Model.Product
{
    public class ProductViewModels
    {
        public List<ProductModels> ListProduct { get; set; }
        public string CateID { get; set; }
        public List<SelectListItem> ListCate { get; set; }
        public string AreaID { get; set; }
        public List<SelectListItem> ListArea { get; set; }

        public ProductViewModels()
        {
            
        }
    }
}
