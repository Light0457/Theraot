﻿using System;
using System.Collections.Generic;
using System.Linq;
using Theraot.Collections.Specialized;
using Theraot.Core;

namespace Theraot.Collections
{
    public static partial class Extensions
    {
        public static IEnumerable<IGrouping<TKey, TSource>> GroupProgressiveBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            return GroupProgressiveBy(source, keySelector, null);
        }

        public static IEnumerable<IGrouping<TKey, TSource>> GroupProgressiveBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (keySelector == null)
            {
                throw new ArgumentNullException(nameof(keySelector));
            }
            return GroupBuilder<TKey, TSource, TSource>.CreateGroups(source, comparer, keySelector, FuncHelper.GetIdentityFunc<TSource>());
        }

        public static IEnumerable<IGrouping<TKey, TElement>> GroupProgressiveBy<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
        {
            return GroupProgressiveBy(source, keySelector, elementSelector, null);
        }

        public static IEnumerable<IGrouping<TKey, TElement>> GroupProgressiveBy<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> resultSelector, IEqualityComparer<TKey> comparer)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (keySelector == null)
            {
                throw new ArgumentNullException(nameof(keySelector));
            }
            if (resultSelector == null)
            {
                throw new ArgumentNullException(nameof(resultSelector));
            }
            return GroupBuilder<TKey, TSource, TElement>.CreateGroups(source, comparer, keySelector, resultSelector);
        }

        public static IEnumerable<TResult> GroupProgressiveBy<TSource, TKey, TElement, TResult>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, Func<TKey, IEnumerable<TElement>, TResult> resultSelector)
        {
            return GroupProgressiveBy(source, keySelector, elementSelector, resultSelector, null);
        }

        public static IEnumerable<TResult> GroupProgressiveBy<TSource, TKey, TElement, TResult>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, Func<TKey, IEnumerable<TElement>, TResult> resultSelector, IEqualityComparer<TKey> comparer)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (keySelector == null)
            {
                throw new ArgumentNullException(nameof(keySelector));
            }
            if (elementSelector == null)
            {
                throw new ArgumentNullException(nameof(elementSelector));
            }
            if (resultSelector == null)
            {
                throw new ArgumentNullException(nameof(resultSelector));
            }
            return CreateGroupByIterator();
            IEnumerable<TResult> CreateGroupByIterator()
            {
                var groups = GroupProgressiveBy(source, keySelector, elementSelector, comparer);

                foreach (var group in groups)
                {
                    yield return resultSelector(group.Key, group);
                }
            }
        }

        public static IEnumerable<TResult> GroupProgressiveBy<TSource, TKey, TResult>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TKey, IEnumerable<TSource>, TResult> resultSelector)
        {
            return GroupProgressiveBy(source, keySelector, resultSelector, null);
        }

        public static IEnumerable<TResult> GroupProgressiveBy<TSource, TKey, TResult>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TKey, IEnumerable<TSource>, TResult> resultSelector, IEqualityComparer<TKey> comparer)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (keySelector == null)
            {
                throw new ArgumentNullException(nameof(keySelector));
            }
            if (resultSelector == null)
            {
                throw new ArgumentNullException(nameof(resultSelector));
            }
            return CreateGroupByIterator();

            IEnumerable<TResult> CreateGroupByIterator()
            {
                var groups = GroupProgressiveBy(source, keySelector, comparer);

                foreach (var group in groups)
                {
                    yield return resultSelector(group.Key, group);
                }
            }
        }
    }
}