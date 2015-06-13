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
    public class IntersectTests : BaseTest
    {
        #region Intersect

        [Test]
        public void IntersectDeferredExecution()
        {
            Data<object> first = DummyData;
            Data<object> second = DummyData;

            first.Intersect(second);
            first.AssertDeferredExecution();
            second.AssertDeferredExecution();
        }

        [Test]
        public void IntersectSecondEnumeratedBeforeFirst()
        {
            Data<object> first = DummyData;
            Data<object> second = ThrowingData();

            Assert.Throws<ArgumentNullException>(() => first.Intersect(second).Iterate()); // # Second should be buffered before First is enumerated
            first.AssertDeferredExecution();
        }

        [Test]
        public void IntersectFirstStreamingExecution()
        {
            LateThrowingData<object> first = LateThrowingData();
            Data<object> second = Data<object>("Late", "Throwing");

            IEnumerable<object> enumerable = first.Intersect(second);

            Assert.DoesNotThrow(() => enumerable.MoveNext()); // # LateThrownException was not thrown since sequence was not fully enumerated
        }

        [Test]
        public void IntersectSecondNonStreamingExecution()
        {
            Data<object> first = DummyData;
            LateThrowingData<object> second = LateThrowingData();

            IEnumerable<object> enumerable = first.Intersect(second);

            Assert.Throws<LateThrownException>(() => enumerable.MoveNext()); // # Sequence was fully buffered, throwing LateThrownException after enumeration ended
        }

        [Test]
        public void IntersectEmptySource()
        {
            EmptyData.Intersect(EmptyData).AssertEmpty();
        }

        [Test]
        public void IntersectNonEmptySource()
        {
            Data(1, 2, 3, 3, 4).Intersect(Data(3, 4, 4, 5)).AssertEqual(3, 4);
        }

        [Test]
        public void IntersectWithNullElementInSource()
        {
            Data(null, "a", "a", "b", "c").Intersect(Data("c", "d", "d", null)).AssertEqual(null, "c");
        }

        [Test]
        public void IntersectQueryReuse()
        {
            List<int> first = new List<int> { 1, 2 };
            List<int> second = new List<int> { 2, 4 };
            IEnumerable<int> enumerable = first.Intersect(second);

            enumerable.AssertEqual(2);

            first.Add(3);
            second.Add(3);
            enumerable.AssertEqual(2, 3);
        }

        [Test]
        public void IntersectFirstArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => NullData.Intersect(DummyData)).WithParameter("first");
        }

        [Test]
        public void IntersectSecondArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => DummyData.Intersect(NullData)).WithParameter("second");
        }

        #endregion ENDOF: Intersect

        #region Intersect With Comparer

        [Test]
        public void IntersectWithComparerDeferredExecution()
        {
            Data<object> first = DummyData;
            Data<object> second = DummyData;

            first.Intersect(second, EqualityComparer<object>.Default);
            first.AssertDeferredExecution();
            second.AssertDeferredExecution();
        }

        [Test]
        public void IntersectWithComparerSecondEnumeratedBeforeFirst()
        {
            Data<object> first = DummyData;
            Data<object> second = ThrowingData();

            Assert.Throws<ArgumentNullException>(() => first.Intersect(second, EqualityComparer<object>.Default).Iterate()); // # Second should be buffered before First is enumerated
            first.AssertDeferredExecution();
        }

        [Test]
        public void IntersectWithComparerFirstStreamingExecution()
        {
            LateThrowingData<object> first = LateThrowingData();
            Data<object> second = Data<object>("Late", "Throwing");

            IEnumerable<object> enumerable = first.Intersect(second, EqualityComparer<object>.Default);

            Assert.DoesNotThrow(() => enumerable.MoveNext()); // # LateThrownException was not thrown since sequence was not fully enumerated
        }

        [Test]
        public void IntersectWithComparerSecondNonStreamingExecution()
        {
            Data<object> first = DummyData;
            LateThrowingData<object> second = LateThrowingData();

            IEnumerable<object> enumerable = first.Intersect(second, EqualityComparer<object>.Default);

            Assert.Throws<LateThrownException>(() => enumerable.MoveNext()); // # Sequence was fully buffered, throwing LateThrownException after enumeration ended
        }

        [Test]
        public void IntersectWithComparerEmptySource()
        {
            EmptyData.Intersect(EmptyData, EqualityComparer<object>.Default).AssertEmpty();
        }

        [Test]
        public void IntersectWithComparerNonEmptySource()
        {
            Data(1, 2, 3, 3, 4).Intersect(Data(3, 4, 4, 5), EqualityComparer<int>.Default).AssertEqual(3, 4);
        }

        [Test]
        public void IntersectWithComparerWithNullElementInSource()
        {
            Data(null, "a", "a", "b", "c").Intersect(Data("c", "d", "d", null), EqualityComparer<string>.Default).AssertEqual(null, "c");
        }

        [Test]
        public void IntersectWithComparerCallCounts()
        {
            Data<string> first = Data("a", "a", "b", "c");
            Data<string> second = Data("b", "c", "d", "d");
            ThrowingStringEqualityComparer comparer = new ThrowingStringEqualityComparer();

            first.Intersect(second, comparer).Iterate();

            Assert.That(comparer.GetHashCodeCallCount, Is.EqualTo(8)); // # Count
            Assert.That(comparer.EqualsCallCount, Is.EqualTo(3)); // # Count of repetitions in Second + Count of intersection
        }

        [Test]
        public void IntersectWithComparerEqualsIsCalledForRepeatingNullValue()
        {
            Data<string> first = Data(null, "a", "b");
            Data<string> second = Data("b", "c", null);
            ThrowingStringEqualityComparer comparer = new ThrowingStringEqualityComparer();

            Assert.Throws<NullReferenceException>(() => first.Intersect(second, comparer).Iterate());
        }

        [Test]
        public void IntersectWithComparerQueryReuse()
        {
            List<int> first = new List<int> { 1, 2 };
            List<int> second = new List<int> { 2, 4 };
            IEnumerable<int> enumerable = first.Intersect(second, EqualityComparer<int>.Default);

            enumerable.AssertEqual(2);

            first.Add(3);
            second.Add(3);
            enumerable.AssertEqual(2, 3);
        }

        [Test]
        public void IntersectWithComparerComparerArgumentNull()
        {
            Assert.DoesNotThrow(() => DummyData.Intersect(DummyData, null));
        }

        [Test]
        public void IntersectWithComparerFirstArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => NullData.Intersect(DummyData, EqualityComparer<object>.Default)).WithParameter("first");
        }

        [Test]
        public void IntersectWithComparerSecondArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => DummyData.Intersect(NullData, EqualityComparer<object>.Default)).WithParameter("second");
        }

        #endregion ENDOF: Intersect With Comparer
    }
}
