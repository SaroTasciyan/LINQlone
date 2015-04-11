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
using System.Linq;

using NUnit.Framework;

using LINQlone.Test.DataObjects;

namespace LINQlone.Test
{
    [TestFixture]
    public class SingleTests : BaseTest
    {
        #region Single

        [Test]
        public void SingleNonEmptySource()
        {
            Assert.That(Data(1).Single(), Is.EqualTo(1));
        }

        [Test]
        public void SingleNonEmptyListSource()
        {
            ListData<int> data = ListData(1);

            Assert.That(data.Single(), Is.EqualTo(1));
            Assert.That(data.IsEnumerated, Is.False);
        }

        [Test]
        public void SingleEmptySourceInvalidOperation()
        {
            Assert.Throws<InvalidOperationException>(() => EmptyData.Single()).WithMessageNoElements();
        }

        [Test]
        public void SingleEmptyListSourceInvalidOperation()
        {
            ListData<object> data = EmptyListData;

            Assert.Throws<InvalidOperationException>(() => data.Single()).WithMessageNoElements();
            Assert.That(data.IsEnumerated, Is.False); // # Should not enumerate due to IList optimization
        }

        [Test]
        public void SingleMoreThanOneElementInvalidOperation()
        {
            Assert.Throws<InvalidOperationException>(() => Data(1, 2).Single()).WithMessageMoreThanOneElement();
        }

        [Test]
        public void SingleListMoreThanOneElementInvalidOperation()
        {
            Assert.Throws<InvalidOperationException>(() => ListData(1, 2).Single()).WithMessageMoreThanOneElement();
        }

        [Test]
        public void SingleSourceArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => NullData.Single()).WithParameter("source");
        }

        #endregion ENDOF: Single

        #region Single With Predicate

        [Test]
        public void SingleWithPredicateNonEmptySource()
        {
            Assert.That(Data(1, 2, 3).Single(x => x % 2 == 0), Is.EqualTo(2));
        }

        [Test]
        public void SingleWithPredicateEnumerationCount()
        {
            Data<int> data = Data(1, 2, 3, 4);

            Assert.Throws<InvalidOperationException>(() => data.Single(x => x > 1));

            Assert.That(data.EnumerationCount, Is.EqualTo(4 + 1)); // # Enumerates all elements, no optimization
        }

        [Test]
        public void SingleWithPredicateEmptySourceInvalidOperation()
        {
            Assert.Throws<InvalidOperationException>(() => EmptyData.Single(x => true)).WithMessageNoMatchingElement();
        }

        [Test]
        public void SingleWithPredicateNoMatchInvalidOperation()
        {
            Assert.Throws<InvalidOperationException>(() => Data(1, 2, 3).Single(x => x == 0)).WithMessageNoMatchingElement();
        }

        [Test]
        public void SingleWithPredicateMoreThanOneElementInvalidOperation()
        {
            Assert.Throws<InvalidOperationException>(() => Data(1, 2, 3).Single(x => x > 1)).WithMessageMoreThanOneMatchingElement();
        }

        [Test]
        public void SingleWithPredicateSourceArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => NullData.Single(x => true)).WithParameter("source");
        }

        [Test]
        public void SingleWithPredicatePredicateArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => DummyData.Single((Func<object, bool>)null)).WithParameter("predicate");
        }

        #endregion ENDOF: Single With Predicate
    }
}
