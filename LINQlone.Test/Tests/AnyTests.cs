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
    public class AnyTests : BaseTest
    {
        #region Any

        [Test]
        public void AnyEmptySource()
        {
            Assert.That(EmptyData.Any(), Is.False);
        }

        [Test]
        public void AnyNonEmptySource()
        {
            Assert.That(Data(1, 2, 3).Any(), Is.True);
        }

        [Test]
        public void AnyEnumerationCount()
        {
            Data<int> data = Data(1, 2, 3);

            Assert.That(data.Any(), Is.True);
            Assert.That(data.EnumerationCount, Is.EqualTo(1));
        }

        [Test]
        public void AnySourceArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => NullData.Any()).WithParameter("source");
        }

        #endregion ENDOF: Any

        #region Any With Predicate

        [Test]
        public void AnyWithPredicateEmptySource()
        {
            Assert.That(EmptyData.Any(x => true), Is.False);
        }

        [Test]
        public void AnyWithPredicateNonEmptySource()
        {
            Assert.That(Data(1, 2, 3).Any(x => true), Is.True);
        }

        [Test]
        public void AnyWithPredicateDoesNotMatch()
        {
            Assert.That(Data(1, 2, 3).Any(x => x > 5), Is.False);
        }

        [Test]
        public void AnyWithPredicateMatches()
        {
            Assert.That(Data(1, 2, 3).Any(x => x > 2), Is.True);
        }

        [Test]
        public void AnyWithPredicateEnumerationCount()
        {
            Data<int> data = Data(1, 2, 3);

            Assert.That(data.Any(x => x == 1), Is.True);
            Assert.That(data.EnumerationCount, Is.EqualTo(1));
        }

        [Test]
        public void AnyWithPredicateSourceArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => NullData.Any(x => true)).WithParameter("source");
        }

        [Test]
        public void AnyWithPredicatePredicateArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => DummyData.Any(null)).WithParameter("predicate");
        }

        #endregion ENDOF: Any With Predicate
    }
}
