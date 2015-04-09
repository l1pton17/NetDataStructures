using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetDataStructures.Trees.RedBlack
{
    public class RedBlackTree<TKey> :
        RedBlackTreeBase<RedBlackTreeNode<TKey>, TKey, TKey>,
        IRedBlackTree<TKey>
        where TKey : IComparable<TKey>
    {
        public RedBlackTree()
            : base(new RedBlackTreeNode<TKey>.NodeFactory())
        { }

        public IEnumerator<TKey> GetEnumerator()
        {
            return new BinaryTreeEnumerator<RedBlackTreeNode<TKey>, TKey, TKey>(Root, false)
                .Select(v => v.Key)
                .GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public virtual void Add(TKey key)
        {
            AddKey(key);
        }

        public IEnumerable<TKey> Elements(bool descending)
        {
            return new BinaryTreeEnumerator<RedBlackTreeNode<TKey>, TKey, TKey>(Root, descending)
                .Select(v => v.Key);
        }

        public void CopyTo(TKey[] array, int arrayIndex)
        {
            var arrayOut = this.ToArray();

            for (int i = 0; i < arrayOut.Length; i++)
            {
                array[i + arrayIndex] = arrayOut[i];
            }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }
    }
}
