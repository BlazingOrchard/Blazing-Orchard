using System;
using System.Threading.Tasks;
using BlazingOrchard.Contents.Display.Models;
using BlazingOrchard.Contents.Models;
using BlazingOrchard.DisplayManagement.Services;

namespace BlazingOrchard.Contents.Display.Services
{
    public abstract class ContentPartDisplayDriver<TPart> : DisplayDriverBase, IContentPartDisplayDriver
        where TPart : ContentPart
    {
        public Type PartType => typeof(TPart);

        protected virtual ValueTask<IDisplayResult?> BuildDisplayAsync(
            TPart contentPart,
            BuildPartDisplayContext context) =>
            new ValueTask<IDisplayResult?>(BuildDisplay(contentPart, context));

        protected virtual IDisplayResult? BuildDisplay(TPart contentPart, BuildPartDisplayContext context) =>
            BuildDisplay(contentPart);

        protected virtual IDisplayResult? BuildDisplay(TPart contentPart) => default;

        ValueTask<IDisplayResult?> IContentPartDisplayDriver.BuildDisplayAsync(ContentPart contentPart,
            BuildPartDisplayContext context) => BuildDisplayAsync((TPart)contentPart, context);
    }
}