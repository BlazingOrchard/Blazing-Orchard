namespace BlazingOrchard.Services
{
    public interface IAutorouteEntries
    {
        bool TryGetEntryByPath(string path, out string? contentItemId);
    }
}
