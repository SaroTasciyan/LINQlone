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
    public class SelectManyTests : BaseTest
    {
        #region SelectMany

        [Test]
        public void SelectManyDeferredExecution()
        {
            Data<Data<int>> data = Data(Data(1, 2, 3), Data(4, 5, 6));

            data.SelectMany(x => x);
            data.AssertDeferredExecution();
        }

        [Test]
        public void SelectManyStreamingExecution()
        {
            IEnumerable<object> enumerable = LateThrowingData().SelectMany(x => new object[] { 0 });

            Assert.DoesNotThrow(() => enumerable.MoveNext()); // # LateThrownException was not thrown since sequence was not fully enumerated
        }

        [Test]
        public void SelectManyEmptySource()
        {
            Data(EmptyData).SelectMany(x => x).AssertEmpty();
        }

        [Test]
        public void SelectManyProjection()
        {
            Data<Data<int>> data = Data(Data(1, 2, 3), Data(4, 5, 6));

            data.SelectMany(x => x).AssertEqual(1, 2, 3, 4, 5, 6);
        }

        [Test]
        public void SelectManyQueryReuse()
        {
            List<List<int>> data = new List<List<int>> { new List<int> { 1, 2 }, new List<int> { 3, 4 } };
            IEnumerable<int> enumerable = data.SelectMany(x => x);

            enumerable.AssertEqual(1, 2, 3, 4);

            data.Add(new List<int> { 5, 6 });
            enumerable.AssertEqual(1, 2, 3, 4, 5, 6);
        }

        [Test]
        public void SelectManySourceArgumentNull()
        {
            Data<Data<object>> data = null;

            Assert.Throws<ArgumentNullException>(() => data.SelectMany(x => x)).WithParameter("source");
        }

        [Test]
        public void SelectManySelectorArgumentNull()
        {
            Data<Data<object>> data = Data(EmptyData);

            Assert.Throws<ArgumentNullException>(() => data.SelectMany((Func<Data<object>, IEnumerable<object>>)null)).WithParameter("selector");
        }

        #endregion ENDOF: SelectMany

        #region SelectMany With Index

        [Test]
        public void SelectManyWithIndexDeferredExecution()
        {
            Data<Data<int>> data = Data(Data(1, 2, 3), Data(4, 5, 6));

            data.SelectMany((x, i) => x);
            data.AssertDeferredExecution();
        }

        [Test]
        public void SelectManyWithIndexStreamingExecution()
        {
            IEnumerable<object> enumerable = LateThrowingData().SelectMany((x, i) => new object[] { 0 });

            Assert.DoesNotThrow(() => enumerable.MoveNext()); // # LateThrownException was not thrown since sequence was not fully enumerated
        }

        [Test]
        public void SelectManyWithIndexEmptySource()
        {
            Data(EmptyData).SelectMany((x, i) => x).AssertEmpty();
        }

        [Test]
        public void SelectManyWithIndexProjection()
        {
            Data<Data<int>> data = Data(Data(1, 2, 3), Data(4, 5, 6));

            data.SelectMany((x, i) => new int[] { i }).AssertEqual(0, 1);
        }

        [Test]
        public void SelectManyWithIndexQueryReuse()
        {
            List<List<int>> data = new List<List<int>> { new List<int> { 1, 2 }, new List<int> { 3, 4 } };
            IEnumerable<int> enumerable = data.SelectMany((x, i) => x);

            enumerable.AssertEqual(1, 2, 3, 4);

            data.Add(new List<int> { 5, 6 });
            enumerable.AssertEqual(1, 2, 3, 4, 5, 6);
        }

        [Test]
        [Explicit("SelectManyWhileWithIndexOverflow takes very long to execute and slows down entire test process")]
        public void SelectManyWithIndexOverflow()
        {
            Assert.Throws<OverflowException>(() => IntOverflowEnumerable.SelectMany((x, i) => new int[] { }).Iterate());
        }

        [Test]
        public void SelectManyWithIndexSourceArgumentNull()
        {
            Data<Data<object>> data = null;

            Assert.Throws<ArgumentNullException>(() => data.SelectMany((x, i) => x)).WithParameter("source");
        }

        [Test]
        public void SelectManyWithIndexSelectorArgumentNull()
        {
            Data<Data<object>> data = Data(EmptyData);

            Assert.Throws<ArgumentNullException>(() => data.SelectMany((Func<Data<object>, int, IEnumerable<object>>)null)).WithParameter("selector");
        }

        #endregion ENDOF: SelectMany With Index

        #region SelectMany With Result Selector

        [Test]
        public void SelectManyWithResultSelectorDeferredExecution()
        {
            Data<Data<int>> data = Data(Data(1, 2, 3), Data(4, 5, 6));

            data.SelectMany(x => x, (s, c) => s);
            data.AssertDeferredExecution();
        }

        [Test]
        public void SelectManyWithResultSelectorStreamingExecution()
        {
            IEnumerable<object> enumerable = LateThrowingData().SelectMany(x => new object[] { 0 }, (s, c) => s);

            Assert.DoesNotThrow(() => enumerable.MoveNext()); // # LateThrownException was not thrown since sequence was not fully enumerated
        }

        [Test]
        public void SelectManyWithResultSelectorEmptySource()
        {
            Data(EmptyData).SelectMany(x => x, (s, c) => s).AssertEmpty();
        }

        [Test]
        public void SelectManyWithResultSelectorProjection()
        {
            Data<Data<int>> data = Data(Data(1, 2, 3), Data(4, 5, 6));

            data.SelectMany(x => x, (s, c) => c * c).AssertEqual(1, 4, 9, 16, 25, 36);
        }

        [Test]
        public void SelectManyWithResultSelectorQueryReuse()
        {
            List<List<int>> data = new List<List<int>> { new List<int> { 1, 2 }, new List<int> { 3, 4 } };
            IEnumerable<int> enumerable = data.SelectMany(x => x, (s, c) => c * c);

            enumerable.AssertEqual(1, 4, 9, 16);

            data.Add(new List<int> { 5, 6 });
            enumerable.AssertEqual(1, 4, 9, 16, 25, 36);
        }

        [Test]
        public void SelectManyWithResultSelectorSourceArgumentNull()
        {
            Data<Data<object>> data = null;

            Assert.Throws<ArgumentNullException>(() => data.SelectMany(x => x, (s, c) => s)).WithParameter("source");
        }

        [Test]
        public void SelectManyWithResultSelectorCollectionSelectorArgumentNull()
        {
            Data<Data<object>> data = Data(EmptyData);

            Assert.Throws<ArgumentNullException>(() => data.SelectMany((Func<Data<object>, IEnumerable<object>>)null, (s, c) => s)).WithParameter("collectionSelector");
        }

        [Test]
        public void SelectManyWithResultSelectorResultSelectorArgumentNull()
        {
            Data<Data<object>> data = Data(EmptyData);

            Assert.Throws<ArgumentNullException>(() => data.SelectMany(x => x, (Func<Data<object>, object, IEnumerable<object>>)null)).WithParameter("resultSelector");
        }

        #endregion ENDOF: SelectMany With Result Selector

        #region SelectMany With Index & Result Selector

        [Test]
        public void SelectManyWithIndexAndResultSelectorDeferredExecution()
        {
            Data<Data<int>> data = Data(Data(1, 2, 3), Data(4, 5, 6));

            data.SelectMany((x, i) => x, (s, c) => s);
            data.AssertDeferredExecution();
        }

        [Test]
        public void SelectManyWithIndexAndResultSelectorStreamingExecution()
        {
            IEnumerable<object> enumerable = LateThrowingData().SelectMany((x, i) => new object[] { 0 }, (s, c) => s);

            Assert.DoesNotThrow(() => enumerable.MoveNext()); // # LateThrownException was not thrown since sequence was not fully enumerated
        }

        [Test]
        public void SelectManyWithIndexAndResultSelectorEmptySource()
        {
            Data(EmptyData).SelectMany((x, i) => x, (s, c) => s).AssertEmpty();
        }

        [Test]
        public void SelectManyWithIndexAndResultSelectorProjection()
        {
            Data<Data<int>> data = Data(Data(1, 2, 3), Data(4, 5, 6));

            data.SelectMany((x, i) => new int[] { i }, (s, c) => c + c).AssertEqual(0, 2);
        }

        [Test]
        public void SelectManyWithIndexAndResultSelectorQueryReuse()
        {
            List<List<int>> data = new List<List<int>> { new List<int> { 1, 2 }, new List<int> { 3, 4 } };
            IEnumerable<int> enumerable = data.SelectMany((x, i) => x, (s, c) => c * c);

            enumerable.AssertEqual(1, 4, 9, 16);

            data.Add(new List<int> { 5, 6 });
            enumerable.AssertEqual(1, 4, 9, 16, 25, 36);
        }

        [Test]
        [Explicit("SelectManyWithIndexAndResultSelectorOverflow takes very long to execute and slows down entire test process")]
        public void SelectManyWithIndexAndResultSelectorOverflow()
        {
            Assert.Throws<OverflowException>(() => IntOverflowEnumerable.SelectMany((x, i) => new int[] { }, (s, c) => c).Iterate());
        }

        [Test]
        public void SelectManyWithIndexAndResultSelectorSourceArgumentNull()
        {
            Data<Data<object>> data = null;

            Assert.Throws<ArgumentNullException>(() => data.SelectMany((x, i) => x, (s, c) => s)).WithParameter("source");
        }

        [Test]
        public void SelectManyWithIndexAndResultSelectorCollectionSelectorArgumentNull()
        {
            Data<Data<object>> data = Data(EmptyData);

            Assert.Throws<ArgumentNullException>(() => data.SelectMany((Func<Data<object>, int, IEnumerable<object>>)null, (s, c) => s)).WithParameter("collectionSelector");
        }

        [Test]
        public void SelectManyWithIndexAndResultSelectorResultSelectorArgumentNull()
        {
            Data<Data<object>> data = Data(EmptyData);

            Assert.Throws<ArgumentNullException>(() => data.SelectMany((x, i) => x, (Func<Data<object>, object, IEnumerable<object>>)null)).WithParameter("resultSelector");
        }

        #endregion ENDOF: SelectMany With Index & Result Selector
    }
}
