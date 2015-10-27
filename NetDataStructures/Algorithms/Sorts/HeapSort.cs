using System;
using System.Collections.Generic;
using NetDataStructures.Helpers;

namespace NetDataStructures.Algorithms.Sorts
{
    internal interface ISort<T>
        where T : IComparable<T>
    {
        void Sort(IList<T> source);
    }

    internal class HeapSort<T> : ISort<T>
        where T : IComparable<T>
    {
        public void Sort(IList<T> source)
        {
            var count = source.Count;

            for (var i = count/2 - 1; i >= 0; i--)
            {
                ShiftDown(source, i, count);
            }

            for (var i = count - 1; i >= 1; i--)
            {
                ListHelper.Swap(source, 0, i);
                ShiftDown(source, 0, i);
            }
        }

        private int GetLeft(int i)
        {
            return i*2 + 1;
        }

        private int GetRight(int i)
        {
            return i*2 + 2;
        }

        private void ShiftDown(IList<T> source, int i, int j)
        {
            var done = false;

            while (GetLeft(i) < j && !done)
            {
                int maxChild;
                if (GetLeft(i) == j - 1)
                {
                    maxChild = GetLeft(i);
                }
                else if (source[GetLeft(i)].CompareTo(source[GetRight(i)]) > 0)
                {
                    maxChild = GetLeft(i);
                }
                else
                {
                    maxChild = GetRight(i);
                }

                if (source[i].CompareTo(source[maxChild]) < 0)
                {
                    ListHelper.Swap(source, i, maxChild);
                }
                else
                {
                    done = true;
                }
            }
        }
    }
}