namespace BlazingOrchard.Markdown.Models
{
    public class MarkdownFieldSettings
    {
        public bool SanitizeHtml { get; set; } = true;
        public string? Hint { get; set; }
    }
}