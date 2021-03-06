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
//		Federico Di Gregorio <fog@initd.org>
//		Jb Evain <jbevain@novell.com>

using System;
using System.Linq.Expressions;
using NUnit.Framework;

#if TARGETS_NETCORE || TARGETS_NETSTANDARD
using System.Reflection;

#endif

namespace MonoTests.System.Linq.Expressions
{
    [TestFixture]
    public class ExpressionTestOr
    {
        [Test]
        public void Arg1Null()
        {
            Assert.Throws<ArgumentNullException>(() => Expression.Or(null, Expression.Constant(1)));
        }

        [Test]
        public void Arg2Null()
        {
            Assert.Throws<ArgumentNullException>(() => Expression.Or(Expression.Constant(1), null));
        }

        [Test]
        public void ArgTypesDifferent()
        {
            Assert.Throws<InvalidOperationException>(() => Expression.Or(Expression.Constant(1), Expression.Constant(true)));
        }

        [Test]
        public void Boolean()
        {
            var expr = Expression.Or(Expression.Constant(true), Expression.Constant(false));
            Assert.AreEqual(ExpressionType.Or, expr.NodeType, "Or#05");
            Assert.AreEqual(typeof(bool), expr.Type, "Or#06");
            Assert.IsNull(expr.Method, "Or#07");
            Assert.AreEqual("(True Or False)", expr.ToString(), "Or#08");
        }

        [Test]
        public void Double()
        {
            Assert.Throws<InvalidOperationException>(() => Expression.Or(Expression.Constant(1.0), Expression.Constant(2.0)));
        }

        [Test]
        public void Integer()
        {
            var expr = Expression.Or(Expression.Constant(1), Expression.Constant(2));
            Assert.AreEqual(ExpressionType.Or, expr.NodeType, "Or#01");
            Assert.AreEqual(typeof(int), expr.Type, "Or#02");
            Assert.IsNull(expr.Method, "Or#03");
            Assert.AreEqual("(1 | 2)", expr.ToString(), "Or#04");
        }

        [Test]
        public void NoOperatorClass()
        {
            Assert.Throws<InvalidOperationException>(() => Expression.Or(Expression.Constant(new NoOpClass()), Expression.Constant(new NoOpClass())));
        }

        [Test]
        public void OrBoolItem()
        {
            var i = Expression.Parameter(typeof(Item<bool>), "i");
            var compiled = Expression.Lambda<Func<Item<bool>, bool>>
            (
                Expression.Or
                (
                    Expression.Property(i, "Left"),
                    Expression.Property(i, "Right")
                ), i
            ).Compile();

            var item = new Item<bool>(true, false);
            Assert.AreEqual(true, compiled(item));
            Assert.IsTrue(item.LeftCalled);
            Assert.IsTrue(item.RightCalled);
        }

        [Test]
        public void OrBoolNullableTest()
        {
            var a = Expression.Parameter(typeof(bool?), "a");
            var b = Expression.Parameter(typeof(bool?), "b");
            var lambda = Expression.Lambda<Func<bool?, bool?, bool?>>
            (
                Expression.Or(a, b), a, b
            );

            var be = lambda.Body as BinaryExpression;
            Assert.IsNotNull(be);
            Assert.AreEqual(typeof(bool?), be.Type);
            Assert.IsTrue(be.IsLifted);
            Assert.IsTrue(be.IsLiftedToNull);

            var compiled = lambda.Compile();

            Assert.AreEqual(true, compiled(true, true), "o1");
            Assert.AreEqual(true, compiled(true, false), "o2");
            Assert.AreEqual(true, compiled(false, true), "o3");
            Assert.AreEqual(false, compiled(false, false), "o4");

            Assert.AreEqual(true, compiled(true, null), "o5");
            Assert.AreEqual(null, compiled(false, null), "o6");
            Assert.AreEqual(null, compiled(null, false), "o7");
            Assert.AreEqual(true, compiled(true, null), "o8");
            Assert.AreEqual(null, compiled(null, null), "o9");
        }

