using System;
using System.Collections.Generic;
using System.Linq;
using NetDataStructures.Structures;
using NetDataStructures.Structures.Heaps;
using NetDataStructures.Structures.Trees.RedBlack;
using NetDataStructures.Structures.Trees.Splay;
using NUnit.Framework;

namespace NetDataStructures.Tests.Structures.Trees
{
    [TestFixture]
    public class DictionaryAdapterTest
    {
        public IExtendedDictionary<int, int>[] Int32DictionaryCases =
        {
            new RedBlackTreeDictionary<int, int>(),
            new MaxBinaryHeapDictionary<int, int>(),
            new MinBinaryHeapDictionary<int, int>(),
            new SplayTreeDictionary<int, int>()
        };

        [Test]
        [TestCaseSource("Int32DictionaryCases")]
        public void CountTest(IExtendedDictionary<int, int> dictionary)
        {
            int count = dictionary.Count;
            dictionary.Add(1, 1);

            Assert.IsTrue(dictionary.Count == count + 1);
        }

        [Test]
        [TestCaseSource("Int32DictionaryCases")]
        public void ThisKeyTest(IExtendedDictionary<int, int> dictionary)
        {
            dictionary.Add(1, 1);
            dictionary[1] = 3;

            Assert.IsTrue(dictionary[1] == 3);
        }

        [Test]
        [TestCaseSource("Int32DictionaryCases")]
        public void ThisKeyNotFoundTest(IExtendedDictionary<int, int> dictionary)
        {
            dictionary.Add(1, 1);

            Assert.Throws<KeyNotFoundException>(() => dictionary[2] = 3);
        }

        [Test]
        [TestCaseSource("Int32DictionaryCases")]
        public void IsReadOnlyTest(IExtendedDictionary<int, int> dictionary)
        {
            Assert.IsFalse(dictionary.IsReadOnly);
        }

        [Test]
        [TestCaseSource("Int32DictionaryCases")]
        public void KeysTest(IExtendedDictionary<int, int> dictionary)
        {
            dictionary.Add(1, 2);
            dictionary.Add(2, 2);
            dictionary.Add(3, 2);

            var keys = dictionary.Select(v => v.Key).ToList();
            CollectionAssert.AreEquivalent(keys, dictionary.Keys);
        }

        [Test]
        [TestCaseSource("Int32DictionaryCases")]
        public void ValuesTest(IExtendedDictionary<int, int> dictionary)
        {
            dictionary.Add(1, 2);
            dictionary.Add(2, 2);
            dictionary.Add(3, 2);

            var values = dictionary.Select(v => v.Value).ToList();
            CollectionAssert.AreEquivalent(values, dictionary.Values);
        }

        [Test]
        [TestCaseSource("Int32DictionaryCases")]
        public void AddOrUpdate1Test(IExtendedDictionary<int, int> dictionary)
        {
            dictionary.Add(1, 2);
            dictionary.Add(2, 2);
            dictionary.Add(3, 2);

            dictionary.AddOrUpdate(1, 3);
            Assert.That(dictionary[1], Is.EqualTo(3));

            dictionary.AddOrUpdate(4, 5);
            Assert.That(dictionary[4], Is.EqualTo(5));
        }

        [Test]
        [TestCaseSource("Int32DictionaryCases")]
        public void AddOrUpdate2Test(IExtendedDictionary<int, int> dictionary)
        {
            dictionary.Add(1, 2);
            dictionary.Add(2, 2);
            dictionary.Add(3, 2);

            dictionary.AddOrUpdate(4, 3, (k, v) => 3);
            Assert.That(dictionary[4], Is.EqualTo(3));

            dictionary.AddOrUpdate(4, 3, (k, v) => v + 4);
            Assert.That(dictionary[4], Is.EqualTo(7));
        }

        [Test]
        [TestCaseSource("Int32DictionaryCases")]
        public void AddOrUpdate3Test(IExtendedDictionary<int, int> dictionary)
        {
            dictionary.Add(1, 2);
            dictionary.Add(2, 2);
            dictionary.Add(3, 2);

            dictionary.AddOrUpdate(4, k => 4, (k, v) => 3);
            Assert.That(dictionary[4], Is.EqualTo(4));

            dictionary.AddOrUpdate(4, k => 4, (k, v) => v + 4);
            Assert.That(dictionary[4], Is.EqualTo(8));
        }

        [Test]
        [TestCaseSource("Int32DictionaryCases")]
        public void GetOrAdd1Test(IExtendedDictionary<int, int> dictionary)
        {
            dictionary.Add(1, 2);
            dictionary.Add(2, 2);
            dictionary.Add(3, 2);

            int v = dictionary.GetOrAdd(4, 3);
            Assert.That(dictionary[4], Is.EqualTo(3));
            Assert.IsTrue(v == 3);

            v = dictionary.GetOrAdd(2, 4);
            Assert.That(dictionary[2], Is.EqualTo(2));
            Assert.IsTrue(v == 2);
        }

        [Test]
        [TestCaseSource("Int32DictionaryCases")]
        public void GetOrAdd2Test(IExtendedDictionary<int, int> dictionary)
        {
            dictionary.Add(1, 2);
            dictionary.Add(2, 2);
            dictionary.Add(3, 2);

            int v = dictionary.GetOrAdd(4, () => 3);
            Assert.That(dictionary[4], Is.EqualTo(3));
            Assert.IsTrue(v == 3);

            v = dictionary.GetOrAdd(2, () => 3);
            Assert.That(dictionary[2], Is.EqualTo(2));
            Assert.IsTrue(v == 2);
        }

