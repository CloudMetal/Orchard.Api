using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.ContentManagement;
using Orchard.ContentManagement.MetaData;
using Orchard.ContentManagement.MetaData.Builders;
using Orchard.ContentManagement.MetaData.Models;
using Orchard.ContentManagement.ViewModels;

namespace Orchard.Api.Settings
{
    public class ApiPartSettingsEvents : ContentDefinitionEditorEventsBase
    {
        public override IEnumerable<TemplateViewModel> TypePartEditor(ContentTypePartDefinition definition)
        {
            if (definition.PartDefinition.Name != "ApiPart")
                yield break;

            var settings = definition.Settings.GetModel<ApiPartSettings>();

            yield return DefinitionTemplate(settings);
        }

        public override IEnumerable<TemplateViewModel> TypePartEditorUpdate(ContentTypePartDefinitionBuilder builder, IUpdateModel updateModel)
        {
            if (builder.Name != "ApiPart")
                yield break;

            var settings = new ApiPartSettings
            {
                Enabled = "true"
            };

            if (updateModel.TryUpdateModel(settings, "ApiPartSettings", null, null))
            {
                settings.Build(builder);
            }

            yield return DefinitionTemplate(settings);
        }
    }
}