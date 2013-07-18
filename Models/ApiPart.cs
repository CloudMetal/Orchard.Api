using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.ContentManagement;

namespace Orchard.Api.Models
{
    public class ApiPart : ContentPart<ApiPartRecord>
    {
        public bool Enabled {
            get { return Record.Enabled; }
            set { Record.Enabled = value; }
        }
    }
}