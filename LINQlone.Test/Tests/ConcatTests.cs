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
    public class ConcatTests : BaseTest
    {
        #region Concat

        [Test]
        public void ConcatDeferredExecution()
        {
            Data<object> first = DummyData;
            Data<object> second = DummyData;

            first.Concat(second);
            first.AssertDeferredExecution();
            second.AssertDeferredExecution();
        }

        [Test]
        public void ConcatStreamingExecution()
        {
            IEnumerable<object> enumerable = LateThrowingData().Concat<object>(LateThrowingData());

            Assert.DoesNotThrow(() => enumerable.MoveNext()); // # LateThrownException was not thrown since sequence was not fully enumerated
        }

        [Test]
        public void ConcatEmptySource()
        {
            EmptyData.Concat(EmptyData).AssertEmpty();
        }

        [Test]
        public void ConcatNonEmptySource()
        {
            Data(1, 2, 3).Concat(Data(4, 5, 6)).AssertEqual(1, 2, 3, 4, 5, 6);
        }

        [Test]
        public void ConcatQueryReuse()
        {
            List<int> first = new List<int> { 1, 2 };
            List<int> second = new List<int> { 4, 5 };
            IEnumerable<int> enumerable = first.Concat(second);

            enumerable.AssertEqual(1, 2, 4, 5);

            first.Add(3);
            second.Add(6);
            enumerable.AssertEqual(1, 2, 3, 4, 5, 6);
        }

        [Test]
        public void ConcatFirstArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => NullData.Concat(DummyData)).WithParameter("first");
        }

        [Test]
        public void ConcatSecondArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => DummyData.Concat(NullData)).WithParameter("second");
        }

        #endregion ENDOF: Concat
    }
}
