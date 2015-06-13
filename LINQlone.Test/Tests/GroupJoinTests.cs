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
    public class GroupJoinTests : BaseTest
    {
        #region GroupJoin

        [Test]
        public void GroupJoinDeferredExecution()
        {
            Data<object> first = DummyData;
            Data<object> second = DummyData;

            first.GroupJoin(second, x => x, x => x, (x, y) => x);
            first.AssertDeferredExecution();
            second.AssertDeferredExecution();
        }

        [Test]
        public void GroupJoinSecondEnumeratedBeforeFirst()
        {
            Data<object> first = DummyData;
            Data<object> second = ThrowingData();

            Assert.Throws<ArgumentNullException>(() => first.GroupJoin(second, x => x, x => x, (x, y) => x).Iterate()); // # Second should be buffered before First is enumerated
            first.AssertDeferredExecution();
        }

        [Test]
        public void GroupJoinFirstStreamingExecution()
        {
            LateThrowingData<object> first = LateThrowingData();
            Data<object> second = DummyData;

            IEnumerable<object> enumerable = first.GroupJoin(second, x => x, x => x, (x, y) => x);

            Assert.DoesNotThrow(() => enumerable.MoveNext()); // # LateThrownException was not thrown since sequence was not fully enumerated
        }

        [Test]
        public void GroupJoinSecondNonStreamingExecution()
        {
            Data<object> first = DummyData;
            LateThrowingData<object> second = LateThrowingData();

            IEnumerable<object> enumerable = first.GroupJoin(second, x => x, x => x, (x, y) => x);

            Assert.Throws<LateThrownException>(() => enumerable.MoveNext()); // # Sequence was fully buffered, throwing LateThrownException after enumeration ended
        }

        [Test]
        public void GroupJoinEmptySource()
        {
            EmptyData.GroupJoin(EmptyData, x => x, x => x, (x, y) => x).AssertEmpty();
        }

        [Test]
        public void GroupJoinNonEmptySource()
        {
            Data(1, 2, 3).GroupJoin(Data(4, 5, 6), x => x % 2 == 0, x => x % 2 == 0, (x, y) => StringHelper.Join(x, y)).AssertEqual("1,5", "2,4,6", "3,5");
        }

        [Test]
        public void GroupJoinNoMatchingInOuter()
        {
            Data(1, 2).GroupJoin(Data(3, 4, 5), x => x % 3, x => x % 3, (x, y) => StringHelper.Join(x, y)).AssertEqual("1,4", "2,5");
        }

        [Test]
        public void GroupJoinNoMatchingInInner()
        {
            Data(1, 2, 3).GroupJoin(Data(4, 5), x => x % 3, x => x % 3, (x, y) => StringHelper.Join(x, y)).AssertEqual("1,4", "2,5", "3");
        }

        [Test]
        public void GroupJoinNullKey()
        {
            Data(null, "a", "b").GroupJoin(Data("b", "c", null), x => x, x => x, (x, y) => StringHelper.Join(x, y)).AssertEqual("", "a", "b,b");
        }

        [Test]
        public void GroupJoinNoMatching()
        {
            Data("a", "b").GroupJoin(Data("c", "d"), x => x, x => x, (x, y) => StringHelper.Join(x, y)).AssertEqual("a", "b");
        }

        [Test]
        public void GroupJoinQueryReuse()
        {
            List<int> first = new List<int> { 1, 2 };
            List<int> second = new List<int> { 4, 5 };
            IEnumerable<string> enumerable = first.GroupJoin(second, x => x % 2 == 0, x => x % 2 == 0, (x, y) => StringHelper.Join(x, y));

            enumerable.AssertEqual("1,5", "2,4");

            first.Add(3);
            second.Add(6);
            enumerable.AssertEqual("1,5", "2,4,6", "3,5");
        }

        [Test]
        public void GroupJoinOuterArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => NullData.GroupJoin(DummyData, x => x, x => x, (x, y) => x)).WithParameter("outer");
        }

        [Test]
        public void GroupJoinInnerArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => DummyData.GroupJoin(NullData, x => x, x => x, (x, y) => x)).WithParameter("inner");
        }

        [Test]
        public void GroupJoinOuterKeySelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => DummyData.GroupJoin(DummyData, null, x => x, (x, y) => x)).WithParameter("outerKeySelector");
        }

        [Test]
        public void GroupJoinInnerKeySelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => DummyData.GroupJoin(DummyData, x => x, null, (x, y) => x)).WithParameter("innerKeySelector");
        }

        [Test]
        public void GroupJoinResultSelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => DummyData.GroupJoin(DummyData, x => x, x => x, (Func<object, IEnumerable<object>, object>)null)).WithParameter("resultSelector");
        }

        #endregion ENDOF: GroupJoin

        #region GroupJoin With Comparer

        [Test]
        public void GroupJoinWithComparerDeferredExecution()
        {
            Data<object> first = DummyData;
            Data<object> second = DummyData;

            first.GroupJoin(second, x => x, x => x, (x, y) => x, EqualityComparer<object>.Default);
            first.AssertDeferredExecution();
            second.AssertDeferredExecution();
        }

        [Test]
        public void GroupJoinWithComparerSecondEnumeratedBeforeFirst()
        {
            Data<object> first = DummyData;
            Data<object> second = ThrowingData();

            Assert.Throws<ArgumentNullException>(() => first.GroupJoin(second, x => x, x => x, (x, y) => x, EqualityComparer<object>.Default).Iterate()); // # Second should be buffered before First is enumerated
            first.AssertDeferredExecution();
        }

        [Test]
        public void GroupJoinWithComparerFirstStreamingExecution()
        {
            LateThrowingData<object> first = LateThrowingData();
            Data<object> second = DummyData;

            IEnumerable<object> enumerable = first.GroupJoin(second, x => x, x => x, (x, y) => x, EqualityComparer<object>.Default);

            Assert.DoesNotThrow(() => enumerable.MoveNext()); // # LateThrownException was not thrown since sequence was not fully enumerated
        }

        [Test]
        public void GroupJoinWithComparerSecondNonStreamingExecution()
        {
            Data<object> first = DummyData;
            LateThrowingData<object> second = LateThrowingData();

            IEnumerable<object> enumerable = first.GroupJoin(second, x => x, x => x, (x, y) => x, EqualityComparer<object>.Default);

            Assert.Throws<LateThrownException>(() => enumerable.MoveNext()); // # Sequence was fully buffered, throwing LateThrownException after enumeration ended
        }

        [Test]
        public void GroupJoinWithComparerEmptySource()
        {
            EmptyData.GroupJoin(EmptyData, x => x, x => x, (x, y) => x, EqualityComparer<object>.Default).AssertEmpty();
        }

        [Test]
        public void GroupJoinWithComparerNonEmptySource()
        {
            Data(1, 2, 3).GroupJoin(Data(4, 5, 6), x => x % 2 == 0, x => x % 2 == 0, (x, y) => StringHelper.Join(x, y), EqualityComparer<bool>.Default).AssertEqual("1,5", "2,4,6", "3,5");
        }

        [Test]
        public void GroupJoinWithComparerNoMatchingInOuter()
        {
            Data(1, 2).GroupJoin(Data(3, 4, 5), x => x % 3, x => x % 3, (x, y) => StringHelper.Join(x, y), EqualityComparer<int>.Default).AssertEqual("1,4", "2,5");
        }

        [Test]
        public void GroupJoinWithComparerNoMatchingInInner()
        {
            Data(1, 2, 3).GroupJoin(Data(4, 5), x => x % 3, x => x % 3, (x, y) => StringHelper.Join(x, y), EqualityComparer<int>.Default).AssertEqual("1,4", "2,5", "3");
        }

        [Test]
        public void GroupJoinWithComparerNullKey()
        {
            Data(null, "a", "b").GroupJoin(Data("b", "c", null), x => x, x => x, (x, y) => StringHelper.Join(x, y), EqualityComparer<string>.Default).AssertEqual("", "a", "b,b");
        }

        [Test]
        public void GroupJoinWithComparerNoMatching()
        {
            Data("a", "b").GroupJoin(Data("c", "d"), x => x, x => x, (x, y) => StringHelper.Join(x, y), EqualityComparer<string>.Default).AssertEqual("a", "b");
        }

        [Test]
        public void GroupJoinWithComparerQueryReuse()
        {
            List<int> first = new List<int> { 1, 2 };
            List<int> second = new List<int> { 4, 5 };
            IEnumerable<string> enumerable = first.GroupJoin(second, x => x % 2 == 0, x => x % 2 == 0, (x, y) => StringHelper.Join(x, y), EqualityComparer<bool>.Default);

            enumerable.AssertEqual("1,5", "2,4");

            first.Add(3);
            second.Add(6);
            enumerable.AssertEqual("1,5", "2,4,6", "3,5");
        }

        [Test]
        public void GroupJoinWithComparerComparerArgumentNull()
        {
            Assert.DoesNotThrow(() => DummyData.GroupJoin(DummyData, x => x, x => x, (x, y) => x, null));
        }

        [Test]
        public void GroupJoinWithComparerOuterArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => NullData.GroupJoin(DummyData, x => x, x => x, (x, y) => x, EqualityComparer<object>.Default)).WithParameter("outer");
        }

        [Test]
        public void GroupJoinWithComparerInnerArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => DummyData.GroupJoin(NullData, x => x, x => x, (x, y) => x, EqualityComparer<object>.Default)).WithParameter("inner");
        }

        [Test]
        public void GroupJoinWithComparerOuterKeySelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => DummyData.GroupJoin(DummyData, null, x => x, (x, y) => x, EqualityComparer<object>.Default)).WithParameter("outerKeySelector");
        }

        [Test]
        public void GroupJoinWithComparerInnerKeySelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => DummyData.GroupJoin(DummyData, x => x, null, (x, y) => x, EqualityComparer<object>.Default)).WithParameter("innerKeySelector");
        }

        [Test]
        public void GroupJoinWithComparerResultSelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => DummyData.GroupJoin(DummyData, x => x, x => x, (Func<object, IEnumerable<object>, object>)null, EqualityComparer<object>.Default)).WithParameter("resultSelector");
        }

        #endregion ENDOF: GroupJoin With Comperer
    }
}
