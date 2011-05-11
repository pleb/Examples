using System.Web.Mvc;

namespace NinjectExample.Web.Controllers
{
    public class WithoutController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}