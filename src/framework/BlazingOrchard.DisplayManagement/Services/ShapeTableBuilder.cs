using System.Collections.Generic;
using System.Linq;
using BlazingOrchard.DisplayManagement.Models;

namespace BlazingOrchard.DisplayManagement.Services
{
    public class ShapeTableBuilder
    {
        private readonly IList<ShapeAlterationBuilder> _alterationBuilders = new List<ShapeAlterationBuilder>();

        public ShapeAlterationBuilder Describe(string shapeType)
        {
            var alterationBuilder = new ShapeAlterationBuilder(shapeType);
            _alterationBuilders.Add(alterationBuilder);
            return alterationBuilder;
        }

        public IEnumerable<ShapeAlteration> BuildAlterations()
        {
            return _alterationBuilders.Select(b => b.Build());
        }
    }
}