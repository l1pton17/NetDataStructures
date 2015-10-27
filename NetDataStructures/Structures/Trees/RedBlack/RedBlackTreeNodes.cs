using System;

namespace NetDataStructures.Structures.Trees.RedBlack
{
    public sealed class RedBlackTreeNode<TKey> : OnlyKeyCostItem<TKey>,
        IRedBlackTreeNode<RedBlackTreeNode<TKey>, TKey, TKey>
        where TKey : IComparable<TKey>
    {
        public RedBlackTreeNode() {}

        public RedBlackTreeNode(TKey key)
            : base(key) {}

        public RedBlackTreeColor Color { get; set; }
        public RedBlackTreeNode<TKey> Left { get; set; }
        public RedBlackTreeNode<TKey> Right { get; set; }
        public RedBlackTreeNode<TKey> Parent { get; set; }
    }

    public sealed class RedBlackTreeNode<TKey, TValue> : CostItem<TKey, TValue>,
        IRedBlackTreeNode<RedBlackTreeNode<TKey, TValue>, TKey, TValue>
        where TKey : IComparable<TKey>
    {
        public RedBlackTreeNode()
        {
        }

        public RedBlackTreeNode(TKey key)
            : base(key)
        {
        }

        public RedBlackTreeColor Color { get; set; }
        public RedBlackTreeNode<TKey, TValue> Left { get; set; }
        public RedBlackTreeNode<TKey, TValue> Right { get; set; }
        public RedBlackTreeNode<TKey, TValue> Parent { get; set; }
    }
}