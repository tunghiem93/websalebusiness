﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectWebSaleLand.Areas.Administration.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Administration/Customer
        public ActionResult Index()
        {
            return View();
        }
    }
}