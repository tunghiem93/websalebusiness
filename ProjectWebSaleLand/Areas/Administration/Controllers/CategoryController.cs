using ProjectWebSaleLand.Shared;
using ProjectWebSaleLand.Shared.Factory.CategoryFactory;
using ProjectWebSaleLand.Shared.Model.Category;
using ProjectWebSaleLane.Shared.Model.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ProjectWebSaleLand.Areas.Administration.Controllers
{
    public class CategoryController : BaseController
    {
        private CategoryFactory _factory = null;
        // GET: Administration/Category
        public CategoryController()
        {
            _factory = new CategoryFactory(); 
        }

        public ActionResult Index()
        {
            CategoryViewModels model = new CategoryViewModels();
            return View(model);
        }

        public ActionResult Search(CategoryViewModels model)
        {
            try
            {
                var data = _factory.GetListCate();
                model.ListCate = data;
            }
            catch (Exception e)
            {
                NSLog.Logger.Error("GetListCate: ", e);
                return new HttpStatusCodeResult(400, e.Message);
            }
            return PartialView("_ListData", model);
        }

        public CategoryModels GetDetail(string id)
        {
            try
            {
                CategoryModels model = _factory.GetDetailCate(id);
                return model;
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("GetDetailCate: ", ex);
                return null;
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            CategoryModels model = new CategoryModels();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CategoryModels model)
        {
            try
            {    
                if (!ModelState.IsValid)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return PartialView("Create", model);
                }
                string msg = "";
                model.CreatedUser = CurrentUser.UserId;
                var result = _factory.InsertCate(model, ref msg);
                if (result)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("Name", msg);
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return PartialView("Create", model);
                }
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("CateCreate: ", ex);
                ModelState.AddModelError("Name", "Có một lỗi xảy ra!");
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return PartialView("Create", model);
            }
        }

        [HttpGet]
        public new PartialViewResult View(string id)
        {
            CategoryModels model = GetDetail(id);
            return PartialView("_View", model);
        }

        [HttpGet]
        public PartialViewResult Edit(string id)
        {
            CategoryModels model = GetDetail(id);
            return PartialView("_Edit", model);
        }

        [HttpPost]
        public ActionResult Edit(CategoryModels model)
        {
            try
            {                
                if (!ModelState.IsValid)
                {
                    model = GetDetail(model.ID);
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return PartialView("_Edit", model);
                }
                string msg = "";
                model.ModifiedUser = CurrentUser.UserId;
                var result = _factory.UpdateCate(model, ref msg);
                if (result)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    model = GetDetail(model.ID);
                    if (!string.IsNullOrEmpty(msg))
                    {
                        ModelState.AddModelError("Name", msg);
                    }
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return PartialView("_Edit", model);
                }
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("Cate_Edit: ", ex);
                ModelState.AddModelError("Name", ex.Message);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return PartialView("_Edit", model);
            }
        }

        [HttpGet]
        public PartialViewResult Delete(string id)
        {
            CategoryModels model = GetDetail(id);
            return PartialView("_Delete", model);
        }

        [HttpPost]
        public ActionResult Delete(CategoryModels model)
        {
            try
            {
                string msg = "";
                var result = _factory.DeleteCate(model.ID, ref msg);
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
                NSLog.Logger.Error("Xóa thể loại: ", ex);
                ModelState.AddModelError("Name", ("Lỗi khi xóa thông tin thể loại!"));
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return PartialView("_Delete", model);
            }
        }
    }
}