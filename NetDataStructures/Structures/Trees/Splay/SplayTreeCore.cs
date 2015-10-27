using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace NetDataStructures.Structures.Trees.Splay
{
    public class SplayTreeCore<TNode, TKey, TValue> : IBinaryTreeDataStructure<TNode, TKey, TValue>
        where TNode : class, IBinaryTreeNode<TNode, TKey, TValue>, new()
        where TKey : IComparable<TKey>
    {
        public TNode Root { get; private set; }
        public int Count { get; private set; }

        public TNode GetNode(TKey key)
        {
            bool find;
            var root = SplayTreeHelper.Find(Root, key, out find);
            if (root != null)
            {
                Root = root;
            }

            return find ? Root : null;
        }

        public bool Contains(TKey key)
        {
            return GetNode(key).ReturnSuccess();
        }

        public void Clear()
        {
            Root = null;
            Count = 0;
        }

        public TNode Add(TKey key)
        {
            Contract.Requires(key != null);
            Contract.Ensures(Count == Contract.OldValue(Count) + 1);

            var leftRight = SplayTreeHelper.Split(Root, key);
            Root = new TNode
            {
                Key = key,
                Left = leftRight.Item1,
                Right = leftRight.Item2
            };

            Root.KeepParent();

            Count++;

            return Root;
        }

        public void Remove(TNode node)
        {
            Contract.Requires(node != null);
            Contract.Ensures(Count == Contract.OldValue(Count) - 1);

            node.Left.SetParent(null);
            node.Right.SetParent(null);

            Root = SplayTreeHelper.Merge(node.Left, node.Right);

            Count--;
        }

        public TNode GetMinNode()
        {
            return Root
                .With<TNode, TNode>(BinaryTreeHelper.GetMinNode)
                .Return(Splay);
        }

        public TNode GetMaxNode()
        {
            return Root
                .With<TNode, TNode>(BinaryTreeHelper.GetMaxNode)
                .Return(Splay);
        }

        public TNode GetNextNode(TKey key)
        {
            return GetNode(key)
                .With(v => BinaryTreeHelper.GetNextNode(Root, v))
                .Return(Splay);
        }

        public TNode GetPrevNode(TKey key)
        {
            return GetNode(key)
                .With(v => BinaryTreeHelper.GetPrevNode(Root, v))
                .Return(Splay);
        }

        public IEnumerator<TNode> GetEnumerator()
        {
            return new BinaryTreeEnumerator<TNode, TKey, TValue>(Root, false).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private TNode Splay(TNode node)
        {
            return SplayTreeHelper.Splay(node).Do(v => Root = v);
        }
    }
}