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
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;

using LINQlone.Test.DataObjects;

namespace LINQlone.Test
{
    [TestFixture]
    public class LookupTests : BaseTest
    {
        #region GetEnumerator

        [Test]
        public void LookupGetEnumerableGeneric()
        {
            Lookup<bool, int> lookup = (Lookup<bool, int>)Data(1, 2, 3, 4, 5).ToLookup(x => x % 2 == 0);
            IEnumerable<IGrouping<bool, int>> enumerable = lookup;

            foreach (IGrouping<bool, int> group in enumerable)
            {
                Assert.DoesNotThrow(() => group.Iterate());
            }
        }

        [Test]
        public void LookupGetEnumerableNonGeneric()
        {
            Lookup<bool, int> lookup = (Lookup<bool, int>)Data(1, 2, 3, 4, 5).ToLookup(x => x % 2 == 0);
            IEnumerable enumerable = lookup;

            foreach (object group in enumerable)
            {
                Assert.That(group, Is.AssignableTo<IGrouping<bool, int>>());
                Assert.That(group, Is.AssignableTo<IEnumerable<int>>());

                IEnumerable groupEnumerable = (IEnumerable)group;
                foreach (object groupElement in groupEnumerable)
                {
                    Assert.That(groupElement, Is.AssignableTo<int>());
                }
            }
        }

        #endregion ENDOF: GetEnumerator

        #region Contains

        [Test]
        public void LookupContainsEmptySource()
        {
            Lookup<object, object> lookup = (Lookup<object, object>)EmptyData.ToLookup(x => x);

            Assert.That(lookup.Contains(new object()), Is.False);
        }

        [Test]
        public void LookupContainsTrue()
        {
            Lookup<int, int> lookup = (Lookup<int, int>)Data(1, 2, 3, 4, 5).ToLookup(x => x % 2);

            Assert.That(lookup.Contains(1), Is.True);
        }

        [Test]
        public void LookupContainsFalse()
        {
            Lookup<int, int> lookup = (Lookup<int, int>)Data(1, 2, 3, 4, 5).ToLookup(x => x % 2);

            Assert.That(lookup.Contains(2), Is.False);
        }

        [Test]
        public void LookupContainsNullTrue()
        {
            Lookup<string, string> lookup = (Lookup<string, string>)Data("a", "b", null).ToLookup(x => x);

            Assert.That(lookup.Contains(null), Is.True);
        }

        [Test]
        public void LookupContainsNullFalse()
        {
            Lookup<string, string> lookup = (Lookup<string, string>)Data("a", "b").ToLookup(x => x);

            Assert.That(lookup.Contains(null), Is.False);
        }

        [Test]
        public void LookupContainsComparerEqualsIsCalledForExistingNullKey()
        {
            Data<Country> data = Data
            (
                new Country() { Name = "China", Continent = "Asia" },
                new Country() { Name = "Unknown1", Continent = null }
            );
            ThrowingStringEqualityComparer comparer = new ThrowingStringEqualityComparer();
            Lookup<string, Country> lookup = (Lookup<string, Country>)data.ToLookup(x => x.Continent, comparer);

            Assert.Throws<NullReferenceException>(() => lookup.Contains(null));
        }

        [Test]
        public void LookupContainsComparerCallCounts()
        {
            Data<string> data = Data("a", "b", "c", "d", "e");
            ThrowingStringEqualityComparer comparer = new ThrowingStringEqualityComparer();
            Lookup<string, string> lookup = (Lookup<string, string>)data.ToLookup(x => x, comparer);

            comparer.ResetCounts();
            lookup.Contains("d");

            Assert.That(comparer.GetHashCodeCallCount, Is.EqualTo(1));
            Assert.That(comparer.EqualsCallCount, Is.EqualTo(1));
        }

        [Test]
        public void LookupContainsComparerGetHashCodeCalledForEmptySource()
        {
            Data<string> data = Data<string>();
            ThrowingStringEqualityComparer comparer = new ThrowingStringEqualityComparer();
            Lookup<string, string> lookup = (Lookup<string, string>)data.ToLookup(x => x, comparer);

            comparer.ResetCounts();
            lookup.Contains("a");

            Assert.That(comparer.GetHashCodeCallCount, Is.EqualTo(1));
            Assert.That(comparer.EqualsCallCount, Is.EqualTo(0));
        }

        #endregion ENDOF: Contains

        #region Indexer

        [Test]
        public void LookupIndexerEmptySource()
        {
            Lookup<object, object> lookup = (Lookup<object, object>)EmptyData.ToLookup(x => x);

            lookup[new object()].AssertEmpty();
        }

