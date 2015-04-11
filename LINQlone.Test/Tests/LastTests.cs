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
    public class LastTests : BaseTest
    {
        #region Last

        [Test]
        public void LastNonEmptySource()
        {
            Assert.That(Data(1, 2, 3).Last(), Is.EqualTo(3));
        }

        [Test]
        public void LastNonEmptyListSource()
        {
            ListData<int> data = ListData(1, 2, 3);

            Assert.That(data.Last(), Is.EqualTo(3));
            Assert.That(data.IsEnumerated, Is.False);
        }

        [Test]
        public void LastEmptySourceInvalidOperation()
        {
            Assert.Throws<InvalidOperationException>(() => EmptyData.Last()).WithMessageNoElements();
        }

        [Test]
        public void LastEmptyListSource()
        {
            ListData<object> data = EmptyListData;

            Assert.Throws<InvalidOperationException>(() => data.Last()).WithMessageNoElements();
            Assert.That(data.IsEnumerated, Is.False); // # Should not enumerate due to IList optimization
        }

        [Test]
        public void LastSourceArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => NullData.Last()).WithParameter("source");
        }

        #endregion ENDOF: Last

        #region Last With Predicate

        [Test]
        public void LastWithPredicateNonEmptySource()
        {
            Assert.That(Data(1, 2, 3).Last(x => x < 3), Is.EqualTo(2));
        }

        [Test]
        public void LastWithPredicateEmptySourceInvalidOperation()
        {
            Assert.Throws<InvalidOperationException>(() => EmptyData.Last(x => true)).WithMessageNoMatchingElement();
        }

        [Test]
        public void LastWithPredicateNoMatchInvalidOperation()
        {
            Assert.Throws<InvalidOperationException>(() => Data(1, 2, 3).Last(x => x == 0)).WithMessageNoMatchingElement();
        }

        [Test]
        public void LastWithPredicateSourceArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => NullData.Last(x => true)).WithParameter("source");
        }

        [Test]
        public void LastWithPredicatePredicateArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => DummyData.Last((Func<object, bool>)null)).WithParameter("predicate");
        }

        #endregion ENDOF: Last With Predicate
    }
}
