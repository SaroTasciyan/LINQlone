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
    public class UnionTests : BaseTest
    {
        #region Union

        [Test]
        public void UnionDeferredExecution()
        {
            Data<object> first = DummyData;
            Data<object> second = DummyData;

            first.Union(second);
            first.AssertDeferredExecution();
            second.AssertDeferredExecution();
        }

        [Test]
        public void UnionStreamingExecution()
        {
            IEnumerable<object> enumerable = LateThrowingData().Union(LateThrowingData());

            Assert.DoesNotThrow(() => enumerable.MoveNext()); // # LateThrownException was not thrown since sequence was not fully enumerated
        }

        [Test]
        public void UnionEmptySource()
        {
            EmptyData.Union(EmptyData).AssertEmpty();
        }

        [Test]
        public void UnionNonEmptySource()
        {
            Data(1, 2, 2, 3, 4).Union(Data(3, 4, 5, 5, 6)).AssertEqual(1, 2, 3, 4, 5, 6);
        }

        [Test]
        public void UnionWithNullElementInSource()
        {
            Data(null, "a", "a", "b", "c").Union(Data("b", "c", "d", "d", null)).AssertEqual(null, "a", "b", "c", "d");
        }

        [Test]
        public void UnionQueryReuse()
        {
            List<int> first = new List<int> { 1, 2 };
            List<int> second = new List<int> { 2, 3 };
            IEnumerable<int> enumerable = first.Union(second);

            enumerable.AssertEqual(1, 2, 3);

            first.Insert(0, 0);
            second.Add(4);
            enumerable.AssertEqual(0, 1, 2, 3, 4);
        }

        [Test]
        public void UnionFirstArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => NullData.Union(DummyData)).WithParameter("first");
        }

        [Test]
        public void UnionSecondArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => DummyData.Union(NullData)).WithParameter("second");
        }

        #endregion ENDOF: Union

        #region Union With Comparer

        [Test]
        public void UnionWithComparerDeferredExecution()
        {
            Data<object> first = DummyData;
            Data<object> second = DummyData;

            first.Union(second, EqualityComparer<object>.Default);
            first.AssertDeferredExecution();
            second.AssertDeferredExecution();
        }

        [Test]
        public void UnionWithComparerStreamingExecution()
        {
            IEnumerable<object> enumerable = LateThrowingData().Union(LateThrowingData(), EqualityComparer<object>.Default);

            Assert.DoesNotThrow(() => enumerable.MoveNext()); // # LateThrownException was not thrown since sequence was not fully enumerated
        }

        [Test]
        public void UnionWithComparerEmptySource()
        {
            EmptyData.Union(EmptyData, EqualityComparer<object>.Default).AssertEmpty();
        }

        [Test]
        public void UnionWithComparerNonEmptySource()
        {
            Data(1, 2, 2, 3, 4).Union(Data(3, 4, 5, 5, 6), EqualityComparer<int>.Default).AssertEqual(1, 2, 3, 4, 5, 6);
        }

        [Test]
        public void UnionWithComparerWithNullElementInSource()
        {
            Data(null, "a", "a", "b", "c").Union(Data("b", "c", "d", "d", null), EqualityComparer<string>.Default).AssertEqual(null, "a", "b", "c", "d");
        }

        [Test]
        public void UnionWithComparerQueryReuse()
        {
            List<int> first = new List<int> { 1, 2 };
            List<int> second = new List<int> { 2, 3 };
            IEnumerable<int> enumerable = first.Union(second, EqualityComparer<int>.Default);

            enumerable.AssertEqual(1, 2, 3);

            first.Insert(0, 0);
            second.Add(4);
            enumerable.AssertEqual(0, 1, 2, 3, 4);
        }

        [Test]
        public void UnionWithComparerCallCounts()
        {
            Data<string> first = Data("a", "a", "b", "c");
            Data<string> second = Data("b", "c", "d", "d");
            ThrowingStringEqualityComparer comparer = new ThrowingStringEqualityComparer();

            first.Union(second, comparer).Iterate();

            Assert.That(comparer.GetHashCodeCallCount, Is.EqualTo(8)); // # Count
            Assert.That(comparer.EqualsCallCount, Is.EqualTo(4)); // # Count of repetitions (Count - DistinctCount)
        }

        [Test]
        public void UnionWithComparerEqualsIsCalledForRepeatingNullValue()
        {
            Data<string> first = Data(null, "a", "b");
            Data<string> second = Data("b", "c", null);
            ThrowingStringEqualityComparer comparer = new ThrowingStringEqualityComparer();

            Assert.Throws<NullReferenceException>(() => first.Union(second, comparer).Iterate());
        }

        [Test]
        public void UnionWithComparerComparerArgumentNull()
        {
            Assert.DoesNotThrow(() => DummyData.Union(DummyData, null));
        }

        [Test]
        public void UnionWithComparerFirstArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => NullData.Union(DummyData, EqualityComparer<object>.Default)).WithParameter("first");
        }

        [Test]
        public void UnionWithComparerSecondArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => DummyData.Union(NullData, EqualityComparer<object>.Default)).WithParameter("second");
        }

        #endregion ENDOF: Union With Comparer
    }
}
