using System.Web.Http;
using Microsoft.AspNet.Identity;

namespace OwinIdentitySqlServerRepository
{
    public class WebApiController : ApiController
    {
        public IHttpActionResult GetErrorResultOrOk(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }
            if (result.Succeeded) return Ok();
            if (result.Errors != null)
            {
                foreach (string error in result.Errors)
                {
                    ModelState.AddModelError("", error);
                }
            }
            if (ModelState.IsValid)
            {
                // No ModelState errors are available to send, 
                // so just return an empty BadRequest.
                return BadRequest();
            }
            return BadRequest(ModelState);
        }
    }
}
