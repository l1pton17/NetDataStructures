using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace NetDataStructures.Structures
{
    public class DictionaryAdapter<TDataStructure, TItem, TKey, TValue> : IExtendedDictionary<TKey, TValue>
        where TDataStructure : IDataStructure<TItem, TKey, TValue>
        where TItem : class, ICostItem<TKey, TValue>
        where TKey : IComparable<TKey>
    {
        private readonly TDataStructure _dataStructure;

        public DictionaryAdapter(TDataStructure dataStructure)
        {
            Contract.Requires<ArgumentNullException>(dataStructure != null, "dataStructure");
            Contract.Ensures(DataStructure != null);

            _dataStructure = dataStructure;
        }

        protected TDataStructure DataStructure
        {
            get { return _dataStructure; }
        }

        public TValue this[TKey key]
        {
            get { return GetValue(key); }
            set { Update(key, value); }
        }

        public int Count
        {
            get { return DataStructure.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public ICollection<TKey> Keys
        {
            get { return new KeyCollection<TKey, TValue>(this); }
        }

        public ICollection<TValue> Values
        {
            get { return new ValueCollection<TKey, TValue>(this); }
        }

        public void Update(TKey key, TValue value)
        {
            Contract.Requires<ArgumentNullException>(key != null, "key");

            var node = DataStructure.GetNode(key);
            if (node == null) throw new KeyNotFoundException();

            node.Value = value;
        }

        public bool TryUpdate(TKey key, TValue value)
        {
            Contract.Requires<ArgumentNullException>(key != null, "key");

            return DataStructure.GetNode(key).Do(n => n.Value = value).ReturnSuccess();
        }

        public void Add(TKey key, TValue value)
        {
            Contract.Requires<ArgumentNullException>(key != null, "key");

            var node = DataStructure.Add(key);
            node.Value = value;
        }

        public bool ContainsKey(TKey key)
        {
            Contract.Requires(key != null);

            return DataStructure.Contains(key);
        }

        public bool Remove(TKey key)
        {
            return DataStructure.GetNode(key).Do(DataStructure.Remove).ReturnSuccess();
        }

        public void Clear()
        {
            DataStructure.Clear();
        }

        public TValue GetValue(TKey key)
        {
            Contract.Requires(key != null);
            var node = DataStructure.GetNode(key);
            if (node == null) throw new KeyNotFoundException();

            return node.Value;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            Contract.Requires(key != null);

            var node = DataStructure.GetNode(key);
            value = node.Return(n => n.Value);

            return node != null;
        }

        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item.Key, item.Value);
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item)
        {
            return ContainsKey(item.Key);
        }

        void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            Contract.Requires<ArgumentNullException>(array != null, "array");
            Contract.Requires<ArgumentOutOfRangeException>(arrayIndex >= 0 && arrayIndex < array.Length);
            Contract.Requires<ArgumentException>(array.Length - arrayIndex <= Count);

            var index = 0;

            foreach (var item in this)
            {
                array[index + arrayIndex] = item;
                index++;
            }
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
        {
            return Remove(item.Key);
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return DataStructure.Select(v => new KeyValuePair<TKey, TValue>(v.Key, v.Value)).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #region AddOrUpdate

        public TValue AddOrUpdate(TKey key, TValue newValue)
        {
            return AddOrUpdate(key, k => newValue, (k, v) => newValue);
        }

        public TValue AddOrUpdate(TKey key, TValue addValue, Func<TKey, TValue, TValue> updateValueFactory)
        {
            return AddOrUpdate(key, k => addValue, updateValueFactory);
        }

        public TValue AddOrUpdate(TKey key, Func<TKey, TValue> addValueFactory,
            Func<TKey, TValue, TValue> updateValueFactory)
        {
            Contract.Requires<ArgumentNullException>(key != null);
            Contract.Requires<ArgumentNullException>(addValueFactory != null);
            Contract.Requires<ArgumentNullException>(updateValueFactory != null);

            var node = DataStructure.GetNode(key);

            if (node == null)
            {
                node = DataStructure.Add(key);
                node.Value = addValueFactory(key);
            }
            else
            {
                node.Value = updateValueFactory(node.Key, node.Value);
            }

            return node.Value;
        }

        #endregion

        #region GetOrAdd

        public TValue GetOrAdd(TKey key, TValue addValue)
        {
            return GetOrAdd(key, () => addValue);
        }

        public TValue GetOrAdd(TKey key, Func<TValue> addValueFactory)
        {
            Contract.Requires(key != null);
            Contract.Requires(addValueFactory != null);

            var node = DataStructure.GetNode(key);

            if (node == null)
            {
                node = DataStructure.Add(key);
                node.Value = addValueFactory();
            }

            return node.Value;
        }

        #endregion
    }
}