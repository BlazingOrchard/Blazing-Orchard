using BlazingOrchard.ContentFields.Models;
using BlazingOrchard.Contents.Display.Services;
using BlazingOrchard.DisplayManagement.Services;

namespace BlazingOrchard.ContentFields.Drivers
{
    public class TextFieldDisplay : ContentFieldDisplayDriver<TextField>
    {
        protected override IDisplayResult? BuildDisplay(TextField contentField)
        {
            return Shape(
                    nameof(TextField),
                    async context =>
                    {
                        var shape = await context.New.TextField();
                        shape.ContentItem = contentField.ContentItem;
                        shape.Text = contentField.Text!;
                        shape.TextField = contentField;
                        return shape;
                    })
                .DefaultLocation("3");
        }
    }
}