using System;
using NetDataStructures.Structures;
using NUnit;
using NUnit.Framework;

namespace NetDataStructures.Tests.Structures.Trees
{
    [TestFixture]
    public class ReadOnlyCollectionTest
    {
        private static readonly int[] InitialSource = {3, 4, 5};

        private ReadOnlyCollection<int> _testCollection;

        [TestFixtureSetUp]
        public void Init()
        {
            _testCollection = new ReadOnlyCollection<int>(InitialSource);
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AddThrowsException()
        {
            _testCollection.Add(2);
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ClearThrowsException()
        {
            _testCollection.Clear();
        }

        [Test]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RemoveThrowsException()
        {
            _testCollection.Remove(3);
        }

        [Test]
        public void ContainsTest()
        {
            Assert.IsTrue(_testCollection.Contains(InitialSource[0]));
        }

        [Test]
        public void ContainsFalseTest()
        {
            Assert.IsFalse(_testCollection.Contains(-1));
        }

        [Test]
        public void CountTest()
        {
            Assert.That(_testCollection.Count, Is.EqualTo(InitialSource.Length));
        }

        [Test]
        public void IsReadOnlyTest()
        {
            Assert.IsTrue(_testCollection.IsReadOnly);
        }

        [Test]
        public void GetEnumeratorTest()
        {
            CollectionAssert.AreEquivalent(InitialSource, _testCollection);
        }
    }
}
