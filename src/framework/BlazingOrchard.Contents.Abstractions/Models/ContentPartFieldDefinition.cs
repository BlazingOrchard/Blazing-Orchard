namespace BlazingOrchard.Contents.Models
{
    public class ContentPartFieldDefinition : ContentDefinition
    {
        public ContentFieldDefinition FieldDefinition { get; set; } = default!;
        public ContentPartDefinition PartDefinition { get; set; } = default!;
    }
}