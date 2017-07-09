using System.Web.Mvc;

namespace PayU.Client.Filters
{
    public class TryAddJwtTokenToHeader : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var token = filterContext.HttpContext.Request.Cookies.Get("token");

            if (token != null)
                filterContext.HttpContext.Request.Headers.Add("Authorization", $"Bearer {token}");

            base.OnActionExecuting(filterContext);
        }
    }
}