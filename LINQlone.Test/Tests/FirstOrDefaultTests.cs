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
    public class FirstOrDefaultTests : BaseTest
    {
        #region FirstOrDefault

        [Test]
        public void FirstOrDefaultEmptySource()
        {
            Assert.That(EmptyData.FirstOrDefault(), Is.Null);
        }

        [Test]
        public void FirstOrDefaultEmptyListSource()
        {
            ListData<object> data = EmptyListData;

            Assert.That(data.FirstOrDefault(), Is.Null);
            Assert.That(data.IsEnumerated, Is.False); // # Should not enumerate due to IList optimization
        }

        [Test]
        public void FirstOrDefaultNonEmptySource()
        {
            Assert.That(Data(1, 2, 3).FirstOrDefault(), Is.EqualTo(1));
        }

        [Test]
        public void FirstOrDefaultEnumerationCount()
        {
            Data<int> data = Data(1, 2, 3);

            Assert.That(data.FirstOrDefault(), Is.EqualTo(1));
            Assert.That(data.EnumerationCount, Is.EqualTo(1));
        }

        [Test]
        public void FirstOrDefaultNonEmptyListSource()
        {
            ListData<int> data = ListData(1, 2, 3);

            Assert.That(data.FirstOrDefault(), Is.EqualTo(1));
            Assert.That(data.IsEnumerated, Is.False);
        }

        [Test]
        public void FirstOrDefaultSourceArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => NullData.FirstOrDefault()).WithParameter("source");
        }

        #endregion ENDOF: FirstOrDefault

        #region FirstOrDefault With Predicate

        [Test]
        public void FirstOrDefaultWithPredicateEmptySource()
        {
            Assert.That(EmptyData.FirstOrDefault(x => true), Is.Null);
        }

        [Test]
        public void FirstOrDefaultWithPredicateNoMatch()
        {
            Assert.That(Data("A", "B", "C").FirstOrDefault(x => x == "D"), Is.Null);
        }

        [Test]
        public void FirstOrDefaultWithPredicateNonEmptySource()
        {
            Assert.That(Data(1, 2, 3, 4, 5).FirstOrDefault(x => x % 2 == 0), Is.EqualTo(2));
        }

        [Test]
        public void FirstOrDefaultWithPredicateEnumerationCount()
        {
            Data<int> data = Data(1, 2, 3);

            Assert.That(data.FirstOrDefault(x => x > 1), Is.EqualTo(2));
            Assert.That(data.EnumerationCount, Is.EqualTo(2));
        }

        [Test]
        public void FirstOrDefaultWithPredicateSourceArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => NullData.FirstOrDefault(x => true)).WithParameter("source");
        }

        [Test]
        public void FirstOrDefaultWithPredicatePredicateArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => DummyData.FirstOrDefault(null)).WithParameter("predicate");
        }

        #endregion ENDOF: FirstOrDefault With Predicate
    }
}
