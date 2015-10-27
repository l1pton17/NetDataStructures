using System;

namespace NetDataStructures.Structures.Trees
{
    public sealed class BinaryTreeNode<TKey> : OnlyKeyCostItem<TKey>, IBinaryTreeNode<BinaryTreeNode<TKey>, TKey, TKey>
        where TKey : IComparable<TKey>
    {
        public BinaryTreeNode<TKey> Left { get; set; }
        public BinaryTreeNode<TKey> Right { get; set; }
        public BinaryTreeNode<TKey> Parent { get; set; }
    }

    public sealed class BinaryTreeNode<TKey, TValue> : CostItem<TKey, TValue>, IBinaryTreeNode<BinaryTreeNode<TKey, TValue>, TKey, TValue>
        where TKey : IComparable<TKey>
    {
        public BinaryTreeNode<TKey, TValue> Left { get; set; }
        public BinaryTreeNode<TKey, TValue> Right { get; set; }
        public BinaryTreeNode<TKey, TValue> Parent { get; set; }
    }
}