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
    public class OrderByDescendingTests : BaseTest
    {
        #region OrderByDescending

        [Test]
        public void OrderByDescendingDeferredExecution()
        {
            Data<object> data = DummyData;

            data.OrderByDescending(x => x);
            data.AssertDeferredExecution();
        }

        [Test]
        public void OrderByDescendingNonStreamingExecution()
        {
            IEnumerable<object> enumerable = LateThrowingData().OrderByDescending(x => x);

            Assert.Throws<LateThrownException>(() => enumerable.MoveNext()); // # Sequence was fully buffered, throwing LateThrownException after enumeration ended
        }

        [Test]
        public void OrderByDescendingEmptySource()
        {
            EmptyData.OrderByDescending(x => x).AssertEmpty();
        }

        [Test]
        public void OrderByDescendingNonEmptySource()
        {
            Data<int> data = Data(1, 2, 3, 4, 5);

            IOrderedEnumerable<int> orderedData = data.OrderByDescending(x => x % 2);

            orderedData.AssertEqual(1, 3, 5, 2, 4); // # Odd numbers first with original order in source
        }

        [Test]
        public void OrderByDescendingNullKey()
        {
            Country england, france, china, india, unknown;
            Data<Country> data = Data
            (
                england = new Country() { Name = "England", Continent = "Europe" },
                france = new Country() { Name = "France", Continent = "Europe" },
                china = new Country() { Name = "China", Continent = "Asia" },
                india = new Country() { Name = "India", Continent = "Asia" },
                unknown = new Country() { Name = "Unknown", Continent = null }
            );

            IOrderedEnumerable<Country> orderedData = data.OrderByDescending(x => x.Continent);

            orderedData.AssertSame(england, france, china, india, unknown);
        }

        [Test]
        public void OrderByDescendingNullElementInSource()
        {
            Data<string> data = Data("b", "c", "a", null);

            IOrderedEnumerable<string> orderedData = data.OrderByDescending(x => x);

            orderedData.AssertEqual("c", "b", "a", null);
        }

        [Test]
        public void OrderByDescendingQueryReuse()
        {
            List<int> data = new List<int> { 1, 2, 3, 4, 5 };
            IOrderedEnumerable<int> enumerable = data.OrderByDescending(x => x % 2);

            enumerable.AssertEqual(1, 3, 5, 2, 4);

            data.Add(6);
            enumerable.AssertEqual(1, 3, 5, 2, 4, 6);
        }

        [Test]
        public void OrderByDescendingSourceArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => NullData.OrderByDescending(x => x)).WithParameter("source");
        }

        [Test]
        public void OrderByDescendingKeySelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => DummyData.OrderByDescending((Func<object, object>)null)).WithParameter("keySelector");
        }

        #endregion ENDOF: OrderByDescending

        #region OrderByDescending With Comparer

        [Test]
        public void OrderByDescendingWithComparerDeferredExecution()
        {
            Data<object> data = DummyData;

            data.OrderByDescending(x => x, Comparer<object>.Default);
            data.AssertDeferredExecution();
        }

        [Test]
        public void OrderByDescendingWithComparerNonStreamingExecution()
        {
            IEnumerable<object> enumerable = LateThrowingData().OrderByDescending(x => x, Comparer<object>.Default);

            Assert.Throws<LateThrownException>(() => enumerable.MoveNext()); // # Sequence was fully buffered, throwing LateThrownException after enumeration ended
        }

        [Test]
        public void OrderByDescendingWithComparerEmptySource()
        {
            EmptyData.OrderByDescending(x => x, Comparer<object>.Default).AssertEmpty();
        }

        [Test]
        public void OrderByDescendingWithComparerNonEmptySource()
        {
            Data<int> data = Data(1, 2, 3, 4, 5);

            IOrderedEnumerable<int> orderedData = data.OrderByDescending(x => x % 2, Comparer<int>.Default);

            orderedData.AssertEqual(1, 3, 5, 2, 4);
        }

        [Test]
        public void OrderByDescendingWithComparerNullKey()
        {
            Country england, france, china, india, unknown;
            Data<Country> data = Data
            (
                england = new Country() { Name = "England", Continent = "Europe" },
                france = new Country() { Name = "France", Continent = "Europe" },
                china = new Country() { Name = "China", Continent = "Asia" },
                india = new Country() { Name = "India", Continent = "Asia" },
                unknown = new Country() { Name = "Unknown", Continent = null }
            );

            IOrderedEnumerable<Country> orderedData = data.OrderByDescending(x => x.Continent, Comparer<string>.Default);

            orderedData.AssertSame(england, france, china, india, unknown);
        }

        [Test]
        public void OrderByDescendingWithComparerNullElementInSource()
        {
            Data<string> data = Data("b", "c", "a", null);

            IOrderedEnumerable<string> orderedData = data.OrderByDescending(x => x, Comparer<string>.Default);

            orderedData.AssertEqual("c", "b", "a", null);
        }

        [Test]
        public void OrderByDescendingWithComparerQueryReuse()
        {
            List<int> data = new List<int> { 1, 2, 3, 4, 5 };
            IOrderedEnumerable<int> enumerable = data.OrderByDescending(x => x % 2, Comparer<int>.Default);

            enumerable.AssertEqual(1, 3, 5, 2, 4);

            data.Add(6);
            enumerable.AssertEqual(1, 3, 5, 2, 4, 6);
        }

        [Test]
        public void OrderByDescendingWithComparerComparerArgumentNull()
        {
            Assert.DoesNotThrow(() => DummyData.OrderByDescending(x => x, null));
        }

        [Test]
        public void OrderByDescendingWithComparerSourceArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => NullData.OrderByDescending(x => x, Comparer<object>.Default)).WithParameter("source");
        }

        [Test]
        public void OrderByDescendingWithComparerKeySelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => DummyData.OrderByDescending((Func<object, object>)null, Comparer<object>.Default)).WithParameter("keySelector");
        }

        #endregion ENDOF: OrderByDescending With Comparer
    }
}
