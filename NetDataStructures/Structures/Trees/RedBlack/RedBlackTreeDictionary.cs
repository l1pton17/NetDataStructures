using System;

namespace NetDataStructures.Structures.Trees.RedBlack
{
    public class RedBlackTreeDictionary<TKey, TValue> :
        BinaryTreeDictionary
            <IBinaryTreeDataStructure<RedBlackTreeNode<TKey, TValue>, TKey, TValue>, RedBlackTreeNode<TKey, TValue>, TKey,
                TValue>,
        IRedBlackTree<TKey, TValue>
        where TKey : IComparable<TKey>
    {
        public RedBlackTreeDictionary()
            : base(new RedBlackTreeCore<RedBlackTreeNode<TKey, TValue>, TKey, TValue>())
        {
        }
    }
}