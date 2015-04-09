using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetDataStructures.Trees.RedBlack
{
    public enum RedBlackTreeColor
    {
        Red,
        Black
    }

    public interface IRedBlackTreeNode<TNode, TKey, TValue> : IBinaryTreeNode<TNode, TKey, TValue>
        where TNode : class, IRedBlackTreeNode<TNode, TKey, TValue>
        where TKey : IComparable<TKey>
    {
        RedBlackTreeColor Color { get; set; }
    }
}
