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
using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;

namespace LINQlone.Test
{
    [TestFixture]
    public class ToListTest : BaseTest
    {
        #region ToList

        [Test]
        public void ToListEmptySource()
        {
            EmptyData.ToList().AssertEmpty();
        }

        [Test]
        public void ToListIntegers()
        {
            Data(1, 2, 3, 4, 5).ToList().AssertEqual(1, 2, 3, 4, 5);
        }

        [Test]
        public void ToListReturnsNewInstance()
        {
            List<int> integers = new List<int>() { 1, 2, 3 };
            List<int> result = integers.ToList();

            Assert.That(result, Is.Not.SameAs(integers));
        }

        [Test]
        public void ToListSourceArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => NullData.ToList()).WithParameter("source");
        }

        #endregion ENDOF: ToList
    }
}
