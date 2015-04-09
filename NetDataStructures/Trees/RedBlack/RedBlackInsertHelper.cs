using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetDataStructures.Trees.RedBlack
{
    internal static class RedBlackInsertHelper
    {
        public static void FixAfterInsert<TNode, TKey, TValue>(ref TNode root, TNode node)
            where TNode : class, IRedBlackTreeNode<TNode, TKey, TValue>
            where TKey : IComparable<TKey>
        {
            TNode y;

            while (node != root && RedBlackTreeHelper.GetColor(node.Parent) == RedBlackTreeColor.Red)
            {
                if (node.Parent == node.Parent.Parent.Left)	
                {							
                    y = node.Parent.Parent.Right;
                    if (y != null && RedBlackTreeHelper.GetColor(y) == RedBlackTreeColor.Red)
                    {
                        RedBlackTreeHelper.SetColor(node.Parent, RedBlackTreeColor.Black);
                        RedBlackTreeHelper.SetColor(y, RedBlackTreeColor.Black);

                        RedBlackTreeHelper.SetColor(node.Parent.Parent, RedBlackTreeColor.Red);
                        node = node.Parent.Parent;	
                    }
                    else
                    {
                        if (node == node.Parent.Right)
                        {	
                            node = node.Parent;
                            BinaryTreeHelper.RotateLeft<TNode, TKey, TValue>(ref root, node);
                        }

                        RedBlackTreeHelper.SetColor(node.Parent, RedBlackTreeColor.Black);
                        RedBlackTreeHelper.SetColor(node.Parent.Parent, RedBlackTreeColor.Red);
                        BinaryTreeHelper.RotateRight<TNode, TKey, TValue>(ref root, node.Parent.Parent);
                    }
                }
                else
                {
                    y = node.Parent.Parent.Left;
                    if (y != null && RedBlackTreeHelper.GetColor(y) == RedBlackTreeColor.Red)
                    {
                        RedBlackTreeHelper.SetColor(node.Parent, RedBlackTreeColor.Black);
                        RedBlackTreeHelper.SetColor(y, RedBlackTreeColor.Black);
                        RedBlackTreeHelper.SetColor(node.Parent.Parent, RedBlackTreeColor.Red);
                        node = node.Parent.Parent;
                    }
                    else
                    {
                        if (node == node.Parent.Left)
                        {
                            node = node.Parent;
                            BinaryTreeHelper.RotateRight<TNode, TKey, TValue>(ref root, node);
                        }
                        RedBlackTreeHelper.SetColor(node.Parent, RedBlackTreeColor.Black);
                        RedBlackTreeHelper.SetColor(node.Parent.Parent, RedBlackTreeColor.Red);
                        BinaryTreeHelper.RotateLeft<TNode, TKey, TValue>(ref root, node.Parent.Parent);
                    }
                }
            }

            RedBlackTreeHelper.SetColor(root, RedBlackTreeColor.Black);
        }
    }
}
