using Newtonsoft.Json.Linq;

namespace OrchardCore.Client.Models
{
    public class ContentPartFieldDefinition : ContentDefinition
    {
        public ContentPartFieldDefinition(ContentFieldDefinition contentFieldDefinition, string name, JObject settings)
        {
            Name = name;
            FieldDefinition = contentFieldDefinition;
            Settings = settings;
        }

        public ContentFieldDefinition FieldDefinition { get; }
        public ContentPartDefinition PartDefinition { get; set; } = default!;
    }
}