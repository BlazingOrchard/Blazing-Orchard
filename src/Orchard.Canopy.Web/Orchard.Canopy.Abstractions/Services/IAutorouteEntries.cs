using Orchard.Canopy.Abstractions.Models;
using System.Text;

namespace Orchard.Canopy.Abstractions.Services
{
    public interface IAutorouteEntries
    {
        bool TryGetEntryByPath(string path, out string? contentItemId);
    }
}
