using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace VCAS
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }

    public class CustomAuthorize : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "Home/AccessDenied" }));
            }
        }
    }

    public class SessionTimeoutAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctx = HttpContext.Current;
            if (HttpContext.Current.Session["u"] == null)
            {
                /// this handles session when data is requested through Ajax json
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    JsonResult result = new JsonResult { Data = "Session Timeout!" };
                    filterContext.Result = result;
                }
                else
                {
                    /// If session is expired then redirected to logout page which further redirect to login page.                     
                    filterContext.Result = new RedirectResult("~/Account/Logout");
                    return;
                }
            }
        }
    }

}
