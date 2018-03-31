using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectWebSaleLand.Areas.Administration.Controllers
{
    public class LandController : BaseController
    {
        // GET: Administration/Land
        public ActionResult Index()
        {
            return View();
        }
    }
}