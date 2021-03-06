﻿#if LESSTHAN_NET35

#pragma warning disable CA1051 // Do not declare visible instance fields
#pragma warning disable CA1822 // Mark members as static
#pragma warning disable CC0074 // Make field readonly
#pragma warning disable CC0091 // Use static method

using System.Linq.Expressions;
using Theraot;

namespace System.Runtime.CompilerServices
{
    [Obsolete("do not use this type", true)]
    public partial class ExecutionScope
    {
        //These fields are accessed via Reflection
        public object[]? Globals;

        public object[]? Locals;

        public ExecutionScope? Parent;
    }

    public partial class ExecutionScope
    {
        public Delegate CreateDelegate(int indexLambda, object[] locals)
        {
            // Should not be static
            No.Op(indexLambda);
            No.Op(locals);
            throw new NotSupportedException();
        }

        public object[] CreateHoistedLocals()
        {
            // Should not be static
            throw new NotSupportedException();
        }

        public Expression IsolateExpression(Expression expression, object[] locals)
        {
            // Should not be static
            No.Op(expression);
            No.Op(locals);
            throw new NotSupportedException();
        }
    }
}

#endif