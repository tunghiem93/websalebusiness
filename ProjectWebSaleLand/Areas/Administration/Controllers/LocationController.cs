﻿using ProjectWebSaleLand.Shared.Factory.LocationFactory;
using ProjectWebSaleLand.Shared.Model.Location;
using ProjectWebSaleLane.Shared.Model.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ProjectWebSaleLand.Areas.Administration.Controllers
{
    public class LocationController : BaseController
    {
        private LocationFactory _factory = null;
        // GET: Administration/Location
        public LocationController()
        {
            _factory = new LocationFactory();
        }

        public ActionResult Index()
        {
            LocationViewModels model = new LocationViewModels();
            return View(model);
        }

        public ActionResult Search(LocationViewModels model)
        {
            try
            {
                var data = _factory.GetListLocation();
                model.ListLocation = data;
            }
            catch (Exception e)
            {
                NSLog.Logger.Error("GetListLocation: ", e);
                return new HttpStatusCodeResult(400, e.Message);
            }
            return PartialView("_ListData", model);
        }

        public LocationModels GetDetail(string id)
        {
            try
            {
                LocationModels model = _factory.GetDetailLocation(id);
                return model;
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("GetDetailLocation: ", ex);
                return null;
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            LocationModels model = new LocationModels();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(LocationModels model)
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
                var result = _factory.InsertLocation(model, ref msg);
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
                NSLog.Logger.Error("LocationCreate: ", ex);
                ModelState.AddModelError("Name", "Có một lỗi xảy ra!");
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return PartialView("Create", model);
            }
        }

        [HttpGet]
        public new PartialViewResult View(string id)
        {
            LocationModels model = GetDetail(id);
            return PartialView("_View", model);
        }

        [HttpGet]
        public PartialViewResult Edit(string id)
        {
            LocationModels model = GetDetail(id);
            return PartialView("_Edit", model);
        }

        [HttpPost]
        public ActionResult Edit(LocationModels model)
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
                model.CreatedUser = CurrentUser.UserId;
                var result = _factory.UpdateLocation(model, ref msg);
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
                NSLog.Logger.Error("Location_Edit: ", ex);
                ModelState.AddModelError("Name", ex.Message);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return PartialView("_Edit", model);
            }
        }

        [HttpGet]
        public PartialViewResult Delete(string id)
        {
            LocationModels model = GetDetail(id);
            return PartialView("_Delete", model);
        }

        [HttpPost]
        public ActionResult Delete(LocationModels model)
        {
            try
            {
                string msg = "";
                var result = _factory.DeleteLocation(model.ID, ref msg);
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
                NSLog.Logger.Error("Xóa khu vực: ", ex);
                ModelState.AddModelError("Name", ("Lỗi khi xóa thông tin khu vực!"));
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return PartialView("_Delete", model);
            }
        }
    }
}