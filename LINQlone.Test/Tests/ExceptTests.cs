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
    public class ExceptTests : BaseTest
    {
        #region Except

        [Test]
        public void ExceptDeferredExecution()
        {
            Data<object> first = DummyData;
            Data<object> second = DummyData;

            first.Except(second);
            first.AssertDeferredExecution();
            second.AssertDeferredExecution();
        }

        [Test]
        public void ExceptSecondEnumeratedBeforeFirst()
        {
            Data<object> first = DummyData;
            Data<object> second = ThrowingData();

            Assert.Throws<ArgumentNullException>(() => first.Except(second).Iterate()); // # Second should be buffered before First is enumerated
            first.AssertDeferredExecution();
        }

        [Test]
        public void ExceptFirstStreamingExecution()
        {
            LateThrowingData<object> first = LateThrowingData();
            Data<object> second = DummyData;

            IEnumerable<object> enumerable = first.Except(second);

            Assert.DoesNotThrow(() => enumerable.MoveNext()); // # LateThrownException was not thrown since sequence was not fully enumerated
        }

        [Test]
        public void ExceptSecondNonStreamingExecution()
        {
            Data<object> first = DummyData;
            LateThrowingData<object> second = LateThrowingData();

            IEnumerable<object> enumerable = first.Except(second);

            Assert.Throws<LateThrownException>(() => enumerable.MoveNext()); // # Sequence was fully buffered, throwing LateThrownException after enumeration ended
        }

        [Test]
        public void ExceptEmptySource()
        {
            EmptyData.Except(EmptyData).AssertEmpty();
        }

        [Test]
        public void ExceptNonEmptySource()
        {
            Data(1, 2, 2, 3, 4).Except(Data(3, 4, 5, 6)).AssertEqual(1, 2);
        }

        [Test]
        public void ExceptWithNullElementInSource()
        {
            Data(null, "a", "a", "b", "c").Except(Data("c", "d", "d", null)).AssertEqual("a", "b");
        }

        [Test]
        public void ExceptQueryReuse()
        {
            List<int> first = new List<int> { 1, 2, 3 };
            List<int> second = new List<int> { 3, 5, 6 };
            IEnumerable<int> enumerable = first.Except(second);

            enumerable.AssertEqual(1, 2);

            first.Add(4);
            second.Add(2);
            enumerable.AssertEqual(1, 4);
        }

        [Test]
        public void ExceptFirstArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => NullData.Except(DummyData)).WithParameter("first");
        }

        [Test]
        public void ExceptSecondArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => DummyData.Except(NullData)).WithParameter("second");
        }

        #endregion ENDOF: Except

        #region Except With Comparer

        [Test]
        public void ExceptWithComparerDeferredExecution()
        {
            Data<object> first = DummyData;
            Data<object> second = DummyData;

            first.Except(second, EqualityComparer<object>.Default);
            first.AssertDeferredExecution();
            second.AssertDeferredExecution();
        }

        [Test]
        public void ExceptWithComparerSecondEnumeratedBeforeFirst()
        {
            Data<object> first = DummyData;
            Data<object> second = ThrowingData();

            Assert.Throws<ArgumentNullException>(() => first.Except(second, EqualityComparer<object>.Default).Iterate()); // # Second should be buffered before First is enumerated
            first.AssertDeferredExecution();
        }

        [Test]
        public void ExceptWithComparerFirstStreamingExecution()
        {
            LateThrowingData<object> first = LateThrowingData();
            Data<object> second = DummyData;

            IEnumerable<object> enumerable = first.Except(second, EqualityComparer<object>.Default);

            Assert.DoesNotThrow(() => enumerable.MoveNext()); // # LateThrownException was not thrown since sequence was not fully enumerated
        }

        [Test]
        public void ExceptWithComparerSecondNonStreamingExecution()
        {
            Data<object> first = DummyData;
            LateThrowingData<object> second = LateThrowingData();

            IEnumerable<object> enumerable = first.Except(second, EqualityComparer<object>.Default);

            Assert.Throws<LateThrownException>(() => enumerable.MoveNext()); // # Sequence was fully buffered, throwing LateThrownException after enumeration ended
        }

        [Test]
        public void ExceptWithComparerEmptySource()
        {
            EmptyData.Except(EmptyData, EqualityComparer<object>.Default).AssertEmpty();
        }

        [Test]
        public void ExceptWithComparerNonEmptySource()
        {
            Data(1, 2, 2, 3, 4).Except(Data(3, 4, 5, 6), EqualityComparer<int>.Default).AssertEqual(1, 2);
        }

        [Test]
        public void ExceptWithComparerWithNullElementInSource()
        {
            Data(null, "a", "a", "b", "c").Except(Data("c", "d", "d", null), EqualityComparer<string>.Default).AssertEqual("a", "b");
        }

        [Test]
        public void ExceptWithComparerQueryReuse()
        {
            List<int> first = new List<int> { 1, 2, 3 };
            List<int> second = new List<int> { 3, 5, 6 };
            IEnumerable<int> enumerable = first.Except(second, EqualityComparer<int>.Default);

            enumerable.AssertEqual(1, 2);

            first.Add(4);
            second.Add(2);
            enumerable.AssertEqual(1, 4);
        }

        [Test]
        public void ExceptWithComparerCallCounts()
        {
            Data<string> first = Data("a", "a", "b", "c");
            Data<string> second = Data("b", "c", "d", "d");
            ThrowingStringEqualityComparer comparer = new ThrowingStringEqualityComparer();

            first.Except(second, comparer).Iterate();

            Assert.That(comparer.GetHashCodeCallCount, Is.EqualTo(8)); // # Count
            Assert.That(comparer.EqualsCallCount, Is.EqualTo(4)); // # Count of repetitions (Count - DistinctCount)
        }

        [Test]
        public void ExceptWithComparerEqualsIsCalledForRepeatingNullValue()
        {
            Data<string> first = Data<string>(null, "a", "b");
            Data<string> second = Data<string>("b", "c", null);
            ThrowingStringEqualityComparer comparer = new ThrowingStringEqualityComparer();

            Assert.Throws<NullReferenceException>(() => first.Except(second, comparer).Iterate());
        }

        [Test]
        public void ExceptWithComparerComparerArgumentNull()
        {
            Assert.DoesNotThrow(() => DummyData.Except(DummyData, null));
        }

        [Test]
        public void ExceptWithComparerFirstArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => NullData.Except(DummyData, EqualityComparer<object>.Default)).WithParameter("first");
        }

        [Test]
        public void ExceptWithComparerSecondArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => DummyData.Except(NullData, EqualityComparer<object>.Default)).WithParameter("second");
        }

        #endregion ENDOF: Except With Comparer
    }
}
