using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetDataStructures.Heaps
{
    public interface IHeapElement<TKey, TValue>
        where TKey : IComparable<TKey>
    {
        TKey Key { get; set; }
        TValue Value { get; set; }
    }
    
    public class HeapElement<TKey> : IHeapElement<TKey, TKey>
        where TKey : IComparable<TKey>
    {
        public TKey Key { get; set; }
        public TKey Value { get { return Key; } set { } }
    }

    public class HeapElement<TKey, TValue> : IHeapElement<TKey, TValue>
        where TKey : IComparable<TKey>
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }
    }

}
