using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Api.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
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

        
    }
}