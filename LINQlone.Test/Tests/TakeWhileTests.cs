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
    public class TakeWhileTests : BaseTest
    {
        #region TakeWhile

        [Test]
        public void TakeWhileDeferredExecution()
        {
            Data<object> data = DummyData;

            data.TakeWhile(x => true);
            data.AssertDeferredExecution();
        }

        [Test]
        public void TakeWhileStreamingExecution()
        {
            IEnumerable<object> enumerable = LateThrowingData().TakeWhile(x => true);

            Assert.DoesNotThrow(() => enumerable.MoveNext()); // # LateThrownException was not thrown since sequence was not fully enumerated
        }

        [Test]
        public void TakeWhileEmptySource()
        {
            EmptyData.TakeWhile(x => true).AssertEmpty();
        }

        [Test]
        public void TakeWhileEnumerationCount()
        {
            Data<int> data = Data(1, 2, 3, 4, 5);

            data.TakeWhile(x => x < 3).Iterate();
            Assert.That(data.EnumerationCount, Is.EqualTo(3)); // # Should not iterate for the elements that were not checked against predicate
        }

        [Test]
        public void TakeWhileAll()
        {
            Data('A', 'B', 'C').TakeWhile(x => true).AssertEqual('A', 'B', 'C');
        }

        [Test]
        public void TakeWhileCouple()
        {
            Data('A', 'B', 'C').TakeWhile(x => x < 'C').AssertEqual('A', 'B');
        }

        [Test]
        public void TakeWhileNone()
        {
            Data('A', 'B', 'C').TakeWhile(x => false).AssertEmpty();
        }

        [Test]
        public void TakeWhileQueryReuse()
        {
            List<int> data = new List<int> { 1, 2, 3 };
            IEnumerable<int> enumerable = data.TakeWhile(x => x < 3);

            enumerable.AssertEqual(1, 2);

            data.Insert(0, 0);
            enumerable.AssertEqual(0, 1, 2);
        }

        [Test]
        public void TakeWhileSourceArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => NullData.TakeWhile(x => true)).WithParameter("source");
        }

        [Test]
        public void TakeWhileSourcePredicateArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => DummyData.TakeWhile((Func<object, bool>)null)).WithParameter("predicate");
        }

        #endregion ENDOF: TakeWhile

        #region TakeWhile With Index

        [Test]
        public void TakeWhileWithIndexDeferredExecution()
        {
            Data<object> data = DummyData;

            data.TakeWhile((x, i) => true);
            data.AssertDeferredExecution();
        }

        [Test]
        public void TakeWhileWithIndexStreamingExecution()
        {
            IEnumerable<object> enumerable = LateThrowingData().TakeWhile((x, i) => true);

            Assert.DoesNotThrow(() => enumerable.MoveNext()); // # LateThrownException was not thrown since sequence was not fully enumerated
        }

        [Test]
        public void TakeWhileWithIndexEmptySource()
        {
            EmptyData.TakeWhile((x, i) => true).AssertEmpty();
        }

        [Test]
        public void TakeWhileWithIndexEnumerationCount()
        {
            Data<int> data = Data(1, 2, 3, 4, 5);

            data.TakeWhile((x, i) => x < 3).Iterate();
            Assert.That(data.EnumerationCount, Is.EqualTo(3));
        }

        [Test]
        public void TakeWhileWithIndexAll()
        {
            Data('A', 'B', 'C').TakeWhile((x, i) => true).AssertEqual('A', 'B', 'C');
        }

        [Test]
        public void TakeWhileWithIndexCouple()
        {
            Data(1, 2, 3).TakeWhile((x, i) => x >= i * 2).AssertEqual(1, 2);
        }

        [Test]
        public void TakeWhileWithIndexNone()
        {
            Data('A', 'B', 'C').TakeWhile((x, i) => false).AssertEmpty();
        }

        [Test]
        public void TakeWhileWithIndexQueryReuse()
        {
            List<int> data = new List<int> { 1, 2, 3 };
            IEnumerable<int> enumerable = data.TakeWhile((x, i) => x < 3);

            enumerable.AssertEqual(1, 2);

            data.Insert(0, 0);
            enumerable.AssertEqual(0, 1, 2);
        }

        [Test]
        [Explicit("TakeWhileWithIndexOverflow takes very long to execute and slows down entire test process")]
        public void TakeWhileWithIndexOverflow()
        {
            Assert.Throws<OverflowException>(() => IntOverflowEnumerable.TakeWhile((x, i) => true).Iterate());
        }

        [Test]
        public void TakeWhileWithIndexSourceArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => NullData.TakeWhile((x, i) => true)).WithParameter("source");
        }

        [Test]
        public void TakeWhileWithIndexSourcePredicateArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => DummyData.TakeWhile((Func<object, int, bool>)null)).WithParameter("predicate");
        }

        #endregion ENDOF: TakeWhile With Index
    }
}
