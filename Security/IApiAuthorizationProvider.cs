using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http.Controllers;

namespace Orchard.Api.Security
{
    public interface IApiAuthorizationProvider : IDependency {
        bool Authorize(HttpActionContext actionContext);
    }
}
