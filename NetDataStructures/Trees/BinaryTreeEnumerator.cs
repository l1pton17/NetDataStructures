﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetDataStructures.Trees
{
    internal sealed class BinaryTreeEnumerator<TNode, TKey, TValue> : IEnumerable<TNode>
        where TNode : class, IBinaryTreeNode<TNode, TKey, TValue>
        where TKey : IComparable<TKey>
    {
        private readonly bool _descending = false;
        private readonly TNode _root;

        public BinaryTreeEnumerator(TNode root, bool descending)
        {
            Contract.Requires(root != null);

            _descending = descending;
            _root = root;
        }

        private IEnumerable<TNode> Search(TNode node)
        {
            if (node.Left != null)
            {
                foreach (var child in Search(node.Left))
                {
                    yield return child;
                }
            }

            yield return node;

            if (node.Right != null)
            {
                foreach (var child in Search(node.Right))
                {
                    yield return child;
                }
            }
        }

        private IEnumerable<TNode> SearchDescending(TNode node)
        {
            if (node.Right != null)
            {
                foreach (var child in Search(node.Right))
                {
                    yield return child;
                }
            }

            yield return node;

            if (node.Left != null)
            {
                foreach (var child in Search(node.Left))
                {
                    yield return child;
                }
            }
        }

        public IEnumerator<TNode> GetEnumerator()
        {
            return (_descending ? SearchDescending(_root) : Search(_root)).GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
