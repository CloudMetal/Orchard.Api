using System;
using System.Collections.Generic;
using System.Data;
using Orchard.ContentManagement.Drivers;
using Orchard.ContentManagement.MetaData;
using Orchard.ContentManagement.MetaData.Builders;
using Orchard.Core.Contents.Extensions;
using Orchard.Data.Migration;

namespace Orchard.Api
{
    public class Migrations : DataMigrationImpl
    {

        public int Create()
        {
            SchemaBuilder.CreateTable("ApiPartRecord", table => table
                .ContentPartRecord()
                .Column<bool>("Enabled")
                );

            ContentDefinitionManager.AlterPartDefinition("ApiPart", p => p
                .Attachable()
                .WithDescription("Registers a content type for access via REST API."));


            return 1;
        }
    }
}