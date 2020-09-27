using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BlazingOrchard.DisplayManagement.Models
{
    public static class Arguments
    {
        private static readonly ConcurrentDictionary<Type, Func<object, NamedEnumerable<object>>> PropertiesAccessors =
            new ConcurrentDictionary<Type, Func<object, NamedEnumerable<object>>>();

        public static INamedEnumerable<T> FromT<T>(IEnumerable<T> arguments, IEnumerable<string> names) =>
            new NamedEnumerable<T>(arguments, names);

        public static INamedEnumerable<object> From(IEnumerable<object?>? arguments, IEnumerable<string> names) =>
            arguments != null ? new NamedEnumerable<object>(arguments, names) : Empty;

        public static INamedEnumerable<object> From(IDictionary<string, object> dictionary) =>
            From(dictionary.Values, dictionary.Keys);

        public static INamedEnumerable<string> From(IDictionary<string, string> dictionary) =>
            new NamedEnumerable<string>(dictionary.Values, dictionary.Keys);

        public static INamedEnumerable<object> From(object propertyObject)
        {
            var propertiesAccessor = PropertiesAccessors.GetOrAdd(
                propertyObject.GetType(),
                type =>
                {
                    var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
                    var names = properties.Select(x => x.Name).ToArray();

                    return obj =>
                    {
                        // Properties and names are referenced in the closure.
                        var values = properties.Select(x => x.GetValue(obj, null)).ToArray();
                        return new NamedEnumerable<object>(values, names);
                    };
                });

            return propertiesAccessor(propertyObject);
        }

        private class NamedEnumerable<T> : INamedEnumerable<T>
        {
            private readonly List<T> _arguments;
            private readonly List<string> _names;
            private readonly T[] _positional;
            private IDictionary<string, T>? _named;

            public NamedEnumerable(IEnumerable<T> arguments, IEnumerable<string> names)
            {
                var argumentList = arguments as T[] ?? arguments.ToArray();
                var nameList = names as string[] ?? names.ToArray();

                if (argumentList.Count() < nameList.Count())
                    throw new ArgumentException("arguments.Count() < names.Count()");

                _arguments = argumentList.ToList();
                _names = nameList.ToList();

                _positional = Array.Empty<T>();

                if (_arguments.Count != _names.Count)
                    _positional = _arguments.Take(_arguments.Count - _names.Count).ToArray();
            }

            IEnumerator IEnumerable.GetEnumerator() => _arguments.GetEnumerator();
            IEnumerator<T> IEnumerable<T>.GetEnumerator() => _arguments.GetEnumerator();
            IList<T> INamedEnumerable<T>.Positional => _positional;
            IDictionary<string, T> INamedEnumerable<T>.Named => _named ??= new Named(_arguments, _names)!;

            private class Named : IDictionary<string, T>
            {
                private readonly IList<T> _arguments;
                private readonly IList<string> _names;
                private IEnumerable<KeyValuePair<string, T>>? _enumerable;

                public Named(IList<T> arguments, IList<string> names)
                {
                    if (arguments.Count != names.Count)
                        _arguments = arguments.Skip(arguments.Count - names.Count).ToArray();
                    else
                        _arguments = arguments;

                    _names = names;
                }

                private IEnumerable<KeyValuePair<string, T>> MakeEnumerable() =>
                    _enumerable ??= _arguments.Zip(
                        _names,
                        (arg, name) => new KeyValuePair<string, T>(name, arg));

                IEnumerator<KeyValuePair<string, T>> IEnumerable<KeyValuePair<string, T>>.GetEnumerator() =>
                    MakeEnumerable().GetEnumerator();

                IEnumerator IEnumerable.GetEnumerator() => MakeEnumerable().GetEnumerator();

                void ICollection<KeyValuePair<string, T>>.Add(KeyValuePair<string, T> item) =>
                    throw new NotImplementedException();

                void ICollection<KeyValuePair<string, T>>.Clear() => throw new NotImplementedException();

                bool ICollection<KeyValuePair<string, T>>.Contains(KeyValuePair<string, T> item) =>
                    MakeEnumerable().Contains(item);

                void ICollection<KeyValuePair<string, T>>.CopyTo(KeyValuePair<string, T>[] array, int arrayIndex) =>
                    throw new NotImplementedException();

                bool ICollection<KeyValuePair<string, T>>.Remove(KeyValuePair<string, T> item) =>
                    throw new NotImplementedException();

                int ICollection<KeyValuePair<string, T>>.Count => _names.Count;
                bool ICollection<KeyValuePair<string, T>>.IsReadOnly => true;
                bool IDictionary<string, T>.ContainsKey(string key) => _names.Contains(key);
                void IDictionary<string, T>.Add(string key, T value) => throw new NotImplementedException();
                bool IDictionary<string, T>.Remove(string key) => throw new NotImplementedException();

                bool IDictionary<string, T>.TryGetValue(string key, out T value)
                {
                    var pair = MakeEnumerable().FirstOrDefault(kv => kv.Key == key);

                    // pair is a value type. in case of key-miss,
                    // will default to key=(string)null,value=(object)null

                    value = pair.Value;
                    return pair.Key != null;
                }

                //TBD
                T IDictionary<string, T>.this[string key]
                {
                    get => ((IDictionary<string, T>)this).TryGetValue(key, out var result) ? result : default;
                    set => throw new NotImplementedException();
                }

                ICollection<string> IDictionary<string, T>.Keys => _names;
                ICollection<T> IDictionary<string, T>.Values => _arguments;
            }
        }

        public static readonly INamedEnumerable<object> Empty = From(Array.Empty<object>(), Array.Empty<string>());
    }
}