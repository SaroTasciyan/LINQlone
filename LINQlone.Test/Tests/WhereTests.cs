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
    public class WhereTests : BaseTest
    {
        #region Where

        [Test]
        public void WhereDeferredExecution()
        {
            Data<object> data = DummyData;

            data.Where(x => true);
            data.AssertDeferredExecution();
        }

        [Test]
        public void WhereStreamingExecution()
        {
            IEnumerable<object> enumerable = LateThrowingData().Where(x => true);

            Assert.DoesNotThrow(() => enumerable.MoveNext()); // # LateThrownException was not thrown since sequence was not fully enumerated
        }

        [Test]
        public void WhereEmptySource()
        {
            EmptyData.Where(x => true).AssertEmpty();
        }

        [Test]
        public void WhereFilter()
        {
            Data(1, 2, 3, 4, 5).Where(x => x % 2 == 0).AssertEqual(2, 4);
        }

        [Test]
        public void WhereQueryReuse()
        {
            List<int> data = new List<int> { 1, 2, 3 };
            IEnumerable<int> enumerable = data.Where(x => x % 2 == 0);

            enumerable.AssertEqual(2);

            data.Add(4);
            enumerable.AssertEqual(2, 4);
        }

        [Test]
        public void WhereSourceArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => NullData.Where(x => x != null)).WithParameter("source");
        }

        [Test]
        public void WherePredicateArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => DummyData.Where((Func<object, bool>)null)).WithParameter("predicate");
        }

        #endregion ENDOF: Where

        #region Where With Index

        [Test]
        public void WhereWithIndexDeferredExecution()
        {
            Data<object> data = DummyData;

            data.Where((x, i) => true);
            data.AssertDeferredExecution();
        }

        [Test]
        public void WhereWithIndexStreamingExecution()
        {
            IEnumerable<object> enumerable = LateThrowingData().Where((x, i) => true);

            Assert.DoesNotThrow(() => enumerable.MoveNext()); // # LateThrownException was not thrown since sequence was not fully enumerated
        }

        [Test]
        public void WhereWithIndexEmptySource()
        {
            EmptyData.Where((x, i) => true).AssertEmpty();
        }

        [Test]
        public void WhereWithIndexFilter()
        {
            Data(1, 2, 3, 4, 5).Where((x, i) => (x * i) % 4 == 0).AssertEqual(1, 4, 5);
        }

        [Test]
        public void WhereWithIndexQueryReuse()
        {
            List<int> data = new List<int> { 1, 2, 3 };
            IEnumerable<int> enumerable = data.Where((x, i) => x % 2 == 0);

            enumerable.AssertEqual(2);

            data.Add(4);
            enumerable.AssertEqual(2, 4);
        }

        [Test]
        [Explicit("WhereWithIndexOverflow takes very long to execute and slows down entire test process")]
        public void WheretWithIndexOverflow()
        {
            Assert.Throws<OverflowException>(() => IntOverflowEnumerable.Where((x, i) => false).Iterate());
        }

        [Test]
        public void WhereWithIndexSourceArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => NullData.Where((x, i) => true)).WithParameter("source");
        }

        [Test]
        public void WhereWithIndexPredicateArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => DummyData.Where((Func<object, int, bool>)null)).WithParameter("predicate");
        }

        #endregion ENDOF: Where With Index
    }
}