        [Test]
        [TestCaseSource("Int32DictionaryCases")]
        public void UpdateTest(IExtendedDictionary<int, int> dictionary)
        {
            dictionary.Add(1, 2);
            dictionary.Add(2, 2);
            dictionary.Add(3, 2);

            dictionary.Update(3, 4);
            Assert.That(dictionary[3], Is.EqualTo(4));
        }

        [Test]
        [TestCaseSource("Int32DictionaryCases")]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void UpdateFailureTest(IExtendedDictionary<int, int> dictionary)
        {
            dictionary.Update(3, 0);
        }

        [Test]
        [TestCaseSource("Int32DictionaryCases")]
        public void TryUpdateFailureTest(IExtendedDictionary<int, int> dictionary)
        {
            dictionary.Add(1, 2);
            dictionary.Add(2, 2);
            dictionary.Add(3, 2);

            bool result = dictionary.TryUpdate(3, 4);

            Assert.IsTrue(result);

            result = dictionary.TryUpdate(4, 4);

            Assert.IsFalse(result);
        }

        [Test]
        [TestCaseSource("Int32DictionaryCases")]
        public void AddTest(IExtendedDictionary<int, int> dictionary)
        {
            dictionary.Add(1, 2);

            Assert.That(dictionary[1], Is.EqualTo(2));
        }

        [Test]
        [TestCaseSource("Int32DictionaryCases")]
        public void ContainsKeyTest(IExtendedDictionary<int, int> dictionary)
        {
            dictionary.Add(1, 2);

            Assert.IsTrue(dictionary.ContainsKey(1));
        }

        [Test]
        [TestCaseSource("Int32DictionaryCases")]
        public void NotContainsKeyTest(IExtendedDictionary<int, int> dictionary)
        {
            Assert.IsFalse(dictionary.ContainsKey(3));
        }

        [Test]
        [TestCaseSource("Int32DictionaryCases")]
        public void RemoveTest(IExtendedDictionary<int, int> dictionary)
        {
            dictionary.Add(1, 2);

            bool result = dictionary.Remove(1);
            Assert.IsFalse(dictionary.ContainsKey(1));
        }

        [Test]
        [TestCaseSource("Int32DictionaryCases")]
        public void RemoveTest2(IExtendedDictionary<int, int> dictionary)
        {
            dictionary.Add(1, 2);
            dictionary.Add(2, 2);
            dictionary.Add(3, 2);
            dictionary.Add(4, 2);

            dictionary.Remove(2);
            Assert.IsFalse(dictionary.ContainsKey(2));
        }

        [Test]
        [TestCaseSource("Int32DictionaryCases")]
        public void ClearTest(IExtendedDictionary<int, int> dictionary)
        {
            dictionary.Add(1, 2);

            dictionary.Clear();
            Assert.IsTrue(dictionary.Count == 0);
        }

        [Test]
        [TestCaseSource("Int32DictionaryCases")]
        public void GetValueTest(IExtendedDictionary<int, int> dictionary)
        {
            dictionary.Add(1, 2);

            int v = dictionary.GetValue(1);
            Assert.IsTrue(v == 2);
        }

        [Test]
        [TestCaseSource("Int32DictionaryCases")]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void GetValueFailureTest(IExtendedDictionary<int, int> dictionary)
        {
            dictionary.GetValue(1);
        }

        [Test]
        [TestCaseSource("Int32DictionaryCases")]
        public void TryGetValueTest(IExtendedDictionary<int, int> dictionary)
        {
            dictionary.Add(1, 2);

            int v;
            bool result = dictionary.TryGetValue(1, out v);

            Assert.IsTrue(result);
            Assert.That(v, Is.EqualTo(2));
        }

        [Test]
        [TestCaseSource("Int32DictionaryCases")]
        public void TryGetValueFalseTest(IExtendedDictionary<int, int> dictionary)
        {
            int v;
            bool result = dictionary.TryGetValue(1, out v);

            Assert.IsFalse(result);
        }

        [Test]
        [TestCaseSource("Int32DictionaryCases")]
        public void KeyValuePairAddTest(IExtendedDictionary<int, int> dictionary)
        {
            dictionary.Add(new KeyValuePair<int, int>(1, 2));

            Assert.That(dictionary[1], Is.EqualTo(2));
        }

        [Test]
        [TestCaseSource("Int32DictionaryCases")]
        public void KeyValuePairContainsTest(IExtendedDictionary<int, int> dictionary)
        {
            dictionary.Add(new KeyValuePair<int, int>(1, 2));

            Assert.IsTrue(dictionary.Contains(new KeyValuePair<int, int>(1, 2)));
        }

        [Test]
        [TestCaseSource("Int32DictionaryCases")]
        public void KeyValuePairRemoveTest(IExtendedDictionary<int, int> dictionary)
        {
            dictionary.Add(new KeyValuePair<int, int>(1, 2));
            dictionary.Remove(new KeyValuePair<int, int>(1, 2));

            Assert.IsTrue(dictionary.Count == 0);
        }
    }
}
