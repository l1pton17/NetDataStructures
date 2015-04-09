using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetDataStructures.Heaps
{
    public abstract class BinaryHeap<TKey, TValue> : BinaryHeapBase<HeapElement<TKey, TValue>, TKey, TValue>
        where TKey : IComparable<TKey>
    {
        public Tuple<TKey, TValue> this[int index]
        {
            get { return Tuple.Create(_items[index].Key, _items[index].Value); }
        }

        public Tuple<TKey, TValue> Minimum { get { return this[0]; } }
        public Tuple<TKey, TValue> Maximum { get { return this[_items.Count - 1]; } }

        public BinaryHeap()
        {
        }

        public BinaryHeap(int capacity)
            : base(capacity)
        {
        }

        public BinaryHeap(IEnumerable<Tuple<TKey, TValue>> source)
            : base(source.Select(v => new HeapElement<TKey, TValue>
            {
                Key = v.Item1,
                Value = v.Item2
            }))
        {
        }
    }

    public abstract class BinaryHeap<TKey> : BinaryHeapBase<HeapElement<TKey>, TKey, TKey>
        where TKey : IComparable<TKey>
    {
        public TKey this[int index]
        {
            get { return _items[index].Key; }
        }

        public TKey Minimum { get { return this[0]; } }
        public TKey Maximum { get { return this[_items.Count - 1]; } }

        public BinaryHeap()
        {
        }

        public BinaryHeap(int capacity)
            : base(capacity)
        {
        }

        public BinaryHeap(IEnumerable<TKey> source)
            : base(source.Select(v => new HeapElement<TKey>
            {
                Key = v
            }))
        {
        }
    }

    public abstract class BinaryHeapBase<TElement, TKey, TValue> : IEnumerable<TKey>
        where TElement : class, IHeapElement<TKey, TValue>, new()
        where TKey : IComparable<TKey>
    {
        protected readonly List<TElement> _items;

        public int Count { get { return _items.Count; } }

        public virtual TValue this[TKey key]
        {
            get { return GetOrAdd(key, default(TValue)); }
            set { AddOrUpdate(key, value); }
        }

        public BinaryHeapBase()
        {
            _items = new List<TElement>();
        }

        public BinaryHeapBase(int capacity)
        {
            _items = new List<TElement>(capacity);
        }

        public BinaryHeapBase(IEnumerable<TElement> source)
        {
            _items = source.ToList();

            for (int i = Count / 2; i >= 0; i--)
            {
                SiftDown(i);
            }
        }

        protected abstract bool IsParentCorrect(TKey parent, TKey child);

        public IEnumerator<TKey> GetEnumerator()
        {
            return _items.Select(v => v.Key).GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #region Get

        public virtual TValue Get(TKey key)
        {
            Contract.Requires<ArgumentNullException>(key != null);

            int idx = IndexOf(key);
            if (idx == -1) throw new KeyNotFoundException();

            return _items[idx].Value;
        }

        public virtual TValue GetOrAdd(TKey key, TValue value)
        {
            return GetOrAdd(key, () => value);
        }

        public virtual TValue GetOrAdd(TKey key, Func<TValue> valueFactory)
        {
            Contract.Requires<ArgumentNullException>(key != null);
            Contract.Requires<ArgumentNullException>(valueFactory != null);

            int idx = IndexOf(key);

            if (idx == -1)
            {
                var element = AddInternal(key);
                element.Value = valueFactory();
                return element.Value;
            }
            else
            {
                return _items[idx].Value;
            }
        }

        #endregion

        #region Add

        public virtual void Add(TKey key)
        {
            Contract.Requires<ArgumentNullException>(key != null);

            _items.Add(new TElement
            {
                Key = key
            });
            SiftUp(Count - 1);
        }

        public virtual TValue AddOrUpdate(TKey key, TValue newValue)
        {
            return AddOrUpdate(key, k => newValue, (k, v) => newValue);
        }

        public virtual TValue AddOrUpdate(TKey key, TValue addValue, Func<TKey, TValue, TValue> updateValueFactory)
        {
            return AddOrUpdate(key, k => addValue, updateValueFactory);
        }

        public virtual TValue AddOrUpdate(TKey key, Func<TKey, TValue> addValueFactory, Func<TKey, TValue, TValue> updateValueFactory)
        {
            Contract.Requires<ArgumentNullException>(key != null);
            Contract.Requires<ArgumentNullException>(addValueFactory != null);
            Contract.Requires<ArgumentNullException>(updateValueFactory != null);

            int idx = IndexOf(key);

            if (idx == -1)
            {
                var element = AddInternal(key);
                element.Value = addValueFactory(key);

                return element.Value;
            }
            else
            {
                return _items[idx].Value = updateValueFactory(key, _items[idx].Value);
            }
        }

        #endregion

        public virtual Tuple<TKey, TValue> ExtractTop()
        {
            Contract.Requires<IndexOutOfRangeException>(Count > 0); 

            TElement val = _items[0];
            _items[0] = _items[Count - 1];
            _items.RemoveAt(Count - 1);
            SiftDown(0);

            return Tuple.Create(val.Key, val.Value);
        }

        public virtual bool Remove(TKey key)
        {
            Contract.Requires<ArgumentNullException>(key != null);

            int idx = Find(0, key);
            if (idx == -1) return false;

            _items[idx] = _items[Count - 1];
            _items.RemoveAt(Count - 1);
            SiftDown(idx);

            return true;
        }

        public virtual bool ContainsKey(TKey key)
        {
            Contract.Requires<ArgumentNullException>(key != null);

            return IndexOf(key) != -1;
        }

        public virtual int IndexOf(TKey key)
        {
            Contract.Requires<ArgumentNullException>(key != null);

            return Find(0, key);
        }

        private void Swap(int i, int j)
        {
            TElement temp = _items[i];
            _items[i] = _items[j];
            _items[j] = temp;
        }

        private int GetLeft(int i)
        {
            return 2 * i + 1;
        }

        private int GetRight(int i)
        {
            return 2 * i + 2;
        }

        private int GetParent(int i)
        {
            return (i - 1) / 2;
        }

        protected virtual TElement AddInternal(TKey value)
        {
            Contract.Ensures(Contract.Result<TElement>() != null);

            TElement element = new TElement
            {
                Key = value
            };
            _items.Add(element);
            SiftUp(Count - 1);

            return element;
        }

        protected virtual int Find(int indexFrom, TKey value)
        {
            if (indexFrom >= Count || indexFrom < 0) return -1;

            int cmp = _items[indexFrom].Key.CompareTo(value);
            if (cmp == 0) return indexFrom;

            if (IsParentCorrect(_items[indexFrom].Key, value))
            {
                int idx = Find(GetLeft(indexFrom), value);
                if (idx != -1) return idx;

                idx = Find(GetRight(indexFrom), value);
                if (idx != -1) return idx;
            }

            return -1;
        }

        private void SiftDown(int i)
        {
            while (GetLeft(i) < Count)
            {
                int left = GetLeft(i);
                int right = GetRight(i);
                int j = left;

                if (right < Count && IsParentCorrect(_items[right].Key, _items[left].Key))
                {
                    j = right;
                }
                if (!IsParentCorrect(_items[i].Key, _items[j].Key))
                {
                    break;
                }

                Swap(i, j);
                i = j;
            }
        }

        private void SiftUp(int i)
        {
            while (IsParentCorrect(_items[i].Key, _items[GetParent(i)].Key))
            {
                Swap(i, GetParent(i));
                i = GetParent(i);
            }
        }
    }
}
