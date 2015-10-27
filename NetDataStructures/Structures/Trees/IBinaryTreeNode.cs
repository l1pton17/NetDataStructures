using System;

namespace NetDataStructures.Structures.Trees
{
    public interface ITreeNode<TNode, TKey, TValue> : ICostItem<TKey, TValue>
        where TNode : class, ITreeNode<TNode, TKey, TValue>
        where TKey : IComparable<TKey>
    {
        TNode Parent { get; set; }
    }

    public interface IBinaryTreeNode<TNode, TKey, TValue> : ITreeNode<TNode, TKey, TValue>
        where TNode : class, IBinaryTreeNode<TNode, TKey, TValue>
        where TKey : IComparable<TKey>
    {
        TNode Left { get; set; }
        TNode Right { get; set; }
    }
}