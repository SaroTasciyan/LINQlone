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

namespace LINQlone.Test
{
    [TestFixture]
    public class LongCountTests : BaseTest
    {
        #region LongCount

        [Test]
        public void LongCountEmptySource()
        {
            Assert.That(EmptyData.LongCount(), Is.EqualTo(0));
        }

        [Test]
        public void LongCountNonEmptySource()
        {
            Assert.That(Data(1, 2, 3).LongCount(), Is.EqualTo(3));
        }

        [Test]
        [Explicit("LongCountOverflow takes very long to execute and slows down entire test process")]
        public void LongCountOverflow()
        {
            Assert.Throws<OverflowException>(() => LongOverflowEnumerable.LongCount());
        }

        [Test]
        public void LongCountSourceArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => NullData.LongCount()).WithParameter("source");
        }

        #endregion ENDOF: LongCount

        #region LongCount With Predicate

        [Test]
        public void LongCountWithPredicateEmptySource()
        {
            Assert.That(EmptyData.LongCount(x => true), Is.EqualTo(0));
        }

        [Test]
        public void LongCountWithPredicateNonEmptySource()
        {
            Assert.That(Data(1, 2, 3, 4, 5).LongCount(x => x > 2), Is.EqualTo(3));
        }

        [Test]
        [Explicit("LongCountWithPredicateOverflow takes very long to execute and slows down entire test process")]
        public void LongCountWithPredicateOverflow()
        {
            Assert.Throws<OverflowException>(() => LongOverflowEnumerable.LongCount(x => true));
        }

        [Test]
        public void LongCountWithPredicateSourceArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => NullData.LongCount(x => true)).WithParameter("source");
        }

        [Test]
        public void LongCountWithPredicatePredicateArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => DummyData.LongCount((Func<object, bool>)null)).WithParameter("predicate");
        }

        #endregion ENDOF: LongCount With Predicate
    }
}
