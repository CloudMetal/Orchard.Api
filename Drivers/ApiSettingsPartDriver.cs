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
    }
}