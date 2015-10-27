using System;

namespace NetDataStructures.Structures.Trees.RedBlack
{
    internal static class RedBlackRemoveHelper
    {
        public static void FixAfterRemove<TNode, TKey, TValue>(ref TNode root, TNode node)
            where TNode : class, IRedBlackTreeNode<TNode, TKey, TValue>, new()
            where TKey : IComparable<TKey>
        {
            TNode y;

            while (node != root && RedBlackTreeHelper.GetColor(node) == RedBlackTreeColor.Black)
            {
                if (node == node.Parent.Return(p => p.Left))
                {
                    y = node.Parent.Right;
                    if (RedBlackTreeHelper.GetColor(y) == RedBlackTreeColor.Red)
                    {
                        RedBlackTreeHelper.SetColor(y, RedBlackTreeColor.Black);
                        RedBlackTreeHelper.SetColor(node.Parent, RedBlackTreeColor.Red);
                        BinaryTreeHelper.RotateLeft<TNode, TKey, TValue>(ref root, node.Parent);
                        y = node.Parent.Return(p => p.Right);
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
                            y = node.Parent.Return(p => p.Right);
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
                    y = node.Parent.Return(p => p.Left);
                    if (RedBlackTreeHelper.GetColor(y) == RedBlackTreeColor.Red)
                    {
                        RedBlackTreeHelper.SetColor(y, RedBlackTreeColor.Black);
                        RedBlackTreeHelper.SetColor(node.Parent, RedBlackTreeColor.Red);
                        BinaryTreeHelper.RotateRight<TNode, TKey, TValue>(ref root, node.Parent);
                        y = node.Parent.Return(p => p.Left);
                    }
                    if (RedBlackTreeHelper.GetColor(y.Return(p => p.Right)) == RedBlackTreeColor.Black
                        && RedBlackTreeHelper.GetColor(y.Return(p => p.Left)) == RedBlackTreeColor.Black)
                    {
                        RedBlackTreeHelper.SetColor(y, RedBlackTreeColor.Red);
                        node = node.Parent;
                    }
                    else
                    {
                        if (RedBlackTreeHelper.GetColor(y.Return(p => p.Left)) == RedBlackTreeColor.Black)
                        {
                            RedBlackTreeHelper.SetColor(y.Return(p => p.Right), RedBlackTreeColor.Black);
                            RedBlackTreeHelper.SetColor(y, RedBlackTreeColor.Red);
                            BinaryTreeHelper.RotateLeft<TNode, TKey, TValue>(ref root, y);
                            y = node.Parent.Return(p => p.Left);
                        }

                        RedBlackTreeHelper.SetColor(y, node.Parent.Color);
                        RedBlackTreeHelper.SetColor(node.Parent, RedBlackTreeColor.Black);
                        RedBlackTreeHelper.SetColor(y.Return(p => p.Left), RedBlackTreeColor.Black);
                        BinaryTreeHelper.RotateRight<TNode, TKey, TValue>(ref root, node.Parent);
                        node = root;
                    }
                }
            }

            node.Do(n => n.Color = RedBlackTreeColor.Black);
        }
    }
}