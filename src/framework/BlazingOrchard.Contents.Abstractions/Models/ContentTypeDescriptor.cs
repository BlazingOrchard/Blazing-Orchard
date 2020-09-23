using System.Collections.Generic;

namespace BlazingOrchard.Contents.Models
{
    public class ContentTypeDescriptor
    {
        public string Name { get; set; } = default!;
        public ICollection<ContentTypePartDescriptor> Parts { get; set; } = default!;
    }
}