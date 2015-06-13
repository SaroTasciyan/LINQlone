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
    public class MaxTests : BaseTest
    {
        #region Int

        [Test]
        public void MaxIntEmptySource()
        {
            Assert.Throws<InvalidOperationException>(() => Data<int>().Max()).WithMessageNoElements();
        }

        [Test]
        public void MaxIntNonEmptySource()
        {
            Assert.That(Data(2, 1, 4, 3).Max(), Is.EqualTo(4));
        }

        [Test]
        public void MaxIntSourceArgumentNull()
        {
            Data<int> nullData = null;

            Assert.Throws<ArgumentNullException>(() => nullData.Max()).WithParameter("source");
        }

        #endregion ENDOF: Int

        #region Float

        [Test]
        public void MaxFloatEmptySource()
        {
            Assert.Throws<InvalidOperationException>(() => Data<float>().Max()).WithMessageNoElements();
        }

        [Test]
        public void MaxFloatNonEmptySource()
        {
            Assert.That(Data<float>(2, 1, 4, 3).Max(), Is.EqualTo(4));
        }

        [Test]
        public void MaxFloatWithNan()
        {
            Assert.That(Data(Single.NaN, 2, 1, 4, 3, Single.NaN).Max(), Is.EqualTo(4));
        }

        [Test]
        public void MaxFloatSourceArgumentNull()
        {
            Data<float> nullData = null;

            Assert.Throws<ArgumentNullException>(() => nullData.Max()).WithParameter("source");
        }

        #endregion ENDOF: Float

        #region Double

        [Test]
        public void MaxDoubleEmptySource()
        {
            Assert.Throws<InvalidOperationException>(() => Data<double>().Max()).WithMessageNoElements();
        }

        [Test]
        public void MaxDoubleNonEmptySource()
        {
            Assert.That(Data<double>(2, 1, 4, 3).Max(), Is.EqualTo(4));
        }

        [Test]
        public void MaxDoubleWithNan()
        {
            Assert.That(Data(Double.NaN, 2, 1, 4, 3, Double.NaN).Max(), Is.EqualTo(4));
        }

        [Test]
        public void MaxDoubleSourceArgumentNull()
        {
            Data<double> nullData = null;

            Assert.Throws<ArgumentNullException>(() => nullData.Max()).WithParameter("source");
        }

        #endregion ENDOF: Double

        #region Long

        [Test]
        public void MaxLongEmptySource()
        {
            Assert.Throws<InvalidOperationException>(() => Data<long>().Max()).WithMessageNoElements();
        }

        [Test]
        public void MaxLongNonEmptySource()
        {
            Assert.That(Data<long>(2, 1, 4, 3).Max(), Is.EqualTo(4));
        }

        [Test]
        public void MaxLongSourceArgumentNull()
        {
            Data<long> nullData = null;

            Assert.Throws<ArgumentNullException>(() => nullData.Max()).WithParameter("source");
        }

        #endregion ENDOF: Long

        #region Decimal

        [Test]
        public void MaxDecimalEmptySource()
        {
            Assert.Throws<InvalidOperationException>(() => Data<decimal>().Max()).WithMessageNoElements();
        }

        [Test]
        public void MaxDecimalNonEmptySource()
        {
            Assert.That(Data<decimal>(2, 1, 4, 3).Max(), Is.EqualTo(4));
        }

        [Test]
        public void MaxDecimalSourceArgumentNull()
        {
            Data<decimal> nullData = null;

            Assert.Throws<ArgumentNullException>(() => nullData.Max()).WithParameter("source");
        }

        #endregion ENDOF: Decimal

        #region Nullable Int

        [Test]
        public void MaxNullableIntEmptySource()
        {
            Assert.That(Data<int?>().Max(), Is.EqualTo(null));
        }

        [Test]
        public void MaxNullableIntNonEmptySource()
        {
            Assert.That(Data<int?>(null, 2, 1, 4, 3, null).Max(), Is.EqualTo(4));
        }

        [Test]
        public void MaxNullableIntSourceArgumentNull()
        {
            Data<int?> nullData = null;

            Assert.Throws<ArgumentNullException>(() => nullData.Max()).WithParameter("source");
        }

        #endregion ENDOF: Nullable Int

        #region Nullable Float

        [Test]
        public void MaxNullableFloatEmptySource()
        {
            Assert.That(Data<float?>().Max(), Is.EqualTo(null));
        }

        [Test]
        public void MaxNullableFloatNonEmptySource()
        {
            Assert.That(Data<float?>(null, 2, 1, 4, 3, null).Max(), Is.EqualTo(4));
        }

        [Test]
        public void MaxNullableFloatWithNan()
        {
            Assert.That(Data<float?>(null, Single.NaN, 2, 1, 4, 3, Single.NaN, null).Max(), Is.EqualTo(4));
        }

        [Test]
        public void MaxNullableFloatSourceArgumentNull()
        {
            Data<float?> nullData = null;

            Assert.Throws<ArgumentNullException>(() => nullData.Max()).WithParameter("source");
        }

        #endregion ENDOF: Nullable Float

        #region Nullable Double

        [Test]
        public void MaxNullableDoubleEmptySource()
        {
            Assert.That(Data<double?>().Max(), Is.EqualTo(null));
        }

        [Test]
        public void MaxNullableDoubleNonEmptySource()
        {
            Assert.That(Data<double?>(null, 2, 1, 4, 3, null).Max(), Is.EqualTo(4));
        }

        [Test]
        public void MaxNullableDoubleWithNan()
        {
            Assert.That(Data<double?>(null, Double.NaN, 2, 1, 4, 3, Double.NaN, null).Max(), Is.EqualTo(4));
        }

        [Test]
        public void MaxNullableDoubleSourceArgumentNull()
        {
            Data<double?> nullData = null;

            Assert.Throws<ArgumentNullException>(() => nullData.Max()).WithParameter("source");
        }

        #endregion ENDOF: Nullable Double

        #region Nullable Long

        [Test]
        public void MaxNullableLongEmptySource()
        {
            Assert.That(Data<long?>().Max(), Is.EqualTo(null));
        }

        [Test]
        public void MaxNullableLongNonEmptySource()
        {
            Assert.That(Data<long?>(null, 2, 1, 4, 3, null).Max(), Is.EqualTo(4));
        }

        [Test]
        public void MaxNullableLongSourceArgumentNull()
        {
            Data<long?> nullData = null;

            Assert.Throws<ArgumentNullException>(() => nullData.Max()).WithParameter("source");
        }

        #endregion ENDOF: Nullable Long

        #region Nullable Decimal

        [Test]
        public void MaxNullableDecimalEmptySource()
        {
            Assert.That(Data<decimal?>().Max(), Is.EqualTo(null));
        }

        [Test]
        public void MaxNullableDecimalNonEmptySource()
        {
            Assert.That(Data<decimal?>(null, 2, 1, 4, 3, null).Max(), Is.EqualTo(4));
        }

        [Test]
        public void MaxNullableDecimalSourceArgumentNull()
        {
            Data<decimal?> nullData = null;

            Assert.Throws<ArgumentNullException>(() => nullData.Max()).WithParameter("source");
        }

        #endregion ENDOF: Nullable Decimal

        #region Int With Selector

        [Test]
        public void MaxIntWithSelectorEmptySource()
        {
            Assert.Throws<InvalidOperationException>(() => Data<int>().Max(x => x)).WithMessageNoElements();
        }

        [Test]
        public void MaxIntWithSelectorNonEmptySource()
        {
            Assert.That(Data(2, 1, 4, 3).Max(x => x * 2), Is.EqualTo(8));
        }

        [Test]
        public void MaxIntWithSelectorSourceArgumentNull()
        {
            Data<int> nullData = null;

            Assert.Throws<ArgumentNullException>(() => nullData.Max(x => x)).WithParameter("source");
        }

        [Test]
        public void MaxIntWithSelectorSelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => Data<int>().Max((Func<int, int>)null)).WithParameter("selector");
        }

        #endregion ENDOF: Int With Selector

        #region Float With Selector

        [Test]
        public void MaxFloatWithSelectorEmptySource()
        {
            Assert.Throws<InvalidOperationException>(() => Data<float>().Max(x => x)).WithMessageNoElements();
        }

        [Test]
        public void MaxFloatWithSelectorNonEmptySource()
        {
            Assert.That(Data<float>(2, 1, 4, 3).Max(x => x * 2), Is.EqualTo(8));
        }

        [Test]
        public void MaxFloatWithSelectorWithNan()
        {
            Assert.That(Data(Single.NaN, 2, 1, 4, 3, Single.NaN).Max(x => x * 2), Is.EqualTo(8));
        }

        [Test]
        public void MaxFloatWithSelectorSourceArgumentNull()
        {
            Data<float> nullData = null;

            Assert.Throws<ArgumentNullException>(() => nullData.Max(x => x)).WithParameter("source");
        }

        [Test]
        public void MaxFloatWithSelectorSelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => Data<float>().Max((Func<float, float>)null)).WithParameter("selector");
        }

        #endregion ENDOF: Float With Selector

        #region Double With Selector

        [Test]
        public void MaxDoubleWithSelectorEmptySource()
        {
            Assert.Throws<InvalidOperationException>(() => Data<double>().Max(x => x)).WithMessageNoElements();
        }

        [Test]
        public void MaxDoubleWithSelectorNonEmptySource()
        {
            Assert.That(Data<double>(2, 1, 4, 3).Max(x => x * 2), Is.EqualTo(8));
        }

        [Test]
        public void MaxDoubleWithSelectorWithNan()
        {
            Assert.That(Data(Double.NaN, 2, 1, 4, 3, Double.NaN).Max(x => x * 2), Is.EqualTo(8));
        }

        [Test]
        public void MaxDoubleWithSelectorSourceArgumentNull()
        {
            Data<double> nullData = null;

            Assert.Throws<ArgumentNullException>(() => nullData.Max(x => x)).WithParameter("source");
        }

        [Test]
        public void MaxDoubleWithSelectorSelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => Data<double>().Max((Func<double, double>)null)).WithParameter("selector");
        }

        #endregion ENDOF: Double With Selector

        #region Long With Selector

        [Test]
        public void MaxLongWithSelectorEmptySource()
        {
            Assert.Throws<InvalidOperationException>(() => Data<long>().Max(x => x)).WithMessageNoElements();
        }

        [Test]
        public void MaxLongWithSelectorNonEmptySource()
        {
            Assert.That(Data<long>(2, 1, 4, 3).Max(x => x * 2), Is.EqualTo(8));
        }

        [Test]
        public void MaxLongWithSelectorSourceArgumentNull()
        {
            Data<long> nullData = null;

            Assert.Throws<ArgumentNullException>(() => nullData.Max(x => x)).WithParameter("source");
        }

        [Test]
        public void MaxLongWithSelectorSelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => Data<long>().Max((Func<long, long>)null)).WithParameter("selector");
        }

        #endregion ENDOF: Long With Selector

        #region Decimal With Selector

        [Test]
        public void MaxDecimalWithSelectorEmptySource()
        {
            Assert.Throws<InvalidOperationException>(() => Data<decimal>().Max(x => x)).WithMessageNoElements();
        }

        [Test]
        public void MaxDecimalWithSelectorNonEmptySource()
        {
            Assert.That(Data<decimal>(2, 1, 4, 3).Max(x => x * 2), Is.EqualTo(8));
        }

        [Test]
        public void MaxDecimalWithSelectorSourceArgumentNull()
        {
            Data<decimal> nullData = null;

            Assert.Throws<ArgumentNullException>(() => nullData.Max(x => x)).WithParameter("source");
        }

        [Test]
        public void MaxDecimalWithSelectorSelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => Data<decimal>().Max((Func<decimal, decimal>)null)).WithParameter("selector");
        }

        #endregion ENDOF: Decimal With Selector

        #region Nullable Int With Selector

        [Test]
        public void MaxNullableIntWithSelectorEmptySource()
        {
            Assert.That(Data<int?>().Max(x => x), Is.EqualTo(null));
        }

        [Test]
        public void MaxNullableIntWithSelectorNonEmptySource()
        {
            Assert.That(Data<int?>(null, 2, 1, 4, 3, null).Max(x => x * 2), Is.EqualTo(8));
        }

        [Test]
        public void MaxNullableIntWithSelectorSourceArgumentNull()
        {
            Data<int?> nullData = null;

            Assert.Throws<ArgumentNullException>(() => nullData.Max(x => x)).WithParameter("source");
        }

        [Test]
        public void MaxNullableIntWithSelectorSelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => Data<int?>().Max((Func<int?, int?>)null)).WithParameter("selector");
        }

        #endregion ENDOF: Nullable Int With Selector

        #region Nullable Float With Selector

        [Test]
        public void MaxNullableFloatWithSelectorEmptySource()
        {
            Assert.That(Data<float?>().Max(x => x), Is.EqualTo(null));
        }

        [Test]
        public void MaxNullableFloatWithSelectorNonEmptySource()
        {
            Assert.That(Data<float?>(null, 2, 1, 4, 3, null).Max(x => x * 2), Is.EqualTo(8));
        }

        [Test]
        public void MaxNullableFloatWithSelectorWithNan()
        {
            Assert.That(Data<float?>(null, Single.NaN, 2, 1, 4, 3, Single.NaN, null).Max(x => x * 2), Is.EqualTo(8));
        }

        [Test]
        public void MaxNullableFloatWithSelectorSourceArgumentNull()
        {
            Data<float?> nullData = null;

            Assert.Throws<ArgumentNullException>(() => nullData.Max(x => x)).WithParameter("source");
        }

        [Test]
        public void MaxNullableFloatWithSelectorSelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => Data<float?>().Max((Func<float?, float?>)null)).WithParameter("selector");
        }

        #endregion ENDOF: Nullable Float With Selector

        #region Nullable Double With Selector

        [Test]
        public void MaxNullableDoubleWithSelectorEmptySource()
        {
            Assert.That(Data<double?>().Max(x => x), Is.EqualTo(null));
        }

        [Test]
        public void MaxNullableDoubleWithSelectorNonEmptySource()
        {
            Assert.That(Data<double?>(null, 2, 1, 4, 3, null).Max(x => x * 2), Is.EqualTo(8));
        }

        [Test]
        public void MaxNullableDoubleWithSelectorWithNan()
        {
            Assert.That(Data<double?>(null, Double.NaN, 2, 1, 4, 3, Double.NaN, null).Max(x => x * 2), Is.EqualTo(8));
        }

        [Test]
        public void MaxNullableDoubleWithSelectorSourceArgumentNull()
        {
            Data<double?> nullData = null;

            Assert.Throws<ArgumentNullException>(() => nullData.Max((Func<double?, double?>)null)).WithParameter("source");
        }

        [Test]
        public void MaxNullableDoubleWithSelectorSelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => Data<double?>().Max((Func<double?, double?>)null)).WithParameter("selector");
        }

        #endregion ENDOF: Nullable Double With Selector

        #region Nullable Long With Selector

        [Test]
        public void MaxNullableLongWithSelectorEmptySource()
        {
            Assert.That(Data<long?>().Max(x => x), Is.EqualTo(null));
        }

        [Test]
        public void MaxNullableLongWithSelectorNonEmptySource()
        {
            Assert.That(Data<long?>(null, 2, 1, 4, 3, null).Max(x => x * 2), Is.EqualTo(8));
        }

        [Test]
        public void MaxNullableLongWithSelectorSourceArgumentNull()
        {
            Data<long?> nullData = null;

            Assert.Throws<ArgumentNullException>(() => nullData.Max(x => x)).WithParameter("source");
        }

        [Test]
        public void MaxNullableLongWithSelectorSelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => Data<long?>().Max((Func<long?, long?>)null)).WithParameter("selector");
        }

        #endregion ENDOF: Nullable Long With Selector

        #region Nullable Decimal With Selector

        [Test]
        public void MaxNullableDecimalWithSelectorEmptySource()
        {
            Assert.That(Data<decimal?>().Max(x => x), Is.EqualTo(null));
        }

        [Test]
        public void MaxNullableDecimalWithSelectorNonEmptySource()
        {
            Assert.That(Data<decimal?>(null, 2, 1, 4, 3, null).Max(x => x * 2), Is.EqualTo(8));
        }

        [Test]
        public void MaxNullableDecimalWithSelectorSourceArgumentNull()
        {
            Data<decimal?> nullData = null;

            Assert.Throws<ArgumentNullException>(() => nullData.Max(x => x)).WithParameter("source");
        }

        [Test]
        public void MaxNullableDecimalWithSelectorSelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => Data<decimal?>().Max((Func<decimal?, decimal?>)null)).WithParameter("selector");
        }

        #endregion ENDOF: Nullable Decimal With Selector

        #region Generic

        [Test]
        public void MaxGenericValueEmptySource()
        {
            Assert.Throws<InvalidOperationException>(() => Data<int>().Max<int>()).WithMessageNoElements();
        }

        [Test]
        public void MaxGenericReferenceEmptySource()
        {
            Assert.That(Data<string>().Max(), Is.EqualTo(null));
        }

        [Test]
        public void MaxGenericValueNonEmptySource()
        {
            Assert.That(Data(2, 1, 4, 3).Max<int>(), Is.EqualTo(4));
        }

        [Test]
        public void MaxGenericReferenceNonEmptySource()
        {
            Assert.That(Data(null, "b", "a", "d", "c", null).Max(), Is.EqualTo("d"));
        }

        [Test]
        public void MaxGenericSourceArgumentNull()
        {
            Data<string> nullData = null;

            Assert.Throws<ArgumentNullException>(() => nullData.Max()).WithParameter("source");
        }

        #endregion ENDOF: Generic

        #region Generic With Selector

        [Test]
        public void MaxGenericWithSelectorValueEmptySource()
        {
            Assert.Throws<InvalidOperationException>(() => Data<int>().Max<int, int>(x => x)).WithMessageNoElements();
        }

        [Test]
        public void MaxGenericWithSelectorReferenceEmptySource()
        {
            Assert.That(Data<string>().Max(x => x), Is.EqualTo(null));
        }

        [Test]
        public void MaxGenericWithSelectorValueNonEmptySource()
        {
            Assert.That(Data(2, 1, 4, 3).Max<int, int>(x => x * 2), Is.EqualTo(8));
        }

        [Test]
        public void MaxGenericWithSelectorReferenceNonEmptySource()
        {
            Assert.That(Data("b", "a", "d", "c").Max(x => String.Format("{0}?", x)), Is.EqualTo("d?"));
        }

        [Test]
        public void MaxGenericWithSelectorReferenceNonEmptyWithNullSource()
        {
            Assert.That(Data(null, "b", "a", "d", "c", null).Max(x => x), Is.EqualTo("d"));
        }

        [Test]
        public void MaxGenericWithSelectorSourceArgumentNull()
        {
            Data<string> nullData = null;

            Assert.Throws<ArgumentNullException>(() => nullData.Max(x => x)).WithParameter("source");
        }

        [Test]
        public void MaxGenericWithSelectorSelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => DummyData.Max((Func<object, object>)null)).WithParameter("selector");
        }

        #endregion ENDOF: Generic With Selector
    }
}
