using System.Linq;
using System.Threading.Tasks;
using Microsoft.Owin.Security.OAuth;

namespace PayU.Client.Providers
{
    public class MvcJwtAuthProvider : OAuthBearerAuthenticationProvider
    {
        public override Task RequestToken(OAuthRequestTokenContext context)
        {
            var token = context.Request.Cookies.SingleOrDefault(x => x.Key == "token").Value;
            context.Token = token;
            return base.RequestToken(context);
        }

        public override Task ValidateIdentity(OAuthValidateIdentityContext context)
        {
            var identity = context.Ticket.Identity.Claims;
            return base.ValidateIdentity(context);
        }
    }
}