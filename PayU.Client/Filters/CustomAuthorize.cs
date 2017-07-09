using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace PayU.Client.Filters
{
    public class CustomAuthorize : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var token = filterContext.HttpContext.Request.Cookies.Get("token");

            if (token != null)
                filterContext.HttpContext.Request.Headers.Add("Authorization", $"Bearer {token}");

           
            base.OnAuthorization(filterContext);
        }


        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var x = httpContext.User.Identity as ClaimsIdentity;

            return base.AuthorizeCore(httpContext);
        }
    }
}