using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using Orchard.Security;
using Orchard.Users.Events;

namespace Orchard.Api.Security
{
    public class BasicAuthenticationSecurityProvider : IApiAuthorizationProvider
    {
        private IUser ValidateLogOn(IMembershipService membershipService, string userNameOrEmail, string password)
        {
            bool validate = !String.IsNullOrEmpty(userNameOrEmail);

            if (String.IsNullOrEmpty(password))
            {
                validate = false;
            }

            if (!validate)
                return null;

            var user = membershipService.ValidateUser(userNameOrEmail, password);

            return user;
        }

        public bool Authorize(System.Web.Http.Controllers.HttpActionContext actionContext) {
            bool isAuthorized = false;
            var workContext = actionContext.ControllerContext.GetWorkContext();
            var membershipService = workContext.Resolve<IMembershipService>();
            var authenticationService = workContext.Resolve<IAuthenticationService>();
            var userEventHandlers = workContext.Resolve<IEnumerable<IUserEventHandler>>();

            var authorization = actionContext.Request.Headers.Authorization;
            if (authorization != null && authorization.Scheme.ToLower().Equals("basic"))
            {
                var credentials = Encoding.ASCII.GetString(Convert.FromBase64String(authorization.Parameter));
                if (!string.IsNullOrEmpty(credentials))
                {
                    var credentialParts = credentials.Split(new[] { ':' }, 2);
                    if (credentialParts.Length == 2)
                    {
                        var userName = credentialParts[0];
                        var password = credentialParts[1];

                        // now check for authorization...
                        var user = ValidateLogOn(membershipService, userName, password);
                        if (user != null)
                        {
                            authenticationService.SignIn(user, true);

                            foreach (var userHandler in userEventHandlers)
                            {
                                userHandler.LoggedIn(user);
                            }

                            isAuthorized = true;
                        }
                    }
                }
            }
            return isAuthorized;
        }
    }
}