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
    internal class CompositeKey<TFirst, TSecond>
    {
        #region Comparer

        internal sealed class Comparer : IComparer<CompositeKey<TFirst, TSecond>>
        {
            #region Fields

            private readonly IComparer<TFirst> mFirstComparer;
            private readonly IComparer<TSecond> mSecondComparer;

            #endregion ENDOF: Fields

            #region Constructors

            internal Comparer(IComparer<TFirst> firstComparer, IComparer<TSecond> secondComparer)
            {
                mFirstComparer = firstComparer;
                mSecondComparer = secondComparer;
            }

            #endregion ENDOF: Constructors

            #region IComparer<KeyPair<TFirst,TSecond>> Members

            public int Compare(CompositeKey<TFirst, TSecond> x, CompositeKey<TFirst, TSecond> y)
            {
                int comparisonResult = mFirstComparer.Compare(x.First, y.First);

                if (comparisonResult == 0)
                {
                    if (mSecondComparer == null) { return 0; }
                    else { comparisonResult =  mSecondComparer.Compare(x.Second, y.Second); }
                }
                
                return comparisonResult;
            }

            #endregion
        }

        #endregion ENDOF: Comparer

        #region Fields

        private readonly TFirst mFirst;
        private readonly TSecond mSecond;

        #endregion ENDOF: Fields

        #region Properties

        internal TFirst First
        {
            get { return mFirst; }
        }

        internal TSecond Second
        {
            get { return mSecond; }
        }

        #endregion ENDOF: Properties

        #region Constructors

        internal CompositeKey(TFirst first, TSecond second)
        {
            mFirst = first;
            mSecond = second;
        }

        #endregion ENDOF: Constructors
    }
}
