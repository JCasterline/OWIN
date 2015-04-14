using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity.Owin;
using OwinIdentitySqlServerRepository.DataAccess.Models;
using OwinIdentitySqlServerRepository.Identity;

//using Claim = System.Security.Claims.Claim;

namespace OwinIdentitySqlServerRepository.Controllers
{
    [RoutePrefix("api/Account")]
    public class AccountController : WebApiController
    {
        public ApplicationSignInManager SignInManager
        {
            get { return Request.GetOwinContext().Get<ApplicationSignInManager>(); }
        }

        public AccountController()
        {

        }

        //POST api/Account/Register
        [HttpPost]
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await SignInManager.UserManager.CreateAsync(user, "Password123!");
            return GetErrorResultOrOk(result);
        }

        //POST api/Account/Register
        [HttpPost]
        [AllowAnonymous]
        [Route("Create")]
        public async Task<IHttpActionResult> Create()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await SignInManager.UserManager.CreateAsync(new User(){UserName = "Test", Email = "Email@company.com", Name = "Test User"}, "Password123!");

            return GetErrorResultOrOk(result);
        }
    }
}