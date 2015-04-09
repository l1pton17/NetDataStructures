using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetDataStructures.Heaps
{
    public class MaxHeap<TKey> : BinaryHeap<TKey>
        where TKey : IComparable<TKey>
    {
        public MaxHeap()
        {
        }

        public MaxHeap(int capacity)
            : base(capacity)
        {
        }

        public MaxHeap(IEnumerable<TKey> source)
            : base(source)
        {
        }

        protected override bool IsParentCorrect(TKey parent, TKey child)
        {
            return parent.CompareTo(child) > 0;
        }
    }

    public class MaxHeap<TKey, TValue> : BinaryHeap<TKey, TValue>
        where TKey : IComparable<TKey>
    {
        public MaxHeap()
        {
        }

        public MaxHeap(int capacity)
            : base(capacity)
        {
        }

        public MaxHeap(IEnumerable<Tuple<TKey, TValue>> source)
            : base(source)
        {
        }

        protected override bool IsParentCorrect(TKey parent, TKey child)
        {
            return parent.CompareTo(child) > 0;
        }
    }
}
