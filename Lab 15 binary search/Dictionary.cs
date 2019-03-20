/* Dictionary.cs
 * Author: Rod Howell
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksu.Cis300.NameLookup
{
    /// <summary>
    /// A generic dictionary in which keys must implement IComparable.
    /// </summary>
    /// <typeparam name="TKey">The key type.</typeparam>
    /// <typeparam name="TValue">The value type.</typeparam>
    public class Dictionary<TKey, TValue> where TKey : IComparable<TKey>
    {
        /// <summary>
        /// The keys and values in the dictionary.
        /// </summary>
        private List<KeyValuePair<TKey, TValue>> _elements = new List<KeyValuePair<TKey, TValue>>();

        /// <summary>
        /// Checks to see if the given key is null, and if so, throws an
        /// ArgumentNullException.
        /// </summary>
        /// <param name="key">The key to check.</param>
        private static void CheckKey(TKey key)
        {
            if (key == null)
            {
                throw new ArgumentNullException();
            }
        }

        /// <summary>
        /// Finds the location of the given key in the List, or the location at
        /// which it could be inserted if it doesn't exits.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>The location of key, or the location at which it could be
        /// inserted.</returns>
        private int Find(TKey key)
        {
            int start = 0;
            int end = _elements.Count;
            while (start < end)
            {
                int mid = (start + end) / 2;
                int comp = key.CompareTo(_elements[mid].Key);
                if (comp == 0)
                {
                    return mid;
                }
                else if (comp < 0)
                {
                    end = mid;
                }
                else
                {
                    start = mid + 1;
                }
            }
            return start;
        }

        /// <summary>
        /// Tries to get the value associated with the given key.
        /// </summary>
        /// <param name="k">The key.</param>
        /// <param name="v">The value associated with k, or the default value if
        /// k is not in the dictionary.</param>
        /// <returns>Whether k was found as a key in the dictionary.</returns>
        public bool TryGetValue(TKey k, out TValue v)
        {
            CheckKey(k);
            int p = Find(k);
            if (p == _elements.Count || !_elements[p].Key.Equals(k))
            {
                v = default(TValue);
                return false;
            }
            else
            {
                v = _elements[p].Value;
                return true;
            }
        }

        /// <summary>
        /// Adds the given key with the given associated value.
        /// If the given key is already in the dictionary, throws an
        /// InvalidOperationException.
        /// </summary>
        /// <param name="k">The key.</param>
        /// <param name="v">The value.</param>
        public void Add(TKey k, TValue v)
        {
            CheckKey(k);
            int p = Find(k);
            if (p == _elements.Count || !_elements[p].Key.Equals(k))
            {
                _elements.Insert(p, new KeyValuePair<TKey, TValue>(k, v));
            }
            else
            {
                throw new ArgumentException();
            }
        }
    }
}
