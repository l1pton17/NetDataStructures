using System;
using System.Collections.Generic;

namespace NetDataStructures.Structures.Heaps
{
    public interface IBinaryHeapBase<out T>
    {
        T Top { get; }
        T ExtractTop();
    }

    public interface IBinaryHeap<TKey> :
        IBinaryHeapBase<TKey>,
        ICollection<TKey>
        where TKey : IComparable<TKey>
    {
    }

    public interface IBinaryHeap<TKey, TValue> :
        IBinaryHeapBase<KeyValuePair<TKey, TValue>>,
        IExtendedDictionary<TKey, TValue>
        where TKey : IComparable<TKey>
    {
    }
}