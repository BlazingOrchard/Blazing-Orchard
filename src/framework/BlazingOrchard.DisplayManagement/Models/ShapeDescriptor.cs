using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazingOrchard.DisplayManagement.Descriptors;
using BlazingOrchard.DisplayManagement.Services;

namespace BlazingOrchard.DisplayManagement.Models
{
    public class ShapeDescriptor
    {
        public ShapeDescriptor(string shapeType)
        {
            ShapeType = shapeType;
            CreatingAsync = Enumerable.Empty<Func<ShapeCreatingContext, Task>>();
            CreatedAsync = Enumerable.Empty<Func<ShapeCreatedContext, Task>>();
            DisplayingAsync = Enumerable.Empty<Func<ShapeDisplayContext, Task>>();
            ProcessingAsync = Enumerable.Empty<Func<ShapeDisplayContext, Task>>();
            DisplayedAsync = Enumerable.Empty<Func<ShapeDisplayContext, Task>>();
            Wrappers = new List<string>();
            Bindings = new Dictionary<string, ShapeBinding>(StringComparer.OrdinalIgnoreCase);
            Placement = DefaultPlacementAction;
        }

        protected PlacementInfo? DefaultPlacementAction(ShapePlacementContext context)
        {
            // A null default placement means no default placement is specified
            if (DefaultPlacement == null)
            {
                return null;
            }

            return new PlacementInfo
            {
                Location = DefaultPlacement
            };
        }

        public string ShapeType { get; set; }

        /// <summary>
        /// The BindingSource is informational text about the source of the Binding delegate. Not used except for
        /// troubleshooting.
        /// </summary>
        public string? BindingSource =>
            Bindings.TryGetValue(ShapeType, out var binding) ? binding.ComponentType.FullName : null;


        public IDictionary<string, ShapeBinding> Bindings { get; set; }

        public IEnumerable<Func<ShapeCreatingContext, Task>> CreatingAsync { get; set; }
        public IEnumerable<Func<ShapeCreatedContext, Task>> CreatedAsync { get; set; }
        public IEnumerable<Func<ShapeDisplayContext, Task>> DisplayingAsync { get; set; }
        public IEnumerable<Func<ShapeDisplayContext, Task>> ProcessingAsync { get; set; }
        public IEnumerable<Func<ShapeDisplayContext, Task>> DisplayedAsync { get; set; }

        public Func<ShapePlacementContext, PlacementInfo?> Placement { get; set; }
        public string? DefaultPlacement { get; set; }

        public IList<string> Wrappers { get; set; }
    }
}