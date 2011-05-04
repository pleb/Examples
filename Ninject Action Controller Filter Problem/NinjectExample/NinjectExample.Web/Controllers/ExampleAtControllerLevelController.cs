using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NinjectExample.Web.Code;

namespace NinjectExample.Web.Controllers
{
    [LocalOnlyRequest]
    public class ExampleAtControllerLevelController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
