using BlazingOrchard.Contents.Models;
using BlazingOrchard.DisplayManagement.Models;

namespace BlazingOrchard.Contents.Display.Models
{
    public class BuildPartDisplayContext : BuildDisplayContext
    {
        public BuildPartDisplayContext(
            ContentTypePartDescriptor typePartDescriptor,
            BuildDisplayContext context) : base(context) =>
            TypePartDescriptor = typePartDescriptor;

        public ContentTypePartDescriptor TypePartDescriptor { get; }
    }
}