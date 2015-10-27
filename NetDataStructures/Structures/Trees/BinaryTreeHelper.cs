using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace NetDataStructures.Structures.Trees
{
    internal static class BinaryTreeHelper
    {
        public static TNode GetMaxNode<TNode, TKey, TValue>(IBinaryTreeNode<TNode, TKey, TValue> root)
            where TNode : class, IBinaryTreeNode<TNode, TKey, TValue>
            where TKey : IComparable<TKey>
        {
            return root.With(r => r.Right).Return(GetMaxNode) ?? root as TNode;
        }

        public static TNode GetMinNode<TNode, TKey, TValue>(IBinaryTreeNode<TNode, TKey, TValue> root)
            where TNode : class, IBinaryTreeNode<TNode, TKey, TValue>
            where TKey : IComparable<TKey>
        {
            return root.With(r => r.Left).Return(GetMinNode) ?? root as TNode;
        }

        public static TNode FindPrevious<TNode, TKey, TValue>(TNode root, TKey key)
            where TNode : class, IBinaryTreeNode<TNode, TKey, TValue>
            where TKey : IComparable<TKey>
        {
            if (root == null) return null;

            if (root.Key.CompareTo(key) <= 0)
            {
                return root.Right != null ? FindNext<TNode, TKey, TValue>(root.Right, key) : root;
            }
            return FindNext<TNode, TKey, TValue>(root.Left, key);
        }

        public static TNode FindNext<TNode, TKey, TValue>(TNode root, TKey key)
            where TNode : class, IBinaryTreeNode<TNode, TKey, TValue>
            where TKey : IComparable<TKey>
        {
            if (root == null) return null;

            if (root.Key.CompareTo(key) <= 0)
            {
                return FindNext<TNode, TKey, TValue>(root.Right, key);
            }
            return root.Left != null ? FindNext<TNode, TKey, TValue>(root.Left, key) : root;
        }

        public static TNode Find<TNode, TKey, TValue>(IBinaryTreeNode<TNode, TKey, TValue> root, TKey key)
            where TNode : class, IBinaryTreeNode<TNode, TKey, TValue>
            where TKey : IComparable<TKey>
        {
            var node = root;

            while (node != null)
            {
                var cmp = key.CompareTo(node.Key);
                if (cmp == 0)
                    return (TNode) node;
                if (cmp < 0)
                    node = node.Left;
                else
                    node = node.Right;
            }

            return null;
        }

        public static IEnumerable<TNode> FindAll<TNode, TKey, TValue>(IBinaryTreeNode<TNode, TKey, TValue> root,
            TKey key)
            where TNode : class, IBinaryTreeNode<TNode, TKey, TValue>
            where TKey : IComparable<TKey>
        {
            var node = root;

            while (node != null)
            {
                var cmp = key.CompareTo(node.Key);
                if (cmp == 0)
                {
                    yield return (TNode) node;

                    foreach (var child in FindAll(node.Left, key).Union(FindAll(node.Right, key)))
                    {
                        yield return child;
                    }
                }
                else if (cmp < 0)
                    node = node.Left;
                else
                    node = node.Right;
            }
        }

        public static TNode Find<TNode, TKey, TValue>(IBinaryTreeNode<TNode, TKey, TValue> root, TKey key, TValue value)
            where TNode : class, IBinaryTreeNode<TNode, TKey, TValue>
            where TKey : IComparable<TKey>
        {
            var node = root;

            while (node != null)
            {
                var cmp = key.CompareTo(node.Key);
                if (cmp == 0)
                {
                    if (node.Value.Equals(value))
                    {
                        return (TNode) node;
                    }

                    return Find(node.Left, key, value) ?? Find(node.Right, key, value);
                }
                if (cmp < 0)
                    node = node.Left;
                else
                    node = node.Right;
            }

            return null;
        }

        public static TNode Insert<TNode, TKey, TValue>(TNode root, TKey key, out bool find)
            where TNode : class, IBinaryTreeNode<TNode, TKey, TValue>, new()
            where TKey : IComparable<TKey>
        {
            Contract.Requires(root != null);
            Contract.Ensures(Contract.Result<TNode>() != null);

            if (root.Key.CompareTo(key) < 0)
            {
                if (root.Right == null)
                {
                    find = false;
                    root.Right = new TNode
                    {
                        Key = key
                    };
                    root.KeepParent();
                    return root.Right;
                }
                return Insert<TNode, TKey, TValue>(root.Right, key, out find);
            }
            if (root.Key.CompareTo(key) > 0)
            {
                if (root.Left == null)
                {
                    find = false;
                    root.Left = new TNode
                    {
                        Key = key
                    };
                    root.KeepParent();
                    return root.Left;
                }
                return Insert<TNode, TKey, TValue>(root.Left, key, out find);
            }
            find = true;
            return root;
        }

        public static void RotateLeft<TNode, TKey, TValue>(ref TNode root, TNode node)
            where TNode : class, IBinaryTreeNode<TNode, TKey, TValue>
            where TKey : IComparable<TKey>
        {
            var y = node.Right;

            node.Right = y.Left;

            if (node.Parent != null)
            {
                if (node == node.Parent.Left)
                    node.Parent.Left = y;
                else
                    node.Parent.Right = y;

                node.Parent.KeepParent();
            }
            else
                root = y;

            y.Left = node;

            y.KeepParent();
            node.KeepParent();
        }

        public static void RotateRight<TNode, TKey, TValue>(ref TNode root, TNode node)
            where TNode : class, IBinaryTreeNode<TNode, TKey, TValue>
            where TKey : IComparable<TKey>
        {
            var y = node.Left;

            node.Left = y.Right;

            if (node.Parent != null)
            {
                if (node == node.Parent.Right)
                    node.Parent.Right = y;
                else
                    node.Parent.Left = y;

                node.Parent.KeepParent();
            }
            else
                root = y;

            y.Right = node;

            y.KeepParent();
            node.KeepParent();
        }

        public static TNode GetNextNode<TNode, TKey, TValue>(IBinaryTreeNode<TNode, TKey, TValue> root,
            IBinaryTreeNode<TNode, TKey, TValue> node)
            where TNode : class, IBinaryTreeNode<TNode, TKey, TValue>
            where TKey : IComparable<TKey>
        {
            if (node.Right != null)
            {
                return GetMinNode(node.Right);
            }

            IBinaryTreeNode<TNode, TKey, TValue> nextKey = null;
            var current = root;

            while (root != null)
            {
                var cmp = node.Key.CompareTo(root.Key);
                if (cmp < 0)
                {
                    nextKey = root;
                    root = root.Left;
                }
                else if (cmp > 0)
                {
                    root = root.Right;
                }
                else break;
            }

            return nextKey as TNode;
        }

        public static TNode GetPrevNode<TNode, TKey, TValue>(IBinaryTreeNode<TNode, TKey, TValue> root,
            IBinaryTreeNode<TNode, TKey, TValue> node)
            where TNode : class, IBinaryTreeNode<TNode, TKey, TValue>
            where TKey : IComparable<TKey>
        {
            if (node.Left != null)
            {
                return GetMaxNode(node.Left);
            }

            IBinaryTreeNode<TNode, TKey, TValue> nextKey = null;
            var current = root;

            while (root != null)
            {
                var cmp = node.Key.CompareTo(root.Key);
                if (cmp < 0)
                {
                    root = root.Left;
                }
                else if (cmp > 0)
                {
                    nextKey = root;
                    root = root.Right;
                }
                else break;
            }

            return nextKey as TNode;
        }
    }
}