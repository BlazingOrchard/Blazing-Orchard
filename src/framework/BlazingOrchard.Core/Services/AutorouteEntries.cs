using System;
using System.Collections.Generic;
using BlazingOrchard.Services;

namespace BlazingOrchard.Core.Services
{
    public class AutorouteEntries : IAutorouteEntries
    {
        private readonly Dictionary<string, string> _dictionary;

        public AutorouteEntries()
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
