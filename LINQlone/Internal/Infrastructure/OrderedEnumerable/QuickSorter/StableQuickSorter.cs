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

namespace LINQlone.Infrastructure
{
    internal class StableQuickSorter<TSource, TKey> : QuickSorter<KeyIndex<TKey>>
    {
        #region Fields

        private readonly IEnumerable<TSource> mSource;
        private readonly Func<TSource, TKey> mKeySelector;

        #endregion ENDOF: Fields

        #region Constructors

        internal StableQuickSorter(IEnumerable<TSource> source, IComparer<TKey> comparer, Func<TSource, TKey> keySelector)
        {
            mSource = source;
            mKeySelector = keySelector;
            mComparer = new KeyIndex<TKey>.Comparer(comparer);
        }

        #endregion ENDOF: Constructors

        #region Methods

        internal override void Sort()
        {
            throw new NotImplementedException();
        }

        internal IEnumerator<TSource> DeferredSort()
        {
            Buffer<TSource> buffer = new Buffer<TSource>(mSource);

            if (!buffer.IsEmpty)
            {
                mData = new KeyIndex<TKey>[buffer.Count]; // # Sorting will be performed on KeyIndex values created, using index information to achieve stabile order

                for (int index = 0; index < mData.Length; index++)
                {
                    mData[index] = new KeyIndex<TKey>(mKeySelector(buffer[index]), index);
                }

                base.Sort();

                for (int index = 0; index < mData.Length; index++) // # Yielding buffered items in sorted index order
                {
                    yield return buffer[mData[index].Index];
                }
            }
        }

        #endregion ENDOF: Methods
    }
}
