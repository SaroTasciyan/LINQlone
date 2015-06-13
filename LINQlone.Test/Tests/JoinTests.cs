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
    public class JoinTests : BaseTest
    {
        #region Join

        [Test]
        public void JoinDeferredExecution()
        {
            Data<object> first = DummyData;
            Data<object> second = DummyData;

            first.Join(second, x => x, x => x, (x, y) => x);
            first.AssertDeferredExecution();
            second.AssertDeferredExecution();
        }

        [Test]
        public void JoinSecondEnumeratedBeforeFirst()
        {
            Data<object> first = DummyData;
            Data<object> second = ThrowingData();

            Assert.Throws<ArgumentNullException>(() => first.Join(second, x => x, x => x, (x, y) => x).Iterate()); // # Second should be buffered before First is enumerated
            first.AssertDeferredExecution();
        }

        [Test]
        public void JoinFirstStreamingExecution()
        {
            LateThrowingData<object> first = LateThrowingData();
            Data<object> second = Data<object>("Late", "Throwing");

            IEnumerable<object> enumerable = first.Join(second, x => x, x => x, (x, y) => x);

            Assert.DoesNotThrow(() => enumerable.MoveNext()); // # LateThrownException was not thrown since sequence was not fully enumerated
        }

        [Test]
        public void JoinSecondNonStreamingExecution()
        {
            Data<object> first = DummyData;
            LateThrowingData<object> second = LateThrowingData();

            IEnumerable<object> enumerable = first.Join(second, x => x, x => x, (x, y) => x);

            Assert.Throws<LateThrownException>(() => enumerable.MoveNext()); // # Sequence was fully buffered, throwing LateThrownException after enumeration ended
        }

        [Test]
        public void JoinEmptySource()
        {
            EmptyData.Join(EmptyData, x => x, x => x, (x, y) => x).AssertEmpty();
        }

        [Test]
        public void JoinNonEmptySource()
        {
            Data(1, 2, 3).Join(Data(4, 5, 6), x => x % 2 == 0, x => x % 2 == 0, (x, y) => String.Format("{0}:{1}", x, y)).AssertEqual("1:5", "2:4", "2:6", "3:5");
        }

        [Test]
        public void JoinNoMatchingInOuter()
        {
            Data(1, 2).Join(Data(3, 4, 5), x => x % 3, x => x % 3, (x, y) => String.Format("{0}:{1}", x, y)).AssertEqual("1:4", "2:5");
        }

        [Test]
        public void JoinNoMatchingInInner()
        {
            Data(1, 2, 3).Join(Data(4, 5), x => x % 3, x => x % 3, (x, y) => String.Format("{0}:{1}", x, y)).AssertEqual("1:4", "2:5");
        }

        [Test]
        public void JoinNullKey()
        {
            Data(null, "a", "b").Join(Data("b", "c", null), x => x, x => x, (x, y) => String.Format("{0}:{1}", x, y)).AssertEqual("b:b");
        }

        [Test]
        public void JoinNoMatching()
        {
            Data("a", "b").Join(Data("c", "d"), x => x, x => x, (x, y) => String.Format("{0}:{1}", x, y)).AssertEmpty();
        }

        [Test]
        public void JoinQueryReuse()
        {
            List<int> first = new List<int> { 1, 2 };
            List<int> second = new List<int> { 4, 5 };
            IEnumerable<string> enumerable = first.Join(second, x => x % 2 == 0, x => x % 2 == 0, (x, y) => String.Format("{0}:{1}", x, y));

            enumerable.AssertEqual("1:5", "2:4");

            first.Add(3);
            second.Add(6);
            enumerable.AssertEqual("1:5", "2:4", "2:6", "3:5");
        }

        [Test]
        public void JoinOuterArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => NullData.Join(DummyData, x => x, x => x, (x, y) => x)).WithParameter("outer");
        }

        [Test]
        public void JoinInnerArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => DummyData.Join(NullData, x => x, x => x, (x, y) => x)).WithParameter("inner");
        }

        [Test]
        public void JoinOuterKeySelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => DummyData.Join(DummyData, null, x => x, (x, y) => x)).WithParameter("outerKeySelector");
        }

        [Test]
        public void JoinInnerKeySelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => DummyData.Join(DummyData, x => x, null, (x, y) => x)).WithParameter("innerKeySelector");
        }

        [Test]
        public void JoinResultSelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => DummyData.Join(DummyData, x => x, x => x, (Func<object, object, object>)null)).WithParameter("resultSelector");
        }

        #endregion ENDOF: Join

        #region Join With Comparer

        [Test]
        public void JoinWithComparerDeferredExecution()
        {
            Data<object> first = DummyData;
            Data<object> second = DummyData;

            first.Join(second, x => x, x => x, (x, y) => x, EqualityComparer<object>.Default);
            first.AssertDeferredExecution();
            second.AssertDeferredExecution();
        }

        [Test]
        public void JoinWithComparerSecondEnumeratedBeforeFirst()
        {
            Data<object> first = DummyData;
            Data<object> second = ThrowingData();

            Assert.Throws<ArgumentNullException>(() => first.Join(second, x => x, x => x, (x, y) => x, EqualityComparer<object>.Default).Iterate()); // # Second should be buffered before First is enumerated
            first.AssertDeferredExecution();
        }

        [Test]
        public void JoinWithComparerFirstStreamingExecution()
        {
            LateThrowingData<object> first = LateThrowingData();
            Data<object> second = Data<object>("Late", "Throwing");

            IEnumerable<object> enumerable = first.Join(second, x => x, x => x, (x, y) => x, EqualityComparer<object>.Default);

            Assert.DoesNotThrow(() => enumerable.MoveNext()); // # LateThrownException was not thrown since sequence was not fully enumerated
        }

        [Test]
        public void JoinWithComparerSecondNonStreamingExecution()
        {
            Data<object> first = DummyData;
            LateThrowingData<object> second = LateThrowingData();

            IEnumerable<object> enumerable = first.Join(second, x => x, x => x, (x, y) => x, EqualityComparer<object>.Default);

            Assert.Throws<LateThrownException>(() => enumerable.MoveNext()); // # Sequence was fully buffered, throwing LateThrownException after enumeration ended
        }

        [Test]
        public void JoinWithComparerEmptySource()
        {
            EmptyData.Join(EmptyData, x => x, x => x, (x, y) => x, EqualityComparer<object>.Default).AssertEmpty();
        }

        [Test]
        public void JoinWithComparerNonEmptySource()
        {
            Data(1, 2, 3).Join(Data(4, 5, 6), x => x % 2 == 0, x => x % 2 == 0, (x, y) => String.Format("{0}:{1}", x, y), EqualityComparer<bool>.Default).AssertEqual("1:5", "2:4", "2:6", "3:5");
        }

        [Test]
        public void JoinWithComparerNoMatchingInOuter()
        {
            Data(1, 2).Join(Data(3, 4, 5), x => x % 3, x => x % 3, (x, y) => String.Format("{0}:{1}", x, y), EqualityComparer<int>.Default).AssertEqual("1:4", "2:5");
        }

        [Test]
        public void JoinWithComparerNoMatchingInInner()
        {
            Data(1, 2, 3).Join(Data(4, 5), x => x % 3, x => x % 3, (x, y) => String.Format("{0}:{1}", x, y), EqualityComparer<int>.Default).AssertEqual("1:4", "2:5");
        }

        [Test]
        public void JoinWithComparerNullKey()
        {
            Data(null, "a", "b").Join(Data("b", "c", null), x => x, x => x, (x, y) => String.Format("{0}:{1}", x, y), EqualityComparer<string>.Default).AssertEqual("b:b");
        }

        [Test]
        public void JoinWithComparerNoMatching()
        {
            Data("a", "b").Join(Data("c", "d"), x => x, x => x, (x, y) => String.Format("{0}:{1}", x, y), EqualityComparer<string>.Default).AssertEmpty();
        }
        
        [Test]
        public void JoinWithComparerQueryReuse()
        {
            List<int> first = new List<int> { 1, 2 };
            List<int> second = new List<int> { 4, 5 };
            IEnumerable<string> enumerable = first.Join(second, x => x % 2 == 0, x => x % 2 == 0, (x, y) => String.Format("{0}:{1}", x, y), EqualityComparer<bool>.Default);

            enumerable.AssertEqual("1:5", "2:4");

            first.Add(3);
            second.Add(6);
            enumerable.AssertEqual("1:5", "2:4", "2:6", "3:5");
        }

        [Test]
        public void JoinWithComparerComparerArgumentNull()
        {
            Assert.DoesNotThrow(() => DummyData.Join(DummyData, x => x, x => x, (x, y) => x, null));
        }

        [Test]
        public void JoinWithComparerOuterArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => NullData.Join(DummyData, x => x, x => x, (x, y) => x, EqualityComparer<object>.Default)).WithParameter("outer");
        }

        [Test]
        public void JoinWithComparerInnerArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => DummyData.Join(NullData, x => x, x => x, (x, y) => x, EqualityComparer<object>.Default)).WithParameter("inner");
        }

        [Test]
        public void JoinWithComparerOuterKeySelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => DummyData.Join(DummyData, null, x => x, (x, y) => x, EqualityComparer<object>.Default)).WithParameter("outerKeySelector");
        }

        [Test]
        public void JoinWithComparerInnerKeySelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => DummyData.Join(DummyData, x => x, null, (x, y) => x, EqualityComparer<object>.Default)).WithParameter("innerKeySelector");
        }

        [Test]
        public void JoinWithComparerResultSelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => DummyData.Join(DummyData, x => x, x => x, (Func<object, object, object>)null, EqualityComparer<object>.Default)).WithParameter("resultSelector");
        }

        #endregion ENDOF: Join With Comperer
    }
}
