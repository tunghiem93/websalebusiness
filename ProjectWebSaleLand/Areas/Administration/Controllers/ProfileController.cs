using ProjectWebSaleLand.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectWebSaleLand.Areas.Administration.Controllers
{
    public class ProfileController : HQController
    {
        public ActionResult Index()
        {
            if (Session["User"] == null)
                return RedirectToAction("Index", "Home");
            //UserFactory userFactory = new UserFactory();
            UserModels user = new UserModels();//userFactory.GetUserInfoById(((UserSession)Session["User"]).UserID);

            return View(CurrentUser);
        }
    }
}