using ProjectWebSaleLand.Shared.Factory.CategoryFactory;
using ProjectWebSaleLand.Shared.Model.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ProjectWebSaleLand.Areas.Administration.Controllers
{
    public class CategoryController : Controller
    {
        private CategoryFactory _factory = null;
        // GET: Administration/Category
        public CategoryController()
        {
            _factory = new CategoryFactory(); 
        }

        public ActionResult Index()
        {
            CustomersViewModels model = new CustomersViewModels();
            return View(model);
        }

        public ActionResult Search(CustomersViewModels model)
        {
            try
            {
                var data = _factory.GetListData(CurrentUser.OrganizationId);

                foreach (var item in data)
                {
                    item.ImageURL = string.IsNullOrEmpty(item.ImageURL) ? Commons.Image100_100 : item.ImageURL;

                    //if (!string.IsNullOrEmpty(item.Phone) && item.Phone.Length > 8)
                    //{
                    //    string tabs = new String('*', item.Phone.Length - 4);
                    //    item.Phone = tabs + item.Phone.Substring(item.Phone.Length - 4, 4);
                    //}
                    //else
                    //    item.Phone = "********";
                    //if (!string.IsNullOrEmpty(item.Email) && item.Email.Length > 8)
                    //{
                    //    string tabs = new String('*', item.Email.Length - 4);
                    //    item.Email = item.Email.Substring(0, 4) + tabs;
                    //}
                    //else
                    //    item.Email = "********";

                    if (!string.IsNullOrEmpty(item.Phone) && item.Phone.Length > 3)
                    {
                        string tabs = new String('*', item.Phone.Length - 4);
                        item.PhoneDisplay = tabs + item.Phone.Substring(item.Phone.Length - 4, 4);
                    }
                    else if (!string.IsNullOrEmpty(item.Phone))
                        item.PhoneDisplay = "********";

                    if (!string.IsNullOrEmpty(item.Email) && item.Email.Length > 3)
                    {
                        string tabs = new String('*', item.Email.Length - 4);
                        item.EmailDisplay = item.Email.Substring(0, 4) + tabs;
                    }
                    else if (!string.IsNullOrEmpty(item.Email))
                        item.EmailDisplay = "********";

                    if (!string.IsNullOrEmpty(item.IC) && item.IC.Length > 3)
                    {
                        string tabs = new String('*', item.IC.Length - 4);
                        item.ICDisplay = tabs + item.IC.Substring(item.IC.Length - 4, 4);
                    }
                    else if (!string.IsNullOrEmpty(item.IC))
                        item.ICDisplay = "********";

                }
                model.LstCustomer = data;
            }
            catch (Exception e)
            {
                _logger.Error("Customer_Search: " + e);
                return new HttpStatusCodeResult(400, e.Message);
            }
            return PartialView("_ListData", model);
        }

        public CustomerDTO GetDetail(string id)
        {
            try
            {
                CustomerDTO model = _factory.GetDetail(id, CurrentUser.OrganizationId);
                model.ImageURL = string.IsNullOrEmpty(model.ImageURL) ? Commons.Image100_100 : model.ImageURL;
                model.DOB = CommonHelper.ConvertToLocalTime(model.DOB);
                model.Aniversary = CommonHelper.ConvertToLocalTime(model.Aniversary);
                model.DateJoin = CommonHelper.ConvertToLocalTime(model.DateJoin);
                return model;
            }
            catch (Exception ex)
            {
                _logger.Error("Customer_Detail: " + ex);
                return null;
            }
        }

        [HttpGet]
        public PartialViewResult Create()
        {
            CustomerDTO model = new CustomerDTO();
            return PartialView("Create", model);
        }

        [HttpPost]
        public ActionResult Create(CustomerDTO model)
        {
            try
            {
                byte[] photoByte = null;
                if (model.PictureUpload != null && model.PictureUpload.ContentLength > 0)
                {
                    Byte[] imgByte = new Byte[model.PictureUpload.ContentLength];
                    model.PictureUpload.InputStream.Read(imgByte, 0, model.PictureUpload.ContentLength);
                    model.PictureByte = imgByte;
                    model.ImageURL = Guid.NewGuid() + Path.GetExtension(model.PictureUpload.FileName);
                    model.PictureUpload = null;
                    photoByte = imgByte;
                }

                DateTime curDate = DateTime.Now;
                //if (!CommonHelper.IsValidEmail(model.Email))
                //{
                //    ModelState.AddModelError("Email", "Email is not valid");
                //}
                if (model.DOB.Date > curDate.Date)
                {
                    ModelState.AddModelError("DOB", _AttributeForLanguage.CurrentUser.GetLanguageTextFromKey("Birthday can not be greater than current day"));
                }
                if (model.Aniversary.Date > curDate)
                {
                    ModelState.AddModelError("Aniversary", _AttributeForLanguage.CurrentUser.GetLanguageTextFromKey("Aniversary can not be greater than current day"));
                }
                if (!model.PrivacyPolicy)
                {
                    ModelState.AddModelError("PrivacyPolicy", _AttributeForLanguage.CurrentUser.GetLanguageTextFromKey("Please confirm before save"));
                }
                if (!ModelState.IsValid)
                {
                    if ((ModelState["PictureUpload"]) != null && (ModelState["PictureUpload"]).Errors.Count > 0)
                        model.ImageURL = "";
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    //return View(model);
                    return PartialView("Create", model);
                }
                string msg = "";
                var result = _factory.Insert(model, ref msg, CurrentUser.OrganizationId, CurrentUser.UserId);
                if (result)
                {
                    //Save Image on Server
                    if (!string.IsNullOrEmpty(model.ImageURL) && model.PictureByte != null)
                    {
                        var originalDirectory = new DirectoryInfo(string.Format("{0}Uploads\\", Server.MapPath(@"\")));
                        var path = string.Format("{0}{1}", originalDirectory, model.ImageURL);
                        MemoryStream ms = new MemoryStream(photoByte, 0, photoByte.Length);
                        ms.Write(photoByte, 0, photoByte.Length);
                        System.Drawing.Image imageTmp = System.Drawing.Image.FromStream(ms, true);
                        ImageHelper.Me.SaveCroppedImage(imageTmp, path, model.ImageURL, ref photoByte);
                        model.PictureByte = photoByte;
                        FTP.Upload(model.ImageURL, model.PictureByte);
                        ImageHelper.Me.TryDeleteImageUpdated(path);
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("Name", _AttributeForLanguage.CurrentUser.GetLanguageTextFromKey(msg));
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    //return View("Create", model);
                    return PartialView("Create", model);
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Customer_Create: " + ex);
                ModelState.AddModelError("Name", _AttributeForLanguage.CurrentUser.GetLanguageTextFromKey("Have an error"));
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                //return View("Create", model);
                return PartialView("Create", model);
            }
        }

        [HttpGet]
        public new PartialViewResult View(string id)
        {
            CustomerDTO model = GetDetail(id);
            if (!string.IsNullOrEmpty(model.Phone) && model.Phone.Length > 3)
            {
                string tabs = new String('*', model.Phone.Length - 4);
                model.Phone = tabs + model.Phone.Substring(model.Phone.Length - 4, 4);
            }
            else
                model.Phone = "********";
            if (!string.IsNullOrEmpty(model.IC) && model.IC.Length > 3)
            {
                string tabs = new String('*', model.IC.Length - 4);
                model.IC = tabs + model.IC.Substring(model.IC.Length - 4, 4);
            }
            else
                model.IC = "********";

            if (!string.IsNullOrEmpty(model.Email) && model.Email.Length > 3)
            {
                string tabs = new String('*', model.Email.Length - 4);
                model.Email = model.Email.Substring(0, 4) + tabs;
            }
            else
                model.Email = "********";

            return PartialView("_View", model);
        }

        [HttpGet]
        public PartialViewResult Edit(string id)
        {
            CustomerDTO model = GetDetail(id);
            if (!string.IsNullOrEmpty(model.Phone) && model.Phone.Length > 3)
            {
                string tabs = new String('*', model.Phone.Length - 4);
                model.PhoneDisplay = tabs + model.Phone.Substring(model.Phone.Length - 4, 4);
            }
            else if (!string.IsNullOrEmpty(model.Phone))
                model.PhoneDisplay = "********";

            if (!string.IsNullOrEmpty(model.IC) && model.IC.Length > 3)
            {
                string tabs = new String('*', model.IC.Length - 4);
                model.ICDisplay = tabs + model.IC.Substring(model.IC.Length - 4, 4);
            }
            else if (!string.IsNullOrEmpty(model.IC))
                model.ICDisplay = "********";

            if (!string.IsNullOrEmpty(model.Email) && model.Email.Length > 3)
            {
                string tabs = new String('*', model.Email.Length - 4);
                model.EmailDisplay = model.Email.Substring(0, 4) + tabs;
            }
            else if (!string.IsNullOrEmpty(model.Email))
                model.EmailDisplay = "********";

            return PartialView("_Edit", model);
        }

        [HttpPost]
        public ActionResult Edit(CustomerDTO model)
        {
            try
            {
                byte[] photoByte = null;
                if (model.PictureUpload != null && model.PictureUpload.ContentLength > 0)
                {
                    Byte[] imgByte = new Byte[model.PictureUpload.ContentLength];
                    model.PictureUpload.InputStream.Read(imgByte, 0, model.PictureUpload.ContentLength);
                    model.PictureByte = imgByte;
                    model.ImageURL = Guid.NewGuid() + Path.GetExtension(model.PictureUpload.FileName);
                    model.PictureUpload = null;
                    photoByte = imgByte;
                }
                DateTime curDate = DateTime.Now;
                if (model.DOB.Date > curDate.Date)
                {
                    ModelState.AddModelError("DOB", _AttributeForLanguage.CurrentUser.GetLanguageTextFromKey("Birthday can not be greater than current day"));
                }
                if (model.Aniversary.Date > curDate)
                {
                    ModelState.AddModelError("Aniversary", _AttributeForLanguage.CurrentUser.GetLanguageTextFromKey("Aniversary can not be greater than current day"));
                }
                if (!model.PrivacyPolicy)
                {
                    ModelState.AddModelError("PrivacyPolicy", _AttributeForLanguage.CurrentUser.GetLanguageTextFromKey("Please confirm before save"));
                }
                if (!ModelState.IsValid)
                {
                    if ((ModelState["PictureUpload"]) != null && (ModelState["PictureUpload"]).Errors.Count > 0)
                        model.ImageURL = "";
                    model = GetDetail(model.ID);
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return PartialView("_Edit", model);
                }
                string msg = "";
                var result = _factory.Edit(model, ref msg, CurrentUser.UserId, CurrentUser.OrganizationId);
                if (result)
                {
                    ////Save Image on Server
                    if (!string.IsNullOrEmpty(model.ImageURL) && model.PictureByte != null)
                    {
                        var originalDirectory = new DirectoryInfo(string.Format("{0}Uploads\\", Server.MapPath(@"\")));
                        var path = string.Format("{0}{1}", originalDirectory, model.ImageURL);
                        MemoryStream ms = new MemoryStream(photoByte, 0, photoByte.Length);
                        ms.Write(photoByte, 0, photoByte.Length);
                        System.Drawing.Image imageTmp = System.Drawing.Image.FromStream(ms, true);
                        ImageHelper.Me.SaveCroppedImage(imageTmp, path, model.ImageURL, ref photoByte);
                        model.PictureByte = photoByte;
                        FTP.Upload(model.ImageURL, model.PictureByte);
                        ImageHelper.Me.TryDeleteImageUpdated(path);
                    }
                    return RedirectToAction("Index");
                }
                else
                {
                    model = GetDetail(model.ID);
                    if (!string.IsNullOrEmpty(msg))
                    {
                        ModelState.AddModelError("Name", _AttributeForLanguage.CurrentUser.GetLanguageTextFromKey(msg));
                    }
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return PartialView("_Edit", model);
                }
            }
            catch (Exception ex)
            {
                _logger.Error("Customer_Edit: " + ex);
                ModelState.AddModelError("Name", _AttributeForLanguage.CurrentUser.GetLanguageTextFromKey(ex.Message));
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return PartialView("_Edit", model);
            }
        }

        [HttpGet]
        public PartialViewResult Delete(string id)
        {
            return PartialView("_Delete", GetDetail(id));
        }

        [HttpPost]
        public ActionResult Delete(CategoryModels model)
        {
            try
            {
                string msg = "";
                var result = _factory.Delete(model.ID, CurrentUser.UserId, ref msg);
                if (!result)
                {
                    ModelState.AddModelError("Name", (msg));
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return PartialView("_Delete", model);
                }
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("Xóa thể loại: ", ex);
                ModelState.AddModelError("Name", ("Lỗi khi xóa thông tin thể loại!"));
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return PartialView("_Delete", model);
            }
        }
    }
}