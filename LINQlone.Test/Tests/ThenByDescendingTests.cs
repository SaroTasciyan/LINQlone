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
    public class ThenByDescendingTests : BaseTest
    {
        #region ThenByDescending

        [Test]
        public void ThenByDescendingDeferredExecution()
        {
            Data<object> data = DummyData;

            data.OrderBy(x => true).ThenByDescending(x => x);
            data.AssertDeferredExecution();
        }

        [Test]
        public void ThenByDescendingEmptySource()
        {
            EmptyData.OrderBy(x => true).ThenByDescending(x => x).AssertEmpty();
        }

        [Test]
        public void ThenByDescendingNonEmptySource()
        {
            Data<int> data = Data(1, 2, 3, 4, 5);

            IOrderedEnumerable<int> orderedData = data.OrderBy(x => true).ThenByDescending(x => x % 2);

            orderedData.AssertEqual(1, 3, 5, 2, 4); // # Odd numbers first with original order in source
        }

        [Test]
        public void ThenByDescendingNullKey()
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

            IOrderedEnumerable<Country> orderedData = data.OrderBy(x => true).ThenByDescending(x => x.Continent);

            orderedData.AssertSame(england, france, china, india, unknown);
        }

        [Test]
        public void ThenByDescendingNullElementInSource()
        {
            Data<string> data = Data("b", "c", "a", null);

            IOrderedEnumerable<string> orderedData = data.OrderBy(x => true).ThenByDescending(x => x);

            orderedData.AssertEqual("c", "b", "a", null);
        }

        [Test]
        public void ThenByDescendingQueryReuse()
        {
            List<int> data = new List<int> { 1, 2, 3, 4, 5 };
            IOrderedEnumerable<int> enumerable = data.OrderBy(x => true).ThenByDescending(x => x % 2);

            enumerable.AssertEqual(1, 3, 5, 2, 4);

            data.Add(6);
            enumerable.AssertEqual(1, 3, 5, 2, 4, 6);
        }

        [Test]
        public void ThenByDescendingSourceArgumentNull()
        {
            IOrderedEnumerable<object> nullData = null;

            Assert.Throws<ArgumentNullException>(() => nullData.ThenByDescending(x => x)).WithParameter("source");
        }

        [Test]
        public void ThenByDescendingKeySelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => DummyData.OrderBy(x => true).ThenByDescending((Func<object, object>)null)).WithParameter("keySelector");
        }

        #endregion ENDOF: ThenByDescending

        #region ThenByDescending With Comparer

        [Test]
        public void ThenByDescendingWithComparerDeferredExecution()
        {
            Data<object> data = DummyData;

            data.OrderBy(x => true).ThenByDescending(x => x, Comparer<object>.Default);
            data.AssertDeferredExecution();
        }

        [Test]
        public void ThenByDescendingWithComparerEmptySource()
        {
            EmptyData.OrderBy(x => true).ThenByDescending(x => x, Comparer<object>.Default).AssertEmpty();
        }

        [Test]
        public void ThenByDescendingWithComparerNonEmptySource()
        {
            Data<int> data = Data(1, 2, 3, 4, 5);

            IOrderedEnumerable<int> orderedData = data.OrderBy(x => true).ThenByDescending(x => x % 2, Comparer<int>.Default);

            orderedData.AssertEqual(1, 3, 5, 2, 4);
        }

        [Test]
        public void ThenByDescendingWithComparerNullKey()
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

            IOrderedEnumerable<Country> orderedData = data.OrderBy(x => true).ThenByDescending(x => x.Continent, Comparer<string>.Default);

            orderedData.AssertSame(england, france, china, india, unknown);
        }

        [Test]
        public void ThenByDescendingWithComparerNullElementInSource()
        {
            Data<string> data = Data("b", "c", "a", null);

            IOrderedEnumerable<string> orderedData = data.OrderBy(x => true).ThenByDescending(x => x, Comparer<string>.Default);

            orderedData.AssertEqual("c", "b", "a", null);
        }

        [Test]
        public void ThenByDescendingWithComparerQueryReuse()
        {
            List<int> data = new List<int> { 1, 2, 3, 4, 5 };
            IOrderedEnumerable<int> enumerable = data.OrderBy(x => true).ThenByDescending(x => x % 2, Comparer<int>.Default);

            enumerable.AssertEqual(1, 3, 5, 2, 4);

            data.Add(6);
            enumerable.AssertEqual(1, 3, 5, 2, 4, 6);
        }

        [Test]
        public void ThenByDescendingWithComparerComparerArgumentNull()
        {
            Assert.DoesNotThrow(() => DummyData.OrderBy(x => true).ThenByDescending(x => x, null));
        }

        [Test]
        public void ThenByDescendingWithComparerSourceArgumentNull()
        {
            IOrderedEnumerable<object> nullData = null;

            Assert.Throws<ArgumentNullException>(() => nullData.ThenByDescending(x => x, Comparer<object>.Default)).WithParameter("source");
        }

        [Test]
        public void ThenByDescendingWithComparerKeySelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => DummyData.OrderBy(x => true).ThenByDescending(null, Comparer<object>.Default)).WithParameter("keySelector");
        }

        #endregion ENDOF: ThenByDescending With Comparer
    }
}
