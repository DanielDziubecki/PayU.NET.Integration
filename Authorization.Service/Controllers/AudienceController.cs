using System.Web.Http;

namespace AuthorizationService.Controllers
{
    //this will be protected and for example allow for admin only 
    [RoutePrefix("api/audience")]
    public class AudienceController : ApiController
    {
        [Route("")]
        public IHttpActionResult Post(AudienceDto audienceDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newAudience = AudienceStore.AddAudience(audienceDto.Name);
            return Ok(newAudience);
        }
    }
}