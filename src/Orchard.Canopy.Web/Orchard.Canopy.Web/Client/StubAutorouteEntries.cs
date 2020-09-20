using Orchard.Canopy.Abstractions.Services;
using System;
using System.Collections.Generic;

namespace Orchard.Canopy.Web.Client
{
    internal class StubAutorouteEntries : IAutorouteEntries
    {
        private Dictionary<string, string> _dictionary;

        public StubAutorouteEntries()
        {
            _dictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase) {
                ["about"] = "4c85vfravva7sxszvbr5awcmcv",
                ["blog"] = "44xmtbkh1sh673kpxmz2dnqzdc",
                ["blog"] = "44xmtbkh1sh673kpxmz2dnqzdc",
                ["blog/post-1"] = "4wyrh3t1n353htd2zvvkmdpn5p"
            };
        }

        public bool TryGetEntryByPath(string path, out string? contentItemId) => _dictionary.TryGetValue(path, out contentItemId);
    }
}