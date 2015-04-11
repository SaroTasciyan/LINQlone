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
    public class SequenceEqualTests : BaseTest
    {
        #region SequenceEqual

        [Test]
        public void SequenceEqualEmptySource()
        {
            Assert.That(EmptyData.SequenceEqual(EmptyData), Is.True);
        }

        [Test]
        public void SequenceEqualNonEmptySourceTrue()
        {
            Assert.That(Data(1, 2, 3).SequenceEqual(Data(1, 2, 3)), Is.True);
        }

        [Test]
        public void SequenceEqualNonEmptySourceFalse()
        {
            Assert.That(Data(1, 2, 3).SequenceEqual(Data(1, 3, 2)), Is.False);
        }

        [Test]
        public void SequenceEqualLenghtsDoNotMatchFalse()
        {
            Assert.That(Data(1, 2, 3).SequenceEqual(Data(1, 2, 3, 4)), Is.False);
        }

        [Test]
        public void SequenceEqualEnumerationCount()
        {
            Data<int> first = Data(1, 2, 3);
            Data<int> second = Data(1, 10, 100);

            Assert.That(first.SequenceEqual(second), Is.False);
            Assert.That(first.EnumerationCount, Is.EqualTo(2));
            Assert.That(second.EnumerationCount, Is.EqualTo(2));
        }

        [Test]
        public void SequenceEqualFirstArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => NullData.SequenceEqual(DummyData)).WithParameter("first");
        }

        [Test]
        public void SequenceEqualSecondArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => DummyData.SequenceEqual(NullData)).WithParameter("second");
        }

        #endregion ENDOF: SequenceEqual

        #region SequenceEqual With Comparer

        [Test]
        public void SequenceEqualWithComparerEmptySource()
        {
            Assert.That(EmptyData.SequenceEqual(EmptyData, EqualityComparer<object>.Default), Is.True);
        }

        [Test]
        public void SequenceEqualWithComparerNonEmptySourceTrue()
        {
            Assert.That(Data(1, 2, 3).SequenceEqual(Data(1, 2, 3), EqualityComparer<int>.Default), Is.True);
        }

        [Test]
        public void SequenceEqualWithComparerNonEmptySourceFalse()
        {
            Assert.That(Data(1, 2, 3).SequenceEqual(Data(1, 3, 2), EqualityComparer<int>.Default), Is.False);
        }

        [Test]
        public void SequenceEqualWithComparerLenghtsDoNotMatchFalse()
        {
            Assert.That(Data(1, 2, 3).SequenceEqual(Data(1, 2, 3, 4), EqualityComparer<int>.Default), Is.False);
        }

        [Test]
        public void SequenceEqualWithComparerEnumerationCount()
        {
            Data<int> first = Data(1, 2, 3);
            Data<int> second = Data(1, 10, 100);

            Assert.That(first.SequenceEqual(second, EqualityComparer<int>.Default), Is.False);
            Assert.That(first.EnumerationCount, Is.EqualTo(2));
            Assert.That(second.EnumerationCount, Is.EqualTo(2));
        }

        [Test]
        public void SequenceEqualWithComparerComparerArgumentNull()
        {
            Assert.DoesNotThrow(() => DummyData.SequenceEqual(DummyData, null));
        }

        [Test]
        public void SequenceEqualWithComparerCallCounts()
        {
            Data<string> first = Data("a", "b", "c", "d");
            Data<string> second = Data("a", "b", "c", "d");

            ThrowingStringEqualityComparer comparer = new ThrowingStringEqualityComparer();

            first.SequenceEqual(second, comparer);

            Assert.That(comparer.GetHashCodeCallCount, Is.EqualTo(0));
            Assert.That(comparer.EqualsCallCount, Is.EqualTo(4)); // # Count
        }

        [Test]
        public void SequenceEqualWithComparerDiferrentLengthCallCounts()
        {
            Data<string> first = Data("a", "b", "c", "d");
            Data<string> second = Data("a", "b");

            ThrowingStringEqualityComparer comparer = new ThrowingStringEqualityComparer();

            first.SequenceEqual(second, comparer);

            Assert.That(comparer.GetHashCodeCallCount, Is.EqualTo(0));
            Assert.That(comparer.EqualsCallCount, Is.EqualTo(2)); // # Min Length
        }

        [Test]
        public void SequenceEqualWithComparerFirstArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => NullData.SequenceEqual(DummyData, EqualityComparer<object>.Default)).WithParameter("first");
        }

        [Test]
        public void SequenceEqualWithComparerSecondArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => DummyData.SequenceEqual(NullData, EqualityComparer<object>.Default)).WithParameter("second");
        }

        #endregion ENDOF: SequenceEqual With Comparer
    }
}
