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
    public class SkipWhileTests : BaseTest
    {
        #region SkipWhile

        [Test]
        public void SkipWhileDeferredExecution()
        {
            Data<object> data = DummyData;

            data.SkipWhile(x => false);
            data.AssertDeferredExecution();
        }

        [Test]
        public void SkipWhileStreamingExecution()
        {
            IEnumerable<object> enumerable = LateThrowingData().SkipWhile(x => false);

            Assert.DoesNotThrow(() => enumerable.MoveNext()); // # LateThrownException was not thrown since sequence was not fully enumerated
        }

        [Test]
        public void SkipWhileEmptySource()
        {
            EmptyData.SkipWhile(x => false).AssertEmpty();
        }

        [Test]
        public void SkipWhileEnumerationCount()
        {
            Data<int> data = Data(1, 2, 3, 4, 5);

            data.SkipWhile(x => x < 3).Iterate();
            Assert.That(data.EnumerationCount, Is.EqualTo(5 + 1)); // # Should iterate till the end, +1 Is for the last MoveNext() call which returns false
        }

        [Test]
        public void SkipWhileAll()
        {
            Data('A', 'B', 'C').SkipWhile(x => true).AssertEmpty();
        }

        [Test]
        public void SkipWhileCouple()
        {
            Data('A', 'B', 'C').SkipWhile(x => x < 'C').AssertEqual('C');
        }

        [Test]
        public void SkipWhileNone()
        {
            Data('A', 'B', 'C').SkipWhile(x => false).AssertEqual('A', 'B', 'C');
        }

        [Test]
        public void SkipWhileQueryReuse()
        {
            List<int> data = new List<int> { 1, 2, 3 };
            IEnumerable<int> enumerable = data.SkipWhile(x => x < 2);

            enumerable.AssertEqual(2, 3);

            data.Add(4);
            enumerable.AssertEqual(2, 3, 4);
        }

        [Test]
        public void SkipWhileSourceArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => NullData.SkipWhile(x => true)).WithParameter("source");
        }

        [Test]
        public void SkipWhileSourcePredicateArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => DummyData.SkipWhile((Func<object, bool>)null)).WithParameter("predicate");
        }

        #endregion ENDOF: SkipWhile

        #region SkipWhile With Index

        [Test]
        public void SkipWhileWithIndexDeferredExecution()
        {
            Data<object> data = DummyData;

            data.SkipWhile((x, i) => false);
            data.AssertDeferredExecution();
        }

        [Test]
        public void SkipWhileWithIndexStreamingExecution()
        {
            IEnumerable<object> enumerable = LateThrowingData().SkipWhile((x, i) => false);

            Assert.DoesNotThrow(() => enumerable.MoveNext()); // # LateThrownException was not thrown since sequence was not fully enumerated
        }

        [Test]
        public void SkipWhileWithIndexEmptySource()
        {
            EmptyData.SkipWhile((x, i) => false).AssertEmpty();
        }

        [Test]
        public void SkipWhileWithIndexEnumerationCount()
        {
            Data<int> data = Data(1, 2, 3, 4, 5);

            data.SkipWhile((x, i) => x < 3).Iterate();
            Assert.That(data.EnumerationCount, Is.EqualTo(5 + 1));
        }

        [Test]
        public void SkipWhileWithIndexAll()
        {
            Data('A', 'B', 'C').SkipWhile((x, i) => true).AssertEmpty();
        }

        [Test]
        public void SkipWhileWithIndexCouple()
        {
            Data(1, 2, 3).SkipWhile((x, i) => x >= i * 2).AssertEqual(3);
        }

        [Test]
        public void SkipWhileWithIndexNone()
        {
            Data('A', 'B', 'C').SkipWhile((x, i) => false).AssertEqual('A', 'B', 'C');
        }

        [Test]
        public void SkipWhileWithIndexQueryReuse()
        {
            List<int> data = new List<int> { 1, 2, 3 };
            IEnumerable<int> enumerable = data.SkipWhile((x, i) => x < 2);

            enumerable.AssertEqual(2, 3);

            data.Add(4);
            enumerable.AssertEqual(2, 3, 4);
        }

        [Test]
        [Explicit("SkipWhileWithIndexOverflow takes very long to execute and slows down entire test process")]
        public void SkipWhileWithIndexOverflow()
        {
            Assert.Throws<OverflowException>(() => IntOverflowEnumerable.SkipWhile((x, i) => true).Iterate());
        }

        [Test]
        public void SkipWhileWithIndexSourceArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => NullData.SkipWhile((x, i) => true)).WithParameter("source");
        }

        [Test]
        public void SkipWhileWithIndexSourcePredicateArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => DummyData.SkipWhile((Func<object, int, bool>)null)).WithParameter("predicate");
        }

        #endregion ENDOF: SkipWhile With Index
    }
}
