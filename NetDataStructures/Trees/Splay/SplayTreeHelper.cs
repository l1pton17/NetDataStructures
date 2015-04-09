using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetDataStructures.Trees.Splay
{
    internal static class SplayTreeHelper
    {
        public static TNode Merge<TNode, TKey, TValue>(IBinaryTreeNode<TNode, TKey, TValue> left, IBinaryTreeNode<TNode, TKey, TValue> right)
            where TNode : class, IBinaryTreeNode<TNode, TKey, TValue>
            where TKey : IComparable<TKey>
        {
            if (right == null || left == null) return (TNode)(left ?? right);

            bool find;
            right = Find(right, left.Key, out find);
            right.Left = (TNode)left;
            left.Parent = (TNode)right;

            return (TNode)right;
        }

        public static Tuple<TNode, TNode> Split<TNode, TKey, TValue>(IBinaryTreeNode<TNode, TKey, TValue> root, TKey key)
            where TNode : class, IBinaryTreeNode<TNode, TKey, TValue>
            where TKey : IComparable<TKey>
        {
            if (root == null) return new Tuple<TNode, TNode>(null, null);

            bool find;
            root = Find(root, key, out find);
            int cmp = root.Key.CompareTo(key);
            if (cmp == 0)
            {
                BinaryTreeHelper.SetParent(root.Left, null);
                BinaryTreeHelper.SetParent(root.Right, null);
                return Tuple.Create(root.Left, root.Right);
            }
            else if (cmp < 0)
            {
                var right = root.Right;
                root.Right = null;
                BinaryTreeHelper.SetParent(right, null);
                return Tuple.Create((TNode)root, right);
            }
            else
            {
                var left = root.Left;
                root.Left = null;
                BinaryTreeHelper.SetParent(left, null);
                return Tuple.Create(left, (TNode)root);
            }
        }

        public static TNode Find<TNode, TKey, TValue>(IBinaryTreeNode<TNode, TKey, TValue> node, TKey key, out bool find)
            where TNode : class, IBinaryTreeNode<TNode, TKey, TValue>
            where TKey : IComparable<TKey>
        {
            find = false;

            if (node == null) return null;
            int cmp = key.CompareTo(node.Key);
            if (cmp == 0)
            {
                find = true;
                return Splay(node);
            }
            if (cmp < 0 && node.Left != null) return Find(node.Left, key, out find);
            if (cmp > 0 && node.Right != null) return Find(node.Right, key, out find);

            return Splay(node);
        }

        public static TNode Splay<TNode, TKey, TValue>(IBinaryTreeNode<TNode, TKey, TValue> node)
            where TNode : class, IBinaryTreeNode<TNode, TKey, TValue>
            where TKey : IComparable<TKey>
        {
            if (node.Parent == null) return (TNode)node;

            var parent = node.Parent;
            var gparent = parent.Parent;

            if (gparent == null)
            {
                Rotate(parent, node);
                return (TNode)node;
            }
            else
            {
                bool isZigZig = (gparent.Left == parent) == (parent.Left == node);
                if (isZigZig)
                {
                    Rotate(gparent, parent);
                    Rotate(parent, node);
                }
                else
                {
                    Rotate(parent, node);
                    Rotate(gparent, node);
                }

                return Splay(node);
            }
        }

        public static void Rotate<TNode, TKey, TValue>(IBinaryTreeNode<TNode, TKey, TValue> parent, IBinaryTreeNode<TNode, TKey, TValue> child)
            where TNode : class, IBinaryTreeNode<TNode, TKey, TValue>
            where TKey : IComparable<TKey>
        {
            var gparent = parent.Parent;
            if (gparent != null)
            {
                if (gparent.Left == parent)
                {
                    gparent.Left = (TNode)child;
                }
                else
                {
                    gparent.Right = (TNode)child;
                }
            }

            if (parent.Left == child)
            {
                parent.Left = child.Right;
                child.Right = (TNode)parent;
            }
            else
            {
                parent.Right = child.Left;
                child.Left = (TNode)parent;
            }

            BinaryTreeHelper.KeepParent(child);
            BinaryTreeHelper.KeepParent(parent);
            child.Parent = gparent;
        }
    }
}
