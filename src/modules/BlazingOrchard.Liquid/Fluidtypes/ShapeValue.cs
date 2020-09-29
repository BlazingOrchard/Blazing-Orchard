using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using BlazingOrchard.DisplayManagement.Shapes;
using Fluid;
using Fluid.Values;

namespace BlazingOrchard.Liquid.FluidTypes
{
    public class ShapeValue : FluidValue
    {
        private readonly IShape? _value;
        public ShapeValue(IShape value) => _value = value;
        public override FluidValues Type => FluidValues.String;

        public override bool Equals(FluidValue other) =>
            other.IsNil() ? _value == null : _value != null && Equals(_value, other.ToObjectValue());

        protected override FluidValue GetIndex(FluidValue index, TemplateContext context) => NilValue.Instance;
        protected override FluidValue GetValue(string name, TemplateContext context) => NilValue.Instance;
        public override bool ToBooleanValue() => true;
        public override decimal ToNumberValue() => 0;
        public override string ToStringValue() => _value?.Metadata.Type!;

        public override void WriteTo(TextWriter writer, TextEncoder encoder, CultureInfo cultureInfo)
        {
            if (_value != null)
                writer.Write(_value);
        }

        public override object ToObjectValue() => _value!;
        public override bool Contains(FluidValue value) => false;
        public override IEnumerable<FluidValue> Enumerate() => Enumerable.Empty<FluidValue>();

        public override bool Equals(object? other) =>
            other is ShapeValue otherValue && Equals(_value, otherValue._value);

        public override int GetHashCode() => _value != null ? _value.GetHashCode() : 0;
    }
}