using Newtonsoft.Json;
using ProjectWebSaleLand.Shared.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Script.Serialization;

namespace ProjectWebSaleLand
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            
        }

        protected void Application_BeginRequest(Object sender, EventArgs e)
        {
            CultureInfo newCulture = (CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            newCulture.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
            newCulture.DateTimeFormat.DateSeparator = "/";
            Thread.CurrentThread.CurrentCulture = newCulture;

           
        }

        void Application_AcquireRequestState(object sender, EventArgs e)
        {
            HttpCookie _UserClientCookie = Request.Cookies["UserClientCookie"];
            if (_UserClientCookie != null)
            {
                var input = Server.UrlDecode(_UserClientCookie.Value);
                UserSession userSession = JsonConvert.DeserializeObject<UserSession>(input); //new JavaScriptSerializer().Deserialize<UserSession>(input);
                if (userSession != null && HttpContext.Current.Session != null)
                {
                    Session.Add("UserClient", userSession);
                }
            }
        }
    }
}
