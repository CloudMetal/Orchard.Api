using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Orchard.Api.Filters;

namespace Orchard.Api.Controllers.Api
{
    [ApiAuthorizationFilter]
    public class ContentPartDefinitionApiController : ApiController
    {
    }
}