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

using System;
using System.Collections;
using System.Collections.Generic;

namespace LINQlone.Infrastructure
{
    internal class UniqueCollection<T> : ICollection<T>
    {
        #region Fields

        private readonly NullableKeyDictionary<T, object> mDictionary;

        #endregion ENDOF: Fields

        #region Constructors
        
        internal UniqueCollection() : this((IEnumerable<T>)null)
        {
        }

        internal UniqueCollection(IEnumerable<T> source) : this(source, null)
        {
        }

        internal UniqueCollection(IEqualityComparer<T> comparer) : this(null, comparer)
        {
        }

        internal UniqueCollection(IEnumerable<T> source, IEqualityComparer<T> comparer)
        {
            mDictionary = new NullableKeyDictionary<T, object>(comparer);

            if (source != null)
            {
                TryAdd(source);
            }
        }

        #endregion ENDOF: Constructors

        #region Methods

        internal bool TryAdd(T value)
        {
            //TODO # UniqueCollection.TryAdd() optimization
            // # Work around for dictionary Add() method throwing ArgumentException for existing keys
            // # Before adding a new key, Contains() method can be used to check if the key already exists
            // # Using Contains() triggers an extra call to GetHashCode() of comparer
            // # UniqueCollection.TryAdd() method requires an optimized adding mechanism
            try { mDictionary.Add(value, null); }
            catch (ArgumentException) { return false; }

            return true;
        }

        #endregion ENDOF: Methods

        #region ICollection<TKey> Implementation

        public void Add(T item) { mDictionary.Add(item, null); }

        public void Clear() { mDictionary.Clear(); }

        public bool Contains(T item) { return mDictionary.ContainsKey(item); }

        public void CopyTo(T[] array, int arrayIndex)
        {
            T[] sourceArray = new T[mDictionary.Keys.Count];

            int count = 0;
            foreach (T item in mDictionary.Keys)
            {
                sourceArray[count++] = item;
            }

            sourceArray.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return mDictionary.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(T item) { return mDictionary.Remove(item); }

        #endregion ENDOF: ICollection<TKey> Implementation

        #region IEnumerable<TKey> Implementation

        public IEnumerator<T> GetEnumerator()
        {
            foreach (T item in mDictionary.Keys)
            {
                yield return item;
            }
        }

        #endregion ENDOF: IEnumerable<TKey> Implementation

        #region IEnumerable Implementation

        IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }

        #endregion ENDOF: IEnumerable Implementation

        #region Helper Methods

        private void TryAdd(IEnumerable<T> source)
        {
            foreach (T item in source) { TryAdd(item); }
        }

        #endregion ENDOF: Helper Methods
    }
}
