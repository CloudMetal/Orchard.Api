using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Newtonsoft.Json.Linq;

namespace Orchard.Api.Filters
{
    public class ValidationActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var modelState = actionContext.ModelState;

            if (!modelState.IsValid)
            {
                dynamic errors = new JArray();

                foreach (var prop in modelState.Values)
                {
                    if (prop.Errors.Any())
                    {
                        errors.Add(prop.Errors.First().ErrorMessage);
                    }
                }

                var response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                response.Content = new ObjectContent<JArray>(errors, new JsonMediaTypeFormatter());
                actionContext.Response = response;
            }
        }
    }
}