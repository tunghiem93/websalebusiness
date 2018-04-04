using Newtonsoft.Json;
using ProjectWebSaleLand.Shared.Factory.CustomerFactory;
using ProjectWebSaleLand.Shared.Models;
using ProjectWebSaleLand.Shared.Utilities;
using ProjectWebSaleLane.Shared.Model.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace ProjectWebSaleLand.Areas.ClientSite.Controllers
{
    public class LoginController : Controller
    {
        private CustomerFactory _factory = null;
        public LoginController()
        {
            _factory = new CustomerFactory();
        }
        // GET: ClientSite/Login
        public ActionResult SignIn()
        {
            ClientLoginModel model = new ClientLoginModel();
            return View(model);
        }

        public ActionResult Logout()
        {
            try
            {
                HttpCookie cookie = new HttpCookie("UserClientCookie");
                HttpContext.Response.Cookies.Remove("UserClientCookie");
                HttpContext.Response.SetCookie(cookie);

                if (Session["UserClient"] == null)
                    return RedirectToAction("Index", "Home");

                FormsAuthentication.SignOut();
                Session.Remove("UserClient");
                return RedirectToAction("Index", "Home");
            }
            catch(Exception ex)
            {
                NSLog.Logger.Error("Logout ", ex);
                return new HttpStatusCodeResult(400, ex.Message);
            }
        }

        [HttpPost]
        public ActionResult SignIn(ClientLoginModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            model.Password = CommonHelper.Encrypt(model.Password);
            var result = _factory.Login(model);
            if(result != null)
            {
                UserSession userSession = new UserSession();
                userSession.Email = result.Email;
                userSession.UserName = result.DisplayName;
                Session.Add("UserClient", userSession);
                string myObjectJson = JsonConvert.SerializeObject(userSession);  //new JavaScriptSerializer().Serialize(userSession);
                HttpCookie cookie = new HttpCookie("UserClientCookie");
                cookie.Expires = DateTime.Now.AddMonths(1);
                cookie.Value = Server.UrlEncode(myObjectJson);
                HttpContext.Response.Cookies.Add(cookie);
                return RedirectToAction("Index", "Home");

            }
            else
            {
                ModelState.AddModelError("Email", "Thông tin tài khoản không chính xác");
                return View(model);
            }
        }

        public ActionResult SignUp()
        {
            return View();
        }
    }
}