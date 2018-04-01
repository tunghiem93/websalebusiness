using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ProjectWebSaleLand.Areas.Administration.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult Logout()
        {
            try
            {
                if (Session["User"] == null)
                    return RedirectToAction("Login", new { area = "" });

                FormsAuthentication.SignOut();
                Session.Remove("User");

                return RedirectToAction("Index", "Home", new { area = "" });
            }
            catch (Exception ex)
            {
                NSLog.Logger.Error("Logout Error: ", ex);
                return new HttpStatusCodeResult(400, ex.Message);
            }
        }
    }
}