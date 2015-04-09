using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetDataStructures.Trees
{
    public interface ITree<TKey>
        where TKey : IComparable<TKey>
    {
    }

    public interface ITree<TKey, TValue>
        where TKey : IComparable<TKey>
    {
    }
}
