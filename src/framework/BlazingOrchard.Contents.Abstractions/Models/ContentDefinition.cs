using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace BlazingOrchard.Contents.Models
{
    public abstract class ContentDefinition
    {
        public string Name { get; set; } = default!;

        /// <summary>
        /// Do not access this property directly. Migrate to use GetSettings and PopulateSettings.
        /// </summary>
        public JObject? Settings { get; set; } = new JObject();

        public T GetSettings<T>() where T : new()
        {
            var typeName = typeof(T).Name;

            if (Settings == null)
                return new T();

            if (Settings.TryGetValue(typeName, out var value))
                return value.ToObject<T>()!;

            return new T();
        }

        public void PopulateSettings<T>(T target)
        {
            var typeName = typeof(T).Name;

            if (Settings == null)
                return;

            if (Settings.TryGetValue(typeName, out var value)) 
                JsonConvert.PopulateObject(value.ToString(), target!);
        }
    }
}