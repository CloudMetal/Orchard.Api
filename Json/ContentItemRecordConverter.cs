using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Newtonsoft.Json.Converters;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Records;

namespace Orchard.Api.Json
{
    /* sample code at http://stackoverflow.com/questions/14893614/json-net-serialize-dictionary-as-part-of-parent-object 
     http://stackoverflow.com/questions/12314438/self-referencing-loop-in-json-net-jsonserializer-from-custom-jsonconverter-web 
     http://code.msdn.microsoft.com/Loop-Reference-handling-in-caaffaf7 */

    public class ContentItemRecordConverter : CustomCreationConverter<ContentItemRecord>
    {
        public override ContentItemRecord Create(Type objectType)
        {
            return new ContentItemRecord();
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteStartObject();

            // Write properties.
            var propertyInfos = value.GetType().GetProperties();
            foreach (var propertyInfo in propertyInfos)
            {
                // Skip the Versions property.
                if (propertyInfo.Name == "Versions")
                    continue;

                writer.WritePropertyName(propertyInfo.Name);
                var propertyValue = propertyInfo.GetValue(value, BindingFlags.GetProperty, null, null, null);
                serializer.Serialize(writer, propertyValue);
            }

            writer.WriteEndObject();
        }

        public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            return base.ReadJson(reader, objectType, existingValue, serializer);
        }

        public override bool CanWrite
        {
            get { return true; }
        }
    }
}