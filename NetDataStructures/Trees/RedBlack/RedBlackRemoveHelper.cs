using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetDataStructures.Trees.RedBlack
{
    internal static class RedBlackRemoveHelper
    {
        public static void FixAfterRemove<TNode, TKey, TValue>(ref TNode root, TNode node)
            where TNode : class, IRedBlackTreeNode<TNode, TKey, TValue>
            where TKey : IComparable<TKey>
        {
            TNode y;

            while (node != root && RedBlackTreeHelper.GetColor(node) == RedBlackTreeColor.Black)
            {
                if (node == node.Parent.Left)
                {
                    y = node.Parent.Right;
                    if (RedBlackTreeHelper.GetColor(y) == RedBlackTreeColor.Red)
                    {
                        RedBlackTreeHelper.SetColor(y, RedBlackTreeColor.Black);
                        RedBlackTreeHelper.SetColor(node.Parent, RedBlackTreeColor.Red);
                        BinaryTreeHelper.RotateLeft<TNode, TKey, TValue>(ref root, node.Parent);
                        y = node.Parent.Right;
                    }
                    if (RedBlackTreeHelper.GetColor(y.Left) == RedBlackTreeColor.Black
                        && RedBlackTreeHelper.GetColor(y.Right) == RedBlackTreeColor.Black)
                    {
                        RedBlackTreeHelper.SetColor(y, RedBlackTreeColor.Red);
                        node = node.Parent;
                    }
                    else
                    {
                        if (RedBlackTreeHelper.GetColor(y.Right) == RedBlackTreeColor.Black)
                        {
                            RedBlackTreeHelper.SetColor(y.Left, RedBlackTreeColor.Black);
                            RedBlackTreeHelper.SetColor(y, RedBlackTreeColor.Red);
                            BinaryTreeHelper.RotateRight<TNode, TKey, TValue>(ref root, y);
                            y = node.Parent.Right;
                        }

                        RedBlackTreeHelper.SetColor(y, node.Parent.Color);
                        RedBlackTreeHelper.SetColor(node.Parent, RedBlackTreeColor.Black);
                        RedBlackTreeHelper.SetColor(y.Right, RedBlackTreeColor.Black);
                        BinaryTreeHelper.RotateLeft<TNode, TKey, TValue>(ref root, node.Parent);
                        node = root;
                    }
                }
                else
                {
                    y = node.Parent.Left;
                    if (RedBlackTreeHelper.GetColor(y) == RedBlackTreeColor.Red)
                    {
                        RedBlackTreeHelper.SetColor(y, RedBlackTreeColor.Black);
                        RedBlackTreeHelper.SetColor(node.Parent, RedBlackTreeColor.Red);
                        BinaryTreeHelper.RotateRight<TNode, TKey, TValue>(ref root, node.Parent);
                        y = node.Parent.Left;
                    }
                    if (RedBlackTreeHelper.GetColor(y.Right) == RedBlackTreeColor.Black
                        && RedBlackTreeHelper.GetColor(y.Left) == RedBlackTreeColor.Black)
                    {
                        RedBlackTreeHelper.SetColor(y, RedBlackTreeColor.Red);
                        node = node.Parent;
                    }
                    else
                    {
                        if (RedBlackTreeHelper.GetColor(y.Left) == RedBlackTreeColor.Black)
                        {
                            RedBlackTreeHelper.SetColor(y.Right, RedBlackTreeColor.Black);
                            RedBlackTreeHelper.SetColor(y, RedBlackTreeColor.Red);
                            BinaryTreeHelper.RotateLeft<TNode, TKey, TValue>(ref root, y);
                            y = node.Parent.Left;
                        }

                        RedBlackTreeHelper.SetColor(y, node.Parent.Color);
                        RedBlackTreeHelper.SetColor(node.Parent, RedBlackTreeColor.Black);
                        RedBlackTreeHelper.SetColor(y.Left, RedBlackTreeColor.Black);
                        BinaryTreeHelper.RotateRight<TNode, TKey, TValue>(ref root, node.Parent);
                        node = root;
                    }
                }
            }

            node.Color = RedBlackTreeColor.Black;
        }
    }
}
