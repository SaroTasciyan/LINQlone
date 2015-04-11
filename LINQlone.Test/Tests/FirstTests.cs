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
    public class FirstTests : BaseTest
    {
        #region First

        [Test]
        public void FirstNonEmptySource()
        {
            Assert.That(Data(1, 2, 3).First(), Is.EqualTo(1));
        }

        [Test]
        public void FirstNonEmptyListSource()
        {
            ListData<int> data = ListData(1, 2, 3);

            Assert.That(data.First(), Is.EqualTo(1));
            Assert.That(data.IsEnumerated, Is.False);
        }

        [Test]
        public void FirstEmptySourceInvalidOperation()
        {
            Assert.Throws<InvalidOperationException>(() => EmptyData.First()).WithMessageNoElements();
        }

        [Test]
        public void FirstEmptyListSource()
        {
            ListData<object> data = EmptyListData;

            Assert.Throws<InvalidOperationException>(() => data.First()).WithMessageNoElements();
            Assert.That(data.IsEnumerated, Is.False); // # Should not enumerate due to IList optimization
        }

        [Test]
        public void FirstEnumerationCount()
        {
            Data<int> data = Data(1, 2, 3);

            Assert.That(data.First(), Is.EqualTo(1));
            Assert.That(data.EnumerationCount, Is.EqualTo(1));
        }

        [Test]
        public void FirstSourceArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => NullData.First()).WithParameter("source");
        }

        #endregion ENDOF: First

        #region First With Predicate

        [Test]
        public void FirstWithPredicateNonEmptySource()
        {
            Assert.That(Data(1, 2, 3).First(x => x > 1), Is.EqualTo(2));
        }

        [Test]
        public void FirstWithPredicateEnumerationCount()
        {
            Data<int> data = Data(1, 2, 3);

            Assert.That(data.First(x => x > 1), Is.EqualTo(2));
            Assert.That(data.EnumerationCount, Is.EqualTo(2));
        }

        [Test]
        public void FirstWithPredicateEmptySourceInvalidOperation()
        {
            Assert.Throws<InvalidOperationException>(() => EmptyData.First(x => true)).WithMessageNoMatchingElement();
        }

        [Test]
        public void FirstWithPredicateNoMatchInvalidOperation()
        {
            Assert.Throws<InvalidOperationException>(() => Data(1, 2, 3).First(x => x == 0)).WithMessageNoMatchingElement();
        }

        [Test]
        public void FirstWithPredicateSourceArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => NullData.First(x => true)).WithParameter("source");
        }

        [Test]
        public void FirstWithPredicatePredicateArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => DummyData.First((Func<object, bool>)null)).WithParameter("predicate");
        }

        #endregion ENDOF: First With Predicate
    }
}
