using System;
using System.Threading.Tasks;
using BlazingOrchard.Contents.Display.Models;
using BlazingOrchard.Contents.Models;
using BlazingOrchard.DisplayManagement.Models;
using BlazingOrchard.DisplayManagement.Services;

namespace BlazingOrchard.Contents.Display.Services
{
    public abstract class ContentFieldDisplayDriver<TField> : IContentFieldDisplayDriver where TField : ContentField
    {
        public Type FieldType => typeof(TField);

        protected abstract ValueTask<IDisplayResult>
            BuildDisplayAsync(TField contentField, BuildFieldDisplayContext context);

        ValueTask<IDisplayResult> IContentFieldDisplayDriver.BuildDisplayAsync(
            ContentField contentField,
            BuildFieldDisplayContext context) =>
            BuildDisplayAsync((TField)contentField, context);
    }
}