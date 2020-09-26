namespace BlazingOrchard.Contents.Models
{
    public class ContentPartFieldSettings
    {
        /// <summary>
        /// Gets or sets the position of the part field in the editor.
        /// </summary>
        public string Position { get; set; } = default!;
        
        /// <summary>
        /// Gets or sets the display mode of the part field.
        /// </summary>
        public string? DisplayMode { get; set; }
    }
}