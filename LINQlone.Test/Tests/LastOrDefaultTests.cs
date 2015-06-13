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
    public class LastOrDefaultTests : BaseTest
    {
        #region LastOrDefault

        [Test]
        public void LastOrDefaultEmptySource()
        {
            Assert.That(EmptyData.LastOrDefault(), Is.Null);
        }

        [Test]
        public void LastOrDefaultEmptyListSource()
        {
            ListData<object> data = EmptyListData;

            Assert.That(data.LastOrDefault(), Is.Null);
            Assert.That(data.IsEnumerated, Is.False); // # Should not enumerate due to IList optimization
        }

        [Test]
        public void LastOrDefaultNonEmptySource()
        {
            Assert.That(Data(1, 2, 3).LastOrDefault(), Is.EqualTo(3));
        }

        [Test]
        public void LastOrDefaultNonEmptyListSource()
        {
            ListData<int> data = ListData(1, 2, 3);

            Assert.That(data.LastOrDefault(), Is.EqualTo(3));
            Assert.That(data.IsEnumerated, Is.False);
        }

        [Test]
        public void LastOrDefaultSourceArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => NullData.LastOrDefault()).WithParameter("source");
        }

        #endregion ENDOF: LastOrDefault

        #region LastOrDefault With Predicate

        [Test]
        public void LastOrDefaultWithPredicateEmptySource()
        {
            Assert.That(EmptyData.LastOrDefault(x => true), Is.Null);
        }

        [Test]
        public void LastOrDefaultWithPredicateNoMatch()
        {
            Assert.That(Data("A", "B", "C").LastOrDefault(x => x == "D"), Is.Null);
        }

        [Test]
        public void LastOrDefaultWithPredicateNonEmptySource()
        {
            Assert.That(Data(1, 2, 3, 4, 5).LastOrDefault(x => x % 2 == 0), Is.EqualTo(4));
        }

        [Test]
        public void LastOrDefaultWithPredicateSourceArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => NullData.LastOrDefault(x => true)).WithParameter("source");
        }

        [Test]
        public void LastOrDefaultWithPredicatePredicateArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => DummyData.LastOrDefault(null)).WithParameter("predicate");
        }

        #endregion ENDOF: LastOrDefault With Predicate
    }
}
