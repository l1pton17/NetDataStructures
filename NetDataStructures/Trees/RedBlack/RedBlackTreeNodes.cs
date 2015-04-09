using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace NetDataStructures.Trees.RedBlack
{
    public sealed class RedBlackTreeNode<TKey> : IRedBlackTreeNode<RedBlackTreeNode<TKey>, TKey, TKey>
        where TKey : IComparable<TKey>
    {
        public sealed class NodeFactory : ITreeNodeFactory<RedBlackTreeNode<TKey>, TKey, TKey>
        {
            public RedBlackTreeNode<TKey> Create(RedBlackTreeNode<TKey> parent, TKey key)
            {
                return new RedBlackTreeNode<TKey>
                {
                    Key = key,
                    Parent = parent
                };
            }

            public void Copy(RedBlackTreeNode<TKey> from, RedBlackTreeNode<TKey> to)
            {
                to.Key = from.Key;
            }
        }

        public RedBlackTreeColor Color { get; set; }
        public RedBlackTreeNode<TKey> Left { get; set; }
        public RedBlackTreeNode<TKey> Right { get; set; }
        public RedBlackTreeNode<TKey> Parent { get; set; }

        public TKey Key { get; set; }
        public TKey Value { get { return Key; } set { } }
    }

    public sealed class RedBlackTreeNode<TKey, TValue> : IRedBlackTreeNode<RedBlackTreeNode<TKey, TValue>, TKey, TValue>, IBinaryTreeNode<RedBlackTreeNode<TKey, TValue>, TKey, TValue>
        where TKey : IComparable<TKey>
    {
        public sealed class NodeFactory : ITreeNodeFactory<RedBlackTreeNode<TKey, TValue>, TKey, TValue>
        {
            public RedBlackTreeNode<TKey, TValue> Create(RedBlackTreeNode<TKey, TValue> parent, TKey key)
            {
                return new RedBlackTreeNode<TKey, TValue>
                {
                    Key = key,
                    Parent = parent
                };
            }

            public void Copy(RedBlackTreeNode<TKey, TValue> from, RedBlackTreeNode<TKey, TValue> to)
            {
                to.Key = from.Key;
                to.Value = from.Value;
            }
        }

        public RedBlackTreeColor Color { get; set; }
        public RedBlackTreeNode<TKey, TValue> Left { get; set; }
        public RedBlackTreeNode<TKey, TValue> Right { get; set; }
        public RedBlackTreeNode<TKey, TValue> Parent { get; set; }

        public TKey Key { get; set; }
        public TValue Value { get; set; }
    }
}
