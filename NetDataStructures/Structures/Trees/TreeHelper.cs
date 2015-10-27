using System;

namespace NetDataStructures.Structures.Trees
{
    internal static class TreeHelper
    {
        public static TNode GetGrandParent<TNode, TKey, TValue>(ITreeNode<TNode, TKey, TValue> node)
            where TNode : class, ITreeNode<TNode, TKey, TValue>
            where TKey : IComparable<TKey>
        {
            return node.With(v => v.Parent).With(v => v.Parent);
        }
    }
}