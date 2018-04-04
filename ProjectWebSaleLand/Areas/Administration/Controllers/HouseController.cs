using NuWebForSaler.Shared.Utilities;
using ProjectWebSaleLand.Shared;
using ProjectWebSaleLand.Shared.Factory.ProductFactory;
using ProjectWebSaleLand.Shared.Model.Product;
using ProjectWebSaleLand.Web.App_Start;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ProjectWebSaleLand.Areas.Administration.Controllers
{
    [NuAuth]
    public class HouseController : BaseController
    {
        private ProductFactory _factory = null;
        public HouseController()
        {
            _factory = new ProductFactory();
            //ViewBag.Category = getListCategory();
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
                var data = _factory.GetListProduct().Where(x => x.Type == (int)Commons.EProductType.House).ToList();
                data.ForEach(x =>
                {
                    if (!string.IsNullOrEmpty(x.ImageURL))
                        x.ImageURL = Commons.HostImage + x.ImageURL;
                });
                model.ListProduct = data;
            }
            catch (Exception e)
            {
                NSLog.Logger.Error("GetListHouse: ", e);
                return new HttpStatusCodeResult(400, e.Message);
            }
            return PartialView("_ListData", model);
        }

        [HttpGet]
        public PartialViewResult View(string id)
        {
            ProductModels model = GetDetail(id);
            return PartialView("_View", model);
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
                    model.RawImageUrl = Guid.NewGuid() + Path.GetExtension(model.PictureUpload.FileName);
                    model.PictureUpload = null;
                    photoByte = imgByte;
                }

                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                model.ListImageUrl.Add(model.RawImageUrl);
                //========
                Dictionary<int, byte[]> lstImgByte = new Dictionary<int, byte[]>();
                var ListImage = model.ListImg.Where(x => !x.IsDelete).ToList();
                foreach (var item in ListImage)
                {
                    if (item.PictureUpload != null && item.PictureUpload.ContentLength > 0)
                    {
                        Byte[] imgByte = new Byte[item.PictureUpload.ContentLength];
                        item.PictureUpload.InputStream.Read(imgByte, 0, item.PictureUpload.ContentLength);
                        item.PictureByte = imgByte;
                        item.ImageURL = Guid.NewGuid() + Path.GetExtension(item.PictureUpload.FileName);
                        item.PictureUpload = null;
                        lstImgByte.Add(item.OffSet, imgByte);
                        model.ListImageUrl.Add(item.ImageURL);
                    }
                }
                //====================
                string msg = "";
                model.Type = (byte)Commons.EProductType.House;
                bool result = _factory.InsertProduct(model, ref msg);
                if (result)
                {
                    if (!string.IsNullOrEmpty(model.RawImageUrl) && model.PictureByte != null)
                    {
                        var path = Server.MapPath("~/Uploads/Images/Product/" + model.RawImageUrl);
                        MemoryStream ms = new MemoryStream(photoByte, 0, photoByte.Length);
                        ms.Write(photoByte, 0, photoByte.Length);
                        System.Drawing.Image imageTmp = System.Drawing.Image.FromStream(ms, true);

                        ImageHelper.Me.SaveCroppedImage(imageTmp, path, model.RawImageUrl, ref photoByte);
                    }

                    foreach (var item in ListImage)
                    {
                        if (!string.IsNullOrEmpty(item.ImageURL) && item.PictureByte != null)
                        {
                            var path = Server.MapPath("~/Uploads/Images/Product/" + item.ImageURL);
                            MemoryStream ms = new MemoryStream(lstImgByte[item.OffSet], 0, lstImgByte[item.OffSet].Length);
                            ms.Write(lstImgByte[item.OffSet], 0, lstImgByte[item.OffSet].Length);
                            System.Drawing.Image imageTmp = System.Drawing.Image.FromStream(ms, true);

                            ImageHelper.Me.SaveCroppedImage(imageTmp, path, item.ImageURL, ref photoByte);
                        }
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("Name", msg);
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("House_Create: ", ex);
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
                    model.ImageURL = string.IsNullOrEmpty(model.ImageURL) ? Commons.Image100_100 : (Commons.HostImage + model.ImageURL);
                    if (model.ListImg != null && model.ListImg.Count > 0)
                    {
                        model.ListImg.ForEach(o => {
                            o.ImageURL = string.IsNullOrEmpty(o.ImageURL) ? Commons.Image200_100 : (Commons.HostImage + o.ImageURL);
                            o.IsDelete = false;
                        });
                    }
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
                NSLog.Logger.Error("House_Detail: ", ex);
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

                if (!string.IsNullOrEmpty(model.ImageURL))
                {
                    model.ImageURL = model.ImageURL.Replace(Commons._PublicImages, "").Replace(Commons.Image100_100, "");
                }
                if (model.PictureUpload != null && model.PictureUpload.ContentLength > 0)
                {
                    Byte[] imgByte = new Byte[model.PictureUpload.ContentLength];
                    model.PictureUpload.InputStream.Read(imgByte, 0, model.PictureUpload.ContentLength);
                    model.PictureByte = imgByte;
                    model.RawImageUrl = Guid.NewGuid() + Path.GetExtension(model.PictureUpload.FileName);
                    model.PictureUpload = null;
                    photoByte = imgByte;
                }
               

                if (!ModelState.IsValid)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return PartialView("_Edit", model);
                }

                model.ListImageUrl.Add(model.RawImageUrl);
                //========
                Dictionary<int, byte[]> lstImgByte = new Dictionary<int, byte[]>();
                var ListImage = model.ListImg.Where(x => !x.IsDelete).ToList();
                foreach (var item in ListImage)
                {
                    if (item.PictureUpload != null && item.PictureUpload.ContentLength > 0)
                    {
                        Byte[] imgByte = new Byte[item.PictureUpload.ContentLength];
                        item.PictureUpload.InputStream.Read(imgByte, 0, item.PictureUpload.ContentLength);
                        item.PictureByte = imgByte;
                        item.ImageURL = Guid.NewGuid() + Path.GetExtension(item.PictureUpload.FileName);
                        item.PictureUpload = null;
                        lstImgByte.Add(item.OffSet, imgByte);
                        model.ListImageUrl.Add(item.ImageURL);
                    }
                }

                //====================
                string msg = "";
                model.Type = (byte)Commons.EProductType.House;
                var result = _factory.UpdateProduct(model, ref msg);
                if (result)
                {
                    if (!string.IsNullOrEmpty(model.RawImageUrl) && model.PictureByte != null)
                    {
                        var path = Server.MapPath("~/Uploads/Images/Product/" + model.RawImageUrl);
                        MemoryStream ms = new MemoryStream(photoByte, 0, photoByte.Length);
                        ms.Write(photoByte, 0, photoByte.Length);
                        System.Drawing.Image imageTmp = System.Drawing.Image.FromStream(ms, true);

                        ImageHelper.Me.SaveCroppedImage(imageTmp, path, model.RawImageUrl, ref photoByte);
                    }

                    foreach (var item in ListImage)
                    {
                        if (!string.IsNullOrEmpty(item.ImageURL) && item.PictureByte != null)
                        {
                            var path = Server.MapPath("~/Uploads/Images/Product/" + item.ImageURL);
                            MemoryStream ms = new MemoryStream(lstImgByte[item.OffSet], 0, lstImgByte[item.OffSet].Length);
                            ms.Write(lstImgByte[item.OffSet], 0, lstImgByte[item.OffSet].Length);
                            System.Drawing.Image imageTmp = System.Drawing.Image.FromStream(ms, true);

                            ImageHelper.Me.SaveCroppedImage(imageTmp, path, item.ImageURL, ref photoByte);
                        }
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("Name", msg);
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return PartialView("_Edit", model);
                }
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("House_Edit: ", ex);
                model = GetDetail(model.ID);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return new HttpStatusCodeResult(400, ex.Message);
            }
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
                NSLog.Logger.Error("House_Delete: ", ex);
                ModelState.AddModelError("Name", "Sản phẩm này hiện đang sử dụng. Làm ơn kiểm tra lại!");
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return PartialView("_Delete", model);
            }
        }
        public PartialViewResult AddImageItem(int OffSet)
        {
            ImageProduct model = new ImageProduct();
            model.OffSet = OffSet;
            model.ImageURL = Commons.Image200_100;
            return PartialView("_ImageItemProduct", model);
        }
    }
}