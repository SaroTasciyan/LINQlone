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
    public class ElementAtTests : BaseTest
    {
        #region ElementAt

        [Test]
        public void ElementAtNonEmptySource()
        {
            Assert.That(Data(1, 2, 3).ElementAt(1), Is.EqualTo(2));
        }

        [Test]
        public void ElementAtNonEmptyListSource()
        {
            ListData<int> data = ListData(1, 2, 3);

            Assert.That(data.ElementAt(0), Is.EqualTo(1));
            Assert.That(data.IsEnumerated, Is.False);
        }

        [Test]
        public void ElementAtEmptySourceArgumentOutOfRange()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => EmptyData.ElementAt(0)).WithParameter("index");
        }

        [Test]
        public void ElementAtNegativeIndexArgumentOutOfRange()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => EmptyData.ElementAt(-5)).WithParameter("index");
        }

        [Test]
        public void ElementAtOutOfBoundsArgumentOutOfRange()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => EmptyData.ElementAt(5)).WithParameter("index");
        }

        [Test]
        public void ElementAtSourceArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => NullData.ElementAt(0)).WithParameter("source");
        }

        #endregion ENDOF: ElementAt
    }
}
