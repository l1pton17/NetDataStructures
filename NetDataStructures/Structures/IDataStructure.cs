using System;
using System.Collections.Generic;

namespace NetDataStructures.Structures
{
    public interface ITreeDataStructure<TItem, in TKey, TValue> : IDataStructure<TItem, TKey, TValue>
        where TItem : class, ICostItem<TKey, TValue>
        where TKey : IComparable<TKey>
    {
        TItem Root { get; }
    }

    public interface IListDataStructure<TItem, in TKey, TValue> : IDataStructure<TItem, TKey, TValue>
        where TItem : class, ICostItem<TKey, TValue>, new()
        where TKey : IComparable<TKey>
    {
        TItem this[int index] { get; }

        void RemoveAt(int index);
    }

    public interface IDataStructure<TItem, in TKey, TValue> : IEnumerable<TItem>
        where TItem : class, ICostItem<TKey, TValue>
        where TKey : IComparable<TKey>
    {
        int Count { get; }
        bool Contains(TKey key);
        void Remove(TItem node);
        TItem Add(TKey key);
        TItem GetNode(TKey key);
        void Clear();
    }

    public interface IExtendedDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        TValue AddOrUpdate(TKey key, TValue newValue);
        TValue AddOrUpdate(TKey key, TValue addValue, Func<TKey, TValue, TValue> updateValueFactory);
        TValue AddOrUpdate(TKey key, Func<TKey, TValue> addValueFactory, Func<TKey, TValue, TValue> updateValueFactory);
        TValue GetOrAdd(TKey key, TValue addValue);
        TValue GetOrAdd(TKey key, Func<TValue> addValueFactory);
        TValue GetValue(TKey key);
        void Update(TKey key, TValue value);
        bool TryUpdate(TKey key, TValue value);
    }

    public interface ITreeEnumerator<out TKey>
    {
        IEnumerable<TKey> GetElements(bool descending);
    }

    public interface ITreeEnumerator<TKey, TValue>
    {
        IEnumerable<KeyValuePair<TKey, TValue>> GetElements(bool descending);
    }
}