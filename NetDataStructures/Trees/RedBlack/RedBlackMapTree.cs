using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetDataStructures.Trees.RedBlack
{
    public class RedBlackTree<TKey, TValue> : RedBlackTreeBase<RedBlackTreeNode<TKey, TValue>, TKey, TValue>, IRedBlackTree<TKey, TValue>
        where TKey : IComparable<TKey>
    {
        public TValue this[TKey key]
        {
            get { return GetValue(key); }
            set { Update(key, value); }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public ICollection<TKey> Keys
        {
            get { return this.Select(v => v.Key).ToList(); }
        }

        public ICollection<TValue> Values
        {
            get { return this.Select(v => v.Value).ToList(); }
        }

        public RedBlackTree()
            : base(new RedBlackTreeNode<TKey, TValue>.NodeFactory())
        { }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return new BinaryTreeEnumerator<RedBlackTreeNode<TKey, TValue>, TKey, TValue>(Root, false)
                .Select(v => new KeyValuePair<TKey, TValue>(v.Key, v.Value))
                .GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


        public IEnumerable<KeyValuePair<TKey, TValue>> GetElements(bool descending)
        {
            return new BinaryTreeEnumerator<RedBlackTreeNode<TKey, TValue>, TKey, TValue>(Root, descending)
                .Select(v => new KeyValuePair<TKey, TValue>(v.Key, v.Value));
        }

        public IEnumerable<KeyValuePair<TKey, TValue>> GetElementsGreaterThan(TKey key)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<KeyValuePair<TKey, TValue>> GetElementsLessThan(TKey key)
        {
            throw new NotImplementedException();
        }

        public bool Remove(TKey key, TValue value)
        {
            Contract.Requires(key != null);

            return Root.With(r => BinaryTreeHelper.Find(r, key, value)).Do(n => Remove(n)).ReturnSuccess();
        }

        public bool RemoveAll(TKey key)
        {
            Contract.Requires(key != null);
            var nodes = Root.With(r => BinaryTreeHelper.FindAll(r, key));

            if (!nodes.Any()) return false;

            nodes.ForEach(n => Remove(n));

            return true;
        }

        public bool ContainsKey(TKey key)
        {
            Contract.Requires(key != null);

            return Contains(key);
        }

        #region Get Methods

        public IEnumerable<TValue> GetAllValues(TKey key)
        {
            Contract.Requires(key != null);

            return BinaryTreeHelper.FindAll<RedBlackTreeNode<TKey, TValue>, TKey, TValue>(Root, key).Select(v => v.Value);
        }

        public TValue GetValue(TKey key)
        {
            Contract.Requires(key != null);

            return GetNode(key).Value;
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            Contract.Requires(key != null);

            var node = BinaryTreeHelper.Find<RedBlackTreeNode<TKey, TValue>, TKey, TValue>(Root, key);

            if (node != null)
            {
                value = node.Value;
                return true;
            }
            else
            {
                value = default(TValue);
                return false;
            }
        }

        public virtual TValue GetOrAdd(TKey key, TValue addValue)
        {
            return GetOrAdd(key, () => addValue);
        }

        public virtual TValue GetOrAdd(TKey key, Func<TValue> addValueFactory)
        {
            Contract.Requires(key != null);
            Contract.Requires(addValueFactory != null);

            var node = BinaryTreeHelper.Find<RedBlackTreeNode<TKey, TValue>, TKey, TValue>(Root, key);

            if (node == null)
            {
                node = AddKey(key);
                node.Value = addValueFactory();
            }

            return node.Value;
        }

        #endregion

        #region Add/Update Methods

        public void Add(TKey key, TValue value)
        {
            Contract.Requires(key != null);

            var node = AddKey(key);
            node.Value = value;
        }

        public virtual TValue AddOrUpdate(TKey key, TValue newValue)
        {
            return AddOrUpdate(key, k => newValue, (k, v) => newValue);
        }

        public virtual TValue AddOrUpdate(TKey key, TValue addValue, Func<TKey, TValue, TValue> updateValueFactory)
        {
            return AddOrUpdate(key, k => addValue, updateValueFactory);
        }

        public virtual TValue AddOrUpdate(TKey key, Func<TKey, TValue> addValueFactory, Func<TKey, TValue, TValue> updateValueFactory)
        {
            Contract.Requires<ArgumentNullException>(key != null);
            Contract.Requires<ArgumentNullException>(addValueFactory != null);
            Contract.Requires<ArgumentNullException>(updateValueFactory != null);

            var node = BinaryTreeHelper.Find(Root, key);

            if (node == null)
            {
                node = AddKey(key);
                node.Value = addValueFactory(key);
            }
            else
            {
                node.Value = updateValueFactory(node.Key, node.Value);
            }

            return node.Value;
        }

        public void Update(TKey key, TValue value)
        {
            Contract.Requires(key != null);

            GetNode(key).Value = value;
        }

        public bool TryUpdate(TKey key, TValue value)
        {
            Contract.Requires(key != null);

            return BinaryTreeHelper.Find(Root, key).Do(n => n.Value = value).ReturnSuccess();
        }

        #endregion 

        #region ICollection methods

        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item.Key, item.Value);
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item)
        {
            return BinaryTreeHelper.Find(Root, item.Key, item.Value).ReturnSuccess();
        }

        void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            var arrayOut = this.ToArray();

            for (int i = 0; i < arrayOut.Length; i++)
            {
                array[i + arrayIndex] = arrayOut[i];
            }
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
        {
            return Remove(item.Key, item.Value);
        }

        #endregion
    }
}
