using System;

namespace NetDataStructures.Structures.Trees
{
    public static class BinaryTreeNodeExtensions
    {
        public static void SetParent<TNode, TKey, TValue>(this IBinaryTreeNode<TNode, TKey, TValue> child,
            IBinaryTreeNode<TNode, TKey, TValue> parent)
            where TNode : class, IBinaryTreeNode<TNode, TKey, TValue>
            where TKey : IComparable<TKey>
        {
            child.Do(c => c.Parent = (TNode) parent);
        }

        public static void KeepParent<TNode, TKey, TValue>(this IBinaryTreeNode<TNode, TKey, TValue> node)
            where TNode : class, IBinaryTreeNode<TNode, TKey, TValue>
            where TKey : IComparable<TKey>
        {
            node.Left.SetParent(node);
            node.Right.SetParent(node);
        }

        public static bool IsLeaf<TNode, TKey, TValue>(this IBinaryTreeNode<TNode, TKey, TValue> node)
            where TNode : class, IBinaryTreeNode<TNode, TKey, TValue>
            where TKey : IComparable<TKey>
        {
            return node != null && node.Left == null && node.Right == null;
        }

        public static TNode GetUncle<TNode, TKey, TValue>(this IBinaryTreeNode<TNode, TKey, TValue> node)
            where TNode : class, IBinaryTreeNode<TNode, TKey, TValue>
            where TKey : IComparable<TKey>
        {
            return TreeHelper.GetGrandParent(node).With(n => node.Parent == n.Left ? n.Right : n.Left);
        }

        public static TNode GetSibling<TNode, TKey, TValue>(this IBinaryTreeNode<TNode, TKey, TValue> node)
            where TNode : class, IBinaryTreeNode<TNode, TKey, TValue>
            where TKey : IComparable<TKey>
        {
            return node.With(v => v.Parent).With(v => node == v.Parent.Left ? v.Parent.Right : v.Parent.Left);
        }
    }
}