using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace NetDataStructures.Structures.Trees
{
    public class BinaryTreeDictionary<TDataStructure, TItem, TKey, TValue> :
        DictionaryAdapter<TDataStructure, TItem, TKey, TValue>, IBinaryAdapter<TKey, TValue>
        where TDataStructure : IBinaryTreeDataStructure<TItem, TKey, TValue>
        where TItem : class, IBinaryTreeNode<TItem, TKey, TValue>
        where TKey : IComparable<TKey>
    {
        public BinaryTreeDictionary(TDataStructure dataStructure)
            : base(dataStructure)
        {
        }

        public KeyValuePair<TKey, TValue> Max
        {
            get { return DataStructure.GetMaxNode().Return(v => new KeyValuePair<TKey, TValue>(v.Key, v.Value)); }
        }

        public KeyValuePair<TKey, TValue> Min
        {
            get { return DataStructure.GetMinNode().Return(v => new KeyValuePair<TKey, TValue>(v.Key, v.Value)); }
        }

        public KeyValuePair<TKey, TValue> GetNext(TKey key)
        {
            Contract.Requires<ArgumentNullException>(key != null, "key");

            return DataStructure.GetNextNode(key).Return(v => new KeyValuePair<TKey, TValue>(v.Key, v.Value));
        }

        public KeyValuePair<TKey, TValue> GetPrev(TKey key)
        {
            Contract.Requires<ArgumentNullException>(key != null, "key");

            return DataStructure.GetPrevNode(key).Return(v => new KeyValuePair<TKey, TValue>(v.Key, v.Value));
        }

        public IEnumerable<KeyValuePair<TKey, TValue>> GetElements(bool descending)
        {
            return
                new BinaryTreeEnumerator<TItem, TKey, TValue>(DataStructure.Root, false)
                    .Select(v => new KeyValuePair<TKey, TValue>(v.Key, v.Value));
        }
    }
}