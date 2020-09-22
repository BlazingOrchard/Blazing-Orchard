using System.Linq;
using System.Threading.Tasks;
using BlazingOrchard.DisplayManagement.Models;
using BlazingOrchard.DisplayManagement.Services;
using BlazingOrchard.DisplayManagement.Shapes;

namespace BlazingOrchard.DisplayManagement.Extensions
{
    public static class ShapeFactoryExtensions
    {
        public static async ValueTask<IShape> CreateAsync<T>(
            this IShapeFactory factory,
            string shapeType,
            INamedEnumerable<T> parameters)
        {
            if (parameters == null || parameters == Arguments.Empty)
                return await factory.CreateAsync(shapeType);

            var shape = (Shape)await factory.CreateAsync(shapeType);
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
                foreach (var kv in parameters.Named) 
                    shape.Properties[kv.Key] = kv.Value;
            }

            return shape;
        }
    }
}