using System.Collections.Generic;

namespace BlazingOrchard.Contents.Models
{
    public class ContentTypeDefinition : ContentDefinition
    {
        public ICollection<ContentTypePartDefinition> Parts { get; set; } = new List<ContentTypePartDefinition>();
    }
}