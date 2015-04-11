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
    public class OfTypeTests : BaseTest
    {
        #region OfType

        [Test]
        public void OfTypeDeferredExecution()
        {
            Data<object> data = DummyData;

            data.OfType<int>();
            data.AssertDeferredExecution();
        }

        [Test]
        public void OfTypeStreamingExecution()
        {
            IEnumerable<object> enumerable = LateThrowingData().OfType<object>();

            Assert.DoesNotThrow(() => enumerable.MoveNext()); // # LateThrownException was not thrown since sequence was not fully enumerated
        }

        [Test]
        public void OfTypeEmptySource()
        {
            EmptyData.OfType<object>().AssertEmpty();
        }

        [Test]
        public void OfTypeNotCastable()
        {
            Data(1).OfType<double>().AssertEmpty();
        }

        [Test]
        public void OfTypeBoxingReference()
        {
            Data("Carmack", "Romero").OfType<object>().AssertEqual<object>("Carmack", "Romero");
        }

        [Test]
        public void OfTypeUnboxingReference()
        {
            Data<object>("Carmack", "Romero").OfType<string>().AssertEqual("Carmack", "Romero");
        }

        [Test]
        public void OfTypeBoxingValue()
        {
            Data(1).OfType<object>().AssertEqual<object>(1);
        }

        [Test]
        public void OfTypeUnboxingValue()
        {
            Data<object>(1).OfType<int>().AssertEqual(1);
        }

        [Test]
        public void OfTypeQueryReuse()
        {
            List<int> data = new List<int> { 1, 2 };
            IEnumerable<object> enumerable = data.OfType<object>();

            enumerable.AssertEqual<object>(1, 2);

            data.Add(3);
            enumerable.AssertEqual<object>(1, 2, 3);
        }

        [Test]
        public void OfTypeSourceArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => NullData.OfType<object>()).WithParameter("source");
        }

        #endregion ENDOF: OfType
    }
}
