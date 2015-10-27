using System;

namespace NetDataStructures.Structures.Trees.Splay
{
    public interface ISplayTree<TKey> : IBinaryAdapter<TKey>
        where TKey : IComparable<TKey>
    {
    }

    public interface ISplayTree<TKey, TValue> : IBinaryAdapter<TKey, TValue>
        where TKey : IComparable<TKey>
    {
    }
}