using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace NetDataStructures.Structures
{
    public abstract class CollectionAdapter<TDataStructure, TItem, TKey> : ICollection<TKey>
        where TDataStructure : IDataStructure<TItem, TKey, TKey>
        where TItem : class, ICostItem<TKey, TKey>
        where TKey : IComparable<TKey>
    {
        private readonly TDataStructure _dataStructure;

        protected CollectionAdapter(TDataStructure dataStructure)
        {
            Contract.Requires<ArgumentNullException>(dataStructure != null, "dataStructure");
            Contract.Ensures(DataStructure != null);

            _dataStructure = dataStructure;
        }

        protected TDataStructure DataStructure
        {
            get { return _dataStructure; }
        }

        public int Count
        {
            get { return _dataStructure.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public void Add(TKey item)
        {
            _dataStructure.Add(item);
        }

        public void Clear()
        {
            _dataStructure.Clear();
        }

        public bool Contains(TKey item)
        {
            return _dataStructure.Contains(item);
        }

        public void CopyTo(TKey[] array, int arrayIndex)
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

        public bool Remove(TKey item)
        {
            return _dataStructure.GetNode(item).Do(n => _dataStructure.Remove(n)).ReturnSuccess();
        }

        public IEnumerator<TKey> GetEnumerator()
        {
            return _dataStructure.Select(v => v.Key).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}