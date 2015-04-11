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
    public class OrderByTests : BaseTest
    {
        #region OrderBy

        [Test]
        public void OrderByDeferredExecution()
        {
            Data<object> data = DummyData;

            data.OrderBy(x => x);
            data.AssertDeferredExecution();
        }

        [Test]
        public void OrderByNonStreamingExecution()
        {
            IEnumerable<object> enumerable = LateThrowingData().OrderBy(x => x);

            Assert.Throws<LateThrownException>(() => enumerable.MoveNext()); // # Sequence was fully buffered, throwing LateThrownException after enumeration ended
        }

        [Test]
        public void OrderByEmptySource()
        {
            EmptyData.OrderBy(x => x).AssertEmpty();
        }

        [Test]
        public void OrderByNonEmptySource()
        {
            Data<int> data = Data(1, 2, 3, 4, 5);

            IOrderedEnumerable<int> orderedData = data.OrderBy(x => x % 2);

            orderedData.AssertEqual(2, 4, 1, 3, 5); // # Even numbers first with original order in source
        }

        [Test]
        public void OrderByNullKey()
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

            IOrderedEnumerable<Country> orderedData = data.OrderBy(x => x.Continent);

            orderedData.AssertSame(unknown, china, india, england, france);
        }

        [Test]
        public void OrderByNullElementInSource()
        {
            Data<string> data = Data("b", "c", "a", null);

            IOrderedEnumerable<string> orderedData = data.OrderBy(x => x);

            orderedData.AssertEqual(null, "a", "b", "c");
        }

        [Test]
        public void OrderByQueryReuse()
        {
            List<int> data = new List<int> { 1, 2, 3, 4, 5 };
            IOrderedEnumerable<int> enumerable = data.OrderBy(x => x % 2);

            enumerable.AssertEqual(2, 4, 1, 3, 5);

            data.Add(6);
            enumerable.AssertEqual(2, 4, 6, 1, 3, 5);
        }

        [Test]
        public void OrderBySourceArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => NullData.OrderBy(x => x)).WithParameter("source");
        }

        [Test]
        public void OrderByKeySelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => DummyData.OrderBy((Func<object, object>)null)).WithParameter("keySelector");
        }

        #endregion ENDOF: OrderBy

        #region OrderBy With Comparer

        [Test]
        public void OrderByWithComparerDeferredExecution()
        {
            Data<object> data = DummyData;

            data.OrderBy(x => x, Comparer<object>.Default);
            data.AssertDeferredExecution();
        }

        [Test]
        public void OrderByWithComparerNonStreamingExecution()
        {
            IEnumerable<object> enumerable = LateThrowingData().OrderBy(x => x, Comparer<object>.Default);

            Assert.Throws<LateThrownException>(() => enumerable.MoveNext()); // # Sequence was fully buffered, throwing LateThrownException after enumeration ended
        }

        [Test]
        public void OrderByWithComparerEmptySource()
        {
            EmptyData.OrderBy(x => x, Comparer<object>.Default).AssertEmpty();
        }

        [Test]
        public void OrderByWithComparerNonEmptySource()
        {
            Data<int> data = Data(1, 2, 3, 4, 5);

            IOrderedEnumerable<int> orderedData = data.OrderBy(x => x % 2, Comparer<int>.Default);

            orderedData.AssertEqual(2, 4, 1, 3, 5);
        }

        [Test]
        public void OrderByWithComparerNullKey()
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

            IOrderedEnumerable<Country> orderedData = data.OrderBy(x => x.Continent, Comparer<string>.Default);

            orderedData.AssertSame(unknown, china, india, england, france);
        }

        [Test]
        public void OrderByWithComparerNullElementInSource()
        {
            Data<string> data = Data("b", "c", "a", null);

            IOrderedEnumerable<string> orderedData = data.OrderBy(x => x, Comparer<string>.Default);

            orderedData.AssertEqual(null, "a", "b", "c");
        }

        [Test]
        public void OrderByWithComparerQueryReuse()
        {
            List<int> data = new List<int> { 1, 2, 3, 4, 5 };
            IOrderedEnumerable<int> enumerable = data.OrderBy(x => x % 2, Comparer<int>.Default);

            enumerable.AssertEqual(2, 4, 1, 3, 5);

            data.Add(6);
            enumerable.AssertEqual(2, 4, 6, 1, 3, 5);
        }

        [Test]
        public void OrderByWithComparerComparerArgumentNull()
        {
            Assert.DoesNotThrow(() => DummyData.OrderBy(x => x, null));
        }

        [Test]
        public void OrderByWithComparerSourceArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => NullData.OrderBy(x => x, Comparer<object>.Default)).WithParameter("source");
        }

        [Test]
        public void OrderByWithComparerKeySelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => DummyData.OrderBy((Func<object, object>)null, Comparer<object>.Default)).WithParameter("keySelector");
        }

        #endregion ENDOF: OrderBy With Comparer
    }
}
