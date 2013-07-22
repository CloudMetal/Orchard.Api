using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Api.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;
using Orchard.Localization;
using Orchard.Logging;

namespace Orchard.Api.Handlers
{
    public class ApiSettingsPartHandler : ContentHandler
    {
        public ApiSettingsPartHandler(IRepository<ApiSettingsPartRecord> repository) {
            Filters.Add(StorageFilter.For(repository));

            Filters.Add(new ActivatingFilter<ApiSettingsPart>("Site"));

            T = NullLocalizer.Instance;
            Logger = NullLogger.Instance;
        }

        public Localizer T { get; set; }
        public new ILogger Logger { get; set; }

        protected override void GetItemMetadata(GetContentItemMetadataContext context)
        {
            if (context.ContentItem.ContentType != "Site")
                return;
            base.GetItemMetadata(context);
            context.Metadata.EditorGroupInfo.Add(new GroupInfo(T("Api")));
        }
    }
}