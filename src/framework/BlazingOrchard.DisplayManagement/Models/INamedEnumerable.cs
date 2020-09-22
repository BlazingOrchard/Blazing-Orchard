using System.Collections.Generic;

namespace BlazingOrchard.DisplayManagement.Models
{
    public interface INamedEnumerable<T> : IEnumerable<T>
    {
        IList<T> Positional { get; }
        IDictionary<string, T> Named { get; }
    }
}