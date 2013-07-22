using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Orchard.ContentManagement.MetaData.Models;

namespace Orchard.Api.ApiModels
{
    public class ContentFieldDefinitionModel {
        public ContentFieldDefinitionModel() {}

        public ContentFieldDefinitionModel(ContentFieldDefinition contentFieldDefinition) {
            Name = contentFieldDefinition.Name;
            DisplayName = contentFieldDefinition.Name;
            Settings = new Dictionary<string, string>();
        }

        public ContentFieldDefinitionModel(ContentPartFieldDefinition partFieldDefinition) {
            Name = partFieldDefinition.Name;
            DisplayName = partFieldDefinition.DisplayName;
            Settings = new Dictionary<string, string>(partFieldDefinition.Settings);
        }

        [Required]
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public Dictionary<string, string> Settings { get; set; } 
    }
}