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

using LINQlone.Test.DataObjects;

namespace LINQlone.Test
{
    [TestFixture]
    public class SelectTests : BaseTest
    {
        #region Select

        [Test]
        public void SelectDeferredExecution()
        {
            Data<object> data = DummyData;
            
            data.Select(x => x);
            data.AssertDeferredExecution();
        }

        [Test]
        public void SelectStreamingExecution()
        {
            IEnumerable<object> enumerable = LateThrowingData().Select(x => x);

            Assert.DoesNotThrow(() => enumerable.MoveNext()); // # LateThrownException was not thrown since sequence was not fully enumerated
        }

        [Test]
        public void SelectEmptySource()
        {
            EmptyData.Select(x => x).AssertEmpty();
        }

        [Test]
        public void SelectProjection()
        {
            Data(1, 2, 3, 4, 5).Select(x => x * x).AssertEqual(1, 4, 9, 16, 25);
        }

        [Test]
        public void SelectQueryReuse()
        {
            List<int> data = new List<int> { 1, 2, 3 };
            IEnumerable<int> enumerable = data.Select(x => x * x);

            enumerable.AssertEqual(1, 4, 9);

            data.Add(4);
            enumerable.AssertEqual(1, 4, 9, 16);
        }

        [Test]
        public void SelectSourceArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => NullData.Select(x => x)).WithParameter("source");
        }

        [Test]
        public void SelectSelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => DummyData.Select((Func<object, object>)null)).WithParameter("selector");
        }

        #endregion ENDOF: Select

        #region Select With Index

        [Test]
        public void SelectWithIndexDeferredExecution()
        {
            Data<object> data = DummyData;

            data.Select((x, i) => x);
            data.AssertDeferredExecution();
        }

        [Test]
        public void SelectWithIndexStreamingExecution()
        {
            IEnumerable<object> enumerable = LateThrowingData().Select((x, i) => x);

            Assert.DoesNotThrow(() => enumerable.MoveNext()); // # LateThrownException was not thrown since sequence was not fully enumerated
        }

        [Test]
        public void SelectWithIndexEmptySource()
        {
            EmptyData.Select((x, i) => x).AssertEmpty();
        }

        [Test]
        public void SelectWithIndexProjection()
        {
            Data(1, 2, 3, 4, 5).Select((x, i) => x * i).AssertEqual(0, 2, 6, 12, 20);
        }

        [Test]
        public void SelectWithIndexQueryReuse()
        {
            List<int> data = new List<int> { 1, 2, 3 };
            IEnumerable<int> enumerable = data.Select((x, i) => x * i);

            enumerable.AssertEqual(0, 2, 6);

            data.Add(4);
            enumerable.AssertEqual(0, 2, 6, 12);
        }

        [Test]
        [Explicit("SelectWithIndexOverflow takes very long to execute and slows down entire test process")]
        public void SelectWithIndexOverflow()
        {
            Assert.Throws<OverflowException>(() => IntOverflowEnumerable.Select((x, i) => true).Iterate());
        }

        [Test]
        public void SelectWithIndexSourceArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => NullData.Select((x, i) => x)).WithParameter("source");
        }

        [Test]
        public void SelectWithIndexSelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => DummyData.Select((Func<object, int, object>)null)).WithParameter("selector");
        }

        #endregion ENDOF: Select With Index
    }
}
