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
    public class CountTests : BaseTest
    {
        #region Count

        [Test]
        public void CountEmptySource()
        {
            Assert.That(EmptyData.Count(), Is.EqualTo(0));
        }

        [Test]
        public void CountNonEmptySource()
        {
            Assert.That(Data(1, 2, 3).Count(), Is.EqualTo(3));
        }

        [Test]
        public void CountEmptyListSource()
        {
            ListData<object> data = EmptyListData;

            Assert.That(data.Count(), Is.EqualTo(0));
            Assert.That(data.IsEnumerated, Is.False);
        }

        [Test]
        public void CountNonEmptyListSource()
        {
            ListData<int> data = ListData(1, 2, 3);

            Assert.That(data.Count(), Is.EqualTo(3));
            Assert.That(data.IsEnumerated, Is.False);
        }

        [Test]
        [Explicit("CountOverflow takes very long to execute and slows down entire test process")]
        public void CountOverflow()
        {
            Assert.Throws<OverflowException>(() => IntOverflowEnumerable.Count());
        }

        [Test]
        public void CountSourceArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => NullData.Count()).WithParameter("source");
        }

        #endregion ENDOF: Count

        #region Count With Predicate

        [Test]
        public void CountWithPredicateEmptySource()
        {
            Assert.That(EmptyData.Count(x => true), Is.EqualTo(0));
        }

        [Test]
        public void CountWithPredicateNonEmptySource()
        {
            Assert.That(Data(1, 2, 3, 4, 5).Count(x => x > 2), Is.EqualTo(3));
        }

        [Test]
        [Explicit("CountWithPredicateOverflow takes very long to execute and slows down entire test process")]
        public void CountWithPredicateOverflow()
        {
            Assert.Throws<OverflowException>(() => IntOverflowEnumerable.Count(x => true));
        }

        [Test]
        public void CountWithPredicateSourceArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => NullData.Count(x => true)).WithParameter("source");
        }

        [Test]
        public void CountWithPredicatePredicateArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => DummyData.Count(null)).WithParameter("predicate");
        }

        #endregion ENDOF: Count With Predicate
    }
}
