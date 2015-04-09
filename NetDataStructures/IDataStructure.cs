using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetDataStructures.Trees
{
    public interface IDataStructure<T>
        where T : IComparable<T>
    {
        int Count { get; }

        void Add(T key);

        bool Contains(T key);
        void Clear();

        bool Remove(T key);
    }

    public interface IDataStructure<TKey, TValue> : IDictionary<TKey, TValue>
        where TKey : IComparable<TKey>
    {
        int Count { get; }

        TValue this[TKey key] { get; }

        bool ContainsKey(TKey key);

        void Add(TKey key, TValue value);
        TValue AddOrUpdate(TKey key, TValue newValue);
        TValue AddOrUpdate(TKey key, TValue addValue, Func<TKey, TValue, TValue> updateValueFactory);
        TValue AddOrUpdate(TKey key, Func<TKey, TValue> addValueFactory, Func<TKey, TValue, TValue> updateValueFactory);

        TValue GetOrAdd(TKey key, TValue addValue);
        TValue GetOrAdd(TKey key, Func<TValue> addValueFactory);
        bool TryGetValue(TKey key, out TValue value);

        void Update(TKey key, TValue value);
        bool TryUpdate(TKey key, TValue value);

        IEnumerable<KeyValuePair<TKey, TValue>> GetElements(bool descending);
        IEnumerable<KeyValuePair<TKey, TValue>> GetElementsGreaterThan(TKey key);
        IEnumerable<KeyValuePair<TKey, TValue>> GetElementsLessThan(TKey key);

        void Clear();

        bool Remove(TKey key);
        bool RemoveAll(TKey key);
        bool Remove(TKey key, TValue value);
    }
}
