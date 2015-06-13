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
    public class CastTests : BaseTest
    {
        #region Cast

        [Test]
        public void CastDeferredExecution()
        {
            Data<object> data = DummyData;

            data.Cast<int>();
            data.AssertDeferredExecution();
        }

        [Test]
        public void CastStreamingExecution()
        {
            IEnumerable<string> enumerable = LateThrowingData().Cast<string>();

            Assert.DoesNotThrow(() => enumerable.MoveNext()); // # LateThrownException was not thrown since sequence was not fully enumerated
        }

        [Test]
        public void CastEmptySource()
        {
            EmptyData.Cast<string>().AssertEmpty();
        }

        [Test]
        public void CastBoxingReference()
        {
            Data("Carmack", "Romero").Cast<object>().AssertEqual<object>("Carmack", "Romero");
        }

        [Test]
        public void CastUnboxingReference()
        {
            Data<object>("Carmack", "Romero").Cast<string>().AssertEqual("Carmack", "Romero");
        }

        [Test]
        public void CastBoxingValue()
        {
            Data(1).Cast<object>().AssertEqual<object>(1);
        }

        [Test]
        public void CastUnboxingValue()
        {
            Data<object>(1).Cast<int>().AssertEqual(1);
        }

        [Test]
        public void CastSameTypeOptimization()
        {
            Data<object> data = DummyData;

            data.Cast<object>(); // # Not redundant; casting to same type in order to assert data is not enumerated
            
            Assert.That(data.IsEnumerated, Is.False);
        }

        [Test]
        public void CastQueryReuse()
        {
            List<int> data = new List<int> { 1, 2 };
            IEnumerable<object> enumerable = data.Cast<object>();

            enumerable.AssertEqual(1, 2);

            data.Add(3);
            enumerable.AssertEqual(1, 2, 3);
        }

        [Test]
        public void CastSourceArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => NullData.Cast<string>()).WithParameter("source");
        }

        [Test]
        public void CastInvalidCast()
        {
            Assert.Throws<InvalidCastException>(() => Data('c').Cast<DateTime>().Iterate());
        }

        #endregion ENDOF: Cast
    }
}
