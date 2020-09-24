using System;
using System.Collections.Generic;
using System.Linq;
using BlazingOrchard.Contents.Models;
using Newtonsoft.Json.Linq;

namespace BlazingOrchard.Contents
{
    public static class ContentElementExtensions
    {
        public const string WeldedPartSettingsName = "@WeldedPartSettings";

        /// <summary>
        /// These settings instruct merge to replace current value, even for null values.
        /// </summary>
        private static readonly JsonMergeSettings JsonMergeSettings = new JsonMergeSettings
            { MergeArrayHandling = MergeArrayHandling.Replace, MergeNullValueHandling = MergeNullValueHandling.Merge };

        /// <summary>
        /// Gets a content element by its name.
        /// </summary>
        /// <typeparam name="TElement">The expected type of the content element.</typeparam>
        /// <typeparam name="name">The name of the content element.</typeparam>
        /// <returns>The content element instance or <code>null</code> if it doesn't exist.</returns>
        public static TElement? Get<TElement>(this ContentElement contentElement, string name)
            where TElement : ContentElement => (TElement?)contentElement.Get(typeof(TElement), name);

        /// <summary>
        /// Gets whether a content element has a specific element attached.
        /// </summary>
        /// <typeparam name="TElement">The expected type of the content element.</typeparam>
        public static bool Has<TElement>(this ContentElement contentElement) where TElement : ContentElement =>
            contentElement.Has(typeof(TElement).Name);

        /// <summary>
        /// Gets a content element by its name.
        /// </summary>
        /// <typeparam name="contentElementType">The expected type of the content element.</typeparam>
        /// <typeparam name="name">The name of the content element.</typeparam>
        /// <returns>The content element instance or <code>null</code> if it doesn't exist.</returns>
        public static ContentElement? Get(this ContentElement contentElement, Type contentElementType, string name)
        {
            if (contentElement.Elements.TryGetValue(name, out var element))
                return element;

            var data = contentElement.Data.ToString();
            var elemData = contentElement.Data[name];

            if (!(contentElement.Data[name] is JObject elementData))
                return null;

            var result = (ContentElement)elementData.ToObject(contentElementType)!;
            result.Data = elementData;
            result.ContentItem = contentElement.ContentItem;
            contentElement.Elements[name] = result;

            return result;
        }

        /// <summary>
        /// Gets a content element by its name or create a new one.
        /// </summary>
        /// <typeparam name="TElement">The expected type of the content element.</typeparam>
        /// <typeparam name="name">The name of the content element.</typeparam>
        /// <returns>The content element instance or a new one if it doesn't exist.</returns>
        public static TElement GetOrCreate<TElement>(this ContentElement contentElement, string name)
            where TElement : ContentElement, new()
        {
            var existing = contentElement.Get<TElement>(name);

            if (existing == null)
            {
                var newElement = new TElement { ContentItem = contentElement.ContentItem };
                contentElement.Data[name] = newElement.Data;
                contentElement.Elements[name] = newElement;
                return newElement;
            }

            return existing;
        }

        /// <summary>
        /// Adds a content element by name.
        /// </summary>
        /// <typeparam name="name">The name of the content element.</typeparam>
        /// <typeparam name="element">The element to add to the <see cref="ContentItem"/>.</typeparam>
        /// <returns>The current <see cref="ContentItem"/> instance.</returns>
        public static ContentElement Weld(this ContentElement contentElement, string name, ContentElement element)
        {
            if (!contentElement.Data.ContainsKey(name))
            {
                element.Data = JObject.FromObject(element, ContentBuilderSettings.IgnoreDefaultValuesSerializer);
                element.ContentItem = contentElement.ContentItem;

                contentElement.Data[name] = element.Data;
                contentElement.Elements[name] = element;
            }

            return contentElement;
        }

        /// <summary>
        /// Welds a new part to the content item. If a part of the same type is already welded nothing is done.
        /// This part can be not defined in Content Definitions.
        /// </summary>
        /// <typeparam name="TPart">The type of the part to be welded.</typeparam>
        public static ContentElement Weld<TElement>(this ContentElement contentElement, object? settings = null)
            where TElement : ContentElement, new()
        {
            var elementName = typeof(TElement).Name;

            if (!(contentElement.Data[elementName] is JObject))
            {
                // Build and welded the part.
                var part = new TElement();
                contentElement.Weld(elementName, part);
            }

            if (!contentElement.Data.TryGetValue(WeldedPartSettingsName, out var result))
                contentElement.Data[WeldedPartSettingsName] = result = new JObject();

            var weldedPartSettings = (JObject)result;

            var settingsModel = settings == null
                ? new JObject()
                : JObject.FromObject(
                    settings,
                    ContentBuilderSettings.IgnoreDefaultValuesSerializer);
                
            weldedPartSettings[elementName] = settingsModel;

            return contentElement;
        }

        /// <summary>
        /// Updates the content element with the specified name.
        /// </summary>
        /// <typeparam name="name">The name of the element to update.</typeparam>
        /// <typeparam name="element">The content element instance to update.</typeparam>
        /// <returns>The current <see cref="ContentItem"/> instance.</returns>
        public static ContentElement Apply(this ContentElement contentElement, string name, ContentElement element)
        {
            if (contentElement.Data[name] is JObject elementData)
            {
                elementData.Merge(JObject.FromObject(element), JsonMergeSettings);
            }
            else
            {
                elementData = JObject.FromObject(element, ContentBuilderSettings.IgnoreDefaultValuesSerializer);
                contentElement.Data[name] = elementData;
            }

            element.Data = elementData;
            element.ContentItem = contentElement.ContentItem;

            // Replace the existing content element with the new one.
            contentElement.Elements[name] = element;

            if (element is ContentField)
                contentElement.ContentItem.Elements.Clear();

            return contentElement;
        }

        /// <summary>
        /// Updates the whole content.
        /// </summary>
        /// <typeparam name="element">The content element instance to update.</typeparam>
        /// <returns>The current <see cref="ContentItem"/> instance.</returns>
        public static ContentElement Apply(this ContentElement contentElement, ContentElement element)
        {
            if (contentElement.Data != null!)
                contentElement.Data.Merge(JObject.FromObject(element.Data), JsonMergeSettings);
            else
                contentElement.Data = JObject.FromObject(
                    element.Data,
                    ContentBuilderSettings.IgnoreDefaultValuesSerializer);

            contentElement.Elements.Clear();
            return contentElement;
        }

        /// <summary>
        /// Gets all content elements of a specific type.
        /// </summary>
        /// <typeparam name="TElement">The expected type of the content elements.</typeparam>
        /// <returns>The content element instances or empty sequence if no entries exist.</returns>
        public static IEnumerable<TElement> OfType<TElement>(this ContentElement contentElement)
            where TElement : ContentElement =>
            contentElement.Elements.Select(x => x.Value).Where(x => x is TElement).Cast<TElement>();
    }
}