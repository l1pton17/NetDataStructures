using System;

namespace NetDataStructures.Structures.Trees.RedBlack
{
    public enum RedBlackTreeColor : byte
    {
        Red,
        Black
    }

    public interface IRedBlackTreeNode<TNode, TKey, TValue> : IBinaryTreeNode<TNode, TKey, TValue>
        where TNode : class, IRedBlackTreeNode<TNode, TKey, TValue>, new()
        where TKey : IComparable<TKey>
    {
        RedBlackTreeColor Color { get; set; }
    }
}