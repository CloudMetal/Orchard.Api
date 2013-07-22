using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Api.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using Orchard.ContentManagement.Handlers;
using Orchard.Localization;

namespace Orchard.Api.Drivers
{
    public class ApiSettingsPartDriver : ContentPartDriver<ApiSettingsPart>
    {
        private const string TemplateName = "Parts/ApiSettings";

        public IOrchardServices Services { get; set; }
        public Localizer T { get; set; }

        protected override string Prefix
        {
            get { return "ApiSettingsPart"; }
        }

        protected override DriverResult Editor(ApiSettingsPart part, dynamic shapeHelper)
        {
            return ContentShape("Parts_ApiSettings_Edit", () =>
            {
                return shapeHelper.EditorTemplate(TemplateName: TemplateName, Model: part, Prefix: Prefix);
            });
        }

        protected override DriverResult Editor(
            ApiSettingsPart part, IUpdateModel updater, dynamic shapeHelper)
        {

            updater.TryUpdateModel(part, Prefix, null, null);
            return Editor(part, shapeHelper);
        }

        protected override void Importing(ApiSettingsPart part, ImportContentContext context)
        {
            var enabled = context.Attribute(part.PartDefinition.Name, "Enabled");
            if (enabled != null)
            {
                part.Enabled = Convert.ToBoolean(enabled);
            }
        }

        protected override void Exporting(ApiSettingsPart part, ExportContentContext context)
        {
            context.Element(part.PartDefinition.Name).SetAttributeValue("Enabled", part.Enabled ? "true" : "false");
        }
    }
}