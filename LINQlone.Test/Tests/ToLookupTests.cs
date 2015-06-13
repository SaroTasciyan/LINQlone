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
    public class ToLookupTests : BaseTest
    {
        #region ToLookup

        [Test]
        public void ToLookupEmptySource()
        {
            EmptyData.ToLookup(x => x).AssertEmpty();
        }

        [Test]
        public void ToLookupNonEmptySource()
        {
            Data<int> data = Data(1, 2, 3, 4, 5);

            ILookup<bool, int> evenOddLookup = data.ToLookup(x => x % 2 == 0);

            using (IEnumerator<IGrouping<bool, int>> enumerator = evenOddLookup.GetEnumerator())
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
        public void ToLookupNullKey()
        {
            Country china, india, unknown;
            Data<Country> data = Data
            (
                china = new Country { Name = "China", Continent = "Asia" },
                india = new Country { Name = "India", Continent = "Asia" },
                unknown = new Country { Name = "Unknown", Continent = null }
            );

            ILookup<string, Country> continentLookup = data.ToLookup(x => x.Continent);

            using (IEnumerator<IGrouping<string, Country>> enumerator = continentLookup.GetEnumerator())
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
        public void ToLookupNullElementInSource()
        {
            Country china, india, @null;
            Data<Country> data = Data
            (
                china = new Country { Name = "China", Continent = "Asia" },
                india = new Country { Name = "India", Continent = "Asia" },
                @null = null
            );

            ILookup<Country, Country> lookup = data.ToLookup(x => x);

            using (IEnumerator<IGrouping<Country, Country>> enumerator = lookup.GetEnumerator())
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
        public void ToLookupSourceArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => NullData.ToLookup(x => x)).WithParameter("source");
        }

        [Test]
        public void ToLookupKeySelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => DummyData.ToLookup((Func<object, object>)null)).WithParameter("keySelector");
        }

        #endregion ENDOF: ToLookup

        #region ToLookup With Element Selector

        [Test]
        public void ToLookupWithElementSelectorEmptySource()
        {
            EmptyData.ToLookup(k => k, e => e).AssertEmpty();
        }

        [Test]
        public void ToLookupWithElementSelectorNonEmptySource()
        {
            Data<int> data = Data(1, 2, 3, 4, 5);

            ILookup<bool, int> evenOddLookup = data.ToLookup(k => k % 2 == 0, e => e * e);

            using (IEnumerator<IGrouping<bool, int>> enumerator = evenOddLookup.GetEnumerator())
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
        public void ToLookupWithElementSelectorNullKey()
        {
            Data<Country> data = Data
            (
                new Country { Name = "China", Continent = "Asia" },
                new Country { Name = "India", Continent = "Asia" },
                new Country { Name = "Unknown", Continent = null }
            );

            ILookup<string, string> continentLookup = data.ToLookup(k => k.Continent, e => e.Name);

            using (IEnumerator<IGrouping<string, string>> enumerator = continentLookup.GetEnumerator())
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
        public void ToLookupWithElementSelectorNullElementInSource()
        {
            Country china, india, @null;
            Data<Country> data = Data
            (
                china = new Country { Name = "China", Continent = "Asia" },
                india = new Country { Name = "India", Continent = "Asia" },
                @null = null
            );

            ILookup<Country, Country> lookup = data.ToLookup(k => k, e => e);

            using (IEnumerator<IGrouping<Country, Country>> enumerator = lookup.GetEnumerator())
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
        public void ToLookupWithElementSelectorSourceArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => NullData.ToLookup(k => k, e => e)).WithParameter("source");
        }

        [Test]
        public void ToLookupWithElementSelectorKeySelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => DummyData.ToLookup((Func<object, object>)null, e => e)).WithParameter("keySelector");
        }

        [Test]
        public void ToLookupWithElementSelectorElementSelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => DummyData.ToLookup(k => k, (Func<object, object>)null)).WithParameter("elementSelector");
        }

        #endregion ENDOF: ToLookup With Element Selector

        #region ToLookup With Comparer

        [Test]
        public void ToLookupWithComparerEmptySource()
        {
            EmptyData.ToLookup(x => x, EqualityComparer<object>.Default).AssertEmpty();
        }

        [Test]
        public void ToLookupWithComparerNonEmptySource()
        {
            Data<int> data = Data(1, 2, 3, 4, 5);

            ILookup<bool, int> evenOddLookup = data.ToLookup(x => x % 2 == 0, EqualityComparer<bool>.Default);

            using (IEnumerator<IGrouping<bool, int>> enumerator = evenOddLookup.GetEnumerator())
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
        public void ToLookupWithComparerNullKey()
        {
            Country china, india, unknown;
            Data<Country> data = Data
            (
                china = new Country { Name = "China", Continent = "Asia" },
                india = new Country { Name = "India", Continent = "Asia" },
                unknown = new Country { Name = "Unknown", Continent = null }
            );

            ILookup<string, Country> continentLookup = data.ToLookup(x => x.Continent, EqualityComparer<string>.Default);

            using (IEnumerator<IGrouping<string, Country>> enumerator = continentLookup.GetEnumerator())
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
        public void ToLookupWithComparerNullElementInSource()
        {
            Country china, india, @null;
            Data<Country> data = Data
            (
                china = new Country { Name = "China", Continent = "Asia" },
                india = new Country { Name = "India", Continent = "Asia" },
                @null = null
            );

            ILookup<Country, Country> lookup = data.ToLookup(x => x, EqualityComparer<Country>.Default);

            using (IEnumerator<IGrouping<Country, Country>> enumerator = lookup.GetEnumerator())
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
        public void ToLookupWithComparerEqualsIsCalledForRepeatingNullKey()
        {
            Data<Country> data = Data
            (
                new Country { Name = "China", Continent = "Asia" },
                new Country { Name = "Unknown1", Continent = null },
                new Country { Name = "Unknown2", Continent = null }
            );
            ThrowingStringEqualityComparer comparer = new ThrowingStringEqualityComparer();

            Assert.Throws<NullReferenceException>(() => data.ToLookup(x => x.Continent, comparer));
        }

        [Test]
        public void ToLookupWithComparerComparerArgumentNull()
        {
            Assert.DoesNotThrow(() => DummyData.ToLookup(x => x, null));
        }

        [Test]
        public void ToLookupWithComparerSourceArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => NullData.ToLookup(x => x, EqualityComparer<object>.Default)).WithParameter("source");
        }

        [Test]
        public void ToLookupWithComparerKeySelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => DummyData.ToLookup(null, EqualityComparer<object>.Default)).WithParameter("keySelector");
        }

        #endregion ENDOF: ToLookup With Comparer

        #region ToLookup With Element Selector & Comparer

        [Test]
        public void ToLookupWithElementSelectorComparerEmptySource()
        {
            EmptyData.ToLookup(k => k, e => e, EqualityComparer<object>.Default).AssertEmpty();
        }

        [Test]
        public void ToLookupWithElementSelectorComparerNonEmptySource()
        {
            Data<int> data = Data(1, 2, 3, 4, 5);

            ILookup<bool, int> evenOddLookup = data.ToLookup(k => k % 2 == 0, e => e * e, EqualityComparer<bool>.Default);

            using (IEnumerator<IGrouping<bool, int>> enumerator = evenOddLookup.GetEnumerator())
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
        public void ToLookupWithElementSelectorComparerNullKey()
        {
            Data<Country> data = Data
            (
                new Country { Name = "China", Continent = "Asia" },
                new Country { Name = "India", Continent = "Asia" },
                new Country { Name = "Unknown", Continent = null }
            );

            ILookup<string, string> continentLookup = data.ToLookup(k => k.Continent, e => e.Name, EqualityComparer<string>.Default);

            using (IEnumerator<IGrouping<string, string>> enumerator = continentLookup.GetEnumerator())
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
        public void ToLookupWithElementSelectorComparerNullElementInSource()
        {
            Country china, india, @null;
            Data<Country> data = Data
            (
                china = new Country { Name = "China", Continent = "Asia" },
                india = new Country { Name = "India", Continent = "Asia" },
                @null = null
            );

            ILookup<Country, Country> lookup = data.ToLookup(k => k, e => e, EqualityComparer<Country>.Default);

            using (IEnumerator<IGrouping<Country, Country>> enumerator = lookup.GetEnumerator())
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
        public void ToLookupWithElementSelectorComparerEqualsIsCalledForRepeatingNullKey()
        {
            Data<Country> data = Data
            (
                new Country { Name = "China", Continent = "Asia" },
                new Country { Name = "Unknown1", Continent = null },
                new Country { Name = "Unknown2", Continent = null }
            );
            ThrowingStringEqualityComparer comparer = new ThrowingStringEqualityComparer();

            Assert.Throws<NullReferenceException>(() => data.ToLookup(k => k.Continent, e => e.Name, comparer));
        }

        [Test]
        public void ToLookupWithElementSelectorComparerComparerArgumentNull()
        {
            Assert.DoesNotThrow(() => DummyData.ToLookup(k => k, e => e, null));
        }

        [Test]
        public void ToLookupWithElementSelectorComparerSourceArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => NullData.ToLookup(k => k, e => e, EqualityComparer<object>.Default)).WithParameter("source");
        }

        [Test]
        public void ToLookupWithElementSelectorComparerKeySelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => DummyData.ToLookup(null, e => e, EqualityComparer<object>.Default)).WithParameter("keySelector");
        }

        [Test]
        public void ToLookupWithElementSelectorComparerElementSelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => DummyData.ToLookup(k => k, (Func<object, object>)null, EqualityComparer<object>.Default)).WithParameter("elementSelector");
        }

        #endregion ENDOF: ToLookup With Element Selector & Comparer
    }
}
