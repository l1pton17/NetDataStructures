using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetDataStructures.Trees.RedBlack
{
    public interface IRedBlackTree<TKey> :
        ICollection<TKey>,
        IMaxMinStructure<TKey>,
        IDataStructure<TKey>,
        IKeyNavigator<TKey>
        where TKey : IComparable<TKey>
    {
    }

    public interface IRedBlackTree<TKey, TValue> :
        IDictionary<TKey, TValue>,
        IMaxMinStructure<TKey, TValue>,
        IDataStructure<TKey, TValue>,
        IKeyNavigator<TKey>
        where TKey : IComparable<TKey>
    {
    }
}
