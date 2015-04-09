using NetDataStructures.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetDataStructures.Trees.RedBlack
{
    public abstract class RedBlackTreeBase<TNode, TKey, TValue>
        where TNode : class, IRedBlackTreeNode<TNode, TKey, TValue>
        where TKey : IComparable<TKey>
    {
        private readonly ITreeNodeFactory<TNode, TKey, TValue> _nodeFactory;
        private TNode _root;
        protected TNode Root { get { return _root; } private set { _root = value; } }

        public int Count { get; private set; }

        public RedBlackTreeBase(ITreeNodeFactory<TNode, TKey, TValue> nodeFactory)
        {
            _nodeFactory = nodeFactory;
        }

        #region Add Methods


        #endregion

        #region Get Methods

        public TKey GetMinKey()
        {
            Contract.Requires<NullReferenceException>(Root != null, Resources.TreeIsEmtpy);

            return BinaryTreeHelper.GetMin(Root).Return(v => v.Key);
        }

        public TKey GetMaxKey()
        {
            Contract.Requires<NullReferenceException>(Root != null, Resources.TreeIsEmtpy);

            return BinaryTreeHelper.GetMax(Root).Return(v => v.Key);
        }

        public TKey GetNextKey(TKey key)
        {
            Contract.Requires(key != null);

            return GetNode(key).Return(n => BinaryTreeHelper.GetNextKey(Root, n));
        }

        public TKey GetPrevKey(TKey key)
        {
            Contract.Requires(key != null);

            return GetNode(key).Return(n => BinaryTreeHelper.GetPrevKey(Root, n));
        }

        #endregion

        public virtual bool Contains(TKey key)
        {
            Contract.Requires<ArgumentNullException>(key != null);

            return BinaryTreeHelper.Find<TNode, TKey, TValue>(Root, key).ReturnSuccess();
        }

        public virtual void Clear()
        {
            Contract.Ensures(Root == null);
            Contract.Ensures(Count == 0);

            Root = null;
            Count = 0;
        }

        public bool Remove(TKey item)
        {
            return Root
                .With(r => BinaryTreeHelper.Find<TNode, TKey, TValue>(r, item))
                .Do(n => Remove(n))
                .ReturnSuccess();
        }

        protected virtual void Remove(TNode node)
        {
            Contract.Requires(node != null);
            Contract.Ensures(Count == Contract.OldValue<int>(Count) - 1);

            TNode y = (node.Left == null || node.Right == null)
                ? node
                : BinaryTreeHelper.GetMin(node.Right);

            TNode x = y.Left ?? y.Right;

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
                x = _nodeFactory.Create(node, y.Key);
                x.Color = RedBlackTreeColor.Black;
            }

            x.Parent = y.Parent;

            if (y != node)
            {
                _nodeFactory.Copy(y, node);
            }

            if (y.Color == RedBlackTreeColor.Black)
            {
                RedBlackRemoveHelper.FixAfterRemove<TNode, TKey, TValue>(ref _root, x);
            }

            Count--;
        }

        protected virtual TNode AddKey(TKey key)
        {
            Contract.Requires(key != null);
            Contract.Ensures(Count == Contract.OldValue<int>(Count) + 1);

            bool find;
            var node = Root == null
                ? _nodeFactory.Create(null, key)
                : BinaryTreeHelper.Insert(Root, key, _nodeFactory, out find);
            if (Root == null) Root = node;
            node.Color = RedBlackTreeColor.Red;

            RedBlackInsertHelper.FixAfterInsert<TNode, TKey, TValue>(ref _root, node);
            Count++;

            return (TNode)node;
        }

        protected TNode GetNode(TKey key)
        {
            Contract.Ensures(Contract.Result<TNode>() != null);

            var node = BinaryTreeHelper.Find<TNode, TKey, TValue>(Root, key);
            if (node == null) throw new KeyNotFoundException();

            return node;
        }
    }
}
