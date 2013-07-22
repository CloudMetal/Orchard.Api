using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Orchard.ContentManagement.MetaData.Models;

namespace Orchard.Api.ApiModels
{
    public class ContentTypeDefinitionModel
    {
        public ContentTypeDefinitionModel() {
            Parts = new List<ContentPartDefinitionModel>();
            Settings = new Dictionary<string, string>();
        }

        public ContentTypeDefinitionModel(ContentTypeDefinition contentTypeDefinition) {
            Parts = new List<ContentPartDefinitionModel>();
            Settings = new Dictionary<string, string>(contentTypeDefinition.Settings);

            foreach (var partDefinitionModel in contentTypeDefinition.Parts.Select(partDefinition => new ContentPartDefinitionModel(partDefinition)))
            {
                Parts.Add(partDefinitionModel);
            }

            Name = contentTypeDefinition.Name;
            DisplayName = contentTypeDefinition.DisplayName;
        }

        [Required]
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public IList<ContentPartDefinitionModel> Parts { get; set; }

        public Dictionary<string, string> Settings { get; set; } 
    }
}