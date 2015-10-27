using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace NetDataStructures.Structures.Trees.RedBlack
{
    internal sealed class RedBlackTreeCore<TNode, TKey, TValue> : IBinaryTreeDataStructure<TNode, TKey, TValue>
        where TNode : class, IRedBlackTreeNode<TNode, TKey, TValue>, new()
        where TKey : IComparable<TKey>
    {
        private TNode _root;

        public TNode Root
        {
            get { return _root; }
            private set { _root = value; }
        }

        public int Count { get; private set; }

        public TNode GetMinNode()
        {
            return Root.Return(BinaryTreeHelper.GetMinNode);
        }

        public TNode GetMaxNode()
        {
            return Root.Return(BinaryTreeHelper.GetMaxNode);
        }

        public TNode GetNextNode(TKey key)
        {
            Contract.Requires<ArgumentNullException>(key != null, "key");

            return GetNode(key).Return(n => BinaryTreeHelper.GetNextNode(Root, n));
        }

        public TNode GetPrevNode(TKey key)
        {
            Contract.Requires<ArgumentNullException>(key != null, "key");

            return GetNode(key).Return(n => BinaryTreeHelper.GetPrevNode(Root, n));
        }

        public bool Contains(TKey key)
        {
            Contract.Requires<ArgumentNullException>(key != null, "key");

            return BinaryTreeHelper.Find(Root, key).ReturnSuccess();
        }

        public void Clear()
        {
            Contract.Ensures(Root == null);
            Contract.Ensures(Count == 0);

            Root = null;
            Count = 0;
        }

        public void Remove(TNode node)
        {
            Contract.Requires(node != null);
            Contract.Ensures(Count == Contract.OldValue(Count) - 1);

            var y = (node.Left == null || node.Right == null)
                ? node
                : BinaryTreeHelper.GetMinNode(node.Right);

            var x = y.Left ?? y.Right;

            if (y.Parent != null)
            {
                if (y == y.Parent.Left)
                    y.Parent.Left = x;
                else
                    y.Parent.Right = x;
            }
            else
                Root = x;

            if (x == null)
            {
                x = new TNode
                {
                    Color = RedBlackTreeColor.Black
                };
            }

            x.Parent = y.Parent;

            if (y != node)
            {
                node.Key = y.Key;
                node.Value = y.Value;
            }

            if (y.Color == RedBlackTreeColor.Black)
            {
                RedBlackRemoveHelper.FixAfterRemove<TNode, TKey, TValue>(ref _root, x);
            }

            Count--;
        }

        public TNode Add(TKey key)
        {
            Contract.Requires(key != null);
            Contract.Ensures(Count == Contract.OldValue(Count) + 1);

            bool find;
            var node = Root == null
                ? new TNode
                {
                    Key = key
                }
                : BinaryTreeHelper.Insert<TNode, TKey, TValue>(Root, key, out find);
            if (Root == null) Root = node;
            node.Color = RedBlackTreeColor.Red;

            RedBlackInsertHelper.FixAfterInsert<TNode, TKey, TValue>(ref _root, node);
            Count++;

            return node;
        }

        public TNode GetNode(TKey key)
        {
            return BinaryTreeHelper.Find(Root, key);
        }

        public IEnumerator<TNode> GetEnumerator()
        {
            return new BinaryTreeEnumerator<TNode, TKey, TValue>(Root, false).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}