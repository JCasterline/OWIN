using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using OwinIdentitySqlServerRepository.ResourceAuthorization;

namespace OwinIdentitySqlServerRepository.Controllers
{
    [Authorize]
    public class ValuesController : ApiController
    {
        // GET api/values 
        [ResourceAuthorize("Action", "Resource1", "Resource2")]
        public IEnumerable<string> Get()
        {
            //if (!Request.CheckAccessAsync("Action", "Resource1", "Resource2").Result)
            //    throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.Unauthorized,
            //        "Authorization has been denied for this request."));
            return new[] { "value1", "value2" };
        }

        // GET api/values/5 
        public async Task<string> Get(int id)
        {
            return await Task.FromResult("value"+id);
        }

        // POST api/values 
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5 
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5 
        public void Delete(int id)
        {
        }
    } 
}
