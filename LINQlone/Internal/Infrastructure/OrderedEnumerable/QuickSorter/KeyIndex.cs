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

using System.Collections.Generic;

namespace LINQlone.Infrastructure
{    
    internal class KeyIndex<T>
    {
        #region Comparer

        internal sealed class Comparer : IComparer<KeyIndex<T>>
        {
            #region Fields

            private readonly IComparer<T> mKeyComparer;
            
            #endregion ENDOF: Fields

            #region Constructors

            internal Comparer(IComparer<T> keyComparer)
            {
                mKeyComparer = keyComparer;
            }

            #endregion ENDOF: Constructors

            #region IComparer<KeyIndex<T>> Members

            public int Compare(KeyIndex<T> x, KeyIndex<T> y)
            {
                int keyComparisonResult = mKeyComparer.Compare(x.Key, y.Key);

                if (keyComparisonResult == 0) { return (x.Index - y.Index); } // # Values are equal. Index values are compared to achieve stable order.
                else { return keyComparisonResult; }
            }

            #endregion
        }

        #endregion ENDOF: Comparer

        #region Fields

        private readonly T mKey;
        private readonly int mIndex;

        #endregion ENDOF: Fields

        #region Properties

        internal T Key
        {
            get { return mKey; }
        }

        internal int Index
        {
            get { return mIndex; }
        }

        #endregion ENDOF: Properties

        #region Constructors

        internal KeyIndex(T key, int index)
        {
            mKey = key;
            mIndex = index;
        }

        #endregion ENDOF: Constructors
    }
}
