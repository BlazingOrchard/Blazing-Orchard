using BlazingOrchard.Contents.Models;
using BlazingOrchard.DisplayManagement.Models;

namespace BlazingOrchard.Contents.Display.Models
{
    public class BuildFieldDisplayContext : BuildDisplayContext
    {
        public BuildFieldDisplayContext(
            ContentPart contentPart,
            ContentTypePartDefinition typePartDefinition,
            ContentPartFieldDefinition partFieldDefinition,
            BuildDisplayContext context) : base(context)
        {
            ContentPart = contentPart;
            TypePartDefinition = typePartDefinition;
            PartFieldDefinition = partFieldDefinition;
        }

        public ContentPart ContentPart { get; }
        public ContentTypePartDefinition TypePartDefinition { get; }
        public ContentPartFieldDefinition PartFieldDefinition { get; }
    }
}