namespace BlazingOrchard.Contents.Models
{
    public class ContentTypePartDescriptor
    {
        public string Name { get; set; } = default!;
        public ContentPartDescriptor Part { get; set; } = default!;
        public ContentTypePartSettings Settings { get; set; } = default!;
    }
}