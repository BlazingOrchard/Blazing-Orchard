namespace BlazingOrchard.Abstractions.Services
{
    public interface IAutorouteEntries
    {
        bool TryGetEntryByPath(string path, out string? contentItemId);
    }
}
