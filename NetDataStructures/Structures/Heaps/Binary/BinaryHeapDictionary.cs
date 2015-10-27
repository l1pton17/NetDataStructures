using System;
using System.Collections.Generic;
using System.Linq;

namespace NetDataStructures.Structures.Heaps
{
    public class MaxBinaryHeapDictionary<TKey, TValue> : BinaryHeapDictionary<TKey, TValue>
        where TKey : IComparable<TKey>
    {
        public MaxBinaryHeapDictionary()
            : base(new MaxBinaryHeapCore<CostItem<TKey, TValue>, TKey, TValue>())
        {
        }

        public MaxBinaryHeapDictionary(int capacity)
            : base(new MaxBinaryHeapCore<CostItem<TKey, TValue>, TKey, TValue>(capacity))
        {
        }

        public MaxBinaryHeapDictionary(IDictionary<TKey, TValue> source)
            : base(
                new MaxBinaryHeapCore<CostItem<TKey, TValue>, TKey, TValue>(
                    source.Select(v => new CostItem<TKey, TValue>(v.Key, v.Value))))
        {
        }
    }

    public class MinBinaryHeapDictionary<TKey, TValue> : BinaryHeapDictionary<TKey, TValue>
        where TKey : IComparable<TKey>
    {
        public MinBinaryHeapDictionary()
            : base(new MaxBinaryHeapCore<CostItem<TKey, TValue>, TKey, TValue>())
        {
        }

        public MinBinaryHeapDictionary(int capacity)
            : base(new MaxBinaryHeapCore<CostItem<TKey, TValue>, TKey, TValue>(capacity))
        {
        }

        public MinBinaryHeapDictionary(IDictionary<TKey, TValue> source)
            : base(
                new MaxBinaryHeapCore<CostItem<TKey, TValue>, TKey, TValue>(
                    source.Select(v => new CostItem<TKey, TValue>(v.Key, v.Value))))
        {
        }
    }

    public abstract class BinaryHeapDictionary<TKey, TValue> :
        DictionaryAdapter<IListDataStructure<CostItem<TKey, TValue>, TKey, TValue>, CostItem<TKey, TValue>, TKey, TValue>,
        IBinaryHeap<TKey, TValue>
        where TKey : IComparable<TKey>
    {
        protected BinaryHeapDictionary(IListDataStructure<CostItem<TKey, TValue>, TKey, TValue> core)
            : base(core)
        {
        }

        public KeyValuePair<TKey, TValue> this[int index]
        {
            get
            {
                if (index < 0 || index >= Count) throw new IndexOutOfRangeException();

                return new KeyValuePair<TKey, TValue>(DataStructure[index].Key, DataStructure[index].Value);
            }
        }

        public KeyValuePair<TKey, TValue> Top
        {
            get { return this[0]; }
        }

        public virtual KeyValuePair<TKey, TValue> ExtractTop()
        {
            var top = Top;

            DataStructure.RemoveAt(0);

            return top;
        }
    }
}