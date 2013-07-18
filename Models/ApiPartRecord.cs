using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.ContentManagement.Records;

namespace Orchard.Api.Models
{
    public class ApiPartRecord : ContentPartRecord
    {
        public virtual bool Enabled { get; set; }
    }
}