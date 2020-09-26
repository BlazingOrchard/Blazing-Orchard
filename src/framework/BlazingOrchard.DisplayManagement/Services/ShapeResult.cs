using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlazingOrchard.DisplayManagement.Descriptors;
using BlazingOrchard.DisplayManagement.Models;
using BlazingOrchard.DisplayManagement.Shapes;

namespace BlazingOrchard.DisplayManagement.Services
{
    public class ShapeResult : IDisplayResult
    {
        private readonly string _shapeType;
        private readonly Func<IBuildShapeContext, ValueTask<IShape?>> _shapeBuilder;
        private IDictionary<string, string>? _otherLocations;
        private string? _defaultLocation;
        private string? _differentiator;
        private string? _name;

        public ShapeResult(string shapeType, Func<IBuildShapeContext, ValueTask<IShape?>> shapeBuilder)
        {
            _shapeType = shapeType;
            _shapeBuilder = shapeBuilder;
        }

        public IShape? Shape { get; private set; }

        public async Task ApplyAsync(BuildDisplayContext context)
        {
            var displayType = context.DisplayType;
            
            // If no location is set from the driver, use the one from the context.
            if (String.IsNullOrEmpty(_defaultLocation))
            {
                _defaultLocation = context.DefaultZone;
            }
            
            // Look into specific implementations of placements (like placement.json files and IShapePlacementProviders)
            var placement = context.FindPlacement(_shapeType, _differentiator, displayType, context);

            // Look for mapped display type locations.
            if (_otherLocations != null)
                if (_otherLocations.TryGetValue(displayType, out var displayTypePlacement))
                    _defaultLocation = displayTypePlacement;

            // If no placement is found, use the default location
            if (placement == null) 
                placement = new PlacementInfo
                {
                    Location = _defaultLocation!,
                    DefaultPosition = _defaultLocation // Temporary until decided whether or not to support zones.
                };

            if (placement.DefaultPosition == null) 
                placement.DefaultPosition = context.DefaultPosition;
            
            // If there are no placement or it's explicitly noop then stop rendering execution.
            if (string.IsNullOrEmpty(placement.Location) || placement.Location == "-")
                return;

            var newShape = Shape = await _shapeBuilder(context);

            // Ignore if the driver returned a null shape.
            if (newShape == null)
                return;
            
            var newShapeMetadata = newShape.Metadata;
            newShapeMetadata.Name = _name ?? _differentiator ?? _shapeType;
            newShapeMetadata.Differentiator = _differentiator ?? _shapeType;
            newShapeMetadata.DisplayType = displayType;
            newShapeMetadata.PlacementSource = placement.Source;
            newShapeMetadata.Type = _shapeType;
            
            var position = placement.GetPosition();
            position = !string.IsNullOrEmpty(position) ? position : default;

            dynamic parentShape = context.Shape;

            if (parentShape is Shape shape)
                shape.Add(newShape, position);
        }

        /// <summary>
        /// Sets the location to use for a matching display type.
        /// </summary>
        public ShapeResult Location(string displayType, string location)
        {
            _otherLocations ??= new Dictionary<string, string>(2);
            _otherLocations[displayType] = location;
            return this;
        }
        
        /// <summary>
        /// Sets the default location.
        /// </summary>
        public ShapeResult DefaultLocation(string location)
        {
            _defaultLocation = location;
            return this;
        }

        /// <summary>
        /// Sets a discriminator that is used to find the location of the shape when two shapes of the same type are displayed.
        /// </summary>
        public ShapeResult Differentiator(string differentiator)
        {
            _differentiator = differentiator;
            return this;
        }
        
        /// <summary>
        /// Sets the shape name regardless its 'Differentiator'.
        /// </summary>
        public ShapeResult Name(string name)
        {
            _name = name;
            return this;
        }
    }
}