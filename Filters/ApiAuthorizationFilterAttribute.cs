using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;
using Orchard.Api.Security;

namespace Orchard.Api.Filters
{
    public class ApiAuthorizationFilterAttribute : AuthorizationFilterAttribute {

        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext) {
            bool isAuthorized = false;

            var workContext = actionContext.ControllerContext.GetWorkContext();
            var apiSecurityProviders = workContext.Resolve<IEnumerable<IApiAuthorizationProvider>>();

            foreach (var apiAuthorizationProvider in apiSecurityProviders) {
                isAuthorized = apiAuthorizationProvider.Authorize(actionContext);
                if (isAuthorized)
                    break;
            }

            if (!isAuthorized) {
                actionContext.Response = actionContext.ControllerContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
        }
    }
}