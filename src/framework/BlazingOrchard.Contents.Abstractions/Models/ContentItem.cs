using System;
using Newtonsoft.Json;

namespace BlazingOrchard.Contents.Models
{
    /// <summary>
    /// Represents a content item version.
    /// </summary>
    [JsonConverter(typeof(ContentItemConverter))]
    public class ContentItem : ContentElement
    {
        public ContentItem()
        {
            ContentItem = this;
        }

        /// <summary>
        /// The primary key in the database.
        /// </summary>
        public int Id { get; set; } = default!;

        /// <summary>
        /// The logical identifier of the content item across versions.
        /// </summary>
        public string ContentItemId { get; set; } = default!;

        /// <summary>
        /// The logical identifier of the versioned content item.
        /// </summary>
        public string ContentItemVersionId { get; set; } = default!;

        /// <summary>
        /// The content type of the content item.
        /// </summary>
        public string ContentType { get; set; } = default!;

        /// <summary>
        /// Whether the version is published or not.
        /// </summary>
        public bool Published { get; set; }

        /// <summary>
        /// Whether the version is the latest version of the content item.
        /// </summary>
        public bool Latest { get; set; }

        /// <summary>
        /// When the content item version has been updated.
        /// </summary>
        public DateTime? ModifiedUtc { get; set; }

        /// <summary>
        /// When the content item has been published.
        /// </summary>
        public DateTime? PublishedUtc { get; set; }

        /// <summary>
        /// When the content item has been created or first published.
        /// </summary>
        public DateTime? CreatedUtc { get; set; }

        /// <summary>
        /// The name of the user who first created this content item version
        /// and owns content rights.
        /// </summary>
        public string Owner { get; set; } = default!;

        /// <summary>
        /// The name of the user who last modified this content item version.
        /// </summary>
        public string Author { get; set; } = default!;

        /// <summary>
        /// The text representing this content item.
        /// </summary>
        public string DisplayText { get; set; } = default!;

        public override string ToString() =>
            string.IsNullOrWhiteSpace(DisplayText) ? $"{ContentType} ({ContentItemId})" : DisplayText;
    }
}