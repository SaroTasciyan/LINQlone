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
    public class TakeTests : BaseTest
    {
        #region Take

        [Test]
        public void TakeDeferredExecution()
        {
            Data<object> data = DummyData;

            data.Take(1);
            data.AssertDeferredExecution();
        }

        [Test]
        public void TakeStreamingExecution()
        {
            IEnumerable<object> enumerable = LateThrowingData().Take(Int32.MaxValue);

            Assert.DoesNotThrow(() => enumerable.MoveNext()); // # LateThrownException was not thrown since sequence was not fully enumerated
        }

        [Test]
        public void TakeEmptySource()
        {
            EmptyData.Take(1).AssertEmpty();
        }

        [Test]
        public void TakeEnumerationCount()
        {
            Data<int> data = Data(1, 2, 3, 4, 5);

            data.Take(3).Iterate();
            Assert.That(data.EnumerationCount, Is.EqualTo(3)); // # Should not iterate for the elements that were not taken
        }

        [Test]
        public void TakeAll()
        {
            Data('A', 'B', 'C').Take(3).AssertEqual('A', 'B', 'C');
        }

        [Test]
        public void TakeCouple()
        {
            Data('A', 'B', 'C').Take(2).AssertEqual('A', 'B');
        }

        [Test]
        public void TakeZeroCount()
        {
            Data('A', 'B', 'C').Take(0).AssertEmpty();
        }

        [Test]
        public void TakeNegativeCount()
        {
            Data('A', 'B', 'C').Take(-5).AssertEmpty();
        }

        [Test]
        public void TakeOutOfBounds()
        {
            Data('A', 'B', 'C').Take(5).AssertEqual('A', 'B', 'C');
        }

        [Test]
        public void TakeQueryReuse()
        {
            List<int> data = new List<int> { 1, 2, 3 };
            IEnumerable<int> enumerable = data.Take(2);

            enumerable.AssertEqual(1, 2);

            data.Insert(0, 0);
            enumerable.AssertEqual(0, 1);
        }

        [Test]
        public void TakeSourceArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => NullData.Take(0)).WithParameter("source");
        }

        #endregion ENDOF: Take
    }
}
