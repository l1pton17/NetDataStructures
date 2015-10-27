using System;

namespace NetDataStructures.Structures.Trees.Splay
{
    public sealed class SplayTreeCollection<TKey> :
        BinaryTreeCollection<SplayTreeCore<BinaryTreeNode<TKey>, TKey, TKey>, BinaryTreeNode<TKey>, TKey>,
        IBinaryAdapter<TKey>
        where TKey : IComparable<TKey>
    {
        public SplayTreeCollection()
            : base(new SplayTreeCore<BinaryTreeNode<TKey>, TKey, TKey>())
        {
        }
    }
}