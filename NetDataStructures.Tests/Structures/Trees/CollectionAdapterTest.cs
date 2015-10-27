using System.Collections.Generic;
using NetDataStructures.Structures;
using NetDataStructures.Structures.Heaps;
using NetDataStructures.Structures.Trees.RedBlack;
using NetDataStructures.Structures.Trees.Splay;
using NUnit.Framework;

namespace NetDataStructures.Tests.Structures.Trees
{
    /// <summary>
    /// Summary description for CollectionAdapterTest
    /// </summary>
    [TestFixture]
    public class CollectionAdapterTest
    {
        public ICollection<int>[] Int32CollectionCases =
        {
            new RedBlackTreeCollection<int>(),
            new MaxBinaryHeapCollection<int>(),
            new MinBinaryHeapCollection<int>(),
            new SplayTreeCollection<int>()
        };

        [Test]
        [TestCaseSource("Int32CollectionCases")]
        [Category("Count")]
        public void CountIncreaseOnAddTest(ICollection<int> collection)
        {
            collection.Add(3);

            Assert.IsTrue(collection.Count == 1);
        }

        [Test]
        [TestCaseSource("Int32CollectionCases")]
        [Category("Count")]
        public void CountIncreaseOnMultipleAddTest(ICollection<int> collection)
        {
            collection.Add(3);
            collection.Add(4);
            collection.Add(5);

            Assert.IsTrue(collection.Count == 3);
        }

        [Test]
        [TestCaseSource("Int32CollectionCases")]
        public void ClearTest(ICollection<int> collection)
        {
            collection.Add(3);
            collection.Add(4);
            collection.Add(5);
            collection.Clear();

            Assert.IsTrue(collection.Count == 0);
        }

        [Test]
        [TestCaseSource("Int32CollectionCases")]
        public void ContainsTest(ICollection<int> collection)
        {
            collection.Add(3);
            collection.Add(4);
            collection.Add(5);

            Assert.IsTrue(collection.Contains(3));
            Assert.IsFalse(collection.Contains(0));
        }

        [Test]
        [TestCaseSource("Int32CollectionCases")]
        public void AddTest(ICollection<int> collection)
        {
            int[] items = {3, 4, 5};

            for (int i = 0; i < items.Length; i++)
            {
                collection.Add(items[i]);
            }

            CollectionAssert.AreEquivalent(collection, items);
        }

        [Test]
        [TestCaseSource("Int32CollectionCases")]
        public void RemoveTest(ICollection<int> collection)
        {
            int[] items = { 3, 4, 5 };

            for (int i = 0; i < items.Length; i++)
            {
                collection.Add(items[i]);
            }

            collection.Remove(4);
            Assert.IsFalse(collection.Contains(4));
        }

        [Test]
        [TestCaseSource("Int32CollectionCases")]
        public void CopyToTest(ICollection<int> collection)
        {
            int[] items = { 3, 4, 5 };

            for (int i = 0; i < items.Length; i++)
            {
                collection.Add(items[i]);
            }

            int[] array = new int[collection.Count];
            collection.CopyTo(array, 0);

            CollectionAssert.AreEquivalent(collection, array);
        }

        [Test]
        [TestCaseSource("Int32CollectionCases")]
        public void ReadOnlyIsFalseTest(ICollection<int> collection)
        {
            Assert.IsFalse(collection.IsReadOnly);
        }
    }
}
