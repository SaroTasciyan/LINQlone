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
    internal class NullableKeyDictionary<TKey, TValue> : IDictionary<TKey, TValue>, IDictionary 
    {
        #region NullableKey<T>

        private sealed class NullableKey<T>
        {
            #region Comparer<T>

            internal sealed class Comparer : IEqualityComparer<NullableKey<T>>
            {
                #region Fields

                private readonly IEqualityComparer<T> mComparer;

                #endregion ENDOF: Fields

                #region Constructors

                internal Comparer() : this(null) { }

                internal Comparer(IEqualityComparer<T> comparer)
                {
                    mComparer = comparer ?? EqualityComparer<T>.Default;
                }

                #endregion ENDOF: Constructors

                #region Properties

                public IEqualityComparer<T> InternalComparer
                {
                    get { return mComparer; }
                }

                #endregion ENDOF: Properties

                #region IEqualityComparer<NullableKey<T>> Implementation

                public bool Equals(NullableKey<T> x, NullableKey<T> y)
                {
                    return mComparer.Equals(x.Value, y.Value);
                }

                public int GetHashCode(NullableKey<T> obj)
                {
                    if (obj == null || obj.Value == null) { return 0; } // # Returning zero for hashcode indicating that the value is null
                    if (mComparer == null) { return obj.Value.GetHashCode(); }

                    return mComparer.GetHashCode(obj.Value);
                }

                #endregion ENDOF: IEqualityComparer<NullableKey<T>> Implementation
            }

            #endregion ENDOF: Comparer<T>

            #region Operator Overloads

            public static implicit operator NullableKey<T>(T value) { return new NullableKey<T>(value); }
            public static implicit operator T(NullableKey<T> nullableKey) { return nullableKey.Value; }

            #endregion ENDOF: Operator Overloads

            #region Fields

            private static volatile NullableKey<T> mNull; // # Singleton for null NullableKey<T>

            private readonly T mValue;

            #endregion ENDOF: Fields

            #region Properties

            private static NullableKey<T> Null
            {
                get
                {
                    if (mNull == null) { mNull = new NullableKey<T>(default(T)); }

                    return mNull;
                }
            }

            internal T Value
            {
                get { return mValue; }
            }

            #endregion ENDOF: Properties

            #region Constructors

            private NullableKey(T value)
            {
                mValue = value;
            }

            #endregion ENDOF: Constructors

            #region Methods

            internal static NullableKey<T> Create(T key)
            {
                return (key != null ? new NullableKey<T>(key) : NullableKey<T>.Null);
            }

            #endregion ENDOF: Methods

            #region Overriden Methods

            public override string ToString()
            {
                return (mValue == null ? "Null" : mValue.ToString());
            }

            #endregion ENDOF: Overriden Methods
        }

        #endregion ENDOF: NullableKey<T>

        #region Fields

        private readonly Dictionary<NullableKey<TKey>, TValue> mDictionary;

        #endregion ENDOF: Fields

        #region Constructors

        internal NullableKeyDictionary()
        {
            mDictionary = new Dictionary<NullableKey<TKey>, TValue>(new NullableKey<TKey>.Comparer());
        }

        internal NullableKeyDictionary(int capacity, IEqualityComparer<TKey> comparer)
        {
            mDictionary = new Dictionary<NullableKey<TKey>, TValue>(capacity, new NullableKey<TKey>.Comparer(comparer));
        }

        internal NullableKeyDictionary(IEqualityComparer<TKey> comparer)
        {
            mDictionary = new Dictionary<NullableKey<TKey>, TValue>(new NullableKey<TKey>.Comparer(comparer));
        }

        #endregion ENDOF: Constructors

        #region Properties

        public IEqualityComparer<TKey> Comparer
        {
            get { return ((NullableKey<TKey>.Comparer)mDictionary.Comparer).InternalComparer; }
        }

        #endregion ENDOF: Properties

        #region IDictionary<TKey,TValue> Members

        public void Add(TKey key, TValue value)
        {
            mDictionary.Add(NullableKey<TKey>.Create(key), value);
        }

        public bool ContainsKey(TKey key)
        {
            return mDictionary.ContainsKey(NullableKey<TKey>.Create(key));
        }

        public ICollection<TKey> Keys
        {
            get 
            {
                List<TKey> keyList = new List<TKey>();

                foreach (TKey key in mDictionary.Keys)
                {
                    keyList.Add(key);
                }

                return keyList;
            }
        }

        public bool Remove(TKey key)
        {
            return mDictionary.Remove(NullableKey<TKey>.Create(key));
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return mDictionary.TryGetValue(NullableKey<TKey>.Create(key), out value);
        }

        public ICollection<TValue> Values
        {
            get { return mDictionary.Values; }
        }

        public TValue this[TKey key]
        {
            get { return mDictionary[NullableKey<TKey>.Create(key)]; }
            set { mDictionary.Add(NullableKey<TKey>.Create(key), value); }
        }

        #endregion

        #region IDictionary Members

        public void Add(object key, object value)
        {
            Add((TKey)key, (TValue)value);
        }

        public bool Contains(object key)
        {
            return ContainsKey((TKey)key);
        }

        IDictionaryEnumerator IDictionary.GetEnumerator()
        {
            return mDictionary.GetEnumerator();
        }

        public bool IsFixedSize
        {
            get { return false; }
        }

        ICollection IDictionary.Keys
        {
            get { return (ICollection)Keys; }
        }

        public void Remove(object key)
        {
            Remove((TKey)key);
        }

        ICollection IDictionary.Values
        {
            get { return (ICollection)Values; }
        }

        public object this[object key]
        {
            get { return this[(TKey)key]; }
            set { this[(TKey)key] = (TValue)value; }
        }

        #endregion

        #region ICollection<KeyValuePair<TKey,TValue>> Members

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item.Key, item.Value);
        }

        public void Clear()
        {
            mDictionary.Clear();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            KeyValuePair<NullableKey<TKey>, TValue> keyValuePair = new KeyValuePair<NullableKey<TKey>, TValue>(NullableKey<TKey>.Create(item.Key), item.Value);

            return ((ICollection<KeyValuePair<NullableKey<TKey>, TValue>>)mDictionary).Contains(keyValuePair);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            KeyValuePair<TKey, TValue>[] keyValuePairArray = new KeyValuePair<TKey, TValue>[Count];

            int count = 0;
            foreach (KeyValuePair<TKey, TValue> keyValuePair in this) // # Using GetEnumerator() implementation of 'this'
            {
                keyValuePairArray[count++] = keyValuePair;
            }

            keyValuePairArray.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return mDictionary.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            KeyValuePair<NullableKey<TKey>, TValue> keyValuePair = new KeyValuePair<NullableKey<TKey>, TValue>(NullableKey<TKey>.Create(item.Key), item.Value);

            return ((ICollection<KeyValuePair<NullableKey<TKey>, TValue>>)mDictionary).Remove(keyValuePair);
        }

        #endregion

        #region ICollection Members

        public void CopyTo(Array array, int index)
        {
            object[] objectArray = new object[Count];

            int count = 0;
            foreach (KeyValuePair<TKey, TValue> keyValuePair in this) // # Using GetEnumerator() implementation of 'this'
            {
                objectArray[count++] = keyValuePair;
            }

            objectArray.CopyTo(array, index);
        }

        public bool IsSynchronized
        {
            get { return false; }
        }

        public object SyncRoot
        {
            get { return ((ICollection)mDictionary).SyncRoot; }
        }

        #endregion

        #region IEnumerable<KeyValuePair<TKey,TValue>> Members

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            foreach (KeyValuePair<NullableKey<TKey>, TValue> nullableKeyValuePair in mDictionary)
            {
                yield return new KeyValuePair<TKey, TValue>(nullableKeyValuePair.Key, nullableKeyValuePair.Value);
            }
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}