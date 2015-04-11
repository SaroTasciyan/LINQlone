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

using System.Linq;

using NUnit.Framework;

using LINQlone.Test.DataObjects;

namespace LINQlone.Test
{
    [TestFixture]
    public class AsEnumerableTests : BaseTest
    {
        #region AsEnumerable

        [Test]
        public void AsEnumerableReturnsSource()
        {
            Data<object> data = EmptyData;

            Assert.That(data.AsEnumerable(), Is.SameAs(data));
            Assert.That(data.IsEnumerated, Is.False);
        }

        [Test]
        public void AsEnumerableSourceArgumentNull()
        {
            Assert.DoesNotThrow(() => NullData.AsEnumerable());
        }

        #endregion ENDOF: AsEnumerable
    }
}
