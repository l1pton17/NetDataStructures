using System;
using System.Collections.Generic;

namespace NetDataStructures.Structures
{
    public interface IMaxMinStructure<out TKey>
        where TKey : IComparable<TKey>
    {
        TKey Max { get; }
        TKey Min { get; }
    }

    public interface IMaxMinStructure<TKey, TValue>
        where TKey : IComparable<TKey>
    {
        KeyValuePair<TKey, TValue> Max { get; }
        KeyValuePair<TKey, TValue> Min { get; }
    }
}