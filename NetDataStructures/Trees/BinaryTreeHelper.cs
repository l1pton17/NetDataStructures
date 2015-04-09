using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetDataStructures.Trees
{
    internal static class BinaryTreeHelper
    {
        public static TNode GetUncle<TNode, TKey, TValue>(IBinaryTreeNode<TNode, TKey, TValue> node)
            where TNode : class, IBinaryTreeNode<TNode, TKey, TValue>
            where TKey : IComparable<TKey>
        {
            return TreeHelper.GetGrandParent(node).With(n => node.Parent == n.Left ? n.Right : n.Left);
        }

        public static TNode GetSibling<TNode, TKey, TValue>(IBinaryTreeNode<TNode, TKey, TValue> node)
            where TNode : class, IBinaryTreeNode<TNode, TKey, TValue>
            where TKey : IComparable<TKey>
        {
            return node.With(v => v.Parent).With(v => node == v.Parent.Left ? v.Parent.Right : v.Parent.Left);
        }

        public static bool IsLeaf<TNode, TKey, TValue>(IBinaryTreeNode<TNode, TKey, TValue> root)
            where TNode : class, IBinaryTreeNode<TNode, TKey, TValue>
            where TKey : IComparable<TKey>
        {
            return root != null && root.Left == null && root.Right == null;
        }

        public static void SetParent<TNode, TKey, TValue>(IBinaryTreeNode<TNode, TKey, TValue> parent, IBinaryTreeNode<TNode, TKey, TValue> child)
            where TNode : class, IBinaryTreeNode<TNode, TKey, TValue>
            where TKey : IComparable<TKey>
        {
            if (child != null)
                child.Parent = (TNode)parent;
        }

        public static void KeepParent<TNode, TKey, TValue>(IBinaryTreeNode<TNode, TKey, TValue> node)
            where TNode : class, IBinaryTreeNode<TNode, TKey, TValue>
            where TKey : IComparable<TKey>
        {
            SetParent(node, node.Left);
            SetParent(node, node.Right);
        }

        public static TNode GetMax<TNode, TKey, TValue>(IBinaryTreeNode<TNode, TKey, TValue> root)
            where TNode : class, IBinaryTreeNode<TNode, TKey, TValue>
            where TKey : IComparable<TKey>
        {
            return root.With(r => r.Right).Return(r => GetMax(r)) ?? root as TNode;
        }

        public static TNode GetMin<TNode, TKey, TValue>(IBinaryTreeNode<TNode, TKey, TValue> root)
            where TNode : class, IBinaryTreeNode<TNode, TKey, TValue>
            where TKey : IComparable<TKey>
        {
            return root.With(r => r.Left).Return(r => GetMin(r)) ?? root as TNode;
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
            else
            {
                return FindNext<TNode, TKey, TValue>(root.Left, key);
            }
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
            else
            {
                return root.Left != null ? FindNext<TNode, TKey, TValue>(root.Left, key) : root;
            }
        }

        public static TNode Find<TNode, TKey, TValue>(IBinaryTreeNode<TNode, TKey, TValue> root, TKey key)
            where TNode : class, IBinaryTreeNode<TNode, TKey, TValue>
            where TKey : IComparable<TKey>
        {
            var node = root;

            while (node != null)
            {
                int cmp = key.CompareTo(node.Key);
                if (cmp == 0)
                    return (TNode)node;
                else if (cmp < 0)
                    node = node.Left;
                else
                    node = node.Right;
            }

            return null;
        }

        public static IEnumerable<TNode> FindAll<TNode, TKey, TValue>(IBinaryTreeNode<TNode, TKey, TValue> root, TKey key)
            where TNode : class, IBinaryTreeNode<TNode, TKey, TValue>
            where TKey : IComparable<TKey>
        {
            var node = root;

            while (node != null)
            {
                int cmp = key.CompareTo(node.Key);
                if (cmp == 0)
                {
                    yield return (TNode)node;

                    foreach (var child in FindAll<TNode, TKey, TValue>(node.Left, key).Union(FindAll<TNode, TKey, TValue>(node.Right, key)))
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
                int cmp = key.CompareTo(node.Key);
                if (cmp == 0)
                {
                    if (node.Value.Equals(value))
                    {
                        return (TNode)node;
                    }

                    return Find(node.Left, key, value) ?? Find(node.Right, key, value);
                }
                else if (cmp < 0)
                    node = node.Left;
                else
                    node = node.Right;
            }

            return null;
        }

        public static TNode Insert<TNode, TKey, TValue>(TNode root, TKey key, ITreeNodeFactory<TNode, TKey, TValue> nodeFactory, out bool find)
            where TNode : class, IBinaryTreeNode<TNode, TKey, TValue>
            where TKey : IComparable<TKey>
        {
            Contract.Requires(root != null);
            Contract.Requires(nodeFactory != null);
            Contract.Ensures(Contract.Result<TNode>() != null);

            if (root.Key.CompareTo(key) < 0)
            {
                if (root.Right == null)
                {
                    find = false;
                    root.Right = nodeFactory.Create(root, key);
                    KeepParent(root);
                    return root.Right;
                }
                else
                {
                    return Insert(root.Right, key, nodeFactory, out find);
                }
            }
            else if (root.Key.CompareTo(key) > 0)
            {
                if (root.Left == null)
                {
                    find = false;
                    root.Left = nodeFactory.Create(root, key);
                    KeepParent(root);
                    return root.Left;
                }
                else
                {
                    return Insert(root.Left, key, nodeFactory, out find);
                }
            }
            else
            {
                find = true;
                return root as TNode;
            }
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

                KeepParent(node.Parent);
            }
            else
                root = y;

            y.Left = (TNode)node;

            KeepParent(y);
            KeepParent(node);
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

                KeepParent(node.Parent);
            }
            else
                root = y;

            y.Right = (TNode)node;

            KeepParent(y);
            KeepParent(node);
        }

        public static TKey GetNextKey<TNode, TKey, TValue>(IBinaryTreeNode<TNode, TKey, TValue> root, IBinaryTreeNode<TNode, TKey, TValue> node)
            where TNode : class, IBinaryTreeNode<TNode, TKey, TValue>
            where TKey : IComparable<TKey>
        {
            if (node.Right != null)
            {
                return GetMin(node.Right).Key;
            }

            IBinaryTreeNode<TNode, TKey, TValue> nextKey = null;
            var current = root;

            while (root != null)
            {
                int cmp = node.Key.CompareTo(root.Key);
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

            return nextKey.Key;
        }

        public static TKey GetPrevKey<TNode, TKey, TValue>(IBinaryTreeNode<TNode, TKey, TValue> root, IBinaryTreeNode<TNode, TKey, TValue> node)
            where TNode : class, IBinaryTreeNode<TNode, TKey, TValue>
            where TKey : IComparable<TKey>
        {
            if (node.Left != null)
            {
                return GetMax(node.Left).Key;
            }

            IBinaryTreeNode<TNode, TKey, TValue> nextKey = null;
            var current = root;

            while (root != null)
            {
                int cmp = node.Key.CompareTo(root.Key);
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

            return nextKey.Key;
        }
    }
}
