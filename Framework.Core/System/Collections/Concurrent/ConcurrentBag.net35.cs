﻿#if NET20 || NET30 || NET35

using System.Collections.Generic;
using Theraot.Collections;
using Theraot.Collections.ThreadSafe;

namespace System.Collections.Concurrent
{
    public class ConcurrentBag<T> : IProducerConsumerCollection<T>, IReadOnlyCollection<T>
    {
        private SafeQueue<T> _wrapped;

        public ConcurrentBag()
        {
            _wrapped = new SafeQueue<T>();
        }

        public ConcurrentBag(IEnumerable<T> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }
            _wrapped = new SafeQueue<T>(collection);
        }

        public int Count => _wrapped.Count;

        int IReadOnlyCollection<T>.Count => Count;

        public bool IsEmpty => Count == 0;

        bool ICollection.IsSynchronized => false;

        object ICollection.SyncRoot => throw new NotSupportedException();

        public void Clear()
        {
            _wrapped = new SafeQueue<T>();
        }

        void ICollection.CopyTo(Array array, int index)
        {
            _wrapped.DeprecatedCopyTo(array, index);
        }

        public void CopyTo(T[] array, int index)
        {
            _wrapped.CopyTo(array, index);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _wrapped.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public T[] ToArray()
        {
            return _wrapped.ToArray();
        }

        bool IProducerConsumerCollection<T>.TryAdd(T item)
        {
            Add(item);
            return true;
        }

        public bool TryPeek(out T result)
        {
            return _wrapped.TryPeek(out result);
        }

        public bool TryTake(out T item)
        {
            return _wrapped.TryTake(out item);
        }

        private void Add(T item)
        {
            _wrapped.Add(item);
        }
    }
}

#endif