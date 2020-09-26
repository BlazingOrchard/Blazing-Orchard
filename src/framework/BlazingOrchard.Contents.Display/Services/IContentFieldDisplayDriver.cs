using System;
using System.Threading.Tasks;
using BlazingOrchard.Contents.Display.Models;
using BlazingOrchard.Contents.Models;
using BlazingOrchard.DisplayManagement.Services;

namespace BlazingOrchard.Contents.Display.Services
{
    public interface IContentFieldDisplayDriver
    {
        Type FieldType { get; }
        ValueTask<IDisplayResult?> BuildDisplayAsync(ContentField contentField, BuildFieldDisplayContext context);
    }
}