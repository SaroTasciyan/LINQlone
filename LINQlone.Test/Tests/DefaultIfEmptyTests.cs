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
    public class DefaultIfEmptyTests : BaseTest
    {
        #region DefaultIfEmpty

        [Test]
        public void DefaultIfEmptyDeferredExecution()
        {
            Data<object> data = DummyData;

            data.DefaultIfEmpty();
            data.AssertDeferredExecution();
        }

        [Test]
        public void DefaultIfEmptyStreamingExecution()
        {
            IEnumerable<object> enumerable = LateThrowingData().DefaultIfEmpty();

            Assert.DoesNotThrow(() => enumerable.MoveNext()); // # LateThrownException was not thrown since sequence was not fully enumerated
        }

        [Test]
        public void DefaultIfEmptyEmptySource()
        {
            EmptyData.DefaultIfEmpty().AssertNotEmpty();
        }

        [Test]
        public void DefaultIfEmptyNonEmptySource()
        {
            Data(1, 2, 3).DefaultIfEmpty().AssertEqual(1, 2, 3);
        }

        [Test]
        public void DefaultIfEmptyQueryReuse()
        {
            List<int> data = new List<int>();
            IEnumerable<int> enumerable = data.DefaultIfEmpty();

            enumerable.AssertEqual(0);

            data.Add(1);
            enumerable.AssertEqual(1);
        }

        [Test]
        public void DefaultIfEmptySourceArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => NullData.DefaultIfEmpty()).WithParameter("source");
        }

        #endregion ENDOF: DefaultIfEmpty

        #region DefaultIfEmpty With Value

        [Test]
        public void DefaultIfEmptyWithDefaultValueDeferredExecution()
        {
            Data<object> data = DummyData;

            data.DefaultIfEmpty(new object());
            data.AssertDeferredExecution();
        }

        [Test]
        public void DefaultIfEmptyWithDefaultValueStreamingExecution()
        {
            IEnumerable<object> enumerable = LateThrowingData().DefaultIfEmpty(new object());

            Assert.DoesNotThrow(() => enumerable.MoveNext()); // # LateThrownException was not thrown since sequence was not fully enumerated
        }

        [Test]
        public void DefaultIfEmptyWithDefaultValueEmptySource()
        {
            object @object = new object();

            EmptyData.DefaultIfEmpty(@object).AssertEqual(@object);
        }

        [Test]
        public void DefaultIfEmptyWithDefaultValueNonEmptySource()
        {
            Data(1, 2, 3).DefaultIfEmpty(0).AssertEqual(1, 2, 3);
        }

        [Test]
        public void DefaultIfEmptyWithDefaultValueQueryReuse()
        {
            List<int> data = new List<int>();
            IEnumerable<int> enumerable = data.DefaultIfEmpty(-1);

            enumerable.AssertEqual(-1);

            data.Add(1);
            enumerable.AssertEqual(1);
        }

        [Test]
        public void DefaultIfEmptyWithDefaultValueDefaulValueArgumentNull()
        {
            Assert.DoesNotThrow(() => DummyData.DefaultIfEmpty((object)null));
        }

        [Test]
        public void DefaultIfEmptyWithDefaultValueSourceArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => NullData.DefaultIfEmpty(new object())).WithParameter("source");
        }

        #endregion ENDOF: DefaultIfEmpty With Predicate
    }
}
