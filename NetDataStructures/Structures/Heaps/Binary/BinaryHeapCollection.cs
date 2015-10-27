using System;
using System.Collections.Generic;
using System.Linq;

namespace NetDataStructures.Structures.Heaps
{
    public class MaxBinaryHeapCollection<TKey> : BinaryHeapCollection<TKey>
        where TKey : IComparable<TKey>
    {
        public MaxBinaryHeapCollection()
            : base(new MaxBinaryHeapCore<OnlyKeyCostItem<TKey>, TKey, TKey>())
        {
        }

        public MaxBinaryHeapCollection(int capacity)
            : base(new MaxBinaryHeapCore<OnlyKeyCostItem<TKey>, TKey, TKey>(capacity))
        {
        }

        public MaxBinaryHeapCollection(IEnumerable<TKey> source)
            : base(
                new MaxBinaryHeapCore<OnlyKeyCostItem<TKey>, TKey, TKey>(source.Select(v => new OnlyKeyCostItem<TKey>(v)))
                )
        {
        }
    }

    public class MinBinaryHeapCollection<TKey> : BinaryHeapCollection<TKey>
        where TKey : IComparable<TKey>
    {
        public MinBinaryHeapCollection()
            : base(new MaxBinaryHeapCore<OnlyKeyCostItem<TKey>, TKey, TKey>())
        {
        }

        public MinBinaryHeapCollection(int capacity)
            : base(new MaxBinaryHeapCore<OnlyKeyCostItem<TKey>, TKey, TKey>(capacity))
        {
        }

        public MinBinaryHeapCollection(IEnumerable<TKey> source)
            : base(
                new MaxBinaryHeapCore<OnlyKeyCostItem<TKey>, TKey, TKey>(source.Select(v => new OnlyKeyCostItem<TKey>(v)))
                )
        {
        }
    }

    public abstract class BinaryHeapCollection<TKey> :
        CollectionAdapter<IListDataStructure<OnlyKeyCostItem<TKey>, TKey, TKey>, OnlyKeyCostItem<TKey>, TKey>,
        IBinaryHeap<TKey>
        where TKey : IComparable<TKey>
    {
        protected BinaryHeapCollection(IListDataStructure<OnlyKeyCostItem<TKey>, TKey, TKey> core)
            : base(core)
        {
        }

        public TKey this[int index]
        {
            get { return DataStructure[index].Key; }
        }

        public TKey Top
        {
            get { return this[0]; }
        }

        public TKey ExtractTop()
        {
            var top = Top;

            DataStructure.RemoveAt(0);

            return top;
        }

        public void RemoveAt(int index)
        {
            DataStructure.RemoveAt(index);
        }
    }
}