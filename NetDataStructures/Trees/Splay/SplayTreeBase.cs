using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetDataStructures.Trees.Splay
{
    public class SplayTreeBase<TNode, TKey, TValue>
        where TNode : class, IBinaryTreeNode<TNode, TKey, TValue>
        where TKey : IComparable<TKey>
    {
        private readonly ITreeNodeFactory<TNode, TKey, TValue> _nodeFactory;

        private TNode _root;
        protected TNode Root { get { return _root; } private set { _root = value; } }

        public int Count { get; private set; }
        
        public SplayTreeBase(ITreeNodeFactory<TNode, TKey, TValue> nodeFactory)
        {
            Contract.Requires(nodeFactory != null);

            _nodeFactory = nodeFactory;
        }

        public bool Remove(TKey key)
        {
            Contract.Requires(key != null);

            bool find;
            var root = SplayTreeHelper.Find(_root, key, out find);
            if (!find) return false;

            Remove(root);

            return true;
        }

        protected TNode AddKey(TKey key)
        {
            Contract.Requires(key != null);
            Contract.Ensures(Count == Contract.OldValue<int>(Count) + 1);

            var leftRight = SplayTreeHelper.Split(_root, key);
            _root = _nodeFactory.Create(null, key);
            _root.Left = leftRight.Item1;
            _root.Right = leftRight.Item2;

            BinaryTreeHelper.KeepParent(_root);

            Count++;

            return _root;
        }

        protected void Remove(TNode node)
        {
            Contract.Requires(node != null);
            Contract.Ensures(Count == Contract.OldValue<int>(Count) - 1);

            BinaryTreeHelper.SetParent(node.Left, null);
            BinaryTreeHelper.SetParent(node.Right, null);
            _root = SplayTreeHelper.Merge(node.Left, node.Right);

            Count--;
        }
    }
}
