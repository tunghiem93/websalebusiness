using ProjectWebSaleLand.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace ProjectWebSaleLand.Areas.ClientSite.Controllers
{
    public class HQController : Controller
    {
        public UserSession CurrentUser
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["UserClient"] != null)
                    return (UserSession)System.Web.HttpContext.Current.Session["UserClient"];
                else
                    return new UserSession();
            }
        }
    }
}