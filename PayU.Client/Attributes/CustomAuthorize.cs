using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;
using AuthorizationContext = System.Web.Mvc.AuthorizationContext;


namespace PayU.Client.Attributes
{
    public class ClaimsAuthorizationAttribute : AuthorizeAttribute
    {
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var principal = filterContext.RequestContext.HttpContext.User as ClaimsPrincipal; 

            if (!principal.Identity.IsAuthenticated)
            {
                
               
            }

            if (!principal.HasClaim(x => x.Type == ClaimType && x.Value == ClaimValue))
            {
              
            }
        }
    }
}