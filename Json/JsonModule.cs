using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Autofac;
using Newtonsoft.Json;

namespace Orchard.Api.Json
{
    public class JsonModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // assign global configuration setting....
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new ContentItemRecordConverter());
            //GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        }
    }
}