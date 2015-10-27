using System;
using System.Collections.Generic;

namespace NetDataStructures.Structures
{
    public interface IItem<TKey>
        where TKey : IComparable<TKey>
    {
        TKey Key { get; set; }
    }

    public interface ICostItem<TKey, TValue> : IItem<TKey>
        where TKey : IComparable<TKey>
    {
        TValue Value { get; set; }
    }

    public class OnlyKeyCostItem<TKey> : ICostItem<TKey, TKey>
        where TKey : IComparable<TKey>
    {
        public OnlyKeyCostItem()
        {
        }

        public OnlyKeyCostItem(TKey key)
        {
            Key = key;
        }

        public TKey Key { get; set; }

        public TKey Value
        {
            get { return Key; }
            set { }
        }
    }

    public class CostItem<TKey, TValue> : ICostItem<TKey, TValue>
        where TKey : IComparable<TKey>
    {
        public CostItem()
        {
        }

        public CostItem(TKey key, TValue value)
            : this(key)
        {
            Value = value;
        }

        public CostItem(TKey key)
        {
            Key = key;
        }

        public TKey Key { get; set; }
        public TValue Value { get; set; }

        public static implicit operator KeyValuePair<TKey, TValue>(CostItem<TKey, TValue> source)
        {
            return new KeyValuePair<TKey, TValue>(source.Key, source.Value);
        }
    }
}