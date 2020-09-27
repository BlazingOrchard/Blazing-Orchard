namespace BlazingOrchard.Services
{
    public interface IHtmlSanitizerService
    {
        string Sanitize(string html);
    }
}
