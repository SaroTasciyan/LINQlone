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
    public class AverageTests : BaseTest
    {
        #region Int

        [Test]
        public void AverageIntEmptySource()
        {
            Assert.Throws<InvalidOperationException>(() => Data<int>().Average()).WithMessageNoElements();
        }

        [Test]
        public void AverageIntNonEmptySource()
        {
            Assert.That(Data<int>(1, 2, 3, 4).Average(), Is.EqualTo(2.5D));
        }

        [Test]
        public void AverageIntNoOverflow()
        {
            Assert.That(Data<int>(Int32.MaxValue, 1).Average(), Is.EqualTo(1073741824.0D));
        }

        [Test]
        public void AverageIntSourceArgumentNull()
        {
            Data<int> nullData = null;

            Assert.Throws<ArgumentNullException>(() => nullData.Average()).WithParameter("source");
        }

        #endregion ENDOF: Int

        #region Float

        [Test]
        public void AverageFloatEmptySource()
        {
            Assert.Throws<InvalidOperationException>(() => Data<float>().Average()).WithMessageNoElements();
        }

        [Test]
        public void AverageFloatNonEmptySource()
        {
            Assert.That(Data<float>(1, 2, 3, 4).Average(), Is.EqualTo(2.5D));
        }

        [Test]
        public void AverageFloatMaxValues()
        {
            Assert.That(Data<float>(Single.MaxValue, Single.MaxValue).Average(), Is.EqualTo(3.40282347E+38F));
        }

        [Test]
        public void AverageFloatNoOverflow()
        {
            Assert.That(Data<float>(Single.MaxValue, 1).Average(), Is.EqualTo(1.70141173E+38F));
        }

        [Test]
        public void AverageFloatWithNan()
        {
            Assert.That(Data<float>(1, 2, Single.NaN, 3, 4).Average(), Is.EqualTo(Single.NaN));
        }

        [Test]
        public void AverageFloatSourceArgumentNull()
        {
            Data<float> nullData = null;

            Assert.Throws<ArgumentNullException>(() => nullData.Average()).WithParameter("source");
        }

        #endregion ENDOF: Float

        #region Double

        [Test]
        public void AverageDoubleEmptySource()
        {
            Assert.Throws<InvalidOperationException>(() => Data<double>().Average()).WithMessageNoElements();
        }

        [Test]
        public void AverageDoubleNonEmptySource()
        {
            Assert.That(Data<double>(1, 2, 3, 4).Average(), Is.EqualTo(2.5D));
        }

        [Test]
        public void AverageDoubleNoOverflow()
        {
            Assert.That(Data<double>(Double.MaxValue, 1).Average(), Is.EqualTo(8.9884656743115785E+307D));
        }

        [Test]
        public void AverageDoubleWithNan()
        {
            Assert.That(Data<double>(1, 2, Double.NaN, 3, 4).Average(), Is.EqualTo(Double.NaN));
        }

        [Test]
        public void AverageDoubleSourceArgumentNull()
        {
            Data<double> nullData = null;

            Assert.Throws<ArgumentNullException>(() => nullData.Average()).WithParameter("source");
        }

        #endregion ENDOF: Double

        #region Long

        [Test]
        public void AverageLongEmptySource()
        {
            Assert.Throws<InvalidOperationException>(() => Data<long>().Average()).WithMessageNoElements();
        }

        [Test]
        public void AverageLongNonEmptySource()
        {
            Assert.That(Data<long>(1, 2, 3, 4).Average(), Is.EqualTo(2.5D));
        }

        [Test]
        public void AverageLongOverflow()
        {
            Assert.Throws<OverflowException>(() => Data<long>(Int64.MaxValue, 1).Average());
        }

        [Test]
        public void AverageLongSourceArgumentNull()
        {
            Data<long> nullData = null;

            Assert.Throws<ArgumentNullException>(() => nullData.Average()).WithParameter("source");
        }

        #endregion ENDOF: Long

        #region Decimal

        [Test]
        public void AverageDecimalEmptySource()
        {
            Assert.Throws<InvalidOperationException>(() => Data<decimal>().Average()).WithMessageNoElements();
        }

        [Test]
        public void AverageDecimalNonEmptySource()
        {
            Assert.That(Data<decimal>(1, 2, 3, 4).Average(), Is.EqualTo(2.5D));
        }

        [Test]
        public void AverageDecimalOverflow()
        {
            Assert.Throws<OverflowException>(() => Data<decimal>(Decimal.MaxValue, 1).Average());
        }

        [Test]
        public void AverageDecimalSourceArgumentNull()
        {
            Data<decimal> nullData = null;

            Assert.Throws<ArgumentNullException>(() => nullData.Average()).WithParameter("source");
        }

        #endregion ENDOF: Decimal

        #region Nullable Int

        [Test]
        public void AverageNullableIntEmptySource()
        {
            Assert.That(Data<int?>().Average(), Is.EqualTo(null));
        }

        [Test]
        public void AverageNullableIntNonEmptySource()
        {
            Assert.That(Data<int?>(1, 2, null, 3, 4).Average(), Is.EqualTo(2.5D));
        }

        [Test]
        public void AverageNullableIntNoOverflow()
        {
            Assert.That(Data<int?>(Int32.MaxValue, 1).Average(), Is.EqualTo(1073741824.0D));
        }

        [Test]
        public void AverageNullableIntSourceArgumentNull()
        {
            Data<int?> nullData = null;

            Assert.Throws<ArgumentNullException>(() => nullData.Average()).WithParameter("source");
        }

        #endregion ENDOF: Nullable Int
        
        #region Nullable Float

        [Test]
        public void AverageNullableFloatEmptySource()
        {
            Assert.That(Data<float?>().Average(), Is.EqualTo(null));
        }

        [Test]
        public void AverageNullableFloatNonEmptySource()
        {
            Assert.That(Data<float?>(1, 2, null, 3, 4).Average(), Is.EqualTo(2.5F));
        }

        [Test]
        public void AverageNullableFloatMaxValues()
        {
            Assert.That(Data<float?>(Single.MaxValue, Single.MaxValue).Average(), Is.EqualTo(3.40282347E+38F));
        }

        [Test]
        public void AverageNullableFloatNoOverflow()
        {
            Assert.That(Data<float?>(Single.MaxValue, 1).Average(), Is.EqualTo(1.70141173E+38F));
        }

        [Test]
        public void AverageNullableFloatWithNan()
        {
            Assert.That(Data<float?>(1, 2, Single.NaN, 3, 4).Average(), Is.EqualTo(Single.NaN));
        }

        [Test]
        public void AverageNullableFloatSourceArgumentNull()
        {
            Data<float?> nullData = null;

            Assert.Throws<ArgumentNullException>(() => nullData.Average()).WithParameter("source");
        }

        #endregion ENDOF: Nullable Float

        #region Nullable Double

        [Test]
        public void AverageNullableDoubleEmptySource()
        {
            Assert.That(Data<double?>().Average(), Is.EqualTo(null));
        }

        [Test]
        public void AverageNullableDoubleNonEmptySource()
        {
            Assert.That(Data<double?>(1, 2, null, 3, 4).Average(), Is.EqualTo(2.5D));
        }

        [Test]
        public void AverageNullableDoubleNoOverflow()
        {
            Assert.That(Data<double?>(Double.MaxValue, 1).Average(), Is.EqualTo(8.9884656743115785E+307D));
        }

        [Test]
        public void AverageNullableDoubleWithNan()
        {
            Assert.That(Data<double?>(1, 2, Double.NaN, 3, 4).Average(), Is.EqualTo(Double.NaN));
        }

        [Test]
        public void AverageNullableDoubleSourceArgumentNull()
        {
            Data<double?> nullData = null;

            Assert.Throws<ArgumentNullException>(() => nullData.Average()).WithParameter("source");
        }

        #endregion ENDOF: Nullable Double

        #region Nullable Long

        [Test]
        public void AverageNullableLongEmptySource()
        {
            Assert.That(Data<long?>().Average(), Is.EqualTo(null));
        }

        [Test]
        public void AverageNullableLongNonEmptySource()
        {
            Assert.That(Data<long?>(1, 2, null, 3, 4).Average(), Is.EqualTo(2.5D));
        }

        [Test]
        public void AverageNullableLongOverflow()
        {
            Assert.Throws<OverflowException>(() => Data<long?>(Int64.MaxValue, 1).Average());
        }

        [Test]
        public void AverageNullableLongSourceArgumentNull()
        {
            Data<long?> nullData = null;

            Assert.Throws<ArgumentNullException>(() => nullData.Average()).WithParameter("source");
        }

        #endregion ENDOF: Nullable Long

        #region Nullable Decimal

        [Test]
        public void AverageNullableDecimalEmptySource()
        {
            Assert.That(Data<decimal?>().Average(), Is.EqualTo(null));
        }

        [Test]
        public void AverageNullableDecimalNonEmptySource()
        {
            Assert.That(Data<decimal?>(1, 2, null, 3, 4).Average(), Is.EqualTo(2.5));
        }

        [Test]
        public void AverageNullableDecimalOverflow()
        {
            Assert.Throws<OverflowException>(() => Data<decimal?>(Decimal.MaxValue, 1).Average());
        }

        [Test]
        public void AverageNullableDecimalSourceArgumentNull()
        {
            Data<decimal?> nullData = null;

            Assert.Throws<ArgumentNullException>(() => nullData.Average()).WithParameter("source");
        }

        #endregion ENDOF: Nullable Decimal

        #region Int With Selector

        [Test]
        public void AverageIntWithSelectorEmptySource()
        {
            Assert.Throws<InvalidOperationException>(() => Data<int>().Average(x => x)).WithMessageNoElements();
        }

        [Test]
        public void AverageIntWithSelectorNonEmptySource()
        {
            Assert.That(Data<int>(1, 2, 3, 4).Average(x => x * 2), Is.EqualTo(5));
        }

        [Test]
        public void AverageIntWithSelectorNoOverflow()
        {
            Assert.That(Data<int>(Int32.MaxValue, 1).Average(x => x), Is.EqualTo(1073741824.0D));
        }

        [Test]
        public void AverageIntWithSelectorSourceArgumentNull()
        {
            Data<int> nullData = null;

            Assert.Throws<ArgumentNullException>(() => nullData.Average(x => x)).WithParameter("source");
        }

        [Test]
        public void AverageIntWithSelectorSelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => Data<int>().Average((Func<int, int>)null)).WithParameter("selector");
        }

        #endregion ENDOF: Int With Selector

        #region Float With Selector

        [Test]
        public void AverageFloatWithSelectorEmptySource()
        {
            Assert.Throws<InvalidOperationException>(() => Data<float>().Average(x => x)).WithMessageNoElements();
        }

        [Test]
        public void AverageFloatWithSelectorNonEmptySource()
        {
            Assert.That(Data<float>(1, 2, 3, 4).Average(x => x * 2), Is.EqualTo(5));
        }

        [Test]
        public void AverageFloatWithSelectorMaxValues()
        {
            Assert.That(Data<float>(Single.MaxValue, Single.MaxValue).Average(x => x), Is.EqualTo(3.40282347E+38F));
        }

        [Test]
        public void AverageFloatWithSelectorNoOverflow()
        {
            Assert.That(Data<float>(Single.MaxValue, 1).Average(x => x), Is.EqualTo(1.70141173E+38F));
        }

        [Test]
        public void AverageFloatWithSelectorWithNan()
        {
            Assert.That(Data<float>(1, 2, Single.NaN, 3, 4).Average(x => x), Is.EqualTo(Single.NaN));
        }

        [Test]
        public void AverageFloatWithSelectorSourceArgumentNull()
        {
            Data<float> nullData = null;

            Assert.Throws<ArgumentNullException>(() => nullData.Average(x => x)).WithParameter("source");
        }

        [Test]
        public void AverageFloatWithSelectorSelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => Data<float>().Average((Func<float, float>)null)).WithParameter("selector");
        }

        #endregion ENDOF: Float With Selector

        #region Double With Selector

        [Test]
        public void AverageDoubleWithSelectorEmptySource()
        {
            Assert.Throws<InvalidOperationException>(() => Data<double>().Average(x => x)).WithMessageNoElements();
        }

        [Test]
        public void AverageDoubleWithSelectorNonEmptySource()
        {
            Assert.That(Data<double>(1, 2, 3, 4).Average(x => x * 2), Is.EqualTo(5));
        }

        [Test]
        public void AverageDoubleWithSelectorNoOverflow()
        {
            Assert.That(Data<double>(Double.MaxValue, 1).Average(x => x), Is.EqualTo(8.9884656743115785E+307D));
        }

        [Test]
        public void AverageDoubleWithSelectorWithNan()
        {
            Assert.That(Data<double>(1, 2, Double.NaN, 3, 4).Average(x => x), Is.EqualTo(Double.NaN));
        }

        [Test]
        public void AverageDoubleWithSelectorSourceArgumentNull()
        {
            Data<double> nullData = null;

            Assert.Throws<ArgumentNullException>(() => nullData.Average(x => x)).WithParameter("source");
        }

        [Test]
        public void AverageDoubleWithSelectorSelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => Data<double>().Average((Func<double, double>)null)).WithParameter("selector");
        }

        #endregion ENDOF: Double With Selector

        #region Long With Selector

        [Test]
        public void AverageLongWithSelectorEmptySource()
        {
            Assert.Throws<InvalidOperationException>(() => Data<long>().Average(x => x)).WithMessageNoElements();
        }

        [Test]
        public void AverageLongWithSelectorNonEmptySource()
        {
            Assert.That(Data<long>(1, 2, 3, 4).Average(x => x * 2), Is.EqualTo(5));
        }

        [Test]
        public void AverageLongWithSelectorOverflow()
        {
            Assert.Throws<OverflowException>(() => Data<long>(Int64.MaxValue, 1).Average(x => x));
        }

        [Test]
        public void AverageLongWithSelectorSourceArgumentNull()
        {
            Data<long> nullData = null;

            Assert.Throws<ArgumentNullException>(() => nullData.Average(x => x)).WithParameter("source");
        }

        [Test]
        public void AverageLongWithSelectorSelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => Data<long>().Average((Func<long, long>)null)).WithParameter("selector");
        }

        #endregion ENDOF: Long With Selector

        #region Decimal With Selector

        [Test]
        public void AverageDecimalWithSelectorEmptySource()
        {
            Assert.Throws<InvalidOperationException>(() => Data<decimal>().Average(x => x)).WithMessageNoElements();
        }

        [Test]
        public void AverageDecimalWithSelectorNonEmptySource()
        {
            Assert.That(Data<decimal>(1, 2, 3, 4).Average(x => x * 2), Is.EqualTo(5));
        }

        [Test]
        public void AverageDecimalWithSelectorOverflow()
        {
            Assert.Throws<OverflowException>(() => Data<decimal>(Decimal.MaxValue, 1).Average(x => x));
        }

        [Test]
        public void AverageDecimalWithSelectorSourceArgumentNull()
        {
            Data<decimal> nullData = null;

            Assert.Throws<ArgumentNullException>(() => nullData.Average(x => x)).WithParameter("source");
        }

        [Test]
        public void AverageDecimalWithSelectorSelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => Data<decimal>().Average((Func<decimal, decimal>)null)).WithParameter("selector");
        }

        #endregion ENDOF: Decimal With Selector

        #region Nullable Int With Selector

        [Test]
        public void AverageNullableIntWithSelectorEmptySource()
        {
            Assert.That(Data<int?>().Average(x => x), Is.EqualTo(null));
        }

        [Test]
        public void AverageNullableIntWithSelectorNonEmptySource()
        {
            Assert.That(Data<int?>(1, 2, null, 3, 4).Average(x => x * 2), Is.EqualTo(5));
        }

        [Test]
        public void AverageNullableIntWithSelectorNoOverflow()
        {
            Assert.That(Data<int>(Int32.MaxValue, 1).Average(x => x), Is.EqualTo(1073741824.0D));
        }

        [Test]
        public void AverageNullableIntWithSelectorSourceArgumentNull()
        {
            Data<int?> nullData = null;

            Assert.Throws<ArgumentNullException>(() => nullData.Average(x => x)).WithParameter("source");
        }

        [Test]
        public void AverageNullableIntWithSelectorSelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => Data<int?>().Average((Func<int?, int?>)null)).WithParameter("selector");
        }

        #endregion ENDOF: Nullable Int With Selector

        #region Nullable Float With Selector

        [Test]
        public void AverageNullableFloatWithSelectorEmptySource()
        {
            Assert.That(Data<float?>().Average(x => x), Is.EqualTo(null));
        }

        [Test]
        public void AverageNullableFloatWithSelectorNonEmptySource()
        {
            Assert.That(Data<float?>(1, 2, null, 3, 4).Average(x => x * 2), Is.EqualTo(5));
        }

        [Test]
        public void AverageNullableFloatWithSelectorMaxValues()
        {
            Assert.That(Data<float?>(Single.MaxValue, Single.MaxValue).Average(x => x), Is.EqualTo(3.40282347E+38F));
        }

        [Test]
        public void AverageNullableFloatWithSelectorWithNan()
        {
            Assert.That(Data<float?>(1, 2, Single.NaN, 3, 4).Average(x => x), Is.EqualTo(Single.NaN));
        }

        [Test]
        public void AverageNullableFloatWithSelectorNoOverflow()
        {
            Assert.That(Data<float?>(Single.MaxValue, 1).Average(x => x), Is.EqualTo(1.70141173E+38F));
        }

        [Test]
        public void AverageNullableFloatWithSelectorSourceArgumentNull()
        {
            Data<float?> nullData = null;

            Assert.Throws<ArgumentNullException>(() => nullData.Average(x => x)).WithParameter("source");
        }

        [Test]
        public void AverageNullableFloatWithSelectorSelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => Data<float?>().Average((Func<float?, float?>)null)).WithParameter("selector");
        }

        #endregion ENDOF: Nullable Float With Selector

        #region Nullable Double With Selector

        [Test]
        public void AverageNullableDoubleWithSelectorEmptySource()
        {
            Assert.That(Data<double?>().Average(x => x), Is.EqualTo(null));
        }

        [Test]
        public void AverageNullableDoubleWithSelectorNonEmptySource()
        {
            Assert.That(Data<double?>(1, 2, null, 3, 4).Average(x => x * 2), Is.EqualTo(5));
        }

        [Test]
        public void AverageNullableDoubleWithSelectorNoOverflow()
        {
            Assert.That(Data<double?>(Double.MaxValue, 1).Average(x => x), Is.EqualTo(8.9884656743115785E+307D));
        }

        [Test]
        public void AverageNullableDoubleWithSelectorWithNan()
        {
            Assert.That(Data<double?>(1, 2, Double.NaN, 3, 4).Average(x => x), Is.EqualTo(Double.NaN));
        }

        [Test]
        public void AverageNullableDoubleWithSelectorSourceArgumentNull()
        {
            Data<double?> nullData = null;

            Assert.Throws<ArgumentNullException>(() => nullData.Average((Func<double?, double?>)null)).WithParameter("source");
        }

        [Test]
        public void AverageNullableDoubleWithSelectorSelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => Data<double?>().Average((Func<double?, double?>)null)).WithParameter("selector");
        }

        #endregion ENDOF: Nullable Double With Selector

        #region Nullable Long With Selector

        [Test]
        public void AverageNullableLongWithSelectorEmptySource()
        {
            Assert.That(Data<long?>().Average(x => x), Is.EqualTo(null));
        }

        [Test]
        public void AverageNullableLongWithSelectorNonEmptySource()
        {
            Assert.That(Data<long?>(1, 2, null, 3, 4).Average(x => x * 2), Is.EqualTo(5));
        }

        [Test]
        public void AverageNullableLongWithSelectorOverflow()
        {
            Assert.Throws<OverflowException>(() => Data<long?>(Int64.MaxValue, 1).Average(x => x));
        }

        [Test]
        public void AverageNullableLongWithSelectorSourceArgumentNull()
        {
            Data<long?> nullData = null;

            Assert.Throws<ArgumentNullException>(() => nullData.Average(x => x)).WithParameter("source");
        }

        [Test]
        public void AverageNullableLongWithSelectorSelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => Data<long?>().Average((Func<long?, long?>)null)).WithParameter("selector");
        }

        #endregion ENDOF: Nullable Long With Selector

        #region Nullable Decimal With Selector

        [Test]
        public void AverageNullableDecimalWithSelectorEmptySource()
        {
            Assert.That(Data<decimal?>().Average(x => x), Is.EqualTo(null));
        }

        [Test]
        public void AverageNullableDecimalWithSelectorNonEmptySource()
        {
            Assert.That(Data<decimal?>(1, 2, null, 3, 4).Average(x => x * 2), Is.EqualTo(5));
        }

        [Test]
        public void AverageNullableDecimalWithSelectorOverflow()
        {
            Assert.Throws<OverflowException>(() => Data<decimal?>(Decimal.MaxValue, 1).Average(x => x));
        }

        [Test]
        public void AverageNullableDecimalWithSelectorSourceArgumentNull()
        {
            Data<decimal?> nullData = null;

            Assert.Throws<ArgumentNullException>(() => nullData.Average(x => x)).WithParameter("source");
        }

        [Test]
        public void AverageNullableDecimalWithSelectorSelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => Data<decimal?>().Average((Func<decimal?, decimal?>)null)).WithParameter("selector");
        }

        #endregion ENDOF: Nullable Decimal With Selector
    }
}
