using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetDataStructures.Heaps
{
    public class MinHeap<TKey> : BinaryHeap<TKey>
        where TKey : IComparable<TKey>
    {
        public MinHeap()
        {
        }

        public MinHeap(int capacity)
            : base(capacity)
        {
        }

        public MinHeap(IEnumerable<TKey> source)
            : base(source)
        {
        }

        protected override bool IsParentCorrect(TKey parent, TKey child)
        {
            return parent.CompareTo(child) < 0;
        }
    }

    public class MinHeap<TKey, TValue> : BinaryHeap<TKey, TValue>
        where TKey : IComparable<TKey>
    {
        public MinHeap()
        {
        }

        public MinHeap(int capacity)
            : base(capacity)
        {
        }

        public MinHeap(IEnumerable<Tuple<TKey, TValue>> source)
            : base(source)
        {
        }

        protected override bool IsParentCorrect(TKey parent, TKey child)
        {
            return parent.CompareTo(child) < 0;
        }
    }
}
