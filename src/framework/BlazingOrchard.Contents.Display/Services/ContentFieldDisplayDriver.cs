using System;
using System.Threading.Tasks;
using BlazingOrchard.Contents.Display.Models;
using BlazingOrchard.Contents.Models;
using BlazingOrchard.DisplayManagement.Services;

namespace BlazingOrchard.Contents.Display.Services
{
    public abstract class ContentFieldDisplayDriver<TField> : DisplayDriverBase, IContentFieldDisplayDriver where TField : ContentField
    {
        public Type FieldType => typeof(TField);

        protected virtual ValueTask<IDisplayResult?> BuildDisplayAsync(
            TField contentField,
            BuildFieldDisplayContext context) =>
            new ValueTask<IDisplayResult?>(BuildDisplay(contentField, context));

        protected virtual IDisplayResult? BuildDisplay(TField contentField, BuildFieldDisplayContext context) =>
            BuildDisplay(contentField);

        protected virtual IDisplayResult? BuildDisplay(TField contentField) => default;

        ValueTask<IDisplayResult?> IContentFieldDisplayDriver.BuildDisplayAsync(
            ContentField contentField,
            BuildFieldDisplayContext context) =>
            BuildDisplayAsync((TField)contentField, context);
    }
}