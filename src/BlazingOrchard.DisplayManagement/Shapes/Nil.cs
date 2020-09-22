using System.Dynamic;
using System.Linq.Expressions;

namespace BlazingOrchard.DisplayManagement.Shapes
{
    public class Nil : DynamicObject
    {
        public static Nil Instance { get; } = new Nil();

        private Nil()
        {
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = Instance;
            return true;
        }

        public override bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object result)
        {
            result = Instance;
            return true;
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            result = Instance;
            return true;
        }

        public override bool TryBinaryOperation(BinaryOperationBinder binder, object arg, out object result)
        {
            switch (binder.Operation)
            {
                case ExpressionType.Equal:
                    result = ReferenceEquals(arg, Instance);
                    return true;
                case ExpressionType.NotEqual:
                    result = !ReferenceEquals(arg, Instance);
                    return true;
            }

            return base.TryBinaryOperation(binder, arg, out result);
        }

        public static bool operator ==(Nil a, Nil b) => true;
        public static bool operator !=(Nil a, Nil b) => false;
        public static bool operator ==(Nil a, object b) => ReferenceEquals(a, b) || b == null;
        public static bool operator !=(Nil a, object b) => !(a == b);
        public override bool Equals(object obj) => obj == null || ReferenceEquals(obj, Instance);
        public override int GetHashCode() => 0;

        public override bool TryConvert(ConvertBinder binder, out object result)
        {
            result = null;
            return true;
        }

        public override string ToString() => string.Empty;
    }
}