using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace NetDataStructures.Structures.Heaps
{

    internal sealed class MaxBinaryHeapCore<TItem, TKey, TValue> : BinaryHeapCore<TItem, TKey, TValue>
        where TItem : class, ICostItem<TKey, TValue>, new()
        where TKey : IComparable<TKey>
    {
        public MaxBinaryHeapCore()
        {
        }

        public MaxBinaryHeapCore(int capacity)
            : base(capacity)
        {
        }

        public MaxBinaryHeapCore(IEnumerable<TItem> source)
            : base(source)
        {
        }

        protected override bool IsParentCorrect(TKey parent, TKey child)
        {
            return parent.CompareTo(child) > 0;
        }
    }

    internal sealed class MinBinaryHeapCore<TItem, TKey, TValue> : BinaryHeapCore<TItem, TKey, TValue>
        where TItem : class, ICostItem<TKey, TValue>, new()
        where TKey : IComparable<TKey>
    {
        public MinBinaryHeapCore()
        {
        }

        public MinBinaryHeapCore(int capacity)
            : base(capacity)
        {
        }

        public MinBinaryHeapCore(IEnumerable<TItem> source)
            : base(source)
        {
        }

        protected override bool IsParentCorrect(TKey parent, TKey child)
        {
            return parent.CompareTo(child) < 0;
        }
    }

    internal abstract class BinaryHeapCore<TItem, TKey, TValue> : IListDataStructure<TItem, TKey, TValue>
        where TItem : class, ICostItem<TKey, TValue>, new()
        where TKey : IComparable<TKey>
    {
        protected List<TItem> Items { get; private set; }

        public TItem this[int index] { get { return Items[index]; } }

        public BinaryHeapCore()
        {
            Items = new List<TItem>();
        }

        public BinaryHeapCore(int capacity)
        {
            Items = new List<TItem>(capacity);
        }

        public BinaryHeapCore(IEnumerable<TItem> source)
        {
            Items = source.ToList();

            for (var i = Count/2 - 1; i >= 0; i--)
            {
                SiftDown(i);
            }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public int Count
        {
            get { return Items.Count; }
        }

        public TItem Add(TKey key)
        {
            Contract.Requires<ArgumentNullException>(key != null, "key");
            Contract.Ensures(Contract.Result<TItem>() != null);

            var element = new TItem
            {
                Key = key
            };
            Items.Add(element);
            SiftUp(Count - 1);

            return element;
        }

        public virtual void Clear()
        {
            Contract.Ensures(Count == 0);

            Items.Clear();
        }

        public TItem GetNode(TKey key)
        {
            var idx = IndexOf(key);
            if (idx == -1) return null;

            return Items[idx];
        }

        public IEnumerator<TItem> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Remove(TItem node)
        {
            Contract.Requires<ArgumentNullException>(node != null, "node");

            if (!Remove(node.Key)) throw new KeyNotFoundException();
        }

        public bool Contains(TKey key)
        {
            return IndexOf(key) != -1;
        }

        protected abstract bool IsParentCorrect(TKey parent, TKey child);

        public virtual void RemoveAt(int index)
        {
            Contract.Requires<KeyNotFoundException>(index >= 0 && index < Count);

            Items[index] = Items[Count - 1];
            Items.RemoveAt(Count - 1);
            SiftDown(index);
        }

        public virtual bool Remove(TKey key)
        {
            Contract.Requires<ArgumentNullException>(key != null);

            var idx = IndexOf(key);
            if (idx == -1) return false;

            RemoveAt(idx);

            return true;
        }

        public virtual int IndexOf(TKey key)
        {
            Contract.Requires<ArgumentNullException>(key != null);

            return Find(0, key);
        }

        protected virtual int Find(int indexFrom, TKey value)
        {
            if (indexFrom >= Count || indexFrom < 0) return -1;

            var cmp = Items[indexFrom].Key.CompareTo(value);
            if (cmp == 0) return indexFrom;

            if (IsParentCorrect(Items[indexFrom].Key, value))
            {
                var idx = Find(GetLeft(indexFrom), value);
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
                var left = GetLeft(i);
                var right = GetRight(i);
                var j = left;

                if (right < Count && IsParentCorrect(Items[right].Key, Items[left].Key))
                {
                    j = right;
                }
                if (!IsParentCorrect(Items[i].Key, Items[j].Key))
                {
                    break;
                }

                SwapItems(i, j);
                i = j;
            }
        }

        private void SiftUp(int i)
        {
            for (; IsParentCorrect(Items[i].Key, Items[GetParent(i)].Key); i = GetParent(i))
            {
                SwapItems(i, GetParent(i));
            }
        }

        private void SwapItems(int i, int j)
        {
            var temp = Items[i];
            Items[i] = Items[j];
            Items[j] = temp;
        }

        private int GetLeft(int i)
        {
            return 2*i + 1;
        }

        private int GetRight(int i)
        {
            return 2*i + 2;
        }

        private int GetParent(int i)
        {
            return (i - 1)/2;
        }
    }
}