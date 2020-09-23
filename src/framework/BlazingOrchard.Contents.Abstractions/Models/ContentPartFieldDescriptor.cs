namespace BlazingOrchard.Contents.Models
{
    public class ContentPartFieldDescriptor
    {
        public string Name { get; set; } = default!;
        public ContentFieldDescriptor Field { get; set; } = default!;
    }
}