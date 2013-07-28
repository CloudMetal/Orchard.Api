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
using Orchard.ContentManagement.FieldStorage;

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
            var partObject = new JObject();

            // see if we have a Record property...
            var recordPropertyInfo = part.GetType().GetProperty("Record");
            if (recordPropertyInfo != null) {

                // get the value and serialize it...
                object val = recordPropertyInfo.GetValue(part, BindingFlags.GetProperty, null, null, null);
                if (val != null) {
                    var serializer = new JsonSerializer { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
                    JObject recordObject = JObject.FromObject(val,
                    serializer);
                    partObject.Add("Record", recordObject);
                }
            }

            // now add the fields to the json object....
            var fieldsArray = new JArray();

            foreach (var contentField in part.Fields) {
                var fieldObject = SerializeField(contentField);
                fieldsArray.Add(fieldObject);
            }

            partObject.Add("Fields", fieldsArray);

            // add the settings...
            var settingsObject = new JObject();

            foreach ( var key in part.Settings.Keys) {
                settingsObject.Add(new JProperty(key, part.Settings[key]));
            }

            partObject.Add("Settings", settingsObject);

            // add the part settings...
            var partSettingsObject = new JObject();

            foreach (string key in part.TypePartDefinition.Settings.Keys) {
                partSettingsObject.Add(new JProperty(key, part.TypePartDefinition.Settings[key]));
            }

            partObject.Add("TypePartDefinition.Settings", partSettingsObject);

            return partObject;
        }

        protected JObject SerializeField(ContentField field) {
            var fieldObject = new JObject();
            fieldObject.Add(new JProperty("Name", field.Name));
            fieldObject.Add(new JProperty("DisplayName", field.DisplayName));
            fieldObject.Add(new JProperty("Value", field.Storage.Get<string>()));

            // add the part settings...
            var fieldSettingsObject = new JObject();

            foreach (string key in field.PartFieldDefinition.Settings.Keys)
            {
                fieldSettingsObject.Add(new JProperty(key, field.PartFieldDefinition.Settings[key]));
            }

            fieldObject.Add("PartFieldDefinition.Settings", fieldSettingsObject);

            return fieldObject;
        }
    }
}