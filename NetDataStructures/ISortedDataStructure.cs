using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetDataStructures.Trees
{
    public interface IMaxMinStructure<TKey>
        where TKey:IComparable<TKey>
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
