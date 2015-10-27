using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetDataStructures.Structures;
using NUnit.Framework;

namespace NetDataStructures.Tests.Structures.Trees
{
    [TestFixture]
    public class KeyAndValueCollectionTest
    {
        private static readonly Dictionary<int, int> InitialDictionary =
            new Dictionary<int, int>
            {
                { 1, 2},
                { 2, 4},
                { 3, 52},
                { 6, 84},
                { 0, 7546},
            };

        private KeyCollection<int, int> _testKeyCollection;
        private ValueCollection<int, int> _testValueCollection;

        [TestFixtureSetUp]
        public void Init()
        {
            _testKeyCollection = new KeyCollection<int, int>(InitialDictionary);
            _testValueCollection = new ValueCollection<int, int>(InitialDictionary);
        }

        [Test]
        public void KeyCountTest()
        {
            Assert.That(_testKeyCollection.Count, Is.EqualTo(InitialDictionary.Count));
        }

        [Test]
        public void KeyContainsTest()
        {
            Assert.IsTrue(_testKeyCollection.Contains(InitialDictionary.Keys.First()));
            Assert.IsFalse(
                _testKeyCollection.Contains(
                    Enumerable.Range(0, Int32.MaxValue)
                        .Except(InitialDictionary.Keys)
                        .First()));
        }

        [Test]
        public void ValueCountTest()
        {
            Assert.That(_testValueCollection.Count, Is.EqualTo(InitialDictionary.Count));
        }

        [Test]
        public void KeyGetEnumeratorTest()
        {
            CollectionAssert.AreEquivalent(_testKeyCollection, InitialDictionary.Keys);
        }

        [Test]
        public void ValueGetEnumeratorTest()
        {
            CollectionAssert.AreEquivalent(_testValueCollection, InitialDictionary.Values);
        }
    }
}
