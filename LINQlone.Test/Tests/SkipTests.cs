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
    public class SkipTests : BaseTest
    {
        #region Skip

        [Test]
        public void SkipDeferredExecution()
        {
            Data<object> data = DummyData;

            data.Skip(0);
            data.AssertDeferredExecution();
        }

        [Test]
        public void SkipStreamingExecution()
        {
            IEnumerable<object> enumerable = LateThrowingData().Skip(1);

            Assert.DoesNotThrow(() => enumerable.MoveNext()); // # LateThrownException was not thrown since sequence was not fully enumerated
        }

        [Test]
        public void SkipEmptySource()
        {
            EmptyData.Skip(0).AssertEmpty();
        }

        [Test]
        public void SkipEnumerationCount()
        {
            Data<int> data = Data(1, 2, 3, 4, 5);

            data.Skip(3).Iterate();
            Assert.That(data.EnumerationCount, Is.EqualTo(5 + 1)); // # Should iterate till the end, +1 Is for the last MoveNext() call which returns false
        }

        [Test]
        public void SkipAll()
        {
            Data('A', 'B', 'C').Skip(3).AssertEmpty();
        }

        [Test]
        public void SkipCouple()
        {
            Data('A', 'B', 'C').Skip(2).AssertEqual('C');
        }

        [Test]
        public void SkipZeroCount()
        {
            Data('A', 'B', 'C').Skip(0).AssertEqual('A', 'B', 'C');
        }

        [Test]
        public void SkipNegativeCount()
        {
            Data('A', 'B', 'C').Skip(-5).AssertEqual('A', 'B', 'C');
        }

        [Test]
        public void SkipOutOfBounds()
        {
            Data('A', 'B', 'C').Skip(5).AssertEmpty();
        }

        [Test]
        public void SkipQueryReuse()
        {
            List<int> data = new List<int> { 1, 2, 3 };
            IEnumerable<int> enumerable = data.Skip(1);

            enumerable.AssertEqual(2, 3);

            data.Add(4);
            enumerable.AssertEqual(2, 3, 4);
        }

        [Test]
        public void SkipSourceArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => NullData.Skip(0)).WithParameter("source");
        }

        #endregion ENDOF: Skip
    }
}
