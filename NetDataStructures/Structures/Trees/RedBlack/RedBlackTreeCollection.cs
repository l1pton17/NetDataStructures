using System;
using System.Collections.Generic;

namespace NetDataStructures.Structures.Trees.RedBlack
{
    public static class RedBlackTreeCollection
    {
        public static RedBlackTreeCollection<T> Create<T>(IEnumerable<T> source)
            where T : IComparable<T>
        {
            var tree = new RedBlackTreeCollection<T>();
            source.ForEach(s => tree.Add(s));

            return tree;
        }
    }

    public class RedBlackTreeCollection<TKey> :
        BinaryTreeCollection<IBinaryTreeDataStructure<RedBlackTreeNode<TKey>, TKey, TKey>, RedBlackTreeNode<TKey>, TKey>,
        IRedBlackTree<TKey>
        where TKey : IComparable<TKey>
    {
        public RedBlackTreeCollection()
            : base(new RedBlackTreeCore<RedBlackTreeNode<TKey>, TKey, TKey>())
        {
        }
    }
}