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
    public class ApiPartDriver : ContentPartDriver<ApiPart>
    {
        private const string TemplateName = "Parts/Api";

        public IOrchardServices Services { get; set; }
        public Localizer T { get; set; }

        protected override string Prefix
        {
            get { return "ApiPart"; }
        }

        protected override DriverResult Display(ApiPart part, string displayType, dynamic shapeHelper)
        {
            return ContentShape("Parts_Api_SummaryAdmin", () => shapeHelper.Parts_Api_SummaryAdmin());
        }

        protected override DriverResult Editor(ApiPart part, dynamic shapeHelper)
        {
            return ContentShape("Parts_Api_Edit", () =>
            {
                return shapeHelper.EditorTemplate(TemplateName: TemplateName, Model: part, Prefix: Prefix);
            });
        }

        protected override DriverResult Editor(
            ApiPart part, IUpdateModel updater, dynamic shapeHelper)
        {

            updater.TryUpdateModel(part, Prefix, null, null);
            return Editor(part, shapeHelper);
        }

        protected override void Importing(ApiPart part, ImportContentContext context)
        {
            var enabled = context.Attribute(part.PartDefinition.Name, "Enabled");
            if (enabled != null) {
                part.Enabled = Convert.ToBoolean(enabled);
            }
        }

        protected override void Exporting(ApiPart part, ExportContentContext context)
        {
            context.Element(part.PartDefinition.Name).SetAttributeValue("Enabled", part.Enabled ? "true" : "false");
        }
    }
}