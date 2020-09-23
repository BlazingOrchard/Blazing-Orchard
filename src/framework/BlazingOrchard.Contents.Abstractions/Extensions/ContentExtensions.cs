using BlazingOrchard.Contents.Models;

namespace BlazingOrchard.Contents
{
    public static class ContentExtensions
    {
        /// <summary>
        /// Whether the content element is published or not.
        /// </summary>
        /// <param name="content">The content to check.</param>
        /// <returns><c>True</c> if the content is published, <c>False</c> otherwise.</returns>
        public static bool IsPublished(this IContent content) =>
            content.ContentItem != null! && content.ContentItem.Published;

        /// <summary>
        /// Whether the content element has a draft or not.
        /// </summary>
        /// <param name="content">The content to check.</param>
        /// <returns><c>True</c> if the content has a draft, <c>False</c> otherwise.</returns>
        public static bool HasDraft(this IContent content) =>
            content.ContentItem != null! && (!content.ContentItem.Published || !content.ContentItem.Latest);
    }
}