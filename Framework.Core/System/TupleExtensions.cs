﻿#if LESSTHAN_NET40

#pragma warning disable CA2201 // Do not raise reserved exception types
// ReSharper disable UseDeconstructionOnParameter

// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.ComponentModel;

namespace System
{
    /// <summary>
    ///     Provides extension methods for <see cref="Tuple" /> instances to interop with C# tuples features (deconstruction
    ///     syntax, converting from and to <see cref="ValueTuple" />).
    /// </summary>
    public static class TupleExtensions
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void Deconstruct<T1>(
            this Tuple<T1> value,
            out T1 item1)
        {
            if (value == null)
            {
                throw new NullReferenceException(nameof(value));
            }
            item1 = value.Item1;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void Deconstruct<T1, T2>(
            this Tuple<T1, T2> value,
            out T1 item1, out T2 item2)
        {
            if (value == null)
            {
                throw new NullReferenceException(nameof(value));
            }
            item1 = value.Item1;
            item2 = value.Item2;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void Deconstruct<T1, T2, T3>(
            this Tuple<T1, T2, T3> value,
            out T1 item1, out T2 item2, out T3 item3)
        {
            if (value == null)
            {
                throw new NullReferenceException(nameof(value));
            }
            item1 = value.Item1;
            item2 = value.Item2;
            item3 = value.Item3;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void Deconstruct<T1, T2, T3, T4>(
            this Tuple<T1, T2, T3, T4> value,
            out T1 item1, out T2 item2, out T3 item3, out T4 item4)
        {
            if (value == null)
            {
                throw new NullReferenceException(nameof(value));
            }
            item1 = value.Item1;
            item2 = value.Item2;
            item3 = value.Item3;
            item4 = value.Item4;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void Deconstruct<T1, T2, T3, T4, T5>(
            this Tuple<T1, T2, T3, T4, T5> value,
            out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5)
        {
            if (value == null)
            {
                throw new NullReferenceException(nameof(value));
            }
            item1 = value.Item1;
            item2 = value.Item2;
            item3 = value.Item3;
            item4 = value.Item4;
            item5 = value.Item5;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void Deconstruct<T1, T2, T3, T4, T5, T6>(
            this Tuple<T1, T2, T3, T4, T5, T6> value,
            out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6)
        {
            if (value == null)
            {
                throw new NullReferenceException(nameof(value));
            }
            item1 = value.Item1;
            item2 = value.Item2;
            item3 = value.Item3;
            item4 = value.Item4;
            item5 = value.Item5;
            item6 = value.Item6;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void Deconstruct<T1, T2, T3, T4, T5, T6, T7>(
            this Tuple<T1, T2, T3, T4, T5, T6, T7> value,
            out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6, out T7 item7)
        {
            if (value == null)
            {
                throw new NullReferenceException(nameof(value));
            }
            item1 = value.Item1;
            item2 = value.Item2;
            item3 = value.Item3;
            item4 = value.Item4;
            item5 = value.Item5;
            item6 = value.Item6;
            item7 = value.Item7;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void Deconstruct<T1, T2, T3, T4, T5, T6, T7, T8>(
            this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8>> value,
            out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6, out T7 item7, out T8 item8)
        {
            if (value == null)
            {
                throw new NullReferenceException(nameof(value));
            }
            item1 = value.Item1;
            item2 = value.Item2;
            item3 = value.Item3;
            item4 = value.Item4;
            item5 = value.Item5;
            item6 = value.Item6;
            item7 = value.Item7;
            item8 = value.Rest.Item1;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void Deconstruct<T1, T2, T3, T4, T5, T6, T7, T8, T9>(
            this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9>> value,
            out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6, out T7 item7, out T8 item8, out T9 item9)
        {
            if (value == null)
            {
                throw new NullReferenceException(nameof(value));
            }
            item1 = value.Item1;
            item2 = value.Item2;
            item3 = value.Item3;
            item4 = value.Item4;
            item5 = value.Item5;
            item6 = value.Item6;
            item7 = value.Item7;
            item8 = value.Rest.Item1;
            item9 = value.Rest.Item2;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void Deconstruct<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(
            this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10>> value,
            out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6, out T7 item7, out T8 item8, out T9 item9, out T10 item10)
        {
            if (value == null)
            {
                throw new NullReferenceException(nameof(value));
            }
            item1 = value.Item1;
            item2 = value.Item2;
            item3 = value.Item3;
            item4 = value.Item4;
            item5 = value.Item5;
            item6 = value.Item6;
            item7 = value.Item7;
            item8 = value.Rest.Item1;
            item9 = value.Rest.Item2;
            item10 = value.Rest.Item3;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void Deconstruct<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(
            this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11>> value,
            out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6, out T7 item7, out T8 item8, out T9 item9, out T10 item10, out T11 item11)
        {
            if (value == null)
            {
                throw new NullReferenceException(nameof(value));
            }
            item1 = value.Item1;
            item2 = value.Item2;
            item3 = value.Item3;
            item4 = value.Item4;
            item5 = value.Item5;
            item6 = value.Item6;
            item7 = value.Item7;
            item8 = value.Rest.Item1;
            item9 = value.Rest.Item2;
            item10 = value.Rest.Item3;
            item11 = value.Rest.Item4;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void Deconstruct<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(
            this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12>> value,
            out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6, out T7 item7, out T8 item8, out T9 item9, out T10 item10, out T11 item11, out T12 item12)
        {
            if (value == null)
            {
                throw new NullReferenceException(nameof(value));
            }
            item1 = value.Item1;
            item2 = value.Item2;
            item3 = value.Item3;
            item4 = value.Item4;
            item5 = value.Item5;
            item6 = value.Item6;
            item7 = value.Item7;
            item8 = value.Rest.Item1;
            item9 = value.Rest.Item2;
            item10 = value.Rest.Item3;
            item11 = value.Rest.Item4;
            item12 = value.Rest.Item5;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void Deconstruct<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(
            this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13>> value,
            out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6, out T7 item7, out T8 item8, out T9 item9, out T10 item10, out T11 item11, out T12 item12, out T13 item13)
        {
            if (value == null)
            {
                throw new NullReferenceException(nameof(value));
            }
            item1 = value.Item1;
            item2 = value.Item2;
            item3 = value.Item3;
            item4 = value.Item4;
            item5 = value.Item5;
            item6 = value.Item6;
            item7 = value.Item7;
            item8 = value.Rest.Item1;
            item9 = value.Rest.Item2;
            item10 = value.Rest.Item3;
            item11 = value.Rest.Item4;
            item12 = value.Rest.Item5;
            item13 = value.Rest.Item6;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void Deconstruct<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(
            this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14>> value,
            out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6, out T7 item7, out T8 item8, out T9 item9, out T10 item10, out T11 item11, out T12 item12, out T13 item13, out T14 item14)
        {
            if (value == null)
            {
                throw new NullReferenceException(nameof(value));
            }
            item1 = value.Item1;
            item2 = value.Item2;
            item3 = value.Item3;
            item4 = value.Item4;
            item5 = value.Item5;
            item6 = value.Item6;
            item7 = value.Item7;
            item8 = value.Rest.Item1;
            item9 = value.Rest.Item2;
            item10 = value.Rest.Item3;
            item11 = value.Rest.Item4;
            item12 = value.Rest.Item5;
            item13 = value.Rest.Item6;
            item14 = value.Rest.Item7;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void Deconstruct<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
            this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15>>> value,
            out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6, out T7 item7, out T8 item8, out T9 item9, out T10 item10, out T11 item11, out T12 item12, out T13 item13, out T14 item14, out T15 item15)
        {
            if (value == null)
            {
                throw new NullReferenceException(nameof(value));
            }
            item1 = value.Item1;
            item2 = value.Item2;
            item3 = value.Item3;
            item4 = value.Item4;
            item5 = value.Item5;
            item6 = value.Item6;
            item7 = value.Item7;
            item8 = value.Rest.Item1;
            item9 = value.Rest.Item2;
            item10 = value.Rest.Item3;
            item11 = value.Rest.Item4;
            item12 = value.Rest.Item5;
            item13 = value.Rest.Item6;
            item14 = value.Rest.Item7;
            item15 = value.Rest.Rest.Item1;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void Deconstruct<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(
            this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16>>> value,
            out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6, out T7 item7, out T8 item8, out T9 item9, out T10 item10, out T11 item11, out T12 item12, out T13 item13, out T14 item14, out T15 item15, out T16 item16)
        {
            if (value == null)
            {
                throw new NullReferenceException(nameof(value));
            }
            item1 = value.Item1;
            item2 = value.Item2;
            item3 = value.Item3;
            item4 = value.Item4;
            item5 = value.Item5;
            item6 = value.Item6;
            item7 = value.Item7;
            item8 = value.Rest.Item1;
            item9 = value.Rest.Item2;
            item10 = value.Rest.Item3;
            item11 = value.Rest.Item4;
            item12 = value.Rest.Item5;
            item13 = value.Rest.Item6;
            item14 = value.Rest.Item7;
            item15 = value.Rest.Rest.Item1;
            item16 = value.Rest.Rest.Item2;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void Deconstruct<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>(
            this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17>>> value,
            out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6, out T7 item7, out T8 item8, out T9 item9, out T10 item10, out T11 item11, out T12 item12, out T13 item13, out T14 item14, out T15 item15, out T16 item16, out T17 item17)
        {
            if (value == null)
            {
                throw new NullReferenceException(nameof(value));
            }
            item1 = value.Item1;
            item2 = value.Item2;
            item3 = value.Item3;
            item4 = value.Item4;
            item5 = value.Item5;
            item6 = value.Item6;
            item7 = value.Item7;
            item8 = value.Rest.Item1;
            item9 = value.Rest.Item2;
            item10 = value.Rest.Item3;
            item11 = value.Rest.Item4;
            item12 = value.Rest.Item5;
            item13 = value.Rest.Item6;
            item14 = value.Rest.Item7;
            item15 = value.Rest.Rest.Item1;
            item16 = value.Rest.Rest.Item2;
            item17 = value.Rest.Rest.Item3;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void Deconstruct<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>(
            this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18>>> value,
            out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6, out T7 item7, out T8 item8, out T9 item9, out T10 item10, out T11 item11, out T12 item12, out T13 item13, out T14 item14, out T15 item15, out T16 item16, out T17 item17, out T18 item18)
        {
            if (value == null)
            {
                throw new NullReferenceException(nameof(value));
            }
            item1 = value.Item1;
            item2 = value.Item2;
            item3 = value.Item3;
            item4 = value.Item4;
            item5 = value.Item5;
            item6 = value.Item6;
            item7 = value.Item7;
            item8 = value.Rest.Item1;
            item9 = value.Rest.Item2;
            item10 = value.Rest.Item3;
            item11 = value.Rest.Item4;
            item12 = value.Rest.Item5;
            item13 = value.Rest.Item6;
            item14 = value.Rest.Item7;
            item15 = value.Rest.Rest.Item1;
            item16 = value.Rest.Rest.Item2;
            item17 = value.Rest.Rest.Item3;
            item18 = value.Rest.Rest.Item4;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void Deconstruct<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>(
            this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18, T19>>> value,
            out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6, out T7 item7, out T8 item8, out T9 item9, out T10 item10, out T11 item11, out T12 item12, out T13 item13, out T14 item14, out T15 item15, out T16 item16, out T17 item17, out T18 item18, out T19 item19)
        {
            if (value == null)
            {
                throw new NullReferenceException(nameof(value));
            }
            item1 = value.Item1;
            item2 = value.Item2;
            item3 = value.Item3;
            item4 = value.Item4;
            item5 = value.Item5;
            item6 = value.Item6;
            item7 = value.Item7;
            item8 = value.Rest.Item1;
            item9 = value.Rest.Item2;
            item10 = value.Rest.Item3;
            item11 = value.Rest.Item4;
            item12 = value.Rest.Item5;
            item13 = value.Rest.Item6;
            item14 = value.Rest.Item7;
            item15 = value.Rest.Rest.Item1;
            item16 = value.Rest.Rest.Item2;
            item17 = value.Rest.Rest.Item3;
            item18 = value.Rest.Rest.Item4;
            item19 = value.Rest.Rest.Item5;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void Deconstruct<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>(
            this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18, T19, T20>>> value,
            out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6, out T7 item7, out T8 item8, out T9 item9, out T10 item10, out T11 item11, out T12 item12, out T13 item13, out T14 item14, out T15 item15, out T16 item16, out T17 item17, out T18 item18, out T19 item19, out T20 item20)
        {
            if (value == null)
            {
                throw new NullReferenceException(nameof(value));
            }
            item1 = value.Item1;
            item2 = value.Item2;
            item3 = value.Item3;
            item4 = value.Item4;
            item5 = value.Item5;
            item6 = value.Item6;
            item7 = value.Item7;
            item8 = value.Rest.Item1;
            item9 = value.Rest.Item2;
            item10 = value.Rest.Item3;
            item11 = value.Rest.Item4;
            item12 = value.Rest.Item5;
            item13 = value.Rest.Item6;
            item14 = value.Rest.Item7;
            item15 = value.Rest.Rest.Item1;
            item16 = value.Rest.Rest.Item2;
            item17 = value.Rest.Rest.Item3;
            item18 = value.Rest.Rest.Item4;
            item19 = value.Rest.Rest.Item5;
            item20 = value.Rest.Rest.Item6;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void Deconstruct<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>(
            this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18, T19, T20, T21>>> value,
            out T1 item1, out T2 item2, out T3 item3, out T4 item4, out T5 item5, out T6 item6, out T7 item7, out T8 item8, out T9 item9, out T10 item10, out T11 item11, out T12 item12, out T13 item13, out T14 item14, out T15 item15, out T16 item16, out T17 item17, out T18 item18, out T19 item19, out T20 item20, out T21 item21)
        {
            if (value == null)
            {
                throw new NullReferenceException(nameof(value));
            }
            item1 = value.Item1;
            item2 = value.Item2;
            item3 = value.Item3;
            item4 = value.Item4;
            item5 = value.Item5;
            item6 = value.Item6;
            item7 = value.Item7;
            item8 = value.Rest.Item1;
            item9 = value.Rest.Item2;
            item10 = value.Rest.Item3;
            item11 = value.Rest.Item4;
            item12 = value.Rest.Item5;
            item13 = value.Rest.Item6;
            item14 = value.Rest.Item7;
            item15 = value.Rest.Rest.Item1;
            item16 = value.Rest.Rest.Item2;
            item17 = value.Rest.Rest.Item3;
            item18 = value.Rest.Rest.Item4;
            item19 = value.Rest.Rest.Item5;
            item20 = value.Rest.Rest.Item6;
            item21 = value.Rest.Rest.Item7;
        }

        public static Tuple<T1>
            ToTuple<T1>(
                this ValueTuple<T1> value)
        {
            return Tuple.Create(value.Item1);
        }

        public static Tuple<T1, T2>
            ToTuple<T1, T2>(
                this ValueTuple<T1, T2> value)
        {
            return Tuple.Create(value.Item1, value.Item2);
        }

        public static Tuple<T1, T2, T3>
            ToTuple<T1, T2, T3>(
                this ValueTuple<T1, T2, T3> value)
        {
            return Tuple.Create(value.Item1, value.Item2, value.Item3);
        }

        public static Tuple<T1, T2, T3, T4>
            ToTuple<T1, T2, T3, T4>(
                this ValueTuple<T1, T2, T3, T4> value)
        {
            return Tuple.Create(value.Item1, value.Item2, value.Item3, value.Item4);
        }

        public static Tuple<T1, T2, T3, T4, T5>
            ToTuple<T1, T2, T3, T4, T5>(
                this ValueTuple<T1, T2, T3, T4, T5> value)
        {
            return Tuple.Create(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5);
        }

        public static Tuple<T1, T2, T3, T4, T5, T6>
            ToTuple<T1, T2, T3, T4, T5, T6>(
                this ValueTuple<T1, T2, T3, T4, T5, T6> value)
        {
            return Tuple.Create(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6);
        }

        public static Tuple<T1, T2, T3, T4, T5, T6, T7>
            ToTuple<T1, T2, T3, T4, T5, T6, T7>(
                this ValueTuple<T1, T2, T3, T4, T5, T6, T7> value)
        {
            return Tuple.Create(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7);
        }

        public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8>>
            ToTuple<T1, T2, T3, T4, T5, T6, T7, T8>(
                this ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8>> value)
        {
            return CreateLongRef
            (
                value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7,
                Tuple.Create(value.Rest.Item1)
            );
        }

        public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9>>
            ToTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9>(
                this ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9>> value)
        {
            return CreateLongRef
            (
                value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7,
                Tuple.Create(value.Rest.Item1, value.Rest.Item2)
            );
        }

        public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10>>
            ToTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(
                this ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10>> value)
        {
            return CreateLongRef
            (
                value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7,
                Tuple.Create(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3)
            );
        }

        public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11>>
            ToTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(
                this ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11>> value)
        {
            return CreateLongRef
            (
                value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7,
                Tuple.Create(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4)
            );
        }

        public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12>>
            ToTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(
                this ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12>> value)
        {
            return CreateLongRef
            (
                value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7,
                Tuple.Create(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5)
            );
        }

        public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13>>
            ToTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(
                this ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13>> value)
        {
            return CreateLongRef
            (
                value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7,
                Tuple.Create(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6)
            );
        }

        public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14>>
            ToTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(
                this ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14>> value)
        {
            return CreateLongRef
            (
                value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7,
                Tuple.Create(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6, value.Rest.Item7)
            );
        }

        public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15>>>
            ToTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
                this ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>> value)
        {
            return CreateLongRef
            (
                value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7,
                CreateLongRef
                (
                    value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6, value.Rest.Item7,
                    Tuple.Create(value.Rest.Rest.Item1)
                )
            );
        }

        public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16>>>
            ToTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(
                this ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15, T16>>> value)
        {
            return CreateLongRef
            (
                value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7,
                CreateLongRef
                (
                    value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6, value.Rest.Item7,
                    Tuple.Create(value.Rest.Rest.Item1, value.Rest.Rest.Item2)
                )
            );
        }

        public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17>>>
            ToTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>(
                this ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15, T16, T17>>> value)
        {
            return CreateLongRef
            (
                value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7,
                CreateLongRef
                (
                    value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6, value.Rest.Item7,
                    Tuple.Create(value.Rest.Rest.Item1, value.Rest.Rest.Item2, value.Rest.Rest.Item3)
                )
            );
        }

        public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18>>>
            ToTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>(
                this ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15, T16, T17, T18>>> value)
        {
            return CreateLongRef
            (
                value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7,
                CreateLongRef
                (
                    value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6, value.Rest.Item7,
                    Tuple.Create(value.Rest.Rest.Item1, value.Rest.Rest.Item2, value.Rest.Rest.Item3, value.Rest.Rest.Item4)
                )
            );
        }

        public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18, T19>>>
            ToTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>(
                this ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15, T16, T17, T18, T19>>> value)
        {
            return CreateLongRef
            (
                value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7,
                CreateLongRef
                (
                    value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6, value.Rest.Item7,
                    Tuple.Create(value.Rest.Rest.Item1, value.Rest.Rest.Item2, value.Rest.Rest.Item3, value.Rest.Rest.Item4, value.Rest.Rest.Item5)
                )
            );
        }

        public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18, T19, T20>>>
            ToTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>(
                this ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15, T16, T17, T18, T19, T20>>> value)
        {
            return CreateLongRef
            (
                value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7,
                CreateLongRef
                (
                    value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6, value.Rest.Item7,
                    Tuple.Create(value.Rest.Rest.Item1, value.Rest.Rest.Item2, value.Rest.Rest.Item3, value.Rest.Rest.Item4, value.Rest.Rest.Item5, value.Rest.Rest.Item6)
                )
            );
        }

        public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18, T19, T20, T21>>>
            ToTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>(
                this ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15, T16, T17, T18, T19, T20, T21>>> value)
        {
            return CreateLongRef
            (
                value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7,
                CreateLongRef
                (
                    value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6, value.Rest.Item7,
                    Tuple.Create(value.Rest.Rest.Item1, value.Rest.Rest.Item2, value.Rest.Rest.Item3, value.Rest.Rest.Item4, value.Rest.Rest.Item5, value.Rest.Rest.Item6, value.Rest.Rest.Item7)
                )
            );
        }

        public static ValueTuple<T1>
            ToValueTuple<T1>(
                this Tuple<T1> value)
        {
            if (value == null)
            {
                throw new NullReferenceException(nameof(value));
            }
            return ValueTuple.Create(value.Item1);
        }

        public static ValueTuple<T1, T2>
            ToValueTuple<T1, T2>(
                this Tuple<T1, T2> value)
        {
            if (value == null)
            {
                throw new NullReferenceException(nameof(value));
            }
            return ValueTuple.Create(value.Item1, value.Item2);
        }

        public static ValueTuple<T1, T2, T3>
            ToValueTuple<T1, T2, T3>(
                this Tuple<T1, T2, T3> value)
        {
            if (value == null)
            {
                throw new NullReferenceException(nameof(value));
            }
            return ValueTuple.Create(value.Item1, value.Item2, value.Item3);
        }

        public static ValueTuple<T1, T2, T3, T4>
            ToValueTuple<T1, T2, T3, T4>(
                this Tuple<T1, T2, T3, T4> value)
        {
            if (value == null)
            {
                throw new NullReferenceException(nameof(value));
            }
            return ValueTuple.Create(value.Item1, value.Item2, value.Item3, value.Item4);
        }

        public static ValueTuple<T1, T2, T3, T4, T5>
            ToValueTuple<T1, T2, T3, T4, T5>(
                this Tuple<T1, T2, T3, T4, T5> value)
        {
            if (value == null)
            {
                throw new NullReferenceException(nameof(value));
            }
            return ValueTuple.Create(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5);
        }

        public static ValueTuple<T1, T2, T3, T4, T5, T6>
            ToValueTuple<T1, T2, T3, T4, T5, T6>(
                this Tuple<T1, T2, T3, T4, T5, T6> value)
        {
            if (value == null)
            {
                throw new NullReferenceException(nameof(value));
            }
            return ValueTuple.Create(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6);
        }

        public static ValueTuple<T1, T2, T3, T4, T5, T6, T7>
            ToValueTuple<T1, T2, T3, T4, T5, T6, T7>(
                this Tuple<T1, T2, T3, T4, T5, T6, T7> value)
        {
            if (value == null)
            {
                throw new NullReferenceException(nameof(value));
            }
            return ValueTuple.Create(value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7);
        }

        public static ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8>>
            ToValueTuple<T1, T2, T3, T4, T5, T6, T7, T8>(
                this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8>> value)
        {
            if (value == null)
            {
                throw new NullReferenceException(nameof(value));
            }
            return CreateLong
            (
                value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7,
                ValueTuple.Create(value.Rest.Item1)
            );
        }

        public static ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9>>
            ToValueTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9>(
                this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9>> value)
        {
            if (value == null)
            {
                throw new NullReferenceException(nameof(value));
            }
            return CreateLong
            (
                value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7,
                ValueTuple.Create(value.Rest.Item1, value.Rest.Item2)
            );
        }

        public static ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10>>
            ToValueTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(
                this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10>> value)
        {
            if (value == null)
            {
                throw new NullReferenceException(nameof(value));
            }
            return CreateLong
            (
                value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7,
                ValueTuple.Create(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3)
            );
        }

        public static ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11>>
            ToValueTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(
                this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11>> value)
        {
            if (value == null)
            {
                throw new NullReferenceException(nameof(value));
            }
            return CreateLong
            (
                value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7,
                ValueTuple.Create(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4)
            );
        }

        public static ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12>>
            ToValueTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(
                this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12>> value)
        {
            if (value == null)
            {
                throw new NullReferenceException(nameof(value));
            }
            return CreateLong
            (
                value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7,
                ValueTuple.Create(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5)
            );
        }

        public static ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13>>
            ToValueTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(
                this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13>> value)
        {
            if (value == null)
            {
                throw new NullReferenceException(nameof(value));
            }
            return CreateLong
            (
                value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7,
                ValueTuple.Create(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6)
            );
        }

        public static ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14>>
            ToValueTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(
                this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14>> value)
        {
            if (value == null)
            {
                throw new NullReferenceException(nameof(value));
            }
            return CreateLong
            (
                value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7,
                ValueTuple.Create(value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6, value.Rest.Item7)
            );
        }

        public static ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15>>>
            ToValueTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
                this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15>>> value)
        {
            if (value == null)
            {
                throw new NullReferenceException(nameof(value));
            }
            return CreateLong
            (
                value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7,
                CreateLong
                (
                    value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6, value.Rest.Item7,
                    ValueTuple.Create(value.Rest.Rest.Item1)
                )
            );
        }

        public static ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15, T16>>>
            ToValueTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(
                this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16>>> value)
        {
            if (value == null)
            {
                throw new NullReferenceException(nameof(value));
            }
            return CreateLong
            (
                value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7,
                CreateLong
                (
                    value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6, value.Rest.Item7,
                    ValueTuple.Create(value.Rest.Rest.Item1, value.Rest.Rest.Item2)
                )
            );
        }

        public static ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15, T16, T17>>>
            ToValueTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17>(
                this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17>>> value)
        {
            if (value == null)
            {
                throw new NullReferenceException(nameof(value));
            }
            return CreateLong
            (
                value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7,
                CreateLong
                (
                    value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6, value.Rest.Item7,
                    ValueTuple.Create(value.Rest.Rest.Item1, value.Rest.Rest.Item2, value.Rest.Rest.Item3)
                )
            );
        }

        public static ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15, T16, T17, T18>>>
            ToValueTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18>(
                this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18>>> value)
        {
            if (value == null)
            {
                throw new NullReferenceException(nameof(value));
            }
            return CreateLong
            (
                value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7,
                CreateLong
                (
                    value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6, value.Rest.Item7,
                    ValueTuple.Create(value.Rest.Rest.Item1, value.Rest.Rest.Item2, value.Rest.Rest.Item3, value.Rest.Rest.Item4)
                )
            );
        }

        public static ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15, T16, T17, T18, T19>>>
            ToValueTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19>(
                this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18, T19>>> value)
        {
            if (value == null)
            {
                throw new NullReferenceException(nameof(value));
            }
            return CreateLong
            (
                value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7,
                CreateLong
                (
                    value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6, value.Rest.Item7,
                    ValueTuple.Create(value.Rest.Rest.Item1, value.Rest.Rest.Item2, value.Rest.Rest.Item3, value.Rest.Rest.Item4, value.Rest.Rest.Item5)
                )
            );
        }

        public static ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15, T16, T17, T18, T19, T20>>>
            ToValueTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20>(
                this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18, T19, T20>>> value)
        {
            if (value == null)
            {
                throw new NullReferenceException(nameof(value));
            }
            return CreateLong
            (
                value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7,
                CreateLong
                (
                    value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6, value.Rest.Item7,
                    ValueTuple.Create(value.Rest.Rest.Item1, value.Rest.Rest.Item2, value.Rest.Rest.Item3, value.Rest.Rest.Item4, value.Rest.Rest.Item5, value.Rest.Rest.Item6)
                )
            );
        }

        public static ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8, T9, T10, T11, T12, T13, T14, ValueTuple<T15, T16, T17, T18, T19, T20, T21>>>
            ToValueTuple<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, T17, T18, T19, T20, T21>(
                this Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8, T9, T10, T11, T12, T13, T14, Tuple<T15, T16, T17, T18, T19, T20, T21>>> value)
        {
            if (value == null)
            {
                throw new NullReferenceException(nameof(value));
            }
            return CreateLong
            (
                value.Item1, value.Item2, value.Item3, value.Item4, value.Item5, value.Item6, value.Item7,
                CreateLong
                (
                    value.Rest.Item1, value.Rest.Item2, value.Rest.Item3, value.Rest.Item4, value.Rest.Item5, value.Rest.Item6, value.Rest.Item7,
                    ValueTuple.Create(value.Rest.Rest.Item1, value.Rest.Rest.Item2, value.Rest.Rest.Item3, value.Rest.Rest.Item4, value.Rest.Rest.Item5, value.Rest.Rest.Item6, value.Rest.Rest.Item7)
                )
            );
        }

        private static ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest> CreateLong<T1, T2, T3, T4, T5, T6, T7, TRest>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, TRest rest)
            where TRest : struct
        {
            return new ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest>(item1, item2, item3, item4, item5, item6, item7, rest);
        }

        private static Tuple<T1, T2, T3, T4, T5, T6, T7, TRest> CreateLongRef<T1, T2, T3, T4, T5, T6, T7, TRest>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, TRest rest)
        {
            return new Tuple<T1, T2, T3, T4, T5, T6, T7, TRest>(item1, item2, item3, item4, item5, item6, item7, rest);
        }
    }
}

#endif