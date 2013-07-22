using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.ContentManagement;

namespace Orchard.Api.Models
{
    public class ApiSettingsPart : ContentPart<ApiSettingsPartRecord>
    {
        public bool Enabled {
            get { return Record.Enabled; }
            set { Record.Enabled = value; }
        }
    }
}