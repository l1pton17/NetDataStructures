using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetDataStructures.Trees
{
    public sealed class BinaryTreeNode<TKey> : IBinaryTreeNode<BinaryTreeNode<TKey>, TKey, TKey>
        where TKey : IComparable<TKey>
    {
        public class BinaryTreeNodeFactory<TKey> : ITreeNodeFactory<BinaryTreeNode<TKey>, TKey, TKey>
            where TKey : IComparable<TKey>
        {
            public BinaryTreeNode<TKey> Create(BinaryTreeNode<TKey> parent, TKey key)
            {
                return new BinaryTreeNode<TKey>
                {
                    Key = key,
                    Parent = parent
                };
            }

            public void Copy(BinaryTreeNode<TKey> from, BinaryTreeNode<TKey> to)
            {
                to.Key = from.Key;
            }
        }

        public BinaryTreeNode<TKey> Left { get; set; }
        public BinaryTreeNode<TKey> Right { get; set; }
        public BinaryTreeNode<TKey> Parent { get; set; }

        public TKey Key { get; set; }
        public TKey Value { get { return Key; } set { } }
    }

    public sealed class BinaryTreeNode<TKey, TValue> : IBinaryTreeNode<BinaryTreeNode<TKey, TValue>, TKey, TValue>
        where TKey : IComparable<TKey>
    {
        public class BinaryTreeNodeFactory<TKey, TValue> : ITreeNodeFactory<BinaryTreeNode<TKey, TValue>, TKey, TKey>
            where TKey : IComparable<TKey>
        {
            public BinaryTreeNode<TKey> Create(BinaryTreeNode<TKey> parent, TKey key)
            {
                return new BinaryTreeNode<TKey>
                {
                    Key = key,
                    Parent = parent
                };
            }

            public void Copy(BinaryTreeNode<TKey> from, BinaryTreeNode<TKey> to)
            {
                to.Key = from.Key;
                to.Value = from.Value;
            }
        }

        public BinaryTreeNode<TKey> Left { get; set; }
        public BinaryTreeNode<TKey> Right { get; set; }
        public BinaryTreeNode<TKey> Parent { get; set; }

        public TKey Key { get; set; }
        public TValue Value { get; set; }
    }
}
