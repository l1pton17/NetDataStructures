using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetDataStructures
{
    public interface IKeyNavigator<TKey>
        where TKey : IComparable<TKey>
    {
        TKey GetNextKey(TKey key);
        TKey GetPrevKey(TKey key);
    }
}
