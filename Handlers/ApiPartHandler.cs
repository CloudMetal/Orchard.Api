using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.Api.Models;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;

namespace Orchard.Api.Handlers
{
    public class ApiPartHandler : ContentHandler
    {
        public ApiPartHandler(IRepository<ApiPartRecord> repository) {
            Filters.Add(StorageFilter.For(repository));
        }
    }
}