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
        public string SegmentID { get; set; }
        public List<SelectListItem> ListSeg { get; set; }
        public int TotalProduct { get; set; }
        public ProductViewModels()
        {
            ListProduct = new List<ProductModels>();
            ListCate = new List<SelectListItem>();
            ListArea = new List<SelectListItem>();
            ListSeg = new List<SelectListItem>()
            {
                new SelectListItem(){ Value = Commons.ESegment.Intermediate.ToString("d"), Text = "Trung cấp"},
                new SelectListItem(){ Value = Commons.ESegment.Appellative.ToString("d"), Text = "Phổ thông"},
                new SelectListItem(){ Value = Commons.ESegment.HighUp.ToString("d"), Text = "Cao cấp"},
            };
        }
    }
}
