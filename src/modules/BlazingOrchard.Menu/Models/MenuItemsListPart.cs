using System.Collections.Generic;
using BlazingOrchard.Contents.Models;

namespace BlazingOrchard.Menu.Models
{
    public class MenuItemsListPart : ContentPart
    {
        public ICollection<ContentItem> MenuItems { get; set; } = new List<ContentItem>();
    }
}