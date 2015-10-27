using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace NetDataStructures.Structures
{
    [Serializable]
    internal class ValueCollection<TKey, TValue> : ReadOnlyCollection<TValue>
    {
        private readonly IDictionary<TKey, TValue> _dictionary;

        public ValueCollection(IDictionary<TKey, TValue> dictionary)
            : base(dictionary.Select(v => v.Value))
        {
            Contract.Requires<ArgumentNullException>(dictionary != null, "dictionary");

            _dictionary = dictionary;
        }

        public override int Count
        {
            get { return _dictionary.Count; }
        }
    }

    [Serializable]
    internal class KeyCollection<TKey, TValue> : ReadOnlyCollection<TKey>
    {
        private readonly IDictionary<TKey, TValue> _dictionary;

        public KeyCollection(IDictionary<TKey, TValue> dictionary)
            : base(dictionary.Select(v => v.Key))
        {
            Contract.Requires<ArgumentNullException>(dictionary != null, "dictionary");

            _dictionary = dictionary;
        }

        public override int Count
        {
            get { return _dictionary.Count; }
        }

        public override bool Contains(TKey item)
        {
            return _dictionary.ContainsKey(item);
        }
    }

    [Serializable]
    internal class ReadOnlyCollection<T> : ICollection<T>
    {
        public ReadOnlyCollection(IEnumerable<T> source)
        {
            Contract.Requires<ArgumentNullException>(source != null, "source");

            Source = source;
        }

        protected IEnumerable<T> Source { get; private set; }

        public virtual void Add(T item)
        {
            throw new InvalidOperationException();
        }

        public virtual void Clear()
        {
            throw new InvalidOperationException();
        }

        public virtual bool Contains(T item)
        {
            return Source.Contains(item);
        }

        public virtual void CopyTo(T[] array, int arrayIndex)
        {
            Contract.Requires<ArgumentNullException>(array != null, "array");
            Contract.Requires<ArgumentOutOfRangeException>(arrayIndex < 0 || arrayIndex > array.Length);

            var i = 0;
            foreach (var item in Source)
            {
                array[i + arrayIndex] = item;
                i++;
            }
        }

        public virtual int Count
        {
            get { return Source.Count(); }
        }

        public bool IsReadOnly
        {
            get { return true; }
        }

        public virtual bool Remove(T item)
        {
            throw new InvalidOperationException();
        }

        public virtual IEnumerator<T> GetEnumerator()
        {
            return Source.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}