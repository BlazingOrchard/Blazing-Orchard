using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace BlazingOrchard.DisplayManagement.Shapes
{
    public class Composite : DynamicObject
    {
        public IDictionary<string, object> Properties { get; } = new Dictionary<string, object>();

        public override bool TryGetMember(GetMemberBinder binder, out object result) =>
            TryGetMemberImpl(binder.Name, out result);

        protected virtual bool TryGetMemberImpl(string name, out object result)
        {
            if (Properties.TryGetValue(name, out result))
                return true;

            result = null!;
            return true;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value) => TrySetMemberImpl(binder.Name, value);

        protected virtual bool TrySetMemberImpl(string name, object value)
        {
            Properties[name] = value;
            return true;
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            if (args.Length == 0)
                return TryGetMemberImpl(binder.Name, out result);

            // Method call with one argument will assign the property.
            if (args.Length == 1)
            {
                result = this;
                return TrySetMemberImpl(binder.Name, args.First());
            }

            if (!base.TryInvokeMember(binder, args, out result))
            {
                if (binder.Name == "ToString")
                {
                    result = string.Empty;
                    return true;
                }

                return false;
            }

            return true;
        }

        public override bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object result)
        {
            if (indexes.Length == 1)
            {
                if (indexes[0] is string stringIndex && TryGetMemberImpl(stringIndex, out result))
                    return true;

                // Returning false results in a RuntimeBinderException if the index supplied is not an existing string property name.
                result = null!;
                return false;
            }

            // Returning false results in a RuntimeBinderException if the index supplied is not an existing string property name.
            result = null!;
            return false;
        }

        public override bool TrySetIndex(SetIndexBinder binder, object[] indexes, object value)
        {
            if (indexes.Length == 1)
            {
                // try to access an existing member.
                if (indexes[0] is string stringIndex && TrySetMemberImpl(stringIndex, value))
                    return true;

                // Returning false results in a RuntimeBinderException if the index supplied is not an existing string property name.
                return false;
            }

            // Returning false results in a RuntimeBinderException if the index supplied is not an existing string property name.
            return false;
        }

        public static bool operator ==(Composite a, Nil b) => null! == a;
        public static bool operator !=(Composite a, Nil b) => !(a == b);
        public override int GetHashCode() => (Properties != null! ? Properties.GetHashCode() : 0);

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null!, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != this.GetType())
                return false;
            return Equals((Composite)obj);
        }

        protected bool Equals(Composite other) => Equals(Properties, other.Properties);
    }
}