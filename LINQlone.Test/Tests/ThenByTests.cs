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
    public class ThenByTests : BaseTest
    {
        #region ThenBy

        [Test]
        public void ThenByDeferredExecution()
        {
            Data<object> data = DummyData;

            data.OrderBy(x => true).ThenBy(x => x);
            data.AssertDeferredExecution();
        }

        [Test]
        public void ThenByEmptySource()
        {
            EmptyData.OrderBy(x => true).ThenBy(x => x).AssertEmpty();
        }

        [Test]
        public void ThenByNonEmptySource()
        {
            Data<int> data = Data(1, 2, 3, 4, 5);

            IOrderedEnumerable<int> orderedData = data.OrderBy(x => true).ThenBy(x => x % 2);

            orderedData.AssertEqual(2, 4, 1, 3, 5); // # Even numbers first with original order in source
        }

        [Test]
        public void ThenByNullKey()
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

            IOrderedEnumerable<Country> orderedData = data.OrderBy(x => true).ThenBy(x => x.Continent);

            orderedData.AssertSame(unknown, china, india, england, france);
        }

        [Test]
        public void ThenByNullElementInSource()
        {
            Data<string> data = Data("b", "c", "a", null);

            IOrderedEnumerable<string> orderedData = data.OrderBy(x => true).ThenBy(x => x);

            orderedData.AssertEqual(null, "a", "b", "c");
        }

        [Test]
        public void ThenByQueryReuse()
        {
            List<int> data = new List<int> { 1, 2, 3, 4, 5 };
            IOrderedEnumerable<int> enumerable = data.OrderBy(x => true).ThenBy(x => x % 2);

            enumerable.AssertEqual(2, 4, 1, 3, 5);

            data.Add(6);
            enumerable.AssertEqual(2, 4, 6, 1, 3, 5);
        }

        [Test]
        public void ThenBySourceArgumentNull()
        {
            IOrderedEnumerable<object> nullData = null;

            Assert.Throws<ArgumentNullException>(() => nullData.ThenBy(x => x)).WithParameter("source");
        }

        [Test]
        public void ThenByKeySelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => DummyData.OrderBy(x => true).ThenBy((Func<object, object>)null)).WithParameter("keySelector");
        }

        #endregion ENDOF: ThenBy

        #region ThenBy With Comparer

        [Test]
        public void ThenByWithComparerDeferredExecution()
        {
            Data<object> data = DummyData;

            data.OrderBy(x => true).ThenBy(x => x, Comparer<object>.Default);
            data.AssertDeferredExecution();
        }

        [Test]
        public void ThenByWithComparerEmptySource()
        {
            EmptyData.OrderBy(x => true).ThenBy(x => x, Comparer<object>.Default).AssertEmpty();
        }

        [Test]
        public void ThenByWithComparerNonEmptySource()
        {
            Data<int> data = Data(1, 2, 3, 4, 5);

            IOrderedEnumerable<int> orderedData = data.OrderBy(x => true).ThenBy(x => x % 2, Comparer<int>.Default);

            orderedData.AssertEqual(2, 4, 1, 3, 5);
        }

        [Test]
        public void ThenByWithComparerNullKey()
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

            IOrderedEnumerable<Country> orderedData = data.OrderBy(x => true).ThenBy(x => x.Continent, Comparer<string>.Default);

            orderedData.AssertSame(unknown, china, india, england, france);
        }

        [Test]
        public void ThenByWithComparerNullElementInSource()
        {
            Data<string> data = Data("b", "c", "a", null);

            IOrderedEnumerable<string> orderedData = data.OrderBy(x => true).ThenBy(x => x, Comparer<string>.Default);

            orderedData.AssertEqual(null, "a", "b", "c");
        }

        [Test]
        public void ThenByWithComparerQueryReuse()
        {
            List<int> data = new List<int> { 1, 2, 3, 4, 5 };
            IOrderedEnumerable<int> enumerable = data.OrderBy(x => true).ThenBy(x => x % 2, Comparer<int>.Default);

            enumerable.AssertEqual(2, 4, 1, 3, 5);

            data.Add(6);
            enumerable.AssertEqual(2, 4, 6, 1, 3, 5);
        }

        [Test]
        public void ThenByWithComparerComparerArgumentNull()
        {
            Assert.DoesNotThrow(() => DummyData.OrderBy(x => true).ThenBy(x => x, null));
        }

        [Test]
        public void ThenByWithComparerSourceArgumentNull()
        {
            IOrderedEnumerable<object> nullData = null;

            Assert.Throws<ArgumentNullException>(() => nullData.ThenBy(x => x, Comparer<object>.Default)).WithParameter("source");
        }

        [Test]
        public void ThenByWithComparerKeySelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => DummyData.OrderBy(x => true).ThenBy((Func<object, object>)null, Comparer<object>.Default)).WithParameter("keySelector");
        }

        #endregion ENDOF: ThenBy With Comparer
    }
}
