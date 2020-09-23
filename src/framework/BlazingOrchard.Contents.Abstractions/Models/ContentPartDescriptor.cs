using System.Collections.Generic;

namespace BlazingOrchard.Contents.Models
{
    public class ContentPartDescriptor
    {
        public string Name { get; set; } = default!;
        public ICollection<ContentPartFieldDescriptor> Fields { get; set; } = default!;
    }
}