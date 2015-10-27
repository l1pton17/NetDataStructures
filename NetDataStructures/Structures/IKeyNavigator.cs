using System;
using System.Collections.Generic;

namespace NetDataStructures.Structures
{
    public interface IKeyNavigator<TKey>
        where TKey : IComparable<TKey>
    {
        TKey GetNext(TKey key);
        TKey GetPrev(TKey key);
    }

    public interface IKeyNavigator<TKey, TValue>
        where TKey : IComparable<TKey>
    {
        KeyValuePair<TKey, TValue> GetNext(TKey key);
        KeyValuePair<TKey, TValue> GetPrev(TKey key);
    }
}