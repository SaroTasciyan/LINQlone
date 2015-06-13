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
    public class GroupByTests : BaseTest
    {
        #region GroupBy

        [Test]
        public void GroupByDeferredExecution()
        {
            Data<object> data = DummyData;

            data.GroupBy(x => x);
            data.AssertDeferredExecution();
        }

        [Test]
        public void GroupByNonStreamingExecution()
        {
            IEnumerable<IGrouping<object, object>> enumerable = LateThrowingData().GroupBy(x => x);

            Assert.Throws<LateThrownException>(() => enumerable.MoveNext()); // # Sequence was fully buffered, throwing LateThrownException after enumeration ended
        }

        [Test]
        public void GroupByEmptySource()
        {
            EmptyData.GroupBy(x => x).AssertEmpty();
        }

        [Test]
        public void GroupByNonEmptySource()
        {
            Data<int> data = Data(1, 2, 3, 4, 5);

            IEnumerable<IGrouping<bool, int>> evenOddGroupings = data.GroupBy(x => x % 2 == 0);

            using (IEnumerator<IGrouping<bool, int>> enumerator = evenOddGroupings.GetEnumerator())
            {
                Assert.That(enumerator.MoveNext(), Is.True);
                Assert.That(enumerator.Current.Key, Is.False);
                enumerator.Current.AssertEqual(1, 3, 5);

                Assert.That(enumerator.MoveNext(), Is.True);
                Assert.That(enumerator.Current.Key, Is.True);
                enumerator.Current.AssertEqual(2, 4);

                Assert.That(enumerator.MoveNext(), Is.False);
            }
        }

        [Test]
        public void GroupByNullKey()
        {
            Country china, india, unknown;
            Data<Country> data = Data
            (
                china = new Country { Name = "China", Continent = "Asia" },
                india = new Country { Name = "India", Continent = "Asia" },
                unknown = new Country { Name = "Unknown", Continent = null }
            );

            IEnumerable<IGrouping<string, Country>> continentGroups = data.GroupBy(x => x.Continent);

            using (IEnumerator<IGrouping<string, Country>> enumerator = continentGroups.GetEnumerator())
            {
                Assert.That(enumerator.MoveNext(), Is.True);
                Assert.That(enumerator.Current.Key, Is.EqualTo("Asia"));
                enumerator.Current.AssertSame(china, india);

                Assert.That(enumerator.MoveNext(), Is.True);
                Assert.That(enumerator.Current.Key, Is.EqualTo(null));
                enumerator.Current.AssertSame(unknown);

                Assert.That(enumerator.MoveNext(), Is.False);
            }
        }

        [Test]
        public void GroupByNullElementInSource()
        {
            Country china, india, @null;
            Data<Country> data = Data
            (
                china = new Country { Name = "China", Continent = "Asia" },
                india = new Country { Name = "India", Continent = "Asia" },
                @null = null
            );

            IEnumerable<IGrouping<Country, Country>> groups = data.GroupBy(x => x);

            using (IEnumerator<IGrouping<Country, Country>> enumerator = groups.GetEnumerator())
            {
                Assert.That(enumerator.MoveNext(), Is.True);
                Assert.That(enumerator.Current.Key, Is.SameAs(china));
                enumerator.Current.AssertSame(china);

                Assert.That(enumerator.MoveNext(), Is.True);
                Assert.That(enumerator.Current.Key, Is.SameAs(india));
                enumerator.Current.AssertSame(india);

                Assert.That(enumerator.MoveNext(), Is.True);
                Assert.That(enumerator.Current.Key, Is.SameAs(@null));
                enumerator.Current.AssertSame(@null);

                Assert.That(enumerator.MoveNext(), Is.False);
            }
        }

        [Test]
        public void GroupByQueryReuse()
        {
            List<int> data = new List<int> { 1, 2, 3 };
            IEnumerable<IGrouping<bool, int>> enumerable = data.GroupBy(x => x % 2 == 0);

            using (IEnumerator<IGrouping<bool, int>> enumerator = enumerable.GetEnumerator())
            {
                Assert.That(enumerator.MoveNext(), Is.True);
                Assert.That(enumerator.Current.Key, Is.False);
                enumerator.Current.AssertEqual(1, 3);

                Assert.That(enumerator.MoveNext(), Is.True);
                Assert.That(enumerator.Current.Key, Is.True);
                enumerator.Current.AssertEqual(2);

                Assert.That(enumerator.MoveNext(), Is.False);
            }

            data.Add(4);

            using (IEnumerator<IGrouping<bool, int>> enumerator = enumerable.GetEnumerator())
            {
                Assert.That(enumerator.MoveNext(), Is.True);
                Assert.That(enumerator.Current.Key, Is.False);
                enumerator.Current.AssertEqual(1, 3);

                Assert.That(enumerator.MoveNext(), Is.True);
                Assert.That(enumerator.Current.Key, Is.True);
                enumerator.Current.AssertEqual(2, 4);

                Assert.That(enumerator.MoveNext(), Is.False);
            }
        }

        [Test]
        public void GroupBySourceArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => NullData.GroupBy(x => x)).WithParameter("source");
        }

        [Test]
        public void GroupByKeySelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => DummyData.GroupBy((Func<object, object>)null)).WithParameter("keySelector");
        }

        #endregion ENDOF: GroupBy

        #region GroupBy With Element Selector

        [Test]
        public void GroupByWithElementSelectorEmptySource()
        {
            EmptyData.GroupBy(k => k, e => e).AssertEmpty();
        }

        [Test]
        public void GroupByWithElementSelectorNonStreamingExecution()
        {
            IEnumerable<IGrouping<object, object>> enumerable = LateThrowingData().GroupBy(k => k, e => e);

            Assert.Throws<LateThrownException>(() => enumerable.MoveNext()); // # Sequence was fully buffered, throwing LateThrownException after enumeration ended
        }

        [Test]
        public void GroupByWithElementSelectorNonEmptySource()
        {
            Data<int> data = Data(1, 2, 3, 4, 5);

            IEnumerable<IGrouping<bool, int>> evenOddGroups = data.GroupBy(k => k % 2 == 0, e => e * e);

            using (IEnumerator<IGrouping<bool, int>> enumerator = evenOddGroups.GetEnumerator())
            {
                Assert.That(enumerator.MoveNext(), Is.True);
                Assert.That(enumerator.Current.Key, Is.False);
                enumerator.Current.AssertEqual(1, 9, 25);

                Assert.That(enumerator.MoveNext(), Is.True);
                Assert.That(enumerator.Current.Key, Is.True);
                enumerator.Current.AssertEqual(4, 16);

                Assert.That(enumerator.MoveNext(), Is.False);
            }
        }

        [Test]
        public void GroupByWithElementSelectorNullKey()
        {
            Data<Country> data = Data
            (
                new Country { Name = "China", Continent = "Asia" },
                new Country { Name = "India", Continent = "Asia" },
                new Country { Name = "Unknown", Continent = null }
            );

            IEnumerable<IGrouping<string, string>> continentGroups = data.GroupBy(k => k.Continent, e => e.Name);

            using (IEnumerator<IGrouping<string, string>> enumerator = continentGroups.GetEnumerator())
            {
                Assert.That(enumerator.MoveNext(), Is.True);
                Assert.That(enumerator.Current.Key, Is.EqualTo("Asia"));
                enumerator.Current.AssertEqual("China", "India");

                Assert.That(enumerator.MoveNext(), Is.True);
                Assert.That(enumerator.Current.Key, Is.EqualTo(null));
                enumerator.Current.AssertEqual("Unknown");

                Assert.That(enumerator.MoveNext(), Is.False);
            }
        }

        [Test]
        public void GroupByWithElementSelectorNullElementInSource()
        {
            Country china, india, @null;
            Data<Country> data = Data
            (
                china = new Country { Name = "China", Continent = "Asia" },
                india = new Country { Name = "India", Continent = "Asia" },
                @null = null
            );

            IEnumerable<IGrouping<Country, Country>> groups = data.GroupBy(k => k, e => e);

            using (IEnumerator<IGrouping<Country, Country>> enumerator = groups.GetEnumerator())
            {
                Assert.That(enumerator.MoveNext(), Is.True);
                Assert.That(enumerator.Current.Key, Is.SameAs(china));
                enumerator.Current.AssertSame(china);

                Assert.That(enumerator.MoveNext(), Is.True);
                Assert.That(enumerator.Current.Key, Is.SameAs(india));
                enumerator.Current.AssertSame(india);

                Assert.That(enumerator.MoveNext(), Is.True);
                Assert.That(enumerator.Current.Key, Is.SameAs(@null));
                enumerator.Current.AssertSame(@null);

                Assert.That(enumerator.MoveNext(), Is.False);
            }
        }

        [Test]
        public void GroupByWithElementSelectorQueryReuse()
        {
            List<int> data = new List<int> { 1, 2, 3 };
            IEnumerable<IGrouping<bool, int>> enumerable = data.GroupBy(k => k % 2 == 0, e => e * e);

            using (IEnumerator<IGrouping<bool, int>> enumerator = enumerable.GetEnumerator())
            {
                Assert.That(enumerator.MoveNext(), Is.True);
                Assert.That(enumerator.Current.Key, Is.False);
                enumerator.Current.AssertEqual(1, 9);

                Assert.That(enumerator.MoveNext(), Is.True);
                Assert.That(enumerator.Current.Key, Is.True);
                enumerator.Current.AssertEqual(4);

                Assert.That(enumerator.MoveNext(), Is.False);
            }

            data.Add(4);

            using (IEnumerator<IGrouping<bool, int>> enumerator = enumerable.GetEnumerator())
            {
                Assert.That(enumerator.MoveNext(), Is.True);
                Assert.That(enumerator.Current.Key, Is.False);
                enumerator.Current.AssertEqual(1, 9);

                Assert.That(enumerator.MoveNext(), Is.True);
                Assert.That(enumerator.Current.Key, Is.True);
                enumerator.Current.AssertEqual(4, 16);

                Assert.That(enumerator.MoveNext(), Is.False);
            }
        }

        [Test]
        public void GroupByWithElementSelectorSourceArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => NullData.GroupBy(k => k, e => e)).WithParameter("source");
        }

        [Test]
        public void GroupByWithElementSelectorKeySelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => DummyData.GroupBy((Func<object, object>)null, e => e)).WithParameter("keySelector");
        }

        [Test]
        public void GroupByWithElementSelectorElementSelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => DummyData.GroupBy(k => k, (Func<object, object>)null)).WithParameter("elementSelector");
        }

        #endregion ENDOF: GroupBy With Element Selector

        #region GroupBy With Result Selector

        [Test]
        public void GroupByWithResultSelectorEmptySource()
        {
            EmptyData.GroupBy(k => k, (k, c) => c).AssertEmpty();
        }

        [Test]
        public void GroupByWithResultSelectorNonStreamingExecution()
        {
            IEnumerable<IEnumerable<object>> enumerable = LateThrowingData().GroupBy(k => k, (k, c) => c);

            Assert.Throws<LateThrownException>(() => enumerable.MoveNext()); // # Sequence was fully buffered, throwing LateThrownException after enumeration ended
        }

        [Test]
        public void GroupByWithResultSelectorNonEmptySource()
        {
            Data<int> data = Data(1, 2, 3, 4, 5);

            data.GroupBy(k => k % 2, (k, c) => StringHelper.Join(k, c)).AssertEqual("1,1,3,5", "0,2,4");
        }

        [Test]
        public void GroupByWithResultSelectorNullKey()
        {
            Country china, india, unknown;
            Data<Country> data = Data
            (
                china = new Country { Name = "China", Continent = "Asia" },
                india = new Country { Name = "India", Continent = "Asia" },
                unknown = new Country { Name = "Unknown", Continent = null }
            );

            data.GroupBy(k => k.Continent, (k, c) => StringHelper.Join(k, c)).AssertEqual(String.Format("Asia,{0},{1}", china, india), unknown.ToString());
        }

        [Test]
        public void GroupByWithResultSelectorNullElementInSource()
        {
            Country china, india, @null;
            Data<Country> data = Data
            (
                china = new Country { Name = "China", Continent = "Asia" },
                india = new Country { Name = "India", Continent = "Asia" },
                @null = null
            );

            data.GroupBy(k => k, (k, c) => k).AssertSame(china, india, @null);
        }

        [Test]
        public void GroupByWithResultSelectorQueryReuse()
        {
            List<int> data = new List<int> { 1, 2, 3 };
            IEnumerable<string> enumerable = data.GroupBy(k => k % 2, (k, c) => StringHelper.Join(k, c));

            enumerable.AssertEqual("1,1,3", "0,2");

            data.Add(4);
            enumerable.AssertEqual("1,1,3", "0,2,4");
        }

        [Test]
        public void GroupByWithResultSelectorSourceArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => NullData.GroupBy(k => k, (k, c) => k)).WithParameter("source");
        }

        [Test]
        public void GroupByWithResultSelectorKeySelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => DummyData.GroupBy((Func<object, object>)null, (k, c) => k)).WithParameter("keySelector");
        }

        [Test]
        public void GroupByWithResultSelectorResultSelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => DummyData.GroupBy(k => k, (Func<object, IEnumerable<object>, object>)null)).WithParameter("resultSelector");
        }

        #endregion ENDOF: GroupBy With Result Selector

        #region GroupBy With Element & Result Selector

        [Test]
        public void GroupByWithElementAndResultSelectorEmptySource()
        {
            EmptyData.GroupBy(k => k, e => e, (k, c) => c).AssertEmpty();
        }

        [Test]
        public void GroupByWithElementAndResultSelectorNonStreamingExecution()
        {
            IEnumerable<IEnumerable<object>> enumerable = LateThrowingData().GroupBy(k => k, e => e, (k, c) => c);

            Assert.Throws<LateThrownException>(() => enumerable.MoveNext()); // # Sequence was fully buffered, throwing LateThrownException after enumeration ended
        }

        [Test]
        public void GroupByWithElementAndResultSelectorNonEmptySource()
        {
            Data<int> data = Data(1, 2, 3, 4, 5);

            data.GroupBy(k => k % 2, e => e * e, (k, c) => StringHelper.Join(k, c)).AssertEqual("1,1,9,25", "0,4,16");
        }

        [Test]
        public void GroupByWithElementAndResultSelectorNullKey()
        {
            Data<Country> data = Data
            (
                new Country { Name = "China", Continent = "Asia" },
                new Country { Name = "India", Continent = "Asia" },
                new Country { Name = "Unknown", Continent = null }
            );

            data.GroupBy(k => k.Continent, e => e.Name, (k, c) => StringHelper.Join(k, c)).AssertEqual("Asia,China,India", "Unknown");
        }

        [Test]
        public void GroupByWithElementAndResultSelectorNullElementInSource()
        {
            Country china, india, @null;
            Data<Country> data = Data
            (
                china = new Country { Name = "China", Continent = "Asia" },
                india = new Country { Name = "India", Continent = "Asia" },
                @null = null
            );

            data.GroupBy(k => k, e => e, (k, c) => k).AssertSame(china, india, @null);
        }

        [Test]
        public void GroupByWithElementAndResultSelectorQueryReuse()
        {
            List<int> data = new List<int> { 1, 2, 3 };
            IEnumerable<string> enumerable = data.GroupBy(k => k % 2, e => e * e, (k, c) => StringHelper.Join(k, c));

            enumerable.AssertEqual("1,1,9", "0,4");

            data.Add(4);
            enumerable.AssertEqual("1,1,9", "0,4,16");
        }

        [Test]
        public void GroupByWithElementAndResultSelectorSourceArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => NullData.GroupBy(k => k, e => e, (k, c) => k)).WithParameter("source");
        }

        [Test]
        public void GroupByWithElementAndResultSelectorKeySelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => DummyData.GroupBy((Func<object, object>)null, e => e, (k, c) => k)).WithParameter("keySelector");
        }

        [Test]
        public void GroupByWithElementAndResultSelectorElementSelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => DummyData.GroupBy(k => k, (Func<object, object>)null, (k, c) => k)).WithParameter("elementSelector");
        }

        [Test]
        public void GroupByWithElementAndResultSelectorResultSelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => DummyData.GroupBy(k => k, e => e, (Func<object, IEnumerable<object>, object>)null)).WithParameter("resultSelector");
        }

        #endregion ENDOF: GroupBy With Element & Result Selector

        #region GroupBy With Comparer

        [Test]
        public void GroupByWithComparerDeferredExecution()
        {
            Data<object> data = DummyData;

            data.GroupBy(x => x, EqualityComparer<object>.Default);
            data.AssertDeferredExecution();
        }

        [Test]
        public void GroupByWithComparerNonStreamingExecution()
        {
            IEnumerable<IGrouping<object, object>> enumerable = LateThrowingData().GroupBy(x => x, EqualityComparer<object>.Default);

            Assert.Throws<LateThrownException>(() => enumerable.MoveNext()); // # Sequence was fully buffered, throwing LateThrownException after enumeration ended
        }

        [Test]
        public void GroupByWithComparerEmptySource()
        {
            EmptyData.GroupBy(x => x, EqualityComparer<object>.Default).AssertEmpty();
        }

        [Test]
        public void GroupByWithComparerNonEmptySource()
        {
            Data<int> data = Data(1, 2, 3, 4, 5);

            IEnumerable<IGrouping<bool, int>> evenOddGroups = data.GroupBy(x => x % 2 == 0, EqualityComparer<bool>.Default);

            using (IEnumerator<IGrouping<bool, int>> enumerator = evenOddGroups.GetEnumerator())
            {
                Assert.That(enumerator.MoveNext(), Is.True);
                Assert.That(enumerator.Current.Key, Is.False);
                enumerator.Current.AssertEqual(1, 3, 5);

                Assert.That(enumerator.MoveNext(), Is.True);
                Assert.That(enumerator.Current.Key, Is.True);
                enumerator.Current.AssertEqual(2, 4);

                Assert.That(enumerator.MoveNext(), Is.False);
            }
        }

        [Test]
        public void GroupByWithComparerNullKey()
        {
            Country china, india, unknown;
            Data<Country> data = Data
            (
                china = new Country { Name = "China", Continent = "Asia" },
                india = new Country { Name = "India", Continent = "Asia" },
                unknown = new Country { Name = "Unknown", Continent = null }
            );

            IEnumerable<IGrouping<string, Country>> continentGroups = data.GroupBy(x => x.Continent, EqualityComparer<string>.Default);

            using (IEnumerator<IGrouping<string, Country>> enumerator = continentGroups.GetEnumerator())
            {
                Assert.That(enumerator.MoveNext(), Is.True);
                Assert.That(enumerator.Current.Key, Is.EqualTo("Asia"));
                enumerator.Current.AssertSame(china, india);

                Assert.That(enumerator.MoveNext(), Is.True);
                Assert.That(enumerator.Current.Key, Is.EqualTo(null));
                enumerator.Current.AssertSame(unknown);

                Assert.That(enumerator.MoveNext(), Is.False);
            }
        }

        [Test]
        public void GroupByWithComparerNullElementInSource()
        {
            Country china, india, @null;
            Data<Country> data = Data
            (
                china = new Country { Name = "China", Continent = "Asia" },
                india = new Country { Name = "India", Continent = "Asia" },
                @null = null
            );

            IEnumerable<IGrouping<Country, Country>> groups = data.GroupBy(x => x, EqualityComparer<Country>.Default);

            using (IEnumerator<IGrouping<Country, Country>> enumerator = groups.GetEnumerator())
            {
                Assert.That(enumerator.MoveNext(), Is.True);
                Assert.That(enumerator.Current.Key, Is.SameAs(china));
                enumerator.Current.AssertSame(china);

                Assert.That(enumerator.MoveNext(), Is.True);
                Assert.That(enumerator.Current.Key, Is.SameAs(india));
                enumerator.Current.AssertSame(india);

                Assert.That(enumerator.MoveNext(), Is.True);
                Assert.That(enumerator.Current.Key, Is.SameAs(@null));
                enumerator.Current.AssertSame(@null);

                Assert.That(enumerator.MoveNext(), Is.False);
            }
        }

        [Test]
        public void GroupByWithComparerEqualsIsCalledForRepeatingNullKey()
        {
            Data<Country> data = Data
            (
                new Country { Name = "China", Continent = "Asia" },
                new Country { Name = "Unknown1", Continent = null },
                new Country { Name = "Unknown2", Continent = null }
            );
            ThrowingStringEqualityComparer comparer = new ThrowingStringEqualityComparer();

            Assert.Throws<NullReferenceException>(() => data.GroupBy(x => x.Continent, comparer).Iterate());
        }

        [Test]
        public void GroupByWithComparerQueryReuse()
        {
            List<int> data = new List<int> { 1, 2, 3 };
            IEnumerable<IGrouping<bool, int>> enumerable = data.GroupBy(x => x % 2 == 0, EqualityComparer<bool>.Default);

            using (IEnumerator<IGrouping<bool, int>> enumerator = enumerable.GetEnumerator())
            {
                Assert.That(enumerator.MoveNext(), Is.True);
                Assert.That(enumerator.Current.Key, Is.False);
                enumerator.Current.AssertEqual(1, 3);

                Assert.That(enumerator.MoveNext(), Is.True);
                Assert.That(enumerator.Current.Key, Is.True);
                enumerator.Current.AssertEqual(2);

                Assert.That(enumerator.MoveNext(), Is.False);
            }

            data.Add(4);

            using (IEnumerator<IGrouping<bool, int>> enumerator = enumerable.GetEnumerator())
            {
                Assert.That(enumerator.MoveNext(), Is.True);
                Assert.That(enumerator.Current.Key, Is.False);
                enumerator.Current.AssertEqual(1, 3);

                Assert.That(enumerator.MoveNext(), Is.True);
                Assert.That(enumerator.Current.Key, Is.True);
                enumerator.Current.AssertEqual(2, 4);

                Assert.That(enumerator.MoveNext(), Is.False);
            }
        }

        [Test]
        public void GroupByWithComparerComparerArgumentNull()
        {
            Assert.DoesNotThrow(() => DummyData.GroupBy(x => x, null));
        }

        [Test]
        public void GroupByWithComparerSourceArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => NullData.GroupBy(x => x, EqualityComparer<object>.Default)).WithParameter("source");
        }

        [Test]
        public void GroupByWithComparerKeySelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => DummyData.GroupBy(null, EqualityComparer<object>.Default)).WithParameter("keySelector");
        }

        #endregion ENDOF: GroupBy With Comparer

        #region GroupBy With Element Selector Comparer

        [Test]
        public void GroupByWithElementSelectorComparerEmptySource()
        {
            EmptyData.GroupBy(k => k, e => e, EqualityComparer<object>.Default).AssertEmpty();
        }

        [Test]
        public void GroupByWithElementSelectorComparerNonStreamingExecution()
        {
            IEnumerable<IGrouping<object, object>> enumerable = LateThrowingData().GroupBy(k => k, e => e, EqualityComparer<object>.Default);

            Assert.Throws<LateThrownException>(() => enumerable.MoveNext()); // # Sequence was fully buffered, throwing LateThrownException after enumeration ended
        }

        [Test]
        public void GroupByWithElementSelectorComparerNonEmptySource()
        {
            Data<int> data = Data(1, 2, 3, 4, 5);

            IEnumerable<IGrouping<bool, int>> evenOddGroups = data.GroupBy(k => k % 2 == 0, e => e * e, EqualityComparer<bool>.Default);

            using (IEnumerator<IGrouping<bool, int>> enumerator = evenOddGroups.GetEnumerator())
            {
                Assert.That(enumerator.MoveNext(), Is.True);
                Assert.That(enumerator.Current.Key, Is.False);
                enumerator.Current.AssertEqual(1, 9, 25);

                Assert.That(enumerator.MoveNext(), Is.True);
                Assert.That(enumerator.Current.Key, Is.True);
                enumerator.Current.AssertEqual(4, 16);

                Assert.That(enumerator.MoveNext(), Is.False);
            }
        }

        [Test]
        public void GroupByWithElementSelectorComparerNullKey()
        {
            Data<Country> data = Data
            (
                new Country { Name = "China", Continent = "Asia" },
                new Country { Name = "India", Continent = "Asia" },
                new Country { Name = "Unknown", Continent = null }
            );

            IEnumerable<IGrouping<string, string>> continentGroups = data.GroupBy(k => k.Continent, e => e.Name, EqualityComparer<string>.Default);

            using (IEnumerator<IGrouping<string, string>> enumerator = continentGroups.GetEnumerator())
            {
                Assert.That(enumerator.MoveNext(), Is.True);
                Assert.That(enumerator.Current.Key, Is.EqualTo("Asia"));
                enumerator.Current.AssertEqual("China", "India");

                Assert.That(enumerator.MoveNext(), Is.True);
                Assert.That(enumerator.Current.Key, Is.EqualTo(null));
                enumerator.Current.AssertEqual("Unknown");

                Assert.That(enumerator.MoveNext(), Is.False);
            }
        }

        [Test]
        public void GroupByWithElementSelectorComparerNullElementInSource()
        {
            Country china, india, @null;
            Data<Country> data = Data
            (
                china = new Country { Name = "China", Continent = "Asia" },
                india = new Country { Name = "India", Continent = "Asia" },
                @null = null
            );

            IEnumerable<IGrouping<Country, Country>> groups = data.GroupBy(k => k, e => e, EqualityComparer<Country>.Default);

            using (IEnumerator<IGrouping<Country, Country>> enumerator = groups.GetEnumerator())
            {
                Assert.That(enumerator.MoveNext(), Is.True);
                Assert.That(enumerator.Current.Key, Is.SameAs(china));
                enumerator.Current.AssertSame(china);

                Assert.That(enumerator.MoveNext(), Is.True);
                Assert.That(enumerator.Current.Key, Is.SameAs(india));
                enumerator.Current.AssertSame(india);

                Assert.That(enumerator.MoveNext(), Is.True);
                Assert.That(enumerator.Current.Key, Is.SameAs(@null));
                enumerator.Current.AssertSame(@null);

                Assert.That(enumerator.MoveNext(), Is.False);
            }
        }

        [Test]
        public void GroupByWithElementSelectorComparerEqualsIsCalledForRepeatingNullKey()
        {
            Data<Country> data = Data
            (
                new Country { Name = "China", Continent = "Asia" },
                new Country { Name = "Unknown1", Continent = null },
                new Country { Name = "Unknown2", Continent = null }
            );
            ThrowingStringEqualityComparer comparer = new ThrowingStringEqualityComparer();

            Assert.Throws<NullReferenceException>(() => data.GroupBy(x => x.Continent, e => e, comparer).Iterate());
        }

        [Test]
        public void GroupByWithElementSelectorComparerQueryReuse()
        {
            List<int> data = new List<int> { 1, 2, 3 };
            IEnumerable<IGrouping<bool, int>> enumerable = data.GroupBy(k => k % 2 == 0, e => e * e, EqualityComparer<bool>.Default);

            using (IEnumerator<IGrouping<bool, int>> enumerator = enumerable.GetEnumerator())
            {
                Assert.That(enumerator.MoveNext(), Is.True);
                Assert.That(enumerator.Current.Key, Is.False);
                enumerator.Current.AssertEqual(1, 9);

                Assert.That(enumerator.MoveNext(), Is.True);
                Assert.That(enumerator.Current.Key, Is.True);
                enumerator.Current.AssertEqual(4);

                Assert.That(enumerator.MoveNext(), Is.False);
            }

            data.Add(4);

            using (IEnumerator<IGrouping<bool, int>> enumerator = enumerable.GetEnumerator())
            {
                Assert.That(enumerator.MoveNext(), Is.True);
                Assert.That(enumerator.Current.Key, Is.False);
                enumerator.Current.AssertEqual(1, 9);

                Assert.That(enumerator.MoveNext(), Is.True);
                Assert.That(enumerator.Current.Key, Is.True);
                enumerator.Current.AssertEqual(4, 16);

                Assert.That(enumerator.MoveNext(), Is.False);
            }
        }

        [Test]
        public void GroupByWithElementSelectorComparerComparerArgumentNull()
        {
            Assert.DoesNotThrow(() => DummyData.GroupBy(k => k, e => e, null));
        }

        [Test]
        public void GroupByWithElementSelectorComparerSourceArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => NullData.GroupBy(k => k, e => e, EqualityComparer<object>.Default)).WithParameter("source");
        }

        [Test]
        public void GroupByWithElementSelectorComparerKeySelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => DummyData.GroupBy(null, e => e, EqualityComparer<object>.Default)).WithParameter("keySelector");
        }

        [Test]
        public void GroupByWithElementSelectorComparerElementSelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => DummyData.GroupBy(k => k, (Func<object, object>)null, EqualityComparer<object>.Default)).WithParameter("elementSelector");
        }

        #endregion ENDOF: GroupBy With Element Selector Comparer

        #region GroupBy With Result Selector Comparer

        [Test]
        public void GroupByWithResultSelectorComparerEmptySource()
        {
            EmptyData.GroupBy(k => k, (k, c) => c, EqualityComparer<object>.Default).AssertEmpty();
        }

        [Test]
        public void GroupByWithResultSelectorComparerNonStreamingExecution()
        {
            IEnumerable<IEnumerable<object>> enumerable = LateThrowingData().GroupBy(k => k, (k, c) => c, EqualityComparer<object>.Default);

            Assert.Throws<LateThrownException>(() => enumerable.MoveNext()); // # Sequence was fully buffered, throwing LateThrownException after enumeration ended
        }

        [Test]
        public void GroupByWithResultSelectorComparerNonEmptySource()
        {
            Data<int> data = Data(1, 2, 3, 4, 5);

            data.GroupBy(k => k % 2, (k, c) => StringHelper.Join(k, c), EqualityComparer<int>.Default).AssertEqual("1,1,3,5", "0,2,4");
        }

        [Test]
        public void GroupByWithResultSelectorComparerNullKey()
        {
            Country china, india, unknown;
            Data<Country> data = Data
            (
                china = new Country { Name = "China", Continent = "Asia" },
                india = new Country { Name = "India", Continent = "Asia" },
                unknown = new Country { Name = "Unknown", Continent = null }
            );

            data.GroupBy(k => k.Continent, (k, c) => StringHelper.Join(k, c), EqualityComparer<string>.Default)
                .AssertEqual(String.Format("Asia,{0},{1}", china, india), unknown.ToString());
        }

        [Test]
        public void GroupByWithResultSelectorComparerNullElementInSource()
        {
            Country china, india, @null;
            Data<Country> data = Data
            (
                china = new Country { Name = "China", Continent = "Asia" },
                india = new Country { Name = "India", Continent = "Asia" },
                @null = null
            );

            data.GroupBy(k => k, (k, c) => k, EqualityComparer<Country>.Default).AssertSame(china, india, @null);
        }

        [Test]
        public void GroupByWithResultSelectorComparerEqualsIsCalledForRepeatingNullKey()
        {
            Data<Country> data = Data
            (
                new Country { Name = "China", Continent = "Asia" },
                new Country { Name = "Unknown1", Continent = null },
                new Country { Name = "Unknown2", Continent = null }
            );
            ThrowingStringEqualityComparer comparer = new ThrowingStringEqualityComparer();

            Assert.Throws<NullReferenceException>(() => data.GroupBy(x => x.Continent, (k, c) => k, comparer).Iterate());
        }

        [Test]
        public void GroupByWithResultSelectorComparerQueryReuse()
        {
            List<int> data = new List<int> { 1, 2, 3 };
            IEnumerable<string> enumerable = data.GroupBy(k => k % 2, (k, c) => StringHelper.Join(k, c), EqualityComparer<int>.Default);

            enumerable.AssertEqual("1,1,3", "0,2");

            data.Add(4);
            enumerable.AssertEqual("1,1,3", "0,2,4");
        }

        [Test]
        public void GroupByWithResultSelectorComparerComparerArgumentNull()
        {
            Assert.DoesNotThrow(() => DummyData.GroupBy(k => k, (k, c) => k, null));
        }

        [Test]
        public void GroupByWithResultSelectorComparerSourceArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => NullData.GroupBy(k => k, (k, c) => k, EqualityComparer<object>.Default)).WithParameter("source");
        }

        [Test]
        public void GroupByWithResultSelectorComparerKeySelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => DummyData.GroupBy(null, (k, c) => k, EqualityComparer<object>.Default))
                .WithParameter("keySelector");
        }

        [Test]
        public void GroupByWithResultSelectorComparerResultSelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => DummyData.GroupBy(k => k, (Func<object, IEnumerable<object>, object>)null, EqualityComparer<object>.Default))
                .WithParameter("resultSelector");
        }

        #endregion ENDOF: GroupBy With Result Selector Comparer

        #region GroupBy With Element & Result Selector Comparer

        [Test]
        public void GroupByWithElementAndResultSelectorComparerEmptySource()
        {
            EmptyData.GroupBy(k => k, e => e, (k, c) => c, EqualityComparer<object>.Default).AssertEmpty();
        }

        [Test]
        public void GroupByWithElementAndResultSelectorComparerNonStreamingExecution()
        {
            IEnumerable<IEnumerable<object>> enumerable = LateThrowingData().GroupBy(k => k, e => e, (k, c) => c, EqualityComparer<object>.Default);

            Assert.Throws<LateThrownException>(() => enumerable.MoveNext()); // # Sequence was fully buffered, throwing LateThrownException after enumeration ended
        }

        [Test]
        public void GroupByWithElementAndResultSelectorComparerNonEmptySource()
        {
            Data<int> data = Data(1, 2, 3, 4, 5);

            data.GroupBy(k => k % 2, e => e * e, (k, c) => StringHelper.Join(k, c), EqualityComparer<int>.Default)
                .AssertEqual("1,1,9,25", "0,4,16");
        }

        [Test]
        public void GroupByWithElementAndResultSelectorComparerNullKey()
        {
            Data<Country> data = Data
            (
                new Country { Name = "China", Continent = "Asia" },
                new Country { Name = "India", Continent = "Asia" },
                new Country { Name = "Unknown", Continent = null }
            );

            data.GroupBy(k => k.Continent, e => e.Name, (k, c) => StringHelper.Join(k, c), EqualityComparer<string>.Default)
                .AssertEqual("Asia,China,India", "Unknown");
        }

        [Test]
        public void GroupByWithElementAndResultSelectorComparerNullElementInSource()
        {
            Country china, india, @null;
            Data<Country> data = Data
            (
                china = new Country { Name = "China", Continent = "Asia" },
                india = new Country { Name = "India", Continent = "Asia" },
                @null = null
            );

            data.GroupBy(k => k, e => e, (k, c) => k, EqualityComparer<Country>.Default).AssertSame(china, india, @null);
        }

        [Test]
        public void GroupByWithElementAndResultSelectorComparerEqualsIsCalledForRepeatingNullKey()
        {
            Data<Country> data = Data
            (
                new Country { Name = "China", Continent = "Asia" },
                new Country { Name = "Unknown1", Continent = null },
                new Country { Name = "Unknown2", Continent = null }
            );
            ThrowingStringEqualityComparer comparer = new ThrowingStringEqualityComparer();

            Assert.Throws<NullReferenceException>(() => data.GroupBy(x => x.Continent, e => e, (k, c) => k, comparer).Iterate());
        }

        [Test]
        public void GroupByWithElementAndResultSelectorComparerQueryReuse()
        {
            List<int> data = new List<int> { 1, 2, 3 };
            IEnumerable<string> enumerable = data.GroupBy(k => k % 2, e => e * e, (k, c) => StringHelper.Join(k, c), EqualityComparer<int>.Default);

            enumerable.AssertEqual("1,1,9", "0,4");

            data.Add(4);
            enumerable.AssertEqual("1,1,9", "0,4,16");
        }

        [Test]
        public void GroupByWithElementAndResultSelectorComparerComparerArgumentNull()
        {
            Assert.DoesNotThrow(() => DummyData.GroupBy(k => k, e => e, (k, c) => k, null));
        }

        [Test]
        public void GroupByWithElementAndResultSelectorComparerSourceArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => NullData.GroupBy(k => k, e => e, (k, c) => k, EqualityComparer<object>.Default))
                .WithParameter("source");
        }

        [Test]
        public void GroupByWithElementAndResultSelectorComparerKeySelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => DummyData.GroupBy(null, e => e, (k, c) => k, EqualityComparer<object>.Default))
                .WithParameter("keySelector");
        }

        [Test]
        public void GroupByWithElementAndResultSelectorComparerElementSelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => DummyData.GroupBy(k => k, (Func<object, object>)null, (k, c) => k, EqualityComparer<object>.Default))
                .WithParameter("elementSelector");
        }

        [Test]
        public void GroupByWithElementAndResultSelectorComparerResultSelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => DummyData.GroupBy(k => k, e => e, (Func<object, IEnumerable<object>, object>)null, EqualityComparer<object>.Default))
                .WithParameter("resultSelector");
        }

        #endregion ENDOF: GroupBy With Element & Result Selector Comparer
    }
}
