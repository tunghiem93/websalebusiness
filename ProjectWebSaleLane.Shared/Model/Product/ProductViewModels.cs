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
        public List<SelectListItem> ListCate { get; set; }
        public string CateID { get; set; }
        public List<SelectListItem> ListArea { get; set; }
        public string AreaID { get; set; }

        public ProductViewModels()
        {
            ListProduct = new List<ProductModels>();
            ListCate = new List<SelectListItem>()
            {
                new SelectListItem(){
                    Value = Commons.ECateGgory.Land.ToString("d"),
                    Text = Commons.ECateGgory.Land.ToString()
                },
                new SelectListItem(){
                    Value = Commons.ECateGgory.House.ToString("d"),
                    Text = Commons.ECateGgory.House.ToString()
                }
            };
            ListArea = new List<SelectListItem>()
            {
                new SelectListItem(){
                    Value = Commons.EArea.AnPhuDong.ToString("d"),
                    Text = Commons.EArea.AnPhuDong.ToString()
                },
                new SelectListItem(){
                    Value = Commons.EArea.DongHungThuan.ToString("d"),
                    Text = Commons.EArea.DongHungThuan.ToString()
                },new SelectListItem(){
                    Value = Commons.EArea.HiepThanh.ToString("d"),
                    Text = Commons.EArea.HiepThanh.ToString()
                },
                new SelectListItem(){
                    Value = Commons.EArea.TanChanhHiep.ToString("d"),
                    Text = Commons.EArea.TanChanhHiep.ToString()
                },new SelectListItem(){
                    Value = Commons.EArea.TanHungThuan.ToString("d"),
                    Text = Commons.EArea.TanHungThuan.ToString()
                },
                new SelectListItem(){
                    Value = Commons.EArea.TanThoiHiep.ToString("d"),
                    Text = Commons.EArea.TanThoiHiep.ToString()
                },new SelectListItem(){
                    Value = Commons.EArea.TanThoiNhat.ToString("d"),
                    Text = Commons.EArea.TanThoiNhat.ToString()
                },
                new SelectListItem(){
                    Value = Commons.EArea.ThanhLoc.ToString("d"),
                    Text = Commons.EArea.ThanhLoc.ToString()
                },new SelectListItem(){
                    Value = Commons.EArea.ThanhXuan.ToString("d"),
                    Text = Commons.EArea.ThanhXuan.ToString()
                },
                new SelectListItem(){
                    Value = Commons.EArea.ThoiAn.ToString("d"),
                    Text = Commons.EArea.ThoiAn.ToString()
                },
                new SelectListItem(){
                    Value = Commons.EArea.TrungMyTay.ToString("d"),
                    Text = Commons.EArea.TrungMyTay.ToString()
                }
            };
        }
    }
}
