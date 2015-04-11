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
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using LINQlone.Definitions;

namespace LINQlone.Infrastructure
{
    internal class OrderedEnumerable<TSource, TCompositeKey> : IOrderedEnumerable<TSource>
    {
        #region Fields

        private readonly IEnumerable<TSource> mSource;
        private readonly Func<TSource, TCompositeKey> mKeySelector;
        private readonly IComparer<TCompositeKey> mComparer;

        #endregion ENDOF: Fields

        #region Constructors

        internal OrderedEnumerable(IEnumerable<TSource> source, Func<TSource, TCompositeKey> keySelector, IComparer<TCompositeKey> comparer, bool descending)
        {
            mSource = source;
            mKeySelector = keySelector;
            
            if (descending) { mComparer = new DescendingComparer<TCompositeKey>(comparer); } // # Wrapped descending comparer for descending order
            else { mComparer = comparer ?? Comparer<TCompositeKey>.Default; }
        }

        private OrderedEnumerable(IEnumerable<TSource> source, Func<TSource, TCompositeKey> keySelector, IComparer<TCompositeKey> comparer)
        {
            mSource = source;
            mKeySelector = keySelector;
            mComparer = comparer;
        }

        #endregion ENDOF: Constructors

        #region IOrderedEnumerable<TSource> Members

        public IOrderedEnumerable<TSource> CreateOrderedEnumerable<TKey>(Func<TSource, TKey> keySelector, IComparer<TKey> comparer, bool descending)
        {
            if (keySelector == null) { throw Exceptions.ArgumentNull(Parameter.KeySelector); }

            Func<TSource, CompositeKey<TCompositeKey, TKey>> compositeKeySelector = (x => new CompositeKey<TCompositeKey, TKey>(mKeySelector(x), keySelector(x)));

            IComparer<CompositeKey<TCompositeKey, TKey>> compositeComparer;
            if (descending) { compositeComparer = new CompositeKey<TCompositeKey, TKey>.Comparer(mComparer, new DescendingComparer<TKey>(comparer)); }
            else { compositeComparer = new CompositeKey<TCompositeKey, TKey>.Comparer(mComparer, comparer ?? Comparer<TKey>.Default); }

            return new OrderedEnumerable<TSource, CompositeKey<TCompositeKey, TKey>>(mSource, compositeKeySelector, compositeComparer);
        }

        #endregion

        #region IEnumerable<TSource> Members

        public IEnumerator<TSource> GetEnumerator()
        {
            StableQuickSorter<TSource, TCompositeKey> stableQuickSorter = new StableQuickSorter<TSource, TCompositeKey>(mSource, mComparer, mKeySelector);

            return stableQuickSorter.DeferredSort();
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
