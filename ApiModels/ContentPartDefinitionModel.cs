using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Orchard.ContentManagement.MetaData.Models;

namespace Orchard.Api.ApiModels
{
    public class ContentPartDefinitionModel
    {
        public ContentPartDefinitionModel() {
            Fields = new List<ContentFieldDefinitionModel>();
            Settings = new Dictionary<string, string>();
        }

        public ContentPartDefinitionModel(ContentPartDefinition contentPartDefinition) {
            Fields = new List<ContentFieldDefinitionModel>();
            Settings = new Dictionary<string, string>(contentPartDefinition.Settings);
            Name = contentPartDefinition.Name;
        }

        public ContentPartDefinitionModel(ContentTypePartDefinition typePartDefinition) {
            Fields = new List<ContentFieldDefinitionModel>();
            Settings = new Dictionary<string, string>(typePartDefinition.Settings);
            Name = typePartDefinition.PartDefinition.Name;

            foreach (var field in typePartDefinition.PartDefinition.Fields) {
                Fields.Add(new ContentFieldDefinitionModel(field));
            }
        }

        [Required]
        public string Name { get; set; }

        public IList<ContentFieldDefinitionModel> Fields { get; set; }

        public Dictionary<string, string> Settings { get; set; } 
    }
}