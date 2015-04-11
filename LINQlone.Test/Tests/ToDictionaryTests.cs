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
    public class ToDictionaryTests : BaseTest
    {
        #region ToDictionary

        [Test]
        public void ToDictionaryEmptySource()
        {
            EmptyData.ToDictionary(x => x).AssertEmpty();
        }

        [Test]
        public void ToDictionaryNonEmptySource()
        {
            Data<int> data = Data(1, 2);

            Dictionary<bool, int> dictionary = data.ToDictionary(x => x % 2 == 0);
            using (IEnumerator<KeyValuePair<bool, int>> enumerator = dictionary.GetEnumerator())
            {
                Assert.That(enumerator.MoveNext(), Is.True);
                Assert.That(enumerator.Current.Key, Is.False);
                Assert.That(enumerator.Current.Value, Is.EqualTo(1));

                Assert.That(enumerator.MoveNext(), Is.True);
                Assert.That(enumerator.Current.Key, Is.True);
                Assert.That(enumerator.Current.Value, Is.EqualTo(2));

                Assert.That(enumerator.MoveNext(), Is.False);
            }
        }
        
        [Test]
        public void ToDictionaryNullKey()
        {
            Country china, france, unknown;
            Data<Country> data = Data
            (
                china = new Country() { Name = "China", Continent = "Asia" },
                france = new Country() { Name = "France", Continent = "Europe" },
                unknown = new Country() { Name = "Unknown", Continent = null }
            );

            Assert.Throws<ArgumentNullException>(() => data.ToDictionary(x => x.Continent));
        }

        [Test]
        public void ToDictionarySourceArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => NullData.ToDictionary(x => x)).WithParameter("source");
        }

        [Test]
        public void ToDictionaryKeySelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => DummyData.ToDictionary((Func<object, object>)null)).WithParameter("keySelector");
        }
        
        #endregion ENDOF: ToDictionary

        #region ToDictionary With Element Selector

        [Test]
        public void ToDictionaryWithElementSelectorEmptySource()
        {
            EmptyData.ToDictionary(k => k, e => e).AssertEmpty();
        }
        
        [Test]
        public void ToDictionaryWithElementSelectorNonEmptySource()
        {
            Data<int> data = Data(1, 2);

            Dictionary<bool, int> dictionary = data.ToDictionary(k => k % 2 == 0, e => e * e);
            using (IEnumerator<KeyValuePair<bool, int>> enumerator = dictionary.GetEnumerator())
            {
                Assert.That(enumerator.MoveNext(), Is.True);
                Assert.That(enumerator.Current.Key, Is.False);
                Assert.That(enumerator.Current.Value, Is.EqualTo(1));

                Assert.That(enumerator.MoveNext(), Is.True);
                Assert.That(enumerator.Current.Key, Is.True);
                Assert.That(enumerator.Current.Value, Is.EqualTo(4));

                Assert.That(enumerator.MoveNext(), Is.False);
            }
        }

        [Test]
        public void ToDictionaryWithElementSelectorNullKey()
        {
            Country china, france, unknown;
            Data<Country> data = Data
            (
                china = new Country() { Name = "China", Continent = "Asia" },
                france = new Country() { Name = "France", Continent = "Europe" },
                unknown = new Country() { Name = "Unknown", Continent = null }
            );

            Assert.Throws<ArgumentNullException>(() => data.ToDictionary(k => k.Continent, e => e.Name));
        }

        [Test]
        public void ToDictionaryWithElementSelectorSourceArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => NullData.ToDictionary(k => k, e => e)).WithParameter("source");
        }

        [Test]
        public void ToDictionaryWithElementSelectorKeySelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => DummyData.ToDictionary((Func<object, object>)null, e => e)).WithParameter("keySelector");
        }

        [Test]
        public void ToDictionaryWithElementSelectorElementSelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => DummyData.ToDictionary(k => k, (Func<object, object>)null)).WithParameter("elementSelector");
        }
        
        #endregion ENDOF: ToDictionary With Element Selector
        
        #region ToDictionary With Comparer

        [Test]
        public void ToDictionaryWithComparerEmptySource()
        {
            EmptyData.ToDictionary(x => x, EqualityComparer<object>.Default).AssertEmpty();
        }
       
        [Test]
        public void ToDictionaryWithComparerNonEmptySource()
        {
            Data<int> data = Data(1, 2);

            Dictionary<bool, int> dictionary = data.ToDictionary(x => x % 2 == 0, EqualityComparer<bool>.Default);
            using (IEnumerator<KeyValuePair<bool, int>> enumerator = dictionary.GetEnumerator())
            {
                Assert.That(enumerator.MoveNext(), Is.True);
                Assert.That(enumerator.Current.Key, Is.False);
                Assert.That(enumerator.Current.Value, Is.EqualTo(1));

                Assert.That(enumerator.MoveNext(), Is.True);
                Assert.That(enumerator.Current.Key, Is.True);
                Assert.That(enumerator.Current.Value, Is.EqualTo(2));

                Assert.That(enumerator.MoveNext(), Is.False);
            }
        }
        
        [Test]
        public void ToDictionaryWithComparerNullKey()
        {
            Country china, france, unknown;
            Data<Country> data = Data
            (
                china = new Country() { Name = "China", Continent = "Asia" },
                france = new Country() { Name = "France", Continent = "Europe" },
                unknown = new Country() { Name = "Unknown", Continent = null }
            );

            Assert.Throws<ArgumentNullException>(() => data.ToDictionary(x => x.Continent, EqualityComparer<string>.Default));
        }

        [Test]
        public void ToDictionaryWithComparerComparerArgumentNull()
        {
            Assert.DoesNotThrow(() => DummyData.ToDictionary(x => x, null));
        }
        
        [Test]
        public void ToDictionaryWithComparerSourceArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => NullData.ToDictionary(x => x, EqualityComparer<object>.Default)).WithParameter("source");
        }

        [Test]
        public void ToDictionaryWithComparerKeySelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => DummyData.ToDictionary((Func<object, object>)null, EqualityComparer<object>.Default)).WithParameter("keySelector");
        }

        #endregion ENDOF: ToDictionary With Comparer

        #region ToDictionary With Element Selector & Comparer

        [Test]
        public void ToDictionaryWithElementSelectorComparerEmptySource()
        {
            EmptyData.ToDictionary(k => k, e => e, EqualityComparer<object>.Default).AssertEmpty();
        }
        
        [Test]
        public void ToDictionaryWithElementSelectorComparerNonEmptySource()
        {
            Data<int> data = Data(1, 2);

            Dictionary<bool, int> dictionary = data.ToDictionary(k => k % 2 == 0, e => e * e, EqualityComparer<bool>.Default);
            using (IEnumerator<KeyValuePair<bool, int>> enumerator = dictionary.GetEnumerator())
            {
                Assert.That(enumerator.MoveNext(), Is.True);
                Assert.That(enumerator.Current.Key, Is.False);
                Assert.That(enumerator.Current.Value, Is.EqualTo(1));

                Assert.That(enumerator.MoveNext(), Is.True);
                Assert.That(enumerator.Current.Key, Is.True);
                Assert.That(enumerator.Current.Value, Is.EqualTo(4));

                Assert.That(enumerator.MoveNext(), Is.False);
            }
        }
        
        [Test]
        public void ToDictionaryWithElementSelectorComparerNullKey()
        {
            Country china, france, unknown;
            Data<Country> data = Data
            (
                china = new Country() { Name = "China", Continent = "Asia" },
                france = new Country() { Name = "France", Continent = "Europe" },
                unknown = new Country() { Name = "Unknown", Continent = null }
            );

            Assert.Throws<ArgumentNullException>(() => data.ToDictionary(k => k.Continent, e => e.Name, EqualityComparer<string>.Default));
        }

        [Test]
        public void ToDictionaryWithElementSelectorComparerComparerArgumentNull()
        {
            Assert.DoesNotThrow(() => DummyData.ToDictionary(k => k, e => e, null));
        }

        [Test]
        public void ToDictionaryWithElementSelectorComparerSourceArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => NullData.ToDictionary(k => k, e => e, EqualityComparer<object>.Default)).WithParameter("source");
        }

        [Test]
        public void ToDictionaryWithElementSelectorComparerKeySelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => DummyData.ToDictionary((Func<object, object>)null, e => e, EqualityComparer<object>.Default)).WithParameter("keySelector");
        }

        [Test]
        public void ToDictionaryWithElementSelectorComparerElementSelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => DummyData.ToDictionary(k => k, (Func<object, object>)null, EqualityComparer<object>.Default)).WithParameter("elementSelector");
        }

        #endregion ENDOF: ToDictionary With Element Selector & Comparer
    }
}
