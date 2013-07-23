using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Orchard.Api.ApiModels;
using Orchard.Api.Filters;
using Orchard.ContentManagement.MetaData;

namespace Orchard.Api.Controllers.Api
{
    [ApiAuthorizationFilter]
    public class ContentTypeDefinitionApiController : ApiController {
        private readonly IContentDefinitionManager _contentDefinitionManager;
        public ContentTypeDefinitionApiController(IContentDefinitionManager contentDefinitionManager) {
            _contentDefinitionManager = contentDefinitionManager;
        }

        [Queryable]
        public IQueryable<ContentTypeDefinitionModel> GetAll() {
            var contentTypeDefinitions = _contentDefinitionManager.ListTypeDefinitions().Select(definition => new ContentTypeDefinitionModel(definition)).ToList();
            return contentTypeDefinitions.AsQueryable();
        }

        public ContentTypeDefinitionModel Get(string name) {
            ContentTypeDefinitionModel model = null;
            var contentTypeDefinition = _contentDefinitionManager.GetTypeDefinition(name);
            if (contentTypeDefinition != null) {
                model = new ContentTypeDefinitionModel(contentTypeDefinition);
            }
            return model;
        } 
    }
}