        [Test]
        public void OrBoolTest()
        {
            var a = Expression.Parameter(typeof(bool), "a");
            var b = Expression.Parameter(typeof(bool), "b");
            var lambda = Expression.Lambda<Func<bool, bool, bool>>
            (
                Expression.Or(a, b), a, b
            );

            var be = lambda.Body as BinaryExpression;
            Assert.IsNotNull(be);
            Assert.AreEqual(typeof(bool), be.Type);
            Assert.IsFalse(be.IsLifted);
            Assert.IsFalse(be.IsLiftedToNull);

            var compiled = lambda.Compile();

            Assert.AreEqual(true, compiled(true, true), "o1");
            Assert.AreEqual(true, compiled(true, false), "o2");
            Assert.AreEqual(true, compiled(false, true), "o3");
            Assert.AreEqual(false, compiled(false, false), "o4");
        }

        [Test]
        public void OrIntNullableTest()
        {
            var a = Expression.Parameter(typeof(int?), "a");
            var b = Expression.Parameter(typeof(int?), "b");
            var compiled = Expression.Lambda<Func<int?, int?, int?>>
            (
                Expression.Or(a, b), a, b
            ).Compile();

            Assert.AreEqual((int?)1, compiled(1, 1), "o1");
            Assert.AreEqual((int?)1, compiled(1, 0), "o2");
            Assert.AreEqual((int?)1, compiled(0, 1), "o3");
            Assert.AreEqual((int?)0, compiled(0, 0), "o4");

            Assert.AreEqual(null, compiled(1, null), "o5");
            Assert.AreEqual(null, compiled(0, null), "o6");
            Assert.AreEqual(null, compiled(null, 0), "o7");
            Assert.AreEqual(null, compiled(1, null), "o8");
            Assert.AreEqual(null, compiled(null, null), "o9");
        }

        [Test]
        public void OrIntTest()
        {
            var a = Expression.Parameter(typeof(int), "a");
            var b = Expression.Parameter(typeof(int), "b");
            var compiled = Expression.Lambda<Func<int, int, int>>
            (
                Expression.Or(a, b), a, b
            ).Compile();

            Assert.AreEqual((int?)1, compiled(1, 1), "o1");
            Assert.AreEqual((int?)1, compiled(1, 0), "o2");
            Assert.AreEqual((int?)1, compiled(0, 1), "o3");
            Assert.AreEqual((int?)0, compiled(0, 0), "o4");
        }

        [Test]
        public void OrNullableBoolItem()
        {
            var i = Expression.Parameter(typeof(Item<bool?>), "i");
            var compiled = Expression.Lambda<Func<Item<bool?>, bool?>>
            (
                Expression.Or
                (
                    Expression.Property(i, "Left"),
                    Expression.Property(i, "Right")
                ), i
            ).Compile();

            var item = new Item<bool?>(true, false);
            Assert.AreEqual((bool?)true, compiled(item));
            Assert.IsTrue(item.LeftCalled);
            Assert.IsTrue(item.RightCalled);
        }

        [Test]
        public void UserDefinedClass()
        {
            // We can use the simplest version of GetMethod because we already know only one
            // exists in the very simple class we're using for the tests.
            var method = typeof(OpClass).GetMethod("op_BitwiseOr");

            var expr = Expression.Or(Expression.Constant(new OpClass()), Expression.Constant(new OpClass()));
            Assert.AreEqual(ExpressionType.Or, expr.NodeType, "Or#09");
            Assert.AreEqual(typeof(OpClass), expr.Type, "Or#10");
            Assert.AreEqual(method, expr.Method, "Or#11");
            Assert.AreEqual
            (
                "(value(MonoTests.System.Linq.Expressions.OpClass) | value(MonoTests.System.Linq.Expressions.OpClass))",
                expr.ToString(), "Or#13"
            );
        }
    }
}