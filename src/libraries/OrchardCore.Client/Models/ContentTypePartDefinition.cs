using Newtonsoft.Json.Linq;

namespace OrchardCore.Client.Models
{
    public class ContentTypePartDefinition : ContentDefinition
    {
        public ContentTypePartDefinition(string name, ContentPartDefinition contentPartDefinition, JObject settings)
        {
            Name = name;
            PartDefinition = contentPartDefinition;
            Settings = settings;
        }

        public ContentPartDefinition PartDefinition { get; private set; }
        public ContentTypeDefinition ContentTypeDefinition { get; set; }
    }
}