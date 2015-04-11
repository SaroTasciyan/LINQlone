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
using System.Collections.Generic;

using LINQlone.Definitions;

namespace LINQlone.Infrastructure
{
    internal struct Buffer<T>
    {
        #region Definitions

        private const int ARRAY_INITIAL_ALLOCATION_SIZE = 4;
        private const int ARRAY_EXPANDING_FACTOR = 2;

        #endregion ENDOF: Definitions

        #region Fields

        private T[] mSource;
        private int mCount;

        #endregion ENDOF: Fields

        #region Properties

        internal int Count
        {
            get { return mCount; }
        }

        internal bool IsEmpty
        {
            get { return (mCount <= 0); }
        }

        internal T this[int index]
        {
            get
            {
                if (index >= mCount) { throw Exceptions.ArgumentNull(Parameter.Index); }

                return mSource[index];
            }
        }

        #endregion ENDOF: Properties

        #region Constructors

        internal Buffer(IEnumerable<T> source)
        {
            mSource = null;
            mCount = 0;

            ICollection<T> collection = source as ICollection<T>; // # Optimization for ICollection<T> where number of elements can be obtained via Count property
            if (collection != null)
            {
                mCount = collection.Count;

                if (mCount > 0)
                {
                    mSource = new T[collection.Count];
                    collection.CopyTo(mSource, 0);
                }
            }
            else
            {
                foreach (T item in source)
                {
                    if (mSource == null) { mSource = new T[ARRAY_INITIAL_ALLOCATION_SIZE]; }
                    else if (mSource.Length == mCount) // # Array length is reached, needs resizing
                    {
                        Array.Resize(ref mSource, checked(mSource.Length * ARRAY_EXPANDING_FACTOR));
                    }

                    mSource[mCount++] = item;
                }
            }
        }

        #endregion ENDOF: Constructors

        #region Methods

        internal T[] ToArray()
        {
            if (mCount == 0) { return new T[0]; }
            if (mSource.Length == mCount) { return mSource; }
            else // # Buffer has no excess slots, resizing buffer to proper length
            {
                Array.Resize(ref mSource, mCount);

                return mSource;
            }
        }

        #endregion ENDOF: Methods
    }
}
