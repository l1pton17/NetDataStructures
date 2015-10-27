using System;

namespace NetDataStructures.Structures.Trees.RedBlack
{
    public interface IRedBlackTree<TKey> : IBinaryAdapter<TKey>
        where TKey : IComparable<TKey>
    {
    }

    public interface IRedBlackTree<TKey, TValue> : IBinaryAdapter<TKey, TValue>
        where TKey : IComparable<TKey>
    {
    }
}