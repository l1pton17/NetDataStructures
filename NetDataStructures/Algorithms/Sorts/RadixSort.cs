using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace NetDataStructures.Algorithms.Sorts
{
    public enum RadixSortType
    {
        LeastSignificantDigit,
        MostSignificantDigit
    }

    internal sealed class Int32BitGetter : IBitGetter<int>
    {
        private readonly int _identifyMask;

        public Int32BitGetter()
        {
            NumberOfGroups = (int) Math.Ceiling(NumberOfBits/(double) NumberOfBitsInGroup);
            _identifyMask = (1 << NumberOfBitsInGroup) - 1;
        }

        public int NumberOfBitsInGroup
        {
            get { return 4; }
        }

        public int NumberOfBits
        {
            get { return 32; }
        }

        public int NumberOfGroups { get; private set; }

        public int GetShift(int value, int shift)
        {
            return (value >> shift) & _identifyMask;
        }
    }

    public interface IBitGetter<in T>
    {
        int NumberOfBits { get; }
        int NumberOfBitsInGroup { get; }
        int NumberOfGroups { get; }
        int GetShift(T value, int shift);
    }

    /// <summary>
    ///     Exists only one implementation, because max array's size is Int32.MaxValue.
    ///     So there is no way to use counting sort with Int64 numbers.
    /// </summary>
    public class Int32RadixSort : RadixSort<int>
    {
        public Int32RadixSort()
            : base(new Int32BitGetter())
        {
        }
    }

    public class RadixSort<T> : ISort<T>
        where T : IComparable<T>
    {
        private readonly IBitGetter<T> _bitGetter;

        public RadixSort(IBitGetter<T> bitGetter)
        {
            Contract.Requires<ArgumentNullException>(bitGetter != null, "bitGetter");
            _bitGetter = bitGetter;
        }

        public void Sort(IList<T> source)
        {
            var t = new T[source.Count];

            var count = new int[1 << _bitGetter.NumberOfBitsInGroup];
            var pref = new int[1 << _bitGetter.NumberOfBitsInGroup];

            for (int c = 0, shift = 0; c < _bitGetter.NumberOfGroups; shift += _bitGetter.NumberOfBitsInGroup)
            {
                for (var j = 0; j < count.Length; j++)
                {
                    count[j] = 0;
                }

                foreach (var item in source)
                {
                    count[_bitGetter.GetShift(item, shift)]++;
                }

                pref[0] = 0;
                for (var i = 1; i < count.Length; i++)
                {
                    pref[i] = pref[i - 1] + count[i - 1];
                }

                for (var i = 0; i < source.Count; i++)
                {
                    t[pref[_bitGetter.GetShift(source[i], shift)]++] = source[i];
                }

                for (var i = 0; i < source.Count; i++)
                {
                    source[i] = t[i];
                }
            }
        }
    }
}