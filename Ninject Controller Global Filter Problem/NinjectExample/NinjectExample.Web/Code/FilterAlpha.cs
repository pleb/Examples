using System.Web.Mvc;

namespace NinjectExample.Web.Code
{
    public class FilterAlpha : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            filterContext.Controller.ViewData["alpha"] = "here";
        }
    }
}