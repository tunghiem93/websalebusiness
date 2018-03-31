using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectWebSaleLand.Areas.Administration.Controllers
{
    public class HouseController : BaseController
    {
        // GET: Administration/House
        public ActionResult Index()
        {
            return View();
        }
    }
}