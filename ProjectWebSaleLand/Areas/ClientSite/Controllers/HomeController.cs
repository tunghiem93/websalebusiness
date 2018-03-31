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
        private ProductFactory _productFactory = null;

        public HomeController()
        {
            _productFactory = new ProductFactory();
        }
        // GET: ClientSite/Home
        public ActionResult Index()
        {
            try
            {
                ProductModels model = new ProductModels();
                //var listProduct = _productFactory.GetPromotion();
                return View();
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