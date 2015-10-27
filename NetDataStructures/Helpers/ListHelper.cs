using System.Collections.Generic;

namespace NetDataStructures.Helpers
{
    internal static class ListHelper
    {
        public static void Swap<T>(IList<T> items, int a, int b)
        {
            ThrowHelper.ThrowIfIndexOutOfRange(items, a);
            ThrowHelper.ThrowIfIndexOutOfRange(items, b);

            var temp = items[a];
            items[a] = items[b];
            items[b] = temp;
        }
    }
}