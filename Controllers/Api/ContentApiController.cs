using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Reflection;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Orchard.Api.Filters;
using Orchard.ContentManagement;

namespace Orchard.Api.Controllers.Api
{
    [ApiAuthorizationFilter]
    public class ContentApiController : ApiController {
        private readonly IContentManager _contentManager;
        public ContentApiController(IContentManager contentManager) {
            _contentManager = contentManager;
        }

        // get all content of a certain type...
        public HttpResponseMessage GetContent(string contentType) {
            HttpResponseMessage message = null;
            var contentQuery = _contentManager.Query(contentType);

            var contentArray = new JArray();

            foreach (var contentItem in contentQuery.List()) {
                contentArray.Add(SerializeContentItem(contentItem));
            }

            message = Request.CreateResponse(HttpStatusCode.OK);
            message.Content = new ObjectContent<JArray>(contentArray, new JsonMediaTypeFormatter());
            return message;
        }

        // get content directly by id...
        public HttpResponseMessage Get(int id) {
            var contentItem = _contentManager.Get(id);

            if (contentItem == null)
                return new HttpResponseMessage(HttpStatusCode.NotFound);

            var message = Request.CreateResponse(HttpStatusCode.OK);
            message.Content = new ObjectContent<JObject>(SerializeContentItem(contentItem), new JsonMediaTypeFormatter());
            return message;
        }

        protected JObject SerializeContentItem(ContentItem item) {
            var jsonItem = new JObject();
            jsonItem.Add(new JProperty("Id", item.Id));
            jsonItem.Add(new JProperty("ContentType",  item.ContentType));
            jsonItem.Add(new JProperty("Version", item.Version));

            var partsArray = new JArray();
            foreach (var part in item.Parts) {
                partsArray.Add(SerializePart(part));
            }

            jsonItem.Add("Parts", partsArray);

            return jsonItem;
        }

        protected JObject SerializePart(ContentPart part) {
            JObject partObject = null;

            // see if we have a Record property...
            var recordPropertyInfo = part.GetType().GetProperty("Record");
            if (recordPropertyInfo != null) {
                var serializer = new JsonSerializer {ReferenceLoopHandling = ReferenceLoopHandling.Ignore};

                // get the value and serialize it...
                partObject = JObject.FromObject(recordPropertyInfo.GetValue(part, BindingFlags.GetProperty, null, null, null),
                    serializer);
            }

            if (partObject == null) {
                partObject = new JObject();
            }

            // now add the fields to the json object....

            return partObject;
        }

        protected JObject SerializeField(ContentField field) {
            JObject fieldObject = null;

            return fieldObject;
        }
    }
}