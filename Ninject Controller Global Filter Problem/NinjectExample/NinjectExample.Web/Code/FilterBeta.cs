using System;
using System.Web.Mvc;

namespace NinjectExample.Web.Code
{
    public class FilterBeta : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {

        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            filterContext.Controller.ViewData["beta"] = "here";
        }
    }
}