        [Test]
        public void LookupIndexerNonEmptySource()
        {
            Lookup<bool, int> lookup = (Lookup<bool, int>)Data(1, 2, 3, 4, 5).ToLookup(x => x % 2 == 0);

            lookup[true].AssertEqual(2, 4);
            lookup[false].AssertEqual(1, 3, 5);
        }

        [Test]
        public void LookupIndexerNoMatch()
        {
            Lookup<bool, int> lookup = (Lookup<bool, int>)Data(2, 4, 6).ToLookup(x => x % 2 == 0);

            lookup[false].AssertEmpty();
        }

        [Test]
        public void LookupIndexerNullKey()
        {
            Lookup<string, string> lookup = (Lookup<string, string>)Data(null, "a", "b").ToLookup(x => x);

            lookup[null].AssertEqual((string)null);
        }

        [Test]
        public void LookupIndexerNoMatchNullKey()
        {
            Lookup<string, string> lookup = (Lookup<string, string>)Data("a", "b").ToLookup(x => x);

            lookup[null].AssertEmpty();
        }

        [Test]
        public void LookupIndexerComparerEqualsIsCalledForExistingNullKey()
        {
            Data<string> data = Data(null, "a", "b");
            ThrowingStringEqualityComparer comparer = new ThrowingStringEqualityComparer();

            Lookup<string, string> lookup = (Lookup<string, string>)data.ToLookup(x => x, comparer);
            
            IEnumerable<string> dontCare;
            Assert.Throws<NullReferenceException>(() => dontCare = lookup[null]);
        }

        [Test]
        public void LookupIndexerComparerCallCounts()
        {
            Data<string> data = Data("a", "b", "c", "d", "e");
            ThrowingStringEqualityComparer comparer = new ThrowingStringEqualityComparer();
            Lookup<string, string> lookup = (Lookup<string, string>)data.ToLookup(x => x, comparer);

            comparer.ResetCounts();
            IEnumerable<string> dontCare = lookup["d"];

            Assert.That(comparer.GetHashCodeCallCount, Is.EqualTo(1));
            Assert.That(comparer.EqualsCallCount, Is.EqualTo(1));
        }

        #endregion ENDOF: Indexer

        #region ApplyResultSelector

        [Test]
        public void LookupApplyResultSelectorEmptySource()
        {
            Lookup<object, object> lookup = (Lookup<object, object>)EmptyData.ToLookup(x => x);

            lookup.ApplyResultSelector((k, g) => (string)null).AssertEmpty();
        }

        [Test]
        public void LookupApplyResultSelectorNonEmptySource()
        {
            Lookup<bool, int> lookup = (Lookup<bool, int>)Data(1, 2, 3, 4, 5).ToLookup(x => x % 2 == 0);

            lookup.ApplyResultSelector((k, g) => (k ? "Even" : "Odd")).AssertEqual("Odd", "Even");
        }

        [Test]
        public void LookupApplyResultSelectorNullKey()
        {
            Lookup<string, string> lookup = (Lookup<string, string>)Data("a", "b", null).ToLookup(x => x);

            lookup.ApplyResultSelector((k, g) => (k ?? "NULL")).AssertEqual("a", "b", "NULL");
        }

        [Test]
        public void LookupApplyResultSelectorKeyAndElementsTransformation()
        {
            Lookup<bool, int> lookup = (Lookup<bool, int>)Data(1, 2, 3, 4, 5).ToLookup(x => x % 2 == 0);

            lookup.ApplyResultSelector
            (
                (k, g) =>
                {
                    int first = 0;
                    foreach (int @int in g) { first = @int; break; }

                    return String.Format("First {0}: {1}", (k ? "Even" : "Odd"), first);
                }
            ).AssertEqual("First Odd: 1", "First Even: 2");
        }

        #endregion ENDOF: ApplyResultSelector

        #region Count

        [Test]
        public void LookupCountEmptySource()
        {
            Lookup<object, object> lookup = (Lookup<object, object>)EmptyData.ToLookup(x => x);

            Assert.That(lookup.Count, Is.EqualTo(0));
        }

        [Test]
        public void LookupCountNonEmptySource()
        {
            Lookup<int, int> lookup = (Lookup<int, int>)Data(1, 2, 3, 4, 5).ToLookup(x => x % 2);

            Assert.That(lookup.Count, Is.EqualTo(2));
        }

        #endregion ENDOF: Count
    }
}
