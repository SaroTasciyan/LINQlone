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
    public class SingleOrDefaultTests : BaseTest
    {
        #region SingleOrDefault

        [Test]
        public void SingleOrDefaultEmptySource()
        {
            Assert.That(EmptyData.SingleOrDefault(), Is.Null);
        }

        [Test]
        public void SingleOrDefaultEmptyListSource()
        {
            ListData<object> data = EmptyListData;

            Assert.That(data.SingleOrDefault(), Is.Null);
            Assert.That(data.IsEnumerated, Is.False); // # Should not enumerate due to IList optimization
        }

        [Test]
        public void SingleOrDefaultNonEmptySource()
        {
            Assert.That(Data(1).SingleOrDefault(), Is.EqualTo(1));
        }

        [Test]
        public void SingleOrDefaultNonEmptyListSource()
        {
            ListData<int> data = ListData(1);

            Assert.That(data.SingleOrDefault(), Is.EqualTo(1));
            Assert.That(data.IsEnumerated, Is.False);
        }

        [Test]
        public void SingleOrDefaultEnumerationCount()
        {
            Data<int> data = Data(1, 2, 3);

            Assert.Throws<InvalidOperationException>(() => data.SingleOrDefault());
            
            Assert.That(data.EnumerationCount, Is.EqualTo(2));
        }

        [Test]
        public void SingleOrDefaultMoreThanOneElementInvalidOperation()
        {
            Assert.Throws<InvalidOperationException>(() => Data(1, 2).SingleOrDefault()).WithMessageMoreThanOneElement();
        }

        [Test]
        public void SingleOrDefaultMoreThanOneElementListInvalidOperation()
        {
            Assert.Throws<InvalidOperationException>(() => ListData(1, 2).SingleOrDefault()).WithMessageMoreThanOneElement();
        }

        [Test]
        public void SingleOrDefaultSourceArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => NullData.SingleOrDefault()).WithParameter("source");
        }

        #endregion ENDOF: SingleOrDefault

        #region SingleOrDefault With Predicate

        [Test]
        public void SingleOrDefaultWithPredicateEmptySource()
        {
            Assert.That(EmptyData.SingleOrDefault(x => true), Is.Null);
        }

        [Test]
        public void SingleOrDefaultWithPredicateNonEmptySource()
        {
            Assert.That(Data(1, 2).SingleOrDefault(x => x % 2 == 0), Is.EqualTo(2));
        }

        [Test]
        public void SingleOrDefaultWithPredicateNoMatch()
        {
            Assert.That(Data("A", "B", "C").SingleOrDefault(x => x == "D"), Is.Null);
        }

        [Test]
        public void SingleOrDefaultWithPredicateEnumerationCount()
        {
            Data<int> data = Data(1, 2, 3, 4);

            Assert.Throws<InvalidOperationException>(() => data.SingleOrDefault(x => x > 1));

            Assert.That(data.EnumerationCount, Is.EqualTo(4 + 1)); // # Enumerates all elements, no optimization
        }

        [Test]
        public void SingleOrDefaultWithPredicateMoreThanOneMatchingElementInvalidOperation()
        {
            Assert.Throws<InvalidOperationException>(() => Data(1, 2, 3).SingleOrDefault(x => x > 1)).WithMessageMoreThanOneMatchingElement();
        }

        [Test]
        public void SingleOrDefaultWithPredicatePredicateArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => DummyData.SingleOrDefault(null)).WithParameter("predicate");
        }

        [Test]
        public void SingleOrDefaultWithPredicateSourceArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => NullData.SingleOrDefault(x => true)).WithParameter("source");
        }

        #endregion ENDOF: SingleOrDefault With Predicate
    }
}
