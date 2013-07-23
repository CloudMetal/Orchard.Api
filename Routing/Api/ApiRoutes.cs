using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Orchard.Mvc.Routes;
using Orchard.WebApi.Routes;

namespace Orchard.Api.Routing.Api
{
    public class ApiRoutes : IHttpRouteProvider
    {
        public IEnumerable<Mvc.Routes.RouteDescriptor> GetRoutes()
        {
            return new[]{
				 new HttpRouteDescriptor {
											Name = "ContentTypeApiRoute",
											Priority = 20,
											RouteTemplate = "api/ContentType/{name}",
											Defaults = new	{ area = "Orchard.Api", controller = "ContentTypeDefinitionApi", name = RouteParameter.Optional}		
										},
                new HttpRouteDescriptor {
											Name = "ContentByTypeApiRoute",
											Priority = 10,
											RouteTemplate = "api/{contentType}",
											Defaults = new	{ area = "Orchard.Api", controller = "ContentApi" }		
										},
                new HttpRouteDescriptor {
											Name = "ContentApiRoute",
											Priority = 10,
											RouteTemplate = "api/Content/{id}",
											Defaults = new	{ area = "Orchard.Api", controller = "ContentApi" }		
										},
                new HttpRouteDescriptor {
											Name = "ContentPartApiRoute",
											Priority = 10,
											RouteTemplate = "api/ContentPart/{name}",
											Defaults = new	{ area = "Orchard.Api", controller = "ContentPartDefinitionApi", name = RouteParameter.Optional }		
										},
                new HttpRouteDescriptor {
											Name = "ContentFieldApiRoute",
											Priority = 10,
											RouteTemplate = "api/ContentField/{name}",
											Defaults = new	{ area = "Orchard.Api", controller = "ContentFieldDefinitionApi", name = RouteParameter.Optional }		
										}
			};
        }

        public void GetRoutes(ICollection<Mvc.Routes.RouteDescriptor> routes)
        {
            foreach (RouteDescriptor routeDescriptor in GetRoutes())
            {
                routes.Add(routeDescriptor);
            }
        }
    }
}