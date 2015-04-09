using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetDataStructures
{
    public interface IKeyValue<TKey, TValue>
    {
        TKey Key { get; }
        TValue Value { get; }
    }

    internal sealed class KeyValue<TKey, TValue> : IKeyValue<TKey, TValue>
    {
        public TKey Key { get; private set; }
        public TValue Value { get; private set; }

        public KeyValue(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }
    }
}
