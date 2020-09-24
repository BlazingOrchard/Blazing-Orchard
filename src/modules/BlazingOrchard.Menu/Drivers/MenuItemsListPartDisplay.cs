using BlazingOrchard.Contents.Display.Services;
using BlazingOrchard.DisplayManagement.Services;
using BlazingOrchard.Menu.Models;

namespace BlazingOrchard.Menu.Drivers
{
    public class MenuItemsListPartDisplay : ContentPartDisplayDriver<MenuItemsListPart>
    {
        protected override IDisplayResult? BuildDisplay(MenuItemsListPart contentPart)
        {
            return Shape(
                "MenuItemsListPart",
                async context =>
                {
                    var shape = await context.New.MenuItemsListPart();
                    shape.ContentItem = contentPart.ContentItem;
                    shape.MenuItems = contentPart.MenuItems;
                    shape.MenuItemsListPart = contentPart;
                    return shape;
                });
        }
    }
}