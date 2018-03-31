
//using ProjectWebSaleLand.Shared.Models;
using ProjectWebSaleLand.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ProjectWebSaleLand.Web.App_Start
{
    public class NuAuthAttribute : AuthorizeAttribute
    {
        private UserSession _CurrentUser;

        private string ActionType;

        private string Controller;
        private string Action;

        private List<string> _Views = new List<string> { "index", "default", "view", "detail", "get", "load", "filter", "search", "apply" };
        private List<string> _ControllerDenies = new List<string> { "ACModule", "IngIngredients" };

        private List<String> _ViewTimeoutSession = new List<string>
        {
            "LoadIngredient", "LoadIngredientIngredient",       //Ingrident
            "AddTab", "AddDishes", "CheckDish",                 //SetMenu 
            "AddModifiers","LoadModifiers","CheckModifier",      //Dish
            "LoadDetail",
            "AddSubPayment"                                 //Payment
        };

        /*Factory*/

        public NuAuthAttribute()
        {
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (HttpContext.Current.Session["User"] == null)
                _CurrentUser = new UserSession();
            else
                _CurrentUser = (UserSession)HttpContext.Current.Session["User"];
                        
            //Alias Controller //Action
            Controller = httpContext.Request.RequestContext.RouteData.Values["controller"].ToString();
            Action = httpContext.Request.RequestContext.RouteData.Values["action"].ToString().ToLower();

            bool isViewAction = _Views.Any(s => Action.Contains(s));
            ActionType = isViewAction ? "View" : "Action";

            return IsPermission();
        }

        protected bool IsPermission()
        {
            try
            {
                // If user not logged in, require login
                if (!_CurrentUser.IsAuthenticated)
                    return false;
                else
                {
                    if (_CurrentUser.IsSuperAdmin || Controller.ToLower().Equals("home"))
                    {
                        return true;
                    }
                    bool IsModPer = true;
                    return IsModPer;

                }

            }
            catch (Exception e)
            {
                string error = e.ToString();
                return false;
            }
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (!_CurrentUser.IsAuthenticated)
            {
                string controller = filterContext.RouteData.Values["controller"].ToString();
                string action = filterContext.RouteData.Values["action"].ToString();
                bool isChange = false;
                if (_ViewTimeoutSession.Contains(action))
                {
                    isChange = true;
                }
                if (isChange) //TimeoutSession
                {
                    filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(
                        new
                        {
                            controller = "Error",
                            action = "TimeOutSession",
                            area = string.Empty,
                        })
                    );
                }
                else //Login
                {
                    filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(
                        new
                        {
                            controller = "Login",
                            action = "Index",
                            area = string.Empty,
                            isAjax = filterContext.HttpContext.Request.IsAjaxRequest(),
                            returnUrl = filterContext.HttpContext.Request.Url.ToString().Replace("/Report", "")
                        })
                    );
                }
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(
                        new
                        {
                            controller = "Error",
                            action = "Unauthorised",
                            area = string.Empty,
                        })
                    );
            }
        }
    }
}