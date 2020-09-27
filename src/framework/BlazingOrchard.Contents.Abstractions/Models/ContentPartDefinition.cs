using System.Collections.Generic;

namespace BlazingOrchard.Contents.Models
{
    public class ContentPartDefinition : ContentDefinition
    {
        public ICollection<ContentPartFieldDefinition> Fields { get; set; } = new List<ContentPartFieldDefinition>();
    }
}