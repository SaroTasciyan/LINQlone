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

using System.Linq;
using System.Collections;
using System.Collections.Generic;

using LINQlone.Definitions;

namespace LINQlone.Infrastructure
{
    internal class Grouping<TKey, TElement> : IGrouping<TKey, TElement>, IList<TElement>
    {
        #region Fields

        private readonly TKey mKey;
        private readonly IList<TElement> mElements;

        #endregion ENDOF: Fields

        #region Constructors

        internal Grouping(TKey key)
        {
            mKey = key;
            mElements = new List<TElement>();
        }

        #endregion ENDOF: Constructors

        #region Methods

        internal void Add(TElement item)
        {
            mElements.Add(item);
        }

        #endregion ENDOF: Methods

        #region IGrouping<TKey,TElement> Implementation

        public TKey Key
        {
            get { return mKey; }
        }

        #endregion ENDOF: IGrouping<TKey,TElement> Implementation

        #region IList<TElement> Members

        public int IndexOf(TElement item) { return mElements.IndexOf(item); }

        public void Insert(int index, TElement item) { throw Exceptions.NotSupported(); }

        public void RemoveAt(int index) { throw Exceptions.NotSupported(); }

        public TElement this[int index]
        {
            get
            {
                if (index < 0 || index >= mElements.Count) { throw Exceptions.ArgumentOutOfRange(Parameter.Index); }

                return mElements[index];
            }
            set { throw Exceptions.NotSupported(); }
        }

        #endregion

        #region ICollection<TElement> Implementation

        void ICollection<TElement>.Add(TElement item) { throw Exceptions.NotSupported(); }

        public void Clear() { throw Exceptions.NotSupported(); }

        public bool Contains(TElement item) { return mElements.Contains(item); }

        public void CopyTo(TElement[] array, int arrayIndex) { mElements.CopyTo(array, arrayIndex); }

        public int Count 
        {
            get { return mElements.Count; } 
        }

        public bool IsReadOnly
        {
            get { return true; }
        }

        public bool Remove(TElement item) { throw Exceptions.NotSupported(); }

        #endregion ENDOF: ICollection<TElement> Implementation

        #region IEnumerable<TElement> Implementation

        public IEnumerator<TElement> GetEnumerator() { return mElements.GetEnumerator(); }

        #endregion ENDOF: IEnumerable<TElement> Implementation

        #region IEnumerable Implementation

        IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }

        #endregion ENDOF: IEnumerable Implementation
    }
}
