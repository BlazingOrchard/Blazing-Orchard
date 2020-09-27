using System;
using System.Collections.Generic;
using Markdig;

namespace BlazingOrchard.Markdown.Options
{
    public class MarkdownPipelineOptions
    {
        public List<Action<MarkdownPipelineBuilder>> Configure { get; } = new List<Action<MarkdownPipelineBuilder>>();
    }
}