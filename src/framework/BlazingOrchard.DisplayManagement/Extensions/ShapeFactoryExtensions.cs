using System;
using System.Linq;
using System.Threading.Tasks;
using BlazingOrchard.DisplayManagement.Models;
using BlazingOrchard.DisplayManagement.Services;
using BlazingOrchard.DisplayManagement.Shapes;

namespace BlazingOrchard.DisplayManagement.Extensions
{
    public static class ShapeFactoryExtensions
    {
        private static readonly Func<ValueTask<IShape>> NewShape = () => new ValueTask<IShape>(new Shape());

        public static ValueTask<IShape> CreateAsync(this IShapeFactory factory, string shapeType) =>
            factory.CreateAsync(shapeType, NewShape);

        public static async ValueTask<IShape> CreateAsync<T>(
            this IShapeFactory factory,
            string shapeType,
            INamedEnumerable<T> parameters)
        {
            if (parameters == null! || parameters == Arguments.Empty)
                return await factory.CreateAsync(shapeType, NewShape);

            var shape = (Shape)await factory.CreateAsync(shapeType, NewShape);
            var initializer = parameters.Positional.SingleOrDefault();

            // If only one non-Type, use it as the source object to copy.
            if (initializer != null)
            {
                // Use the Arguments class to optimize reflection code.
                var arguments = Arguments.From(initializer);

                foreach (var prop in arguments.Named)
                    shape.Properties[prop.Key] = prop.Value;
            }
            else
            {
                foreach (var (key, value) in parameters.Named)
                    shape.Properties[key] = value!;
            }

            return shape;
        }
    }
}