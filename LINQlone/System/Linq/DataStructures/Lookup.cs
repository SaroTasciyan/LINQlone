#region Copyright & License Information

// Copyright 2014 Saro Taşciyan, Bedir Yılmaz
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#endregion ENDOF: Copyright & License Information

using System.Collections;
using System.Collections.Generic;

using LINQlone.Infrastructure;

namespace System.Linq
{
    /// <summary>
    /// Represents a collection of keys each mapped to one or more values.
    /// </summary>
    /// <typeparam name="TKey">The type of the keys in the System.Linq.Lookup&lt;TKey,TElement&gt;.</typeparam>
    /// <typeparam name="TElement">The type of the elements of each System.Collections.Generic.IEnumerable&lt;T&gt; value in the System.Linq.Lookup&lt;TKey,TElement&gt;.</typeparam>
    public class Lookup<TKey, TElement> : ILookup<TKey, TElement> // # Lookup implementation is immutable externally but is mutable internally via Add() method.
    {
        #region Fields

        private readonly NullableKeyDictionary<TKey, IGrouping<TKey, TElement>> mKeyGroupPairs;

        #endregion ENDOF: Fields

        #region Constructors
      
        internal Lookup(IEqualityComparer<TKey> comparer)
        {
            mKeyGroupPairs = new NullableKeyDictionary<TKey, IGrouping<TKey, TElement>>(comparer);
        }

        #endregion ENDOF: Construcors

        #region Methods

        internal void Add(TKey key, TElement element)
        {
            //TODO # Lookup.Add() optimization            
            // # Before adding a new key, TryGetValue() method is used to obtain grouping if the key already exists
            // # Using TryGetValue() triggers an extra call to GetHashCode() of comparer
            // # Lookup.Add() method requires an optimized adding mechanism
            IGrouping<TKey, TElement> grouping;

            if (!mKeyGroupPairs.TryGetValue(key, out grouping))
            {
                grouping = new Grouping<TKey, TElement>(key);
                mKeyGroupPairs.Add(key, grouping);
            }

            ((Grouping<TKey, TElement>)grouping).Add(element);
        }

        /// <summary>
        /// Applies a transform function to each key and its associated values and returns the results.
        /// </summary>
        /// <param name="resultSelector">A function to project a result value from each key and its associated values.</param>
        /// <typeparam name="TResult">The type of the result values produced by resultSelector.</typeparam>
        /// <returns>A collection that contains one value for each key/value collection pair in the System.Linq.Lookup&lt;TKey,TElement&gt;.</returns>
        public IEnumerable<TResult> ApplyResultSelector<TResult>(Func<TKey, IEnumerable<TElement>, TResult> resultSelector)
        {
            foreach (KeyValuePair<TKey, IGrouping<TKey, TElement>> item in mKeyGroupPairs)
            {
                yield return resultSelector(item.Key, item.Value);
            }
        }

        #endregion ENDOF: Methods

        #region ILookup<TKey,TElement> Implementation

        /// <summary>
        /// Gets the number of key/value collection pairs in the System.Linq.Lookup&lt;TKey,TElement&gt;.
        /// </summary>
        /// <returns>The number of key/value collection pairs in the System.Linq.Lookup&lt;TKey,TElement&gt;.</returns>
        public int Count
        {
            get { return mKeyGroupPairs.Count; }
        }

        /// <summary>
        /// Gets the collection of values indexed by the specified key.
        /// </summary>
        /// <param name="key">The key of the desired collection of values.</param>
        /// <returns>The collection of values indexed by the specified key.</returns>
        public IEnumerable<TElement> this[TKey key]
        {
            get 
            {
                IGrouping<TKey, TElement> grouping;

                if (!mKeyGroupPairs.TryGetValue(key, out grouping))
                {
                    return new TElement[0]; // # No grouping paired with given key, retuning empty IEnumerable
                }

                return grouping;
            }
        }

        /// <summary>
        /// Determines whether a specified key is in the System.Linq.Lookup&lt;TKey,TElement&gt;.
        /// </summary>
        /// <param name="key">The key to find in the System.Linq.Lookup&lt;TKey,TElement&gt;.</param>
        /// <returns>true if key is in the System.Linq.Lookup&lt;TKey,TElement&gt;; otherwise, false.</returns>
        public bool Contains(TKey key) 
        {
            //HACK # Lookup.Contains() hack
            // # Work around for doomed (not optimized) System.Linq.Lookup implementation calling GetHashCode() for given key even it holds no elements. 
            // # Underlying dictionary is optimized and DOES NOT call GetHashCode() for given key if it is EMPTY because it holds NO ELEMENTS TO COMPARE afterwards.
            // # Calling for GetHashCode() manually for the sake of System.Linq.Lookup behavior.
            if (mKeyGroupPairs.Count == 0) { mKeyGroupPairs.Comparer.GetHashCode(key); }

            return mKeyGroupPairs.ContainsKey(key); 
        }

        #endregion ENDOF: ILookup<TKey,TElement> Implementation

        #region IEnumerable<IGrouping<TKey,TElement>> Implementation

        /// <summary>
        /// Returns a generic enumerator that iterates through the System.Linq.Lookup&lt;TKey,TElement&gt;.
        /// </summary>
        /// <returns>An enumerator for the System.Linq.Lookup&lt;TKey,TElement&gt;.</returns>
        public IEnumerator<IGrouping<TKey, TElement>> GetEnumerator()
        {
            foreach (KeyValuePair<TKey, IGrouping<TKey, TElement>> item in mKeyGroupPairs)
            {
                yield return item.Value;
            }
        }

        #endregion ENDOF: IEnumerable<IGrouping<TKey,TElement>> Implementation

        #region IEnumerable Implementation

        IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }

        #endregion ENDOF: IEnumerable Implementation
    }
}
