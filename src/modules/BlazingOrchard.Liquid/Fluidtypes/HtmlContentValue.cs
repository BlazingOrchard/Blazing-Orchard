using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using Fluid;
using Fluid.Values;

namespace BlazingOrchard.Liquid.FluidTypes
{
    public class HtmlContentValue : FluidValue
    {
        private readonly string? _value;
        public HtmlContentValue(string value) => _value = value;
        public override FluidValues Type => FluidValues.String;

        public override bool Equals(FluidValue other) =>
            other.IsNil() ? _value == null : _value != null && _value == other.ToStringValue();

        protected override FluidValue GetIndex(FluidValue index, TemplateContext context) => NilValue.Instance;
        protected override FluidValue GetValue(string name, TemplateContext context) => NilValue.Instance;
        public override bool ToBooleanValue() => true;
        public override decimal ToNumberValue() => 0;
        public override string ToStringValue() => _value!;

        public override void WriteTo(TextWriter writer, TextEncoder encoder, CultureInfo cultureInfo)
        {
            if (_value != null)
                writer.Write(_value, (HtmlEncoder)encoder);
        }

        public override object ToObjectValue() => _value!;
        public override bool Contains(FluidValue value) => false;
        public override IEnumerable<FluidValue> Enumerate() => Enumerable.Empty<FluidValue>();

        public override bool Equals(object? other) =>
            other is HtmlContentValue otherValue && Equals(_value, otherValue._value);

        public override int GetHashCode() => _value != null ? _value.GetHashCode() : 0;
    }
}