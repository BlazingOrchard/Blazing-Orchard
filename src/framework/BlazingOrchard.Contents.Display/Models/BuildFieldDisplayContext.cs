using BlazingOrchard.Contents.Models;
using BlazingOrchard.DisplayManagement.Models;

namespace BlazingOrchard.Contents.Display.Models
{
    public class BuildFieldDisplayContext : BuildDisplayContext
    {
        public BuildFieldDisplayContext(
            ContentTypePartDescriptor typePartDescriptor,
            ContentPartFieldDescriptor partFieldDescriptor,
            BuildDisplayContext context) : base(context)
        {
            TypePartDescriptor = typePartDescriptor;
            PartFieldDescriptor = partFieldDescriptor;
        }

        public ContentTypePartDescriptor TypePartDescriptor { get; }
        public ContentPartFieldDescriptor PartFieldDescriptor { get; }
    }
}