using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetDataStructures.Trees
{
    public interface ITreeNodeFactory<TNode, TKey, TValue>
        where TNode : class, ITreeNode<TNode, TKey, TValue>
        where TKey : IComparable<TKey>
    {
        TNode Create(TNode parent, TKey key);
        void Copy(TNode from, TNode to);
    }

    public interface ITreeNode<TNode, TKey, TValue>
        where TNode : class, ITreeNode<TNode, TKey, TValue>
        where TKey : IComparable<TKey>
    {
        TNode Parent { get; set; }
        TKey Key { get; set; }
        TValue Value { get; set; }
    }
}
