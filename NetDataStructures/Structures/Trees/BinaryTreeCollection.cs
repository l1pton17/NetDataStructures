using System;
using System.Collections.Generic;
using System.Linq;

namespace NetDataStructures.Structures.Trees
{
    public class BinaryTreeCollection<TDataStructure, TItem, TKey> : CollectionAdapter<TDataStructure, TItem, TKey>,
        IBinaryAdapter<TKey>
        where TDataStructure : IBinaryTreeDataStructure<TItem, TKey, TKey>
        where TItem : class, IBinaryTreeNode<TItem, TKey, TKey>
        where TKey : IComparable<TKey>
    {
        public BinaryTreeCollection(TDataStructure dataStructure)
            : base(dataStructure)
        {
        }

        public TKey Max
        {
            get { return DataStructure.GetMaxNode().Return(v => v.Key); }
        }

        public TKey Min
        {
            get { return DataStructure.GetMinNode().Return(v => v.Key); }
        }

        public TKey GetNext(TKey key)
        {
            return DataStructure.GetNextNode(key).Return(v => v.Key);
        }

        public TKey GetPrev(TKey key)
        {
            return DataStructure.GetPrevNode(key).Return(v => v.Key);
        }

        public IEnumerable<TKey> GetElements(bool descending)
        {
            return new BinaryTreeEnumerator<TItem, TKey, TKey>(DataStructure.Root, false).Select(v => v.Key);
        }
    }
}