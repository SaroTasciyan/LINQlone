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
    public class SumTests : BaseTest
    {
        #region Int

        [Test]
        public void SumIntEmptySource()
        {
            Assert.That(Data<int>().Sum(), Is.EqualTo(0));
        }

        [Test]
        public void SumIntNonEmptySource()
        {
            Assert.That(Data<int>(1, 2, 3, 4, 5).Sum(), Is.EqualTo(15));
        }

        [Test]
        public void SumIntOverflow()
        {
            Assert.Throws<OverflowException>(() => Data<int>(Int32.MaxValue, 1).Sum());
        }

        [Test]
        public void SumIntSourceArgumentNull()
        {
            Data<int> nullData = null;

            Assert.Throws<ArgumentNullException>(() => nullData.Sum()).WithParameter("source");
        }

        #endregion ENDOF: Int

        #region Float

        [Test]
        public void SumFloatEmptySource()
        {
            Assert.That(Data<float>().Sum(), Is.EqualTo(0));
        }

        [Test]
        public void SumFloatNonEmptySource()
        {
            Assert.That(Data<float>(1, 2, 3, 4, 5).Sum(), Is.EqualTo(15));
        }

        [Test]
        public void SumFloatMaxValues()
        {
            Assert.That(Data<float>(Single.MaxValue, Single.MaxValue).Sum(), Is.EqualTo(Single.PositiveInfinity));
        }

        [Test]
        public void SumFloatNoOverflow()
        {
            Assert.That(Data<float>(Single.MaxValue, 1).Sum(), Is.EqualTo(3.40282347E+38F));
        }

        [Test]
        public void SumFloatSourceArgumentNull()
        {
            Data<float> nullData = null;

            Assert.Throws<ArgumentNullException>(() => nullData.Sum()).WithParameter("source");
        }

        #endregion ENDOF: Float

        #region Double

        [Test]
        public void SumDoubleEmptySource()
        {
            Assert.That(Data<double>().Sum(), Is.EqualTo(0));
        }

        [Test]
        public void SumDoubleNonEmptySource()
        {
            Assert.That(Data<double>(1, 2, 3, 4, 5).Sum(), Is.EqualTo(15));
        }

        [Test]
        public void SumDoubleNoOverflow()
        {
            Assert.That(Data<double>(Double.MaxValue, 1).Sum(), Is.EqualTo(1.7976931348623157E+308D));
        }

        [Test]
        public void SumDoubleSourceArgumentNull()
        {
            Data<double> nullData = null;

            Assert.Throws<ArgumentNullException>(() => nullData.Sum()).WithParameter("source");
        }

        #endregion ENDOF: Double

        #region Long

        [Test]
        public void SumLongEmptySource()
        {
            Assert.That(Data<long>().Sum(), Is.EqualTo(0));
        }

        [Test]
        public void SumLongNonEmptySource()
        {
            Assert.That(Data<long>(1, 2, 3, 4, 5).Sum(), Is.EqualTo(15));
        }

        [Test]
        public void SumLongOverflow()
        {
            Assert.Throws<OverflowException>(() => Data<long>(Int64.MaxValue, 1).Sum());
        }

        [Test]
        public void SumLongSourceArgumentNull()
        {
            Data<long> nullData = null;

            Assert.Throws<ArgumentNullException>(() => nullData.Sum()).WithParameter("source");
        }

        #endregion ENDOF: Long

        #region Decimal

        [Test]
        public void SumDecimalEmptySource()
        {
            Assert.That(Data<decimal>().Sum(), Is.EqualTo(0));
        }

        [Test]
        public void SumDecimalNonEmptySource()
        {
            Assert.That(Data<decimal>(1, 2 ,3, 4, 5).Sum(), Is.EqualTo(15));
        }

        [Test]
        public void SumDecimalOverflow()
        {
            Assert.Throws<OverflowException>(() => Data<decimal>(Decimal.MaxValue, 1).Sum());
        }

        [Test]
        public void SumDecimalSourceArgumentNull()
        {
            Data<decimal> nullData = null;

            Assert.Throws<ArgumentNullException>(() => nullData.Sum()).WithParameter("source");
        }

        #endregion ENDOF: Decimal

        #region Nullable Int

        [Test]
        public void SumNullableIntEmptySource()
        {
            Assert.That(Data<int?>().Sum(), Is.EqualTo(0));
        }

        [Test]
        public void SumNullableIntNonEmptySource()
        {
            Assert.That(Data<int?>(1, 2, null, 3, 4, 5).Sum(), Is.EqualTo(15));
        }

        [Test]
        public void SumNullableIntOverflow()
        {
            Assert.Throws<OverflowException>(() => Data<int?>(Int32.MaxValue, 1).Sum());
        }

        [Test]
        public void SumNullableIntSourceArgumentNull()
        {
            Data<int?> nullData = null;

            Assert.Throws<ArgumentNullException>(() => nullData.Sum()).WithParameter("source");
        }

        #endregion ENDOF: Nullable Int

        #region Nullable Float

        [Test]
        public void SumNullableFloatEmptySource()
        {
            Assert.That(Data<float?>().Sum(), Is.EqualTo(0));
        }

        [Test]
        public void SumNullableFloatNonEmptySource()
        {
            Assert.That(Data<float?>(1, 2, null, 3, 4, 5).Sum(), Is.EqualTo(15));
        }

        [Test]
        public void SumNullableFloatMaxValues()
        {
            Assert.That(Data<float?>(Single.MaxValue, Single.MaxValue).Sum(), Is.EqualTo(Single.PositiveInfinity));
        }

        [Test]
        public void SumNullableFloatNoOverflow()
        {
            Assert.That(Data<float?>(Single.MaxValue, 1).Sum(), Is.EqualTo(3.40282347E+38F));
        }

        [Test]
        public void SumNullableFloatSourceArgumentNull()
        {
            Data<float?> nullData = null;

            Assert.Throws<ArgumentNullException>(() => nullData.Sum()).WithParameter("source");
        }

        #endregion ENDOF: Nullable Float

        #region Nullable Double

        [Test]
        public void SumNullableDoubleEmptySource()
        {
            Assert.That(Data<double?>().Sum(), Is.EqualTo(0));
        }

        [Test]
        public void SumNullableDoubleNonEmptySource()
        {
            Assert.That(Data<double?>(1, 2, null, 3, 4, 5).Sum(), Is.EqualTo(15));
        }

        [Test]
        public void SumNullableDoubleNoOverflow()
        {
            Assert.That(Data<double?>(Double.MaxValue, 1).Sum(), Is.EqualTo(1.7976931348623157E+308D));
        }

        [Test]
        public void SumNullableDoubleSourceArgumentNull()
        {
            Data<double?> nullData = null;

            Assert.Throws<ArgumentNullException>(() => nullData.Sum()).WithParameter("source");
        }

        #endregion ENDOF: Nullable Double

        #region Nullable Long

        [Test]
        public void SumNullableLongEmptySource()
        {
            Assert.That(Data<long?>().Sum(), Is.EqualTo(0));
        }

        [Test]
        public void SumNullableLongNonEmptySource()
        {
            Assert.That(Data<long?>(1, 2, null, 3, 4, 5).Sum(), Is.EqualTo(15));
        }

        [Test]
        public void SumNullableLongOverflow()
        {
            Assert.Throws<OverflowException>(() => Data<long?>(Int64.MaxValue, 1).Sum());
        }

        [Test]
        public void SumNullableLongSourceArgumentNull()
        {
            Data<long?> nullData = null;

            Assert.Throws<ArgumentNullException>(() => nullData.Sum()).WithParameter("source");
        }

        #endregion ENDOF: Nullable Long

        #region Nullable Decimal

        [Test]
        public void SumNullableDecimalEmptySource()
        {
            Assert.That(Data<decimal?>().Sum(), Is.EqualTo(0));
        }

        [Test]
        public void SumNullableDecimalNonEmptySource()
        {
            Assert.That(Data<decimal?>(1, 2, null, 3, 4, 5).Sum(), Is.EqualTo(15));
        }

        [Test]
        public void SumNullableDecimalOverflow()
        {
            Assert.Throws<OverflowException>(() => Data<decimal?>(Decimal.MaxValue, 1).Sum());
        }

        [Test]
        public void SumNullableDecimalSourceArgumentNull()
        {
            Data<decimal?> nullData = null;

            Assert.Throws<ArgumentNullException>(() => nullData.Sum()).WithParameter("source");
        }

        #endregion ENDOF: Nullable Decimal

        #region Int With Selector

        [Test]
        public void SumIntWithSelectorEmptySource()
        {
            Assert.That(Data<int>().Sum(x => x), Is.EqualTo(0));
        }

        [Test]
        public void SumIntWithSelectorNonEmptySource()
        {
            Assert.That(Data<int>(1, 2, 3, 4, 5).Sum(x => x * 2), Is.EqualTo(30));
        }

        [Test]
        public void SumIntWithSelectorOverflow()
        {
            Assert.Throws<OverflowException>(() => Data<int>(Int32.MaxValue, 1).Sum(x => x));
        }

        [Test]
        public void SumIntWithSelectorSourceArgumentNull()
        {
            Data<int> nullData = null;

            Assert.Throws<ArgumentNullException>(() => nullData.Sum(x => x)).WithParameter("source");
        }

        [Test]
        public void SumIntWithSelectorSelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => Data<int>().Sum((Func<int, int>)null)).WithParameter("selector");
        }

        #endregion ENDOF: Int With Selector

        #region Float With Selector

        [Test]
        public void SumFloatWithSelectorEmptySource()
        {
            Assert.That(Data<float>().Sum(x => x), Is.EqualTo(0));
        }

        [Test]
        public void SumFloatWithSelectorNonEmptySource()
        {
            Assert.That(Data<float>(1, 2, 3, 4, 5).Sum(x => x * 2), Is.EqualTo(30));
        }

        [Test]
        public void SumFloatWithSelectorMaxValues()
        {
            Assert.That(Data<float>(Single.MaxValue, Single.MaxValue).Sum(x => x), Is.EqualTo(Single.PositiveInfinity));
        }

        [Test]
        public void SumFloatWithSelectorNoOverflow()
        {
            Assert.That(Data<float>(Single.MaxValue, 1).Sum(x => x), Is.EqualTo(3.40282347E+38F));
        }

        [Test]
        public void SumFloatWithSelectorSourceArgumentNull()
        {
            Data<float> nullData = null;

            Assert.Throws<ArgumentNullException>(() => nullData.Sum(x => x)).WithParameter("source");
        }

        [Test]
        public void SumFloatWithSelectorSelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => Data<float>().Sum((Func<float, float>)null)).WithParameter("selector");
        }

        #endregion ENDOF: Float With Selector

        #region Double With Selector

        [Test]
        public void SumDoubleWithSelectorEmptySource()
        {
            Assert.That(Data<double>().Sum(x => x), Is.EqualTo(0));
        }

        [Test]
        public void SumDoubleWithSelectorNonEmptySource()
        {
            Assert.That(Data<double>(1, 2, 3, 4, 5).Sum(x => x * 2), Is.EqualTo(30));
        }

        [Test]
        public void SumDoubleWithSelectorNoOverflow()
        {
            Assert.That(Data<double>(Double.MaxValue, 1).Sum(x => x), Is.EqualTo(1.7976931348623157E+308D));
        }

        [Test]
        public void SumDoubleWithSelectorSourceArgumentNull()
        {
            Data<double> nullData = null;

            Assert.Throws<ArgumentNullException>(() => nullData.Sum(x => x)).WithParameter("source");
        }

        [Test]
        public void SumDoubleWithSelectorSelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => Data<double>().Sum((Func<double, double>)null)).WithParameter("selector");
        }

        #endregion ENDOF: Double With Selector

        #region Long With Selector

        [Test]
        public void SumLongWithSelectorEmptySource()
        {
            Assert.That(Data<long>().Sum(x => x), Is.EqualTo(0));
        }

        [Test]
        public void SumLongWithSelectorNonEmptySource()
        {
            Assert.That(Data<long>(1, 2, 3, 4, 5).Sum(x => x * 2), Is.EqualTo(30));
        }

        [Test]
        public void SumLongWithSelectorOverflow()
        {
            Assert.Throws<OverflowException>(() => Data<long>(Int64.MaxValue, 1).Sum(x => x));
        }

        [Test]
        public void SumLongWithSelectorSourceArgumentNull()
        {
            Data<long> nullData = null;

            Assert.Throws<ArgumentNullException>(() => nullData.Sum(x => x)).WithParameter("source");
        }

        [Test]
        public void SumLongWithSelectorSelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => Data<long>().Sum((Func<long, long>)null)).WithParameter("selector");
        }

        #endregion ENDOF: Long With Selector

        #region Decimal With Selector

        [Test]
        public void SumDecimalWithSelectorEmptySource()
        {
            Assert.That(Data<decimal>().Sum(x => x), Is.EqualTo(0));
        }

        [Test]
        public void SumDecimalWithSelectorNonEmptySource()
        {
            Assert.That(Data<decimal>(1, 2, 3, 4, 5).Sum(x => x * 2), Is.EqualTo(30));
        }

        [Test]
        public void SumDecimalWithSelectorOverflow()
        {
            Assert.Throws<OverflowException>(() => Data<decimal>(Decimal.MaxValue, 1).Sum(x => x));
        }

        [Test]
        public void SumDecimalWithSelectorSourceArgumentNull()
        {
            Data<decimal> nullData = null;

            Assert.Throws<ArgumentNullException>(() => nullData.Sum(x => x)).WithParameter("source");
        }

        [Test]
        public void SumDecimalWithSelectorSelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => Data<decimal>().Sum((Func<decimal, decimal>)null)).WithParameter("selector");
        }

        #endregion ENDOF: Decimal With Selector

        #region Nullable Int With Selector

        [Test]
        public void SumNullableIntWithSelectorEmptySource()
        {
            Assert.That(Data<int?>().Sum(x => x), Is.EqualTo(0));
        }

        [Test]
        public void SumNullableIntWithSelectorNonEmptySource()
        {
            Assert.That(Data<int?>(1, 2, null, 3, 4, 5).Sum(x => x * 2), Is.EqualTo(30));
        }

        [Test]
        public void SumNullableIntWithSelectorOverflow()
        {
            Assert.Throws<OverflowException>(() => Data<int?>(Int32.MaxValue, 1).Sum(x => x));
        }

        [Test]
        public void SumNullableIntWithSelectorSourceArgumentNull()
        {
            Data<int?> nullData = null;

            Assert.Throws<ArgumentNullException>(() => nullData.Sum(x => x)).WithParameter("source");
        }

        [Test]
        public void SumNullableIntWithSelectorSelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => Data<int?>().Sum((Func<int?, int?>)null)).WithParameter("selector");
        }

        #endregion ENDOF: Nullable Int With Selector

        #region Nullable Float With Selector

        [Test]
        public void SumNullableFloatWithSelectorEmptySource()
        {
            Assert.That(Data<float?>().Sum(x => x), Is.EqualTo(0));
        }

        [Test]
        public void SumNullableFloatWithSelectorNonEmptySource()
        {
            Assert.That(Data<float?>(1, 2, null, 3, 4, 5).Sum(x => x * 2), Is.EqualTo(30));
        }

        [Test]
        public void SumNullableFloatWithSelectorMaxValues()
        {
            Assert.That(Data<float?>(Single.MaxValue, Single.MaxValue).Sum(x => x), Is.EqualTo(Single.PositiveInfinity));
        }

        [Test]
        public void SumNullableFloatWithSelectorNoOverflow()
        {
            Assert.That(Data<float?>(Single.MaxValue, 1).Sum(x => x), Is.EqualTo(3.40282347E+38F));
        }

        [Test]
        public void SumNullableFloatWithSelectorSourceArgumentNull()
        {
            Data<float?> nullData = null;

            Assert.Throws<ArgumentNullException>(() => nullData.Sum(x => x)).WithParameter("source");
        }

        [Test]
        public void SumNullableFloatWithSelectorSelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => Data<float?>().Sum((Func<float?, float?>)null)).WithParameter("selector");
        }

        #endregion ENDOF: Nullable Float With Selector

        #region Nullable Double With Selector

        [Test]
        public void SumNullableDoubleWithSelectorEmptySource()
        {
            Assert.That(Data<double?>().Sum(x => x), Is.EqualTo(0));
        }

        [Test]
        public void SumNullableDoubleWithSelectorNonEmptySource()
        {
            Assert.That(Data<double?>(1, 2, null, 3, 4, 5).Sum(x => x * 2), Is.EqualTo(30));
        }

        [Test]
        public void SumNullableDoubleWithSelectorNoOverflow()
        {
            Assert.That(Data<double?>(Double.MaxValue, 1).Sum(x => x), Is.EqualTo(1.7976931348623157E+308D));
        }

        [Test]
        public void SumNullableDoubleWithSelectorSourceArgumentNull()
        {
            Data<double?> nullData = null;

            Assert.Throws<ArgumentNullException>(() => nullData.Sum((Func<double?, double?>)null)).WithParameter("source");
        }

        [Test]
        public void SumNullableDoubleWithSelectorSelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => Data<double?>().Sum((Func<double?, double?>)null)).WithParameter("selector");
        }

        #endregion ENDOF: Nullable Double With Selector

        #region Nullable Long With Selector

        [Test]
        public void SumNullableLongWithSelectorEmptySource()
        {
            Assert.That(Data<long?>().Sum(x => x), Is.EqualTo(0));
        }

        [Test]
        public void SumNullableLongWithSelectorNonEmptySource()
        {
            Assert.That(Data<long?>(1, 2, null, 3, 4, 5).Sum(x => x * 2), Is.EqualTo(30));
        }

        [Test]
        public void SumNullableLongWithSelectorOverflow()
        {
            Assert.Throws<OverflowException>(() => Data<long?>(Int64.MaxValue, 1).Sum(x => x));
        }

        [Test]
        public void SumNullableLongWithSelectorSourceArgumentNull()
        {
            Data<long?> nullData = null;

            Assert.Throws<ArgumentNullException>(() => nullData.Sum(x => x)).WithParameter("source");
        }

        [Test]
        public void SumNullableLongWithSelectorSelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => Data<long?>().Sum((Func<long?, long?>)null)).WithParameter("selector");
        }

        #endregion ENDOF: Nullable Long With Selector

        #region Nullable Decimal With Selector

        [Test]
        public void SumNullableDecimalWithSelectorEmptySource()
        {
            Assert.That(Data<decimal?>().Sum(x => x), Is.EqualTo(0));
        }

        [Test]
        public void SumNullableDecimalWithSelectorNonEmptySource()
        {
            Assert.That(Data<decimal?>(1, 2, null, 3, 4, 5).Sum(x => x * 2), Is.EqualTo(30));
        }

        [Test]
        public void SumNullableDecimalWithSelectorOverflow()
        {
            Assert.Throws<OverflowException>(() => Data<decimal?>(Decimal.MaxValue, 1).Sum(x => x));
        }

        [Test]
        public void SumNullableDecimalWithSelectorSourceArgumentNull()
        {
            Data<decimal?> nullData = null;

            Assert.Throws<ArgumentNullException>(() => nullData.Sum(x => x)).WithParameter("source");
        }

        [Test]
        public void SumNullableDecimalWithSelectorSelectorArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => Data<decimal?>().Sum((Func<decimal?, decimal?>)null)).WithParameter("selector");
        }

        #endregion ENDOF: Nullable Decimal With Selector
    }
}
