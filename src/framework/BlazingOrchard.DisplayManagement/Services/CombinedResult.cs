using System.Collections.Generic;
using System.Threading.Tasks;
using BlazingOrchard.DisplayManagement.Models;

namespace BlazingOrchard.DisplayManagement.Services
{
    public class CombinedResult : IDisplayResult
    {
        private readonly IEnumerable<IDisplayResult> _results;
        public CombinedResult(params IDisplayResult[] results) => _results = results;
        public CombinedResult(IEnumerable<IDisplayResult> results) => _results = results;

        public async Task ApplyAsync(BuildDisplayContext context)
        {
            foreach (var result in _results) 
                await result.ApplyAsync(context);
        }
    }
}