﻿#if LESSTHAN_NET35
extern alias nunitlinq;
#endif

// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
//
// Authors:
//   Miguel de Icaza <miguel@novell.com>
//

using System;
using System.Linq.Expressions;
using System.Reflection;
using NUnit.Framework;
using Theraot;

namespace MonoTests.System.Linq.Expressions
{
    [TestFixture]
    public class ExpressionTestMakeBinary
    {
        public static int GoodMethod(string a, double d)
        {
            No.Op(a);
            No.Op(d);
            return 1;
        }

        public static int BadMethodSig_1()
        {
            return 1;
        }

        public static int BadMethodSig_2(int a)
        {
            No.Op(a);
            return 1;
        }

        public static int BadMethodSig_3(int a, int b, int c)
        {
            No.Op(a);
            No.Op(b);
            No.Op(c);
            return 1;
        }

        private static MethodInfo Gm(string n)
        {
            var methods = typeof(ExpressionTestMakeBinary).GetMethods
            (
                BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public
            );

            foreach (var m in methods)
            {
                if (m.Name == n)
                {
                    return m;
                }
            }

            throw new Exception(string.Format("Method {0} not found", n));
        }

        private static void PassInt(ExpressionType nt)
        {
            Expression left = Expression.Constant(1);
            Expression right = Expression.Constant(1);

            Expression.MakeBinary(nt, left, right);
        }

        private static void FailInt(ExpressionType nt)
        {
            Expression left = Expression.Constant(1);
            Expression right = Expression.Constant(1);

            try
            {
                Expression.MakeBinary(nt, left, right);
            }
            catch (ArgumentException)
            {
                return;
            }
            catch (InvalidOperationException)
            {
                return;
            }

            // If we get here, there was an error
            Assert.Fail("FailInt failed while creating an {0}", nt);
        }

        public T CodeGen<T>(Func<Expression, Expression, Expression> bin, T v1, T v2)
        {
            if (bin == null)
            {
                throw new ArgumentNullException("bin");
            }

            var compiled = Expression.Lambda<Func<T>>(bin(v1.ToConstant(), v2.ToConstant())).Compile();
            return compiled();
        }

        private void CTest<T>(ExpressionType node, bool r, T a, T b)
        {
            var pa = Expression.Parameter(typeof(T), "a");
            var pb = Expression.Parameter(typeof(T), "b");

            var p = Expression.MakeBinary(node, Expression.Constant(a), Expression.Constant(b));
            var lambda = Expression.Lambda<Func<T, T, bool>>
            (
                p, pa, pb
            );

            var compiled = lambda.Compile();
            Assert.AreEqual(r, compiled(a, b), string.Format("{0} ({1},{2}) == {3}", node, a, b, r));
        }

        [Test]
        public void ComparisonTests()
        {
            var t = ExpressionType.Equal;

            CTest<byte>(t, true, 10, 10);
            CTest<sbyte>(t, false, 1, 5);
            CTest<sbyte>(t, true, 1, 1);
            CTest(t, true, 1, 1);
            CTest(t, true, 1.0, 1.0);
            CTest(t, true, "", "");
            CTest(t, true, "Hey", "Hey");
            CTest(t, false, "Hey", "There");

            t = ExpressionType.NotEqual;

            CTest<byte>(t, false, 10, 10);
            CTest<sbyte>(t, true, 1, 5);
            CTest<sbyte>(t, false, 1, 1);
            CTest(t, false, 1, 1);
            CTest(t, false, 1.0, 1.0);
            CTest(t, false, 1.0, 1.0);
            CTest(t, false, "", "");
            CTest(t, false, "Hey", "Hey");
            CTest(t, true, "Hey", "There");

            t = ExpressionType.GreaterThan;
            CTest<byte>(t, true, 5, 1);
            CTest<byte>(t, false, 10, 10);
            CTest<sbyte>(t, false, 1, 5);
            CTest<sbyte>(t, false, 1, 1);
            CTest(t, false, 1, 1);
            CTest<uint>(t, true, 1, 0);
            CTest<ulong>(t, true, long.MaxValue, 0);
            CTest(t, false, 1.0, 1.0);
            CTest(t, false, 1.0, 1.0);

            t = ExpressionType.LessThan;
            CTest<byte>(t, false, 5, 1);
            CTest<byte>(t, false, 10, 10);
            CTest<sbyte>(t, true, 1, 5);
            CTest<sbyte>(t, false, 1, 1);
            CTest(t, false, 1, 1);
            CTest<uint>(t, false, 1, 0);
            CTest<ulong>(t, false, long.MaxValue, 0);
            CTest(t, false, 1.0, 1.0);
            CTest(t, false, 1.0, 1.0);

            t = ExpressionType.GreaterThanOrEqual;
            CTest<byte>(t, true, 5, 1);
            CTest<byte>(t, true, 10, 10);
            CTest<sbyte>(t, false, 1, 5);
            CTest<sbyte>(t, true, 1, 1);
            CTest(t, true, 1, 1);
            CTest<uint>(t, true, 1, 0);
            CTest<ulong>(t, true, long.MaxValue, 0);
            CTest(t, true, 1.0, 1.0);
            CTest(t, true, 1.0, 1.0);

            t = ExpressionType.LessThanOrEqual;
            CTest<byte>(t, false, 5, 1);
            CTest<byte>(t, true, 10, 10);
            CTest<sbyte>(t, true, 1, 5);
            CTest<sbyte>(t, true, 1, 1);
            CTest(t, true, 1, 1);
            CTest<uint>(t, false, 1, 0);
            CTest<ulong>(t, false, long.MaxValue, 0);
            CTest(t, true, 1.0, 1.0);
            CTest(t, true, 1.0, 1.0);
        }

        [Test]
        public void MakeArrayIndex()
        {
            var array = Expression.Constant(new[] {1, 2}, typeof(int[]));
            var index = Expression.Constant(1);

            var arrayIndex = Expression.MakeBinary
            (
                ExpressionType.ArrayIndex,
                array,
                index
            );

            Assert.AreEqual(ExpressionType.ArrayIndex, arrayIndex.NodeType);
        }

        [Test]
        public void MethodCheck_BadArgs()
        {
            Assert.Throws<ArgumentException>
            (
                () =>
                {
                    Expression left = Expression.Constant("");
                    Expression right = Expression.Constant(1.0);

                    Expression.Add(left, right, Gm("BadMethodSig_1"));
                }
            );
        }

        [Test]
        public void MethodCheck_BadArgs2()
        {
            Assert.Throws<ArgumentException>
            (
                () =>
                {
                    Expression left = Expression.Constant("");
                    Expression right = Expression.Constant(1.0);

                    Expression.Add(left, right, Gm("BadMethodSig_2"));
                }
            );
        }

        [Test]
        public void MethodCheck_BadArgs3()
        {
            Assert.Throws<ArgumentException>
            (
                () =>
                {
                    Expression left = Expression.Constant("");
                    Expression right = Expression.Constant(1.0);

                    Expression.Add(left, right, Gm("BadMethodSig_3"));
                }
            );
        }

        [Test]
        public void MethodChecks()
        {
            Expression left = Expression.Constant("");
            Expression right = Expression.Constant(1.0);

            var r = Expression.Add(left, right, Gm("GoodMethod"));
            Assert.AreEqual(r.Type, typeof(int));
        }

        //
        // Checks that we complain on the proper ExpressionTypes
        //
        [Test]
        public void TestBinaryCtor()
        {
            PassInt(ExpressionType.Add);
            PassInt(ExpressionType.AddChecked);
            PassInt(ExpressionType.And);
            PassInt(ExpressionType.Divide);
            PassInt(ExpressionType.Equal);
            PassInt(ExpressionType.ExclusiveOr);
            PassInt(ExpressionType.GreaterThan);
            PassInt(ExpressionType.GreaterThanOrEqual);
            PassInt(ExpressionType.LeftShift);
            PassInt(ExpressionType.LessThan);
            PassInt(ExpressionType.LessThanOrEqual);
            PassInt(ExpressionType.Multiply);
            PassInt(ExpressionType.MultiplyChecked);
            PassInt(ExpressionType.NotEqual);
            PassInt(ExpressionType.Or);
            PassInt(ExpressionType.Modulo);
            PassInt(ExpressionType.RightShift);
            PassInt(ExpressionType.Subtract);
            PassInt(ExpressionType.SubtractChecked);

            FailInt(ExpressionType.AndAlso);
            FailInt(ExpressionType.OrElse);
            FailInt(ExpressionType.Power);
            FailInt(ExpressionType.ArrayLength);
            FailInt(ExpressionType.ArrayIndex);
            FailInt(ExpressionType.Call);
            FailInt(ExpressionType.Coalesce);
            FailInt(ExpressionType.Conditional);
            FailInt(ExpressionType.Constant);
            FailInt(ExpressionType.Convert);
            FailInt(ExpressionType.ConvertChecked);
            FailInt(ExpressionType.Invoke);
            FailInt(ExpressionType.Lambda);
            FailInt(ExpressionType.ListInit);
            FailInt(ExpressionType.MemberAccess);
            FailInt(ExpressionType.MemberInit);
            FailInt(ExpressionType.Negate);
            FailInt(ExpressionType.UnaryPlus);
            FailInt(ExpressionType.NegateChecked);
            FailInt(ExpressionType.New);
            FailInt(ExpressionType.NewArrayInit);
            FailInt(ExpressionType.NewArrayBounds);
            FailInt(ExpressionType.Not);
            FailInt(ExpressionType.Parameter);
            FailInt(ExpressionType.Quote);
            FailInt(ExpressionType.TypeAs);
            FailInt(ExpressionType.TypeIs);
        }

        [Test]
        public void TestOperations()
        {
            Assert.AreEqual(30, CodeGen(Expression.Add, 10, 20));
            Assert.AreEqual(-12, CodeGen(Expression.Subtract, 11, 23));
            Assert.AreEqual(253, CodeGen(Expression.Multiply, 11, 23));
            Assert.AreEqual(33, CodeGen(Expression.Divide, 100, 3));
            Assert.AreEqual(100.0 / 3, CodeGen<double>(Expression.Divide, 100, 3));
        }
    }
}