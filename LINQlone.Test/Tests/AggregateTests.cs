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
    public class AggregateTests : BaseTest
    {
        #region Aggregate

        [Test]
        public void AggregateEmptySource()
        {
            Assert.Throws<InvalidOperationException>(() => Data<int>().Aggregate((x, y) => 0)).WithMessageNoElements();
        }

        [Test]
        public void AggregateSingleElement()
        {
            Assert.That(Data(1).Aggregate((x, y) => 0), Is.EqualTo(1)); // # Element value is returned without delegation
        }

        [Test]
        public void AggregateNonEmptySource()
        {
            Assert.That(Data(1, 2, 3, 4).Aggregate((x, y) => x * y), Is.EqualTo(24));
        }

        [Test]
        public void AggregateSourceArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => NullData.Aggregate((x, y) => true)).WithParameter("source");
        }

        [Test]
        public void AggregateFuncArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => Data(1, 2, 3).Aggregate(null)).WithParameter("func");
        }

        #endregion ENDOF: Aggregate

        #region Aggregate With Seed

        [Test]
        public void AggregateWithSeedEmptySource()
        {
            Assert.That(Data<int>().Aggregate(1, (x, y) => 0), Is.EqualTo(1)); // # Seed value is returned without delegation
        }

        [Test]
        public void AggregateWithSeedNonEmptySource()
        {
            Assert.That(Data(1, 2, 3, 4).Aggregate(-1, (x, y) => x + y), Is.EqualTo(9));
        }

        [Test]
        public void AggregateWithSeedSourceArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => NullData.Aggregate(new object(), (x, y) => true)).WithParameter("source");
        }

        [Test]
        public void AggregateWithSeedFuncArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => Data(1, 2, 3).Aggregate(0, null)).WithParameter("func");
        }

        #endregion ENDOF: Aggregate With Seed

        #region Aggregate With Seed & Result Selector

        [Test]
        public void AggregateWithSeedAndResultSelectorEmptySource()
        {
            Assert.That(Data<int>().Aggregate(1, (x, y) => 0, x => -1), Is.EqualTo(-1));
        }

        [Test]
        public void AggregateWithSeedAndResultSelectorNonEmptySource()
        {
            Assert.That(Data(1, 2, 3, 4).Aggregate(-1, (x, y) => x + y, x => x * 2), Is.EqualTo(18));
        }

        [Test]
        public void AggregateWithSeedAndResultSelectorSourceArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => NullData.Aggregate(new object(), (x, y) => true, x => true)).WithParameter("source");
        }

        [Test]
        public void AggregateWithSeedAndResultSelectorFuncArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => Data(1, 2, 3).Aggregate(0, null, x => true)).WithParameter("func");
        }

        [Test]
        public void AggregateWithSeedAndResultSelectorResultSelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => Data(1, 2, 3).Aggregate(0, (x ,y) => 0, (Func<int, int>)null)).WithParameter("resultSelector");
        }

        #endregion ENDOF: Aggregate With Seed & Result Selector
    }
}
