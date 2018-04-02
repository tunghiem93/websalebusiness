using NuWebForSaler.Shared.Utilities;
using ProjectWebSaleLand.Shared;
using ProjectWebSaleLand.Shared.Factory.ProductFactory;
using ProjectWebSaleLand.Shared.Model.Product;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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

                if (model.PictureUpload1 != null && model.PictureUpload1.ContentLength > 0)
                {
                    Byte[] imgByte = new Byte[model.PictureUpload1.ContentLength];
                    model.PictureUpload1.InputStream.Read(imgByte, 0, model.PictureUpload1.ContentLength);
                    model.PictureByte1 = imgByte;
                    model.ImageURL1 = Guid.NewGuid() + Path.GetExtension(model.PictureUpload1.FileName);
                    model.PictureUpload1 = null;
                    photoByte = imgByte;
                }

                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                //====================
                string msg = "";
                var tmp = model.PictureByte1;
                model.PictureByte1 = null;
                model.Type = (int)Commons.EProductType.House;
                bool result = _factory.InsertProduct(model, ref msg);
                if (result)
                {
                    model.PictureByte1 = tmp;
                    //Save Image on Server
                    if (!string.IsNullOrEmpty(model.ImageURL1) && model.PictureByte1 != null)
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
                        model.PictureByte1 = photoByte;
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

        [HttpPost]
        public ActionResult Edit(ProductModels model)
        {
            try
            {
                byte[] photoByte = null;

                if (string.IsNullOrEmpty(model.ImageURL1))
                {
                    model.ImageURL1 = model.ImageURL1.Replace(Commons._PublicImages, "").Replace(Commons.Image100_100, "");
                }
                if (model.PictureUpload1 != null && model.PictureUpload1.ContentLength > 0)
                {
                    Byte[] imgByte = new Byte[model.PictureUpload1.ContentLength];
                    model.PictureUpload1.InputStream.Read(imgByte, 0, model.PictureUpload1.ContentLength);
                    model.PictureByte1 = imgByte;
                    model.ImageURL1 = Guid.NewGuid() + Path.GetExtension(model.PictureUpload1.FileName);
                    model.PictureUpload1 = null;
                    photoByte = imgByte;
                }
                else
                    model.ImageURL1 = model.ImageURL1.Replace(Commons._PublicImages, "").Replace(Commons.Image100_100, "");

                
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return PartialView("_Edit", model);
                }
                //====================
                string msg = "";
                var tmp = model.PictureByte1;
                model.PictureByte1 = null;
                var result = _factory.UpdateProduct(model, ref msg);
                if (result)
                {
                    model.PictureByte1 = tmp;
                    //Save Image on Server
                    if (!string.IsNullOrEmpty(model.ImageURL1) && model.PictureByte1 != null)
                    {
                        var path = Server.MapPath("~/Uploads/" + model.ImageURL1);
                        MemoryStream ms = new MemoryStream(photoByte, 0, photoByte.Length);
                        ms.Write(photoByte, 0, photoByte.Length);
                        System.Drawing.Image imageTmp = System.Drawing.Image.FromStream(ms, true);

                        ImageHelper.Me.SaveCroppedImage(imageTmp, path, model.ImageURL1, ref photoByte);
                        model.PictureByte1 = photoByte;
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("CategoryID", msg);
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return PartialView("_Edit", model);
                }
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("House_Edit: " , ex);
                model = GetDetail(model.ID);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return new HttpStatusCodeResult(400, ex.Message);
            }
        }

        [HttpGet]
        public PartialViewResult View(string id)
        {
            ProductModels model = GetDetail(id);
            return PartialView("_View", model);
        }

        [HttpGet]
        public PartialViewResult Delete(string id)
        {
            ProductModels model = GetDetail(id);
            return PartialView("_Delete", model);
        }

        [HttpPost]
        public ActionResult Delete(ProductModels model)
        {
            try
            {
                string msg = "";
                var result = _factory.DeleteProduct(model.ID, ref msg);
                if (!result)
                {
                    ModelState.AddModelError("Name", msg);
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return PartialView("_Delete", model);
                }
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("House_Delete: " , ex);
                ModelState.AddModelError("Name", "Sản phẩm này hiện đang sử dụng. Làm ơn kiểm tra lại!");
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return PartialView("_Delete", model);
            }
        }
    }
}