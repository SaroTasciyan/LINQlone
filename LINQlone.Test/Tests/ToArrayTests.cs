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
    public class ToArrayTests : BaseTest
    {
        #region ToArray

        [Test]
        public void ToArrayEmptySource()
        {
            EmptyData.ToArray().AssertEmpty();
        }

        [Test]
        public void ToArrayEmptyListSource()
        {
            ListData<object> data = EmptyListData;

            EmptyListData.ToArray().AssertEmpty();
            Assert.That(data.IsEnumerated, Is.False); // # Should not enumerate due to ICollection optimization
        }

        [Test]
        public void ToArrayNonEmptySourceTrue()
        {
            Data(1, 2, 3).ToArray().AssertEqual(1, 2, 3);
        }

        [Test]
        [Explicit("ToArrayOverflow takes very long to execute and slows down entire test process")]
        public void ToArrayOverflow()
        {
            Assert.Throws<OverflowException>(() => IntOverflowEnumerable.ToArray());
        }

        [Test]
        public void ToArrayNonEmptyListSource()
        {
            ListData<int> data = ListData(1, 2, 3);

            data.ToArray().AssertEqual(1, 2, 3);
            Assert.That(data.IsEnumerated, Is.False);
        }

        [Test]
        public void ToArraySourceArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => NullData.ToArray()).WithParameter("source");
        }

        #endregion ENDOF: ToArray
    }
}
