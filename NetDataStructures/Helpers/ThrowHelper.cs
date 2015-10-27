using System;
using System.Collections.Generic;

namespace NetDataStructures.Helpers
{
    internal static class ThrowHelper
    {
        public static void ThrowIfIndexOutOfRange<T>(IList<T> items, int index)
        {
            if (index < 0 || index >= items.Count) throw new IndexOutOfRangeException();
        }
    }
}