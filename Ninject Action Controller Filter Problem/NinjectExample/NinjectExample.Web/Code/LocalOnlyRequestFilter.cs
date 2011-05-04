using System.Web.Mvc;

namespace NinjectExample.Web.Code
{
    public class LocalOnlyRequestFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (!filterContext.RequestContext.HttpContext.Request.IsLocal)
            {
                throw new LocalOnlyRequestException();
            }
        }
    }
}