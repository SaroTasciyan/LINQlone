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
    public class MinTests : BaseTest
    {
        #region Int

        [Test]
        public void MinIntEmptySource()
        {
            Assert.Throws<InvalidOperationException>(() => Data<int>().Min()).WithMessageNoElements();
        }

        [Test]
        public void MinIntNonEmptySource()
        {
            Assert.That(Data<int>(2, 1, 4, 3).Min(), Is.EqualTo(1));
        }

        [Test]
        public void MinIntSourceArgumentNull()
        {
            Data<int> nullData = null;

            Assert.Throws<ArgumentNullException>(() => nullData.Min()).WithParameter("source");
        }

        #endregion ENDOF: Int

        #region Float

        [Test]
        public void MinFloatEmptySource()
        {
            Assert.Throws<InvalidOperationException>(() => Data<float>().Min()).WithMessageNoElements();
        }

        [Test]
        public void MinFloatNonEmptySource()
        {
            Assert.That(Data<float>(2, 1, 4, 3).Min(), Is.EqualTo(1));
        }

        [Test]
        public void MinFloatWithNan()
        {
            Assert.That(Data<float>(Single.NaN, 2, 1, 4, 3, Single.NaN).Min(), Is.EqualTo(Single.NaN));
        }

        [Test]
        public void MinFloatWithNanAsSecond()
        {
            Assert.That(Data<float>(0, Single.NaN, 2, 1, 4, 3, Single.NaN).Min(), Is.EqualTo(Single.NaN));
        }

        [Test]
        public void MinFloatSourceArgumentNull()
        {
            Data<float> nullData = null;

            Assert.Throws<ArgumentNullException>(() => nullData.Min()).WithParameter("source");
        }

        #endregion ENDOF: Float

        #region Double

        [Test]
        public void MinDoubleEmptySource()
        {
            Assert.Throws<InvalidOperationException>(() => Data<double>().Min()).WithMessageNoElements();
        }

        [Test]
        public void MinDoubleNonEmptySource()
        {
            Assert.That(Data<double>(2, 1, 4, 3).Min(), Is.EqualTo(1));
        }

        [Test]
        public void MinDoubleWithNan()
        {
            Assert.That(Data<double>(Double.NaN, 2, 1, 4, 3, Double.NaN).Min(), Is.EqualTo(Double.NaN));
        }

        [Test]
        public void MinDoubleWithNanAsSecond()
        {
            Assert.That(Data<double>(0, Double.NaN, 2, 1, 4, 3, Double.NaN).Min(), Is.EqualTo(Double.NaN));
        }

        [Test]
        public void MinDoubleSourceArgumentNull()
        {
            Data<double> nullData = null;

            Assert.Throws<ArgumentNullException>(() => nullData.Min()).WithParameter("source");
        }

        #endregion ENDOF: Double

        #region Long

        [Test]
        public void MinLongEmptySource()
        {
            Assert.Throws<InvalidOperationException>(() => Data<long>().Min()).WithMessageNoElements();
        }

        [Test]
        public void MinLongNonEmptySource()
        {
            Assert.That(Data<long>(2, 1, 4, 3).Min(), Is.EqualTo(1));
        }

        [Test]
        public void MinLongSourceArgumentNull()
        {
            Data<long> nullData = null;

            Assert.Throws<ArgumentNullException>(() => nullData.Min()).WithParameter("source");
        }

        #endregion ENDOF: Long

        #region Decimal

        [Test]
        public void MinDecimalEmptySource()
        {
            Assert.Throws<InvalidOperationException>(() => Data<decimal>().Min()).WithMessageNoElements();
        }

        [Test]
        public void MinDecimalNonEmptySource()
        {
            Assert.That(Data<decimal>(2, 1, 4, 3).Min(), Is.EqualTo(1));
        }

        [Test]
        public void MinDecimalSourceArgumentNull()
        {
            Data<decimal> nullData = null;

            Assert.Throws<ArgumentNullException>(() => nullData.Min()).WithParameter("source");
        }

        #endregion ENDOF: Decimal

        #region Nullable Int

        [Test]
        public void MinNullableIntEmptySource()
        {
            Assert.That(Data<int?>().Min(), Is.EqualTo(null));
        }

        [Test]
        public void MinNullableIntNonEmptySource()
        {
            Assert.That(Data<int?>(null, 2, 1, 4, 3, null).Min(), Is.EqualTo(1));
        }

        [Test]
        public void MinNullableIntSourceArgumentNull()
        {
            Data<int?> nullData = null;

            Assert.Throws<ArgumentNullException>(() => nullData.Min()).WithParameter("source");
        }

        #endregion ENDOF: Nullable Int

        #region Nullable Float

        [Test]
        public void MinNullableFloatEmptySource()
        {
            Assert.That(Data<float?>().Min(), Is.EqualTo(null));
        }

        [Test]
        public void MinNullableFloatNonEmptySource()
        {
            Assert.That(Data<float?>(null, 2, 1, 4, 3, null).Min(), Is.EqualTo(1));
        }

        [Test]
        public void MinNullableFloatWithNan()
        {
            Assert.That(Data<float?>(null, Single.NaN, 2, 1, 4, 3, Single.NaN, null).Min(), Is.EqualTo(Single.NaN));
        }

        [Test]
        public void MinNullableFloatSourceArgumentNull()
        {
            Data<float?> nullData = null;

            Assert.Throws<ArgumentNullException>(() => nullData.Min()).WithParameter("source");
        }

        #endregion ENDOF: Nullable Float

        #region Nullable Double

        [Test]
        public void MinNullableDoubleEmptySource()
        {
            Assert.That(Data<double?>().Min(), Is.EqualTo(null));
        }

        [Test]
        public void MinNullableDoubleNonEmptySource()
        {
            Assert.That(Data<double?>(null, 2, 1, 4, 3, null).Min(), Is.EqualTo(1));
        }

        [Test]
        public void MinNullableDoubleWithNan()
        {
            Assert.That(Data<double?>(null, Double.NaN, 2, 1, 4, 3, Double.NaN, null).Min(), Is.EqualTo(Double.NaN));
        }

        [Test]
        public void MinNullableDoubleSourceArgumentNull()
        {
            Data<double?> nullData = null;

            Assert.Throws<ArgumentNullException>(() => nullData.Min()).WithParameter("source");
        }

        #endregion ENDOF: Nullable Double

        #region Nullable Long

        [Test]
        public void MinNullableLongEmptySource()
        {
            Assert.That(Data<long?>().Min(), Is.EqualTo(null));
        }

        [Test]
        public void MinNullableLongNonEmptySource()
        {
            Assert.That(Data<long?>(null, 2, 1, 4, 3, null).Min(), Is.EqualTo(1));
        }

        [Test]
        public void MinNullableLongSourceArgumentNull()
        {
            Data<long?> nullData = null;

            Assert.Throws<ArgumentNullException>(() => nullData.Min()).WithParameter("source");
        }

        #endregion ENDOF: Nullable Long

        #region Nullable Decimal

        [Test]
        public void MinNullableDecimalEmptySource()
        {
            Assert.That(Data<decimal?>().Min(), Is.EqualTo(null));
        }

        [Test]
        public void MinNullableDecimalNonEmptySource()
        {
            Assert.That(Data<decimal?>(null, 2, 1, 4, 3, null).Min(), Is.EqualTo(1));
        }

        [Test]
        public void MinNullableDecimalSourceArgumentNull()
        {
            Data<decimal?> nullData = null;

            Assert.Throws<ArgumentNullException>(() => nullData.Min()).WithParameter("source");
        }

        #endregion ENDOF: Nullable Decimal

        #region Int With Selector

        [Test]
        public void MinIntWithSelectorEmptySource()
        {
            Assert.Throws<InvalidOperationException>(() => Data<int>().Min(x => x)).WithMessageNoElements();
        }

        [Test]
        public void MinIntWithSelectorNonEmptySource()
        {
            Assert.That(Data<int>(2, 1, 4, 3).Min(x => x * 2), Is.EqualTo(2));
        }

        [Test]
        public void MinIntWithSelectorSourceArgumentNull()
        {
            Data<int> nullData = null;

            Assert.Throws<ArgumentNullException>(() => nullData.Min(x => x)).WithParameter("source");
        }

        [Test]
        public void MinIntWithSelectorSelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => Data<int>().Min((Func<int, int>)null)).WithParameter("selector");
        }

        #endregion ENDOF: Int With Selector

        #region Float With Selector

        [Test]
        public void MinFloatWithSelectorEmptySource()
        {
            Assert.Throws<InvalidOperationException>(() => Data<float>().Min(x => x)).WithMessageNoElements();
        }

        [Test]
        public void MinFloatWithSelectorNonEmptySource()
        {
            Assert.That(Data<float>(2, 1, 4, 3).Min(x => x * 2), Is.EqualTo(2));
        }

        [Test]
        public void MinFloatWithSelectorWithNan()
        {
            Assert.That(Data<float>(Single.NaN, 2, 1, 4, 3, Single.NaN).Min(x => x * 2), Is.EqualTo(Single.NaN));
        }

        [Test]
        public void MinFloatWithSelectorWithNanAsSecond()
        {
            Assert.That(Data<float>(0, Single.NaN, 2, 1, 4, 3, Single.NaN).Min(x => x * 2), Is.EqualTo(Single.NaN));
        }

        [Test]
        public void MinFloatWithSelectorSourceArgumentNull()
        {
            Data<float> nullData = null;

            Assert.Throws<ArgumentNullException>(() => nullData.Min(x => x)).WithParameter("source");
        }

        [Test]
        public void MinFloatWithSelectorSelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => Data<float>().Min((Func<float, float>)null)).WithParameter("selector");
        }

        #endregion ENDOF: Float With Selector

        #region Double With Selector

        [Test]
        public void MinDoubleWithSelectorEmptySource()
        {
            Assert.Throws<InvalidOperationException>(() => Data<double>().Min(x => x)).WithMessageNoElements();
        }

        [Test]
        public void MinDoubleWithSelectorNonEmptySource()
        {
            Assert.That(Data<double>(2, 1, 4, 3).Min(x => x * 2), Is.EqualTo(2));
        }

        [Test]
        public void MinDoubleWithSelectorWithNan()
        {
            Assert.That(Data<double>(Double.NaN, 2, 1, 4, 3, Double.NaN).Min(x => x * 2), Is.EqualTo(Double.NaN));
        }

        [Test]
        public void MinDoubleWithSelectorWithNanAsSecond()
        {
            Assert.That(Data<double>(0, Double.NaN, 2, 1, 4, 3, Double.NaN).Min(x => x * 2), Is.EqualTo(Double.NaN));
        }

        [Test]
        public void MinDoubleWithSelectorSourceArgumentNull()
        {
            Data<double> nullData = null;

            Assert.Throws<ArgumentNullException>(() => nullData.Min(x => x)).WithParameter("source");
        }

        [Test]
        public void MinDoubleWithSelectorSelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => Data<double>().Min((Func<double, double>)null)).WithParameter("selector");
        }

        #endregion ENDOF: Double With Selector

        #region Long With Selector

        [Test]
        public void MinLongWithSelectorEmptySource()
        {
            Assert.Throws<InvalidOperationException>(() => Data<long>().Min(x => x)).WithMessageNoElements();
        }

        [Test]
        public void MinLongWithSelectorNonEmptySource()
        {
            Assert.That(Data<long>(2, 1, 4, 3).Min(x => x * 2), Is.EqualTo(2));
        }

        [Test]
        public void MinLongWithSelectorSourceArgumentNull()
        {
            Data<long> nullData = null;

            Assert.Throws<ArgumentNullException>(() => nullData.Min(x => x)).WithParameter("source");
        }

        [Test]
        public void MinLongWithSelectorSelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => Data<long>().Min((Func<long, long>)null)).WithParameter("selector");
        }

        #endregion ENDOF: Long With Selector

        #region Decimal With Selector

        [Test]
        public void MinDecimalWithSelectorEmptySource()
        {
            Assert.Throws<InvalidOperationException>(() => Data<decimal>().Min(x => x)).WithMessageNoElements();
        }

        [Test]
        public void MinDecimalWithSelectorNonEmptySource()
        {
            Assert.That(Data<decimal>(2, 1, 4, 3).Min(x => x * 2), Is.EqualTo(2));
        }

        [Test]
        public void MinDecimalWithSelectorSourceArgumentNull()
        {
            Data<decimal> nullData = null;

            Assert.Throws<ArgumentNullException>(() => nullData.Min(x => x)).WithParameter("source");
        }

        [Test]
        public void MinDecimalWithSelectorSelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => Data<decimal>().Min((Func<decimal, decimal>)null)).WithParameter("selector");
        }

        #endregion ENDOF: Decimal With Selector

        #region Nullable Int With Selector

        [Test]
        public void MinNullableIntWithSelectorEmptySource()
        {
            Assert.That(Data<int?>().Min(x => x), Is.EqualTo(null));
        }

        [Test]
        public void MinNullableIntWithSelectorNonEmptySource()
        {
            Assert.That(Data<int?>(null, 2, 1, 4, 3, null).Min(x => x * 2), Is.EqualTo(2));
        }

        [Test]
        public void MinNullableIntWithSelectorSourceArgumentNull()
        {
            Data<int?> nullData = null;

            Assert.Throws<ArgumentNullException>(() => nullData.Min(x => x)).WithParameter("source");
        }

        [Test]
        public void MinNullableIntWithSelectorSelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => Data<int?>().Min((Func<int?, int?>)null)).WithParameter("selector");
        }

        #endregion ENDOF: Nullable Int With Selector

        #region Nullable Float With Selector

        [Test]
        public void MinNullableFloatWithSelectorEmptySource()
        {
            Assert.That(Data<float?>().Min(x => x), Is.EqualTo(null));
        }

        [Test]
        public void MinNullableFloatWithSelectorNonEmptySource()
        {
            Assert.That(Data<float?>(null, 2, 1, 4, 3, null).Min(x => x * 2), Is.EqualTo(2));
        }

        [Test]
        public void MinNullableFloatWithSelectorWithNan()
        {
            Assert.That(Data<float?>(null, Single.NaN, 2, 1, 4, 3, Single.NaN, null).Min(x => x * 2), Is.EqualTo(Single.NaN));
        }

        [Test]
        public void MinNullableFloatWithSelectorSourceArgumentNull()
        {
            Data<float?> nullData = null;

            Assert.Throws<ArgumentNullException>(() => nullData.Min(x => x)).WithParameter("source");
        }

        [Test]
        public void MinNullableFloatWithSelectorSelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => Data<float?>().Min((Func<float?, float?>)null)).WithParameter("selector");
        }

        #endregion ENDOF: Nullable Float With Selector

        #region Nullable Double With Selector

        [Test]
        public void MinNullableDoubleWithSelectorEmptySource()
        {
            Assert.That(Data<double?>().Min(x => x), Is.EqualTo(null));
        }

        [Test]
        public void MinNullableDoubleWithSelectorNonEmptySource()
        {
            Assert.That(Data<double?>(null, 2, 1, 4, 3, null).Min(x => x * 2), Is.EqualTo(2));
        }

        [Test]
        public void MinNullableDoubleWithSelectorWithNan()
        {
            Assert.That(Data<double?>(null, Double.NaN, 2, 1, 4, 3, Double.NaN, null).Min(x => x * 2), Is.EqualTo(Single.NaN));
        }

        [Test]
        public void MinNullableDoubleWithSelectorSourceArgumentNull()
        {
            Data<double?> nullData = null;

            Assert.Throws<ArgumentNullException>(() => nullData.Min((Func<double?, double?>)null)).WithParameter("source");
        }

        [Test]
        public void MinNullableDoubleWithSelectorSelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => Data<double?>().Min((Func<double?, double?>)null)).WithParameter("selector");
        }

        #endregion ENDOF: Nullable Double With Selector

        #region Nullable Long With Selector

        [Test]
        public void MinNullableLongWithSelectorEmptySource()
        {
            Assert.That(Data<long?>().Min(x => x), Is.EqualTo(null));
        }

        [Test]
        public void MinNullableLongWithSelectorNonEmptySource()
        {
            Assert.That(Data<long?>(null, 2, 1, 4, 3, null).Min(x => x * 2), Is.EqualTo(2));
        }

        [Test]
        public void MinNullableLongWithSelectorSourceArgumentNull()
        {
            Data<long?> nullData = null;

            Assert.Throws<ArgumentNullException>(() => nullData.Min(x => x)).WithParameter("source");
        }

        [Test]
        public void MinNullableLongWithSelectorSelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => Data<long?>().Min((Func<long?, long?>)null)).WithParameter("selector");
        }

        #endregion ENDOF: Nullable Long With Selector

        #region Nullable Decimal With Selector

        [Test]
        public void MinNullableDecimalWithSelectorEmptySource()
        {
            Assert.That(Data<decimal?>().Min(x => x), Is.EqualTo(null));
        }

        [Test]
        public void MinNullableDecimalWithSelectorNonEmptySource()
        {
            Assert.That(Data<decimal?>(null, 2, 1, 4, 3, null).Min(x => x * 2), Is.EqualTo(2));
        }

        [Test]
        public void MinNullableDecimalWithSelectorSourceArgumentNull()
        {
            Data<decimal?> nullData = null;

            Assert.Throws<ArgumentNullException>(() => nullData.Min(x => x)).WithParameter("source");
        }

        [Test]
        public void MinNullableDecimalWithSelectorSelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => Data<decimal?>().Min((Func<decimal?, decimal?>)null)).WithParameter("selector");
        }

        #endregion ENDOF: Nullable Decimal With Selector

        #region Generic

        [Test]
        public void MinGenericValueEmptySource()
        {
            Assert.Throws<InvalidOperationException>(() => Data<int>().Min<int>()).WithMessageNoElements();
        }

        [Test]
        public void MinGenericReferenceEmptySource()
        {
            Assert.That(Data<string>().Min(), Is.EqualTo(null));
        }

        [Test]
        public void MinGenericValueNonEmptySource()
        {
            Assert.That(Data<int>(2, 1, 4, 3).Min<int>(), Is.EqualTo(1));
        }

        [Test]
        public void MinGenericReferenceNonEmptySource()
        {
            Assert.That(Data<string>(null, "b", "a", "d", "c", null).Min(), Is.EqualTo("a"));
        }

        [Test]
        public void MinGenericSourceArgumentNull()
        {
            Data<string> nullData = null;

            Assert.Throws<ArgumentNullException>(() => nullData.Min()).WithParameter("source");
        }

        #endregion ENDOF: Generic

        #region Generic With Selector

        [Test]
        public void MinGenericWithSelectorValueEmptySource()
        {
            Assert.Throws<InvalidOperationException>(() => Data<int>().Min<int, int>(x => x)).WithMessageNoElements();
        }

        [Test]
        public void MinGenericWithSelectorReferenceEmptySource()
        {
            Assert.That(Data<string>().Min(x => x), Is.EqualTo(null));
        }

        [Test]
        public void MinGenericWithSelectorValueNonEmptySource()
        {
            Assert.That(Data<int>(2, 1, 4, 3).Min<int, int>(x => x * 2), Is.EqualTo(2));
        }

        [Test]
        public void MinGenericWithSelectorReferenceNonEmptySource()
        {
            Assert.That(Data<string>(null, "b", "a", "d", "c", null).Min(x => x ?? String.Empty), Is.SameAs(String.Empty));
        }

        [Test]
        public void MinGenericWithSelectorSourceArgumentNull()
        {
            Data<string> nullData = null;

            Assert.Throws<ArgumentNullException>(() => nullData.Min(x => x)).WithParameter("source");
        }

        [Test]
        public void MinGenericWithSelectorSelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => DummyData.Min((Func<object, object>)null)).WithParameter("selector");
        }

        #endregion ENDOF: Generic With Selector
    }
}
