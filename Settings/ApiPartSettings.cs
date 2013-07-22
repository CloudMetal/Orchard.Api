using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.ContentManagement.MetaData.Builders;

namespace Orchard.Api.Settings
{
    public class ApiPartSettings
    {
        public string Enabled { get; set; }

        public void Build(ContentTypePartDefinitionBuilder builder)
        {
            builder.WithSetting(
                "ApiPartSettings.Enabled", Enabled);
        }
    }
}