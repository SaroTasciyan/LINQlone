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
    public class DistinctTests : BaseTest
    {
        #region Distinct

        [Test]
        public void DistinctDeferredExecution()
        {
            Data<object> data = DummyData;

            data.Distinct();
            data.AssertDeferredExecution();
        }

        [Test]
        public void DistinctStreamingExecution()
        {
            IEnumerable<object> enumerable = LateThrowingData().Distinct();

            Assert.DoesNotThrow(() => enumerable.MoveNext()); // # LateThrownException was not thrown since sequence was not fully enumerated
        }

        [Test]
        public void DistinctEmptySource()
        {
            EmptyData.Distinct().AssertEmpty();
        }

        [Test]
        public void DistinctNonEmptySource()
        {
            Data(1, 2, 2, 3, 3, 3).Distinct().AssertEqual(1, 2, 3);
        }

        [Test]
        public void DistinctWithNullElementInSource()
        {
            Data(null, "a", "b", "c", null).Distinct().AssertEqual(null, "a", "b", "c");
        }

        [Test]
        public void DistinctQueryReuse()
        {
            List<int> data = new List<int> { 1, 2, 2 };
            IEnumerable<int> enumerable = data.Distinct();

            enumerable.AssertEqual(1, 2);

            data.Add(3);
            enumerable.AssertEqual(1, 2, 3);
        }

        [Test]
        public void DistinctSourceArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => NullData.Distinct()).WithParameter("source");
        }

        #endregion ENDOF: Distinct

        #region Distinct With Comparer

        [Test]
        public void DistinctWithComparerDeferredExecution()
        {
            Data<object> data = DummyData;

            data.Distinct(EqualityComparer<object>.Default);
            data.AssertDeferredExecution();
        }

        [Test]
        public void DistinctWithComparerStreamingExecution()
        {
            IEnumerable<object> enumerable = LateThrowingData().Distinct(EqualityComparer<object>.Default);
            
            Assert.DoesNotThrow(() => enumerable.MoveNext()); // # LateThrownException was not thrown since sequence was not fully enumerated
        }

        [Test]
        public void DistinctWithComparerEmptySource()
        {
            EmptyData.Distinct(EqualityComparer<object>.Default).AssertEmpty();
        }

        [Test]
        public void DistinctWithComparerNonEmptySource()
        {
            Data(1, 2, 2, 3, 3, 3).Distinct(EqualityComparer<int>.Default).AssertEqual(1, 2, 3);
        }

        [Test]
        public void DistinctWithComparerWithNullElementInSource()
        {
            Data(null, "a", "b", "c", null).Distinct(EqualityComparer<string>.Default).AssertEqual(null, "a", "b", "c");
        }

        [Test]
        public void DistinctWithComparerQueryReuse()
        {
            List<int> data = new List<int> { 1, 2, 2 };
            IEnumerable<int> enumerable = data.Distinct(EqualityComparer<int>.Default);

            enumerable.AssertEqual(1, 2);

            data.Add(3);
            enumerable.AssertEqual(1, 2, 3);
        }

        [Test]
        public void DistinctWithComparerCallCounts()
        {
            Data<string> data = Data("a", "b", "b", "c", "c", "c");
            ThrowingStringEqualityComparer comparer = new ThrowingStringEqualityComparer();

            data.Distinct(comparer).Iterate();

            Assert.That(comparer.GetHashCodeCallCount, Is.EqualTo(6)); // # Count
            Assert.That(comparer.EqualsCallCount, Is.EqualTo(3)); // # Count of repetitions (Count - DistinctCount)
        }

        [Test]
        public void DistinctWithComparerEqualsIsCalledForRepeatingNullValue()
        {
            Data<string> data = Data<string>(null, null);
            ThrowingStringEqualityComparer comparer = new ThrowingStringEqualityComparer();

            Assert.Throws<NullReferenceException>(() => data.Distinct(comparer).Iterate());
        }

        [Test]
        public void DistinctWithComparerComparerArgumentNull()
        {
            Assert.DoesNotThrow(() => DummyData.Distinct(null));
        }

        [Test]
        public void DistinctWithComparerSourceArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => NullData.Distinct(EqualityComparer<object>.Default)).WithParameter("source");
        }

        #endregion ENDOF: Distinct With Comparer
    }
}
