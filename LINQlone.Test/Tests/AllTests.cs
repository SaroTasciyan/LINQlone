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
    public class AllTests : BaseTest
    {
        #region All

        [Test]
        public void AllEmptySource()
        {
            Assert.That(EmptyData.All(x => true), Is.True);
        }

        [Test]
        public void AllNonEmptySource()
        {
            Assert.That(Data(1, 2, 3).All(x => true), Is.True);
        }

        [Test]
        public void AllDoesNotMatch()
        {
            Assert.That(Data(1, 2, 3).All(x => x < 3), Is.False);
        }

        [Test]
        public void AllMatches()
        {
            Assert.That(Data(1, 2, 3).All(x => x > 0), Is.True);
        }

        [Test]
        public void AllEnumerationCount()
        {
            Data<int> data = Data(1, 2, 3);

            Assert.That(data.All(x => x == 1), Is.False);
            Assert.That(data.EnumerationCount, Is.EqualTo(2));
        }

        [Test]
        public void AllSourceArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => NullData.All(x => true)).WithParameter("source");
        }

        [Test]
        public void AllPredicateArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => DummyData.All((Func<object, bool>)null)).WithParameter("predicate");
        }

        #endregion ENDOF: All
    }
}
