using System;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using OwinIdentityAuthentication.Authorization;

namespace OwinIdentityAuthentication.ResourceAuthorization
{
    public class ResourceAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly string _action;
        private readonly string[] _resources;

        public ResourceAuthorizeAttribute()
        {
        }

        public ResourceAuthorizeAttribute(string action, params string[] resources)
        {
            _action = action;
            _resources = resources;
        }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            if (!string.IsNullOrWhiteSpace(_action))
            {
                return CheckAccess(_action, _resources);
            }

            var actionName = actionContext.ActionDescriptor.ActionName;
            var controllerName = actionContext.ControllerContext.ControllerDescriptor.ControllerName;
            var result = CheckAccess(actionName, controllerName);
            return result;
        }

        protected virtual bool CheckAccess(string action, params string[] resources)
        {
            if (resources.Length == 0)
                resources = new[] { string.Empty };
            var task = Task.FromResult(AuthorizationManager.CheckAccess(action, resources));
            if (task.Wait(5000))
                return task.Result;
            throw new TimeoutException();
        }
    }
}
