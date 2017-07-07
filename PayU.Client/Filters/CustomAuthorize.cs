using System.Net;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Mvc;

namespace PayU.Client.Filters
{
    public class CustomAuthorize : AuthorizeAttribute
    {
        public string Roles { get; set; }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var x = filterContext.HttpContext.User.Identity as ClaimsIdentity;
            base.OnAuthorization(filterContext);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);
        }
    }
}