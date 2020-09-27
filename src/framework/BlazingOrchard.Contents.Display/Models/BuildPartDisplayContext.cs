using BlazingOrchard.Contents.Models;
using BlazingOrchard.DisplayManagement.Models;

namespace BlazingOrchard.Contents.Display.Models
{
    public class BuildPartDisplayContext : BuildDisplayContext
    {
        public BuildPartDisplayContext(
            ContentTypePartDefinition typePartDefinition,
            BuildDisplayContext context) : base(context) =>
            TypePartDefinition = typePartDefinition;

        public ContentTypePartDefinition TypePartDefinition { get; }
    }
}