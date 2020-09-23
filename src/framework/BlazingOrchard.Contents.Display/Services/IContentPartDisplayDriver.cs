using System;
using System.Threading.Tasks;
using BlazingOrchard.Contents.Display.Models;
using BlazingOrchard.Contents.Models;
using BlazingOrchard.DisplayManagement.Services;

namespace BlazingOrchard.Contents.Display.Services
{
    public interface IContentPartDisplayDriver
    {
        Type PartType { get; }
        ValueTask<IDisplayResult?> BuildDisplayAsync(ContentPart contentPart, BuildPartDisplayContext context);
    }
}