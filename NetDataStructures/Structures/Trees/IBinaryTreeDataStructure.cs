using System;
using System.Collections.Generic;

namespace NetDataStructures.Structures.Trees
{
    public interface IBinaryAdapter<TKey> :
        IMaxMinStructure<TKey>,
        ICollection<TKey>,
        IKeyNavigator<TKey>,
        ITreeEnumerator<TKey>
        where TKey : IComparable<TKey>
    {
    }

    public interface IBinaryAdapter<TKey, TValue> :
        IMaxMinStructure<TKey, TValue>,
        IExtendedDictionary<TKey, TValue>,
        IKeyNavigator<TKey, TValue>,
        ITreeEnumerator<TKey, TValue>
        where TKey : IComparable<TKey>
    {
    }

    public interface IBinaryTreeDataStructure<TNode, in TKey, TValue> : ITreeDataStructure<TNode, TKey, TValue>
        where TNode : class, IBinaryTreeNode<TNode, TKey, TValue>
        where TKey : IComparable<TKey>
    {
        TNode GetMinNode();
        TNode GetMaxNode();
        TNode GetNextNode(TKey key);
        TNode GetPrevNode(TKey key);
    }
}