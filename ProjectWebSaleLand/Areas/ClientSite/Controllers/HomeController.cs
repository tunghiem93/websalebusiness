using ProjectWebSaleLand.Shared;
using ProjectWebSaleLand.Shared.Factory.LocationFactory;
using ProjectWebSaleLand.Shared.Factory.ProductFactory;
using ProjectWebSaleLand.Shared.Model.Product;
using ProjectWebSaleLand.Shared.Utilities;
using ProjectWebSaleLane.Shared.Model.Product;
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
                var data = _factoryPro.GetListProduct();

                var temp = data.OrderByDescending(x => x.CreatedDate)
                                                      .Skip(0).Take(6).ToList();
                model.TotalProduct = data.Count;
                temp.ForEach(x =>
                {
                    if (!string.IsNullOrEmpty(x.ImageURL))
                        x.ImageURL = Commons.HostImage + x.ImageURL;
                });
                model.ListProduct = temp;

                model.ListCate = new List<SelectListItem>()
                {
                    new SelectListItem() {  Text = "Đất", Value = Commons.EProductType.Land.ToString("d")},
                    new SelectListItem() { Text = "Nhà", Value = Commons.EProductType.House.ToString("d")}
                };
                var lstLoc = _factoryLoc.GetListLocation();
                model.ListArea = lstLoc.Where(w => w.IsActive).Select(o => new SelectListItem()
                {
                    Value = o.ID,
                    Text = o.Name,
                }).OrderBy(o => o.Text).ToList();
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
            ProductViewModels model = new ProductViewModels();
            var data = _factoryPro.GetListProduct().OrderByDescending(x => x.CreatedDate)
                                                   .ToList();
            data.ForEach(x =>
            {
                if (!string.IsNullOrEmpty(x.ImageURL))
                    x.ImageURL = Commons.HostImage + x.ImageURL;
            });
            model.ListProduct = data;
            return View(model);
        }

        public ActionResult PropertiesDetail(string id)
        {
            ProductDetailViewModels model = new ProductDetailViewModels();
            if (!string.IsNullOrEmpty(id))
            {

                var data = _factoryPro.GetListProduct();
                var oldData = data.Where(x => !x.ID.Equals(id)).OrderBy(x => x.CreatedDate).Skip(0).Take(3).ToList();
                oldData.ForEach(x =>
                {
                    if (!string.IsNullOrEmpty(x.ImageURL))
                        x.ImageURL = Commons.HostImage + x.ImageURL;
                });
                var dataDetail = data.Where(x => x.ID.Equals(id)).FirstOrDefault();
                if (!string.IsNullOrEmpty(dataDetail.ImageURL))
                    dataDetail.ImageURL = Commons.HostImage + dataDetail.ImageURL;
                if (dataDetail.ListImg != null)
                {
                    dataDetail.ListImg.ForEach(x =>
                    {
                        x.ImageURL = Commons.HostImage + x.ImageURL;
                    });
                }
                model.ListProduct = oldData;
                model.Product = dataDetail;

                //// get list location
                var _location = _factoryLoc.GetListLocation();
                _location.ForEach(x =>
                {
                    x.Total = data.Count(z => z.LocationID.Equals(x.ID));
                });
                model.ListLocation = _location;
                return View(model);
            }
            else
            {
                return RedirectToAction("Index");
            }

        }

        public ActionResult Search(ProductViewModels model)
        {
            if (model == null)
                model = new ProductViewModels();

            var data = _factoryPro.GetListProduct()
                                    .Where(x => (!string.IsNullOrEmpty(model.SegmentID) ? x.Segment == Convert.ToInt16(model.SegmentID) : 1 == 1)
                                            && (!string.IsNullOrEmpty(model.AreaID) ? x.LocationID.Equals(model.AreaID) : 1 == 1)
                                            && (!string.IsNullOrEmpty(model.CateID) ? x.Type == Convert.ToInt16(model.CateID) : 1 == 1))
                                    .OrderByDescending(x => x.CreatedDate)
                                                   .ToList();
            data.ForEach(x =>
            {
                if (!string.IsNullOrEmpty(x.ImageURL))
                    x.ImageURL = Commons.HostImage + x.ImageURL;
            });
            model.ListProduct = data;
            return View(model);
        }
        public ActionResult SearchLocation(string id)
        {
            var model = new ProductViewModels();

            var data = _factoryPro.GetListProduct()
                                    .Where(x => x.LocationID.Equals(id))
                                    .OrderByDescending(x => x.CreatedDate)
                                                   .ToList();
            data.ForEach(x =>
            {
                if (!string.IsNullOrEmpty(x.ImageURL))
                    x.ImageURL = Commons.HostImage + x.ImageURL;
            });
            model.ListProduct = data;
            return View("Search", model);
        }

        [HttpPost]
        public ActionResult SearchKeyWord(ProductDetailViewModels model)
        {
            var modelView = new ProductViewModels();
            if (string.IsNullOrEmpty(model.KeyWord))
                model.KeyWord = "";
            var data = _factoryPro.GetListProduct()
                                    .Where(x =>CommonHelper.RemoveUnicode(x.Name.ToLower()).Contains(CommonHelper.RemoveUnicode(model.KeyWord.ToLower())))
                                    .OrderByDescending(x => x.CreatedDate)
                                                   .ToList();
            data.ForEach(x =>
            {
                if (!string.IsNullOrEmpty(x.ImageURL))
                    x.ImageURL = Commons.HostImage + x.ImageURL;
            });
            modelView.ListProduct = data;
            return View("Search", modelView);
        }
    }
}