using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetDataStructures.Trees
{
    public interface IBinaryTreeNode<TNode, TKey, TValue> : ITreeNode<TNode, TKey, TValue>
        where TNode : class, IBinaryTreeNode<TNode, TKey, TValue>
        where TKey : IComparable<TKey>
    {
        TNode Left { get; set; }
        TNode Right { get; set; }
    }
}
