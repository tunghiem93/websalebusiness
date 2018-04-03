
using ProjectWebSaleLand.Shared.Factory.LocationFactory;
using ProjectWebSaleLand.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectWebSaleLand.Areas.Administration.Controllers
{
    public class HQController : Controller
    {
        public UserSession CurrentUser
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["User"] != null)
                    return (UserSession)System.Web.HttpContext.Current.Session["User"];
                else
                    return new UserSession();
            }
        }

        //public List<SelectListItem> getListCategory()
        //{
        //    var _lstCate = new List<SelectListItem>();
        //    try
        //    {
        //        CategoryFactory _factory = new CategoryFactory();
        //        var data = _factory.GetListCate();
        //        foreach(var item in data)
        //        {
        //            _lstCate.Add(new SelectListItem
        //            {
        //                Value = item.ID,
        //                Text = item.Name
        //            });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        NSLog.Logger.Error("getListCategory", ex);
        //    }
        //    return _lstCate;
        //}

        public List<SelectListItem> getListLocation()
        {
            var _lstLocation = new List<SelectListItem>();
            try
            {
                LocationFactory _factory = new LocationFactory();
                var data = _factory.GetListLocation();
                foreach(var item in data)
                {
                    _lstLocation.Add(new SelectListItem
                    {
                        Value = item.ID,
                        Text = item.Name
                    });
                }
            }catch(Exception ex)
            {
                NSLog.Logger.Error("getListLocation", ex);
            }
            return _lstLocation;
        }
    }
}