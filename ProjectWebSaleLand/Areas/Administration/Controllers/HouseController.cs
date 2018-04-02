using NuWebForSaler.Shared.Utilities;
using ProjectWebSaleLand.Shared;
using ProjectWebSaleLand.Shared.Factory.ProductFactory;
using ProjectWebSaleLand.Shared.Model.Product;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectWebSaleLand.Areas.Administration.Controllers
{
    public class HouseController : BaseController
    {
        private ProductFactory _factory = null;
        public HouseController()
        {
            _factory = new ProductFactory();
            ViewBag.Category = getListCategory();
            ViewBag.Location = getListLocation();
        }
        // GET: Administration/House
        public ActionResult Index()
        {
            ProductViewModels model = new ProductViewModels();
            return View(model);
        }

        public ActionResult Search(ProductViewModels model)
        {
            try
            {
                var data = _factory.GetListProduct().Where(x=>x.Type == (int)Commons.EProductType.House).ToList();
                data.ForEach(x =>
                {
                   if(!string.IsNullOrEmpty(x.ImageURL1))
                        x.ImageURL1 = Commons.HostImage + x.ImageURL1;
                });
                model.ListProduct = data;
            }
            catch (Exception e)
            {
                NSLog.Logger.Error("GetListProduct: ", e);
                return new HttpStatusCodeResult(400, e.Message);
            }
            return PartialView("_ListData", model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ProductModels model = new ProductModels();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(ProductModels model)
        {
            try
            {
                byte[] photoByte = null;

                if (model.PictureUpload != null && model.PictureUpload.ContentLength > 0)
                {
                    Byte[] imgByte = new Byte[model.PictureUpload.ContentLength];
                    model.PictureUpload.InputStream.Read(imgByte, 0, model.PictureUpload.ContentLength);
                    model.PictureByte = imgByte;
                    model.ImageURL1 = Guid.NewGuid() + Path.GetExtension(model.PictureUpload.FileName);
                    model.PictureUpload = null;
                    photoByte = imgByte;
                }

                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                //====================
                string msg = "";
                var tmp = model.PictureByte;
                model.PictureByte = null;
                model.Type = (int)Commons.EProductType.House;
                bool result = _factory.InsertProduct(model, ref msg);
                if (result)
                {
                    model.PictureByte = tmp;
                    //Save Image on Server
                    if (!string.IsNullOrEmpty(model.ImageURL1) && model.PictureByte != null)
                    {
                        //var originalDirectory = new DirectoryInfo(string.Format("{0}Uploads\\", Server.MapPath(@"\")));
                        //var path = string.Format("{0}{1}", originalDirectory, model.ImageURL1);
                       // var originalDirectory = "~/Uploads/";
                       // var path = string.Format("{0}{1}", originalDirectory, model.ImageURL1);
                        var path =  Server.MapPath("~/Uploads/" + model.ImageURL1);
                        MemoryStream ms = new MemoryStream(photoByte, 0, photoByte.Length);
                        ms.Write(photoByte, 0, photoByte.Length);
                        System.Drawing.Image imageTmp = System.Drawing.Image.FromStream(ms, true);

                        ImageHelper.Me.SaveCroppedImage(imageTmp, path, model.ImageURL1, ref photoByte);
                        model.PictureByte = photoByte;
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("CategoryID", msg);
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("Categories_Create: " , ex);
                return new HttpStatusCodeResult(400, ex.Message);
            }
        }

        public ProductModels GetDetail(string id)
        {
            try
            {
                ProductModels model = _factory.GetDetailProduct(id);
                if (model != null)
                {
                    model.ImageURL1 = string.IsNullOrEmpty(model.ImageURL1) ? Commons.Image100_100 : (Commons.HostImage+ model.ImageURL1);
                    return model;
                }
                else
                {
                    model = new ProductModels();
                    return model;
                }
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("House_Detail: " , ex);
                return null;
            }
        }

        public PartialViewResult Edit(string id)
        {
            ProductModels model = GetDetail(id);
            return PartialView("_Edit", model);
        }

        [HttpGet]
        public PartialViewResult View(string id)
        {
            ProductModels model = GetDetail(id);
            return PartialView("_View", model);
        }
    }
}