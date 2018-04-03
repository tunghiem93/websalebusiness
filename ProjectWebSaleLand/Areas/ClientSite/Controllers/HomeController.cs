using ProjectWebSaleLand.Shared.Factory.LocationFactory;
using ProjectWebSaleLand.Shared.Factory.ProductFactory;
using ProjectWebSaleLand.Shared.Model.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectWebSaleLand.Areas.ClientSite.Controllers
{
    public class HomeController : Controller
    {
        private ProductFactory _factoryPro = null;
        private LocationFactory _factoryLoc = null;

        public HomeController()
        {
            _factoryPro = new ProductFactory();
            _factoryLoc = new LocationFactory();
        }
        // GET: ClientSite/Home
        public ActionResult Index()
        {
            try
            {
                ProductViewModels model = new ProductViewModels();
                //var data = _factoryPro.GetListProduct();
                //model.ListProduct = data;
                //model.ListProduct = model.ListProduct.OrderBy(o => o.CreatedDate).ToList();
                //var lstCate = _factoryCate.GetListCate();
                //model.ListCate = lstCate.Where(w => w.IsActive).Select(o => new SelectListItem()
                //{
                //    Value = o.ID,
                //    Text = o.Name,
                //}).OrderBy(o => o.Text).ToList();
                //var lstLoc = _factoryLoc.GetListLocation();
                //model.ListArea = lstLoc.Where(w => w.IsActive).Select(o => new SelectListItem()
                //{
                //    Value = o.ID,
                //    Text = o.Name,
                //}).OrderBy(o => o.Text).ToList();
                return View(model);
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("Index : ", ex);
                return new HttpStatusCodeResult(400, ex.Message);
            }            
        }

        public ActionResult MoreProperties()
        {
            return View();
        }

        public ActionResult PropertiesDetail()
        {
            return View();
        }
    }
}