using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ActionExcuted]
    public abstract class BaseAdminController : Controller
    {
        public DataClassesDataContext data = new DataClassesDataContext();
    }

    public class ActionExcutedAttribute : ActionFilterAttribute
    {
        bool hasAdminLoginSession = false;
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            hasAdminLoginSession = checkAdminLoginSession(context);
            base.OnActionExecuting(context);
        }
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
            bool isCurrentLyInLoginPage = filterContext.ActionDescriptor.ActionName.Equals("Login");
            if (!hasAdminLoginSession && !isCurrentLyInLoginPage)
            {
                //Redirect to page Login Admin
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "Admin",
                    action = "Login"
                }));
            }
        }

        private bool checkAdminLoginSession(ActionExecutingContext context)
        {
            Object session = context.HttpContext.Session[Constants.KEY_SESSION_ADMIN_USERNAME];
            if (session != null && !String.IsNullOrEmpty(session.ToString()))
            {
                return true;
            }
            return false;
        }
    }
}