using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetDataStructures.Trees.RedBlack
{
    internal static class RedBlackTreeHelper
    {
        public static RedBlackTreeColor GetColor<TNode, TKey, TValue>(IRedBlackTreeNode<TNode, TKey, TValue> node)
            where TNode : class, IRedBlackTreeNode<TNode, TKey, TValue>
            where TKey : IComparable<TKey>
        {
            return node == null ? RedBlackTreeColor.Black : node.Color;
        }

        public static void SetColor<TNode, TKey, TValue>(IRedBlackTreeNode<TNode, TKey, TValue> node, RedBlackTreeColor color)
            where TNode : class, IRedBlackTreeNode<TNode, TKey, TValue>
            where TKey : IComparable<TKey>
        {
            node.Do(n => n.Color = color);
        }
    }
}
