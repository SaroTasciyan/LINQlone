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

namespace LINQlone.Test.DataObjects
{
    public abstract class CallCountableEqualityComparer<T> : IEqualityComparer<T>
    {
        #region Definitions

        protected const int HASHCODE_SEED = 23; // # Prime number as seed hashcode
        protected const int HASHCODE_FACTOR = 29; // # Another (greater) prime number for hashcode calculation
        protected const int HASHCODE_FOR_NULL = 0; // # Zero will be used as hashcode for null fields

        #endregion ENDOF: Definitions

        #region Abstract Members

        protected abstract bool EqualsImplementation(T x, T y);
        protected abstract int GetHashCodeImplementation(T obj);

        #endregion ENDOF: Abstract Members

        #region Properties

        public int EqualsCallCount { get; private set; } // # Implemented for testing and profiling purposes
        public int GetHashCodeCallCount { get; private set; } // # Implemented for testing and profiling purposes

        #endregion ENDOF: Properties

        #region Constructors

        protected CallCountableEqualityComparer() { }

        #endregion ENDOF: Constructors

        #region IEqualityComparer<T> Members

        public bool Equals(T x, T y)
        {
            EqualsCallCount++;

            return EqualsImplementation(x, y);
        }

        public int GetHashCode(T obj)
        {
            GetHashCodeCallCount++;

            return GetHashCodeImplementation(obj);
        }

        #endregion

        #region Methods

        public void ResetCounts()
        {
            EqualsCallCount = 0;
            GetHashCodeCallCount = 0;
        }

        #endregion ENDOF: Methods
    }
}
