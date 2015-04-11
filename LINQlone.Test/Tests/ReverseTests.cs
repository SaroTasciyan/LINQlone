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
using System.Linq;

using NUnit.Framework;

using LINQlone.Test.DataObjects;

namespace LINQlone.Test
{
    [TestFixture]
    public class ReverseTests : BaseTest
    {
        #region Range

        [Test]
        public void ReverseDeferredExecution()
        {
            Data<int> data = Data(1, 2, 3);

            data.Reverse();
            data.AssertDeferredExecution();
        }

        [Test]
        public void ReverseNonStreamingExecution()
        {
            IEnumerable<object> enumerable = LateThrowingData().Reverse();

            Assert.Throws<LateThrownException>(() => enumerable.MoveNext()); // # Sequence was fully buffered, throwing LateThrownException after enumeration ended
        }

        [Test]
        public void ReverseEmptySource()
        {
            EmptyData.Reverse().AssertEmpty();
        }

        [Test]
        public void ReverseNonEmptySourceTrue()
        {
            Data(1, 2, 3).Reverse().AssertEqual(3, 2, 1);
        }

        [Test]
        public void ReverseQueryReuse()
        {
            List<int> data = new List<int> { 1, 2, 3 };
            IEnumerable<int> enumerable = data.Reverse<int>(); // # Using Reverse<int> (with type parameter) in order to avoid calling List<int>.Reverse()

            enumerable.AssertEqual(3, 2, 1);

            data.Add(4);
            enumerable.AssertEqual(4, 3, 2, 1);
        }

        [Test]
        public void ReverseSourceArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => NullData.Reverse()).WithParameter("source");
        }

        #endregion ENDOF: Range
    }
}
