using System;

namespace NetDataStructures.Structures.Trees.Splay
{
    public class SplayTreeDictionary<TKey, TValue> :
        BinaryTreeDictionary
            <SplayTreeCore<BinaryTreeNode<TKey, TValue>, TKey, TValue>, BinaryTreeNode<TKey, TValue>, TKey, TValue>,
        IBinaryAdapter<TKey, TValue>
        where TKey : IComparable<TKey>
    {
        public SplayTreeDictionary()
            : base(new SplayTreeCore<BinaryTreeNode<TKey, TValue>, TKey, TValue>())
        {
        }
    }
}