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
    public class ElementAtOrDefaultTests : BaseTest
    {
        #region ElementAtOrDefault

        [Test]
        public void ElementAtOrDefaultEmptySource()
        {
            Assert.That(EmptyData.ElementAtOrDefault(0), Is.Null);
        }

        [Test]
        public void ElementAtOrDefaultEmptyListSource()
        {
            ListData<object> data = EmptyListData;

            Assert.That(data.ElementAtOrDefault(0), Is.Null);
            Assert.That(data.IsEnumerated, Is.False); // # Should not enumerate due to IList optimization
        }

        [Test]
        public void ElementAtOrDefaultNegativeIndex()
        {
            Assert.That(Data(1, 2, 3).ElementAtOrDefault(-5), Is.EqualTo(0));
        }

        [Test]
        public void ElementAtOrDefaultListNegativeIndex()
        {
            ListData<int> data = ListData(1, 2, 3);

            Assert.That(data.ElementAtOrDefault(-5), Is.EqualTo(0));
            Assert.That(data.IsEnumerated, Is.False);
        }

        [Test]
        public void ElementAtOrDefaultOutOfBounds()
        {
            Assert.That(Data(1, 2, 3).ElementAtOrDefault(5), Is.EqualTo(0));
        }

        [Test]
        public void ElementAtOrDefaultListOutOfBounds()
        {
            ListData<int> data = ListData(1, 2, 3);

            Assert.That(data.ElementAtOrDefault(5), Is.EqualTo(0));
            Assert.That(data.IsEnumerated, Is.False);
        }

        [Test]
        public void ElementAtOrDefaultNonEmptySource()
        {
            Assert.That(Data(1, 2, 3).ElementAtOrDefault(0), Is.EqualTo(1));
        }

        [Test]
        public void ElementAtOrDefaultNonEmptyListSource()
        {
            ListData<int> data = ListData(1, 2, 3);

            Assert.That(data.ElementAtOrDefault(0), Is.EqualTo(1));
            Assert.That(data.IsEnumerated, Is.False);
        }

        [Test]
        public void ElementAtOrDefaultSourceArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => NullData.ElementAtOrDefault(0)).WithParameter("source");
        }

        #endregion ENDOF: ElementAtOrDefault
    }
}
