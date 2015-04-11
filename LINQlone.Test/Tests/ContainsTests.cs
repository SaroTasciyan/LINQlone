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
    public class ContainsTests : BaseTest
    {
        #region Contains

        [Test]
        public void ContainsEmptySource()
        {
            Assert.That(EmptyData.Contains(new object()), Is.False);
        }

        [Test]
        public void ContainsEmptyListSource()
        {
            Data<object> data = EmptyListData; // ~ Using type Data<T> in order to avoid call to ICollection<T>.Contains()

            Assert.That(data.Contains(new object()), Is.False);
            Assert.That(data.IsEnumerated, Is.False); // ~ Should not enumerate due to ICollection optimization
        }

        [Test]
        public void ContainsNonEmptySourceTrue()
        {
            Assert.That(Data(1, 2, 3).Contains(1), Is.True);
        }

        [Test]
        public void ContainsNonEmptySourceFalse()
        {
            Assert.That(Data(1, 2, 3).Contains(5), Is.False);
        }

        [Test]
        public void ContainsNonEmptyListSource()
        {
            Data<int> data = ListData(1, 2, 3);

            Assert.That(data.Contains(1), Is.True);
            Assert.That(data.IsEnumerated, Is.False);
        }

        [Test]
        public void ContainsEnumerationCount()
        {
            Data<int> data = Data(1, 2, 3);

            Assert.That(data.Contains(2), Is.True);
            Assert.That(data.EnumerationCount, Is.EqualTo(2));
        }

        [Test]
        public void ContainsValueArgumentNull()
        {
            Assert.DoesNotThrow(() => DummyData.Contains(null));
        }

        [Test]
        public void ContainsSourceArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => NullData.Contains(new object())).WithParameter("source");
        }

        #endregion ENDOF: Contains

        #region Contains With Comparer

        [Test]
        public void ContainsWithComparerEmptySource()
        {
            Assert.That(EmptyData.Contains(new object(), EqualityComparer<object>.Default), Is.False);
        }

        [Test]
        public void ContainsWithComparerNonEmptySourceTrue()
        {
            Assert.That(Data(1, 2, 3).Contains(1, EqualityComparer<int>.Default), Is.True);
        }

        [Test]
        public void ContainsWithComparerNonEmptySourceFalse()
        {
            Assert.That(Data(1, 2, 3).Contains(5, EqualityComparer<int>.Default), Is.False);
        }

        [Test]
        public void ContainsWithComparerEnumerationCount()
        {
            Data<int> data = Data(1, 2, 3);

            Assert.That(data.Contains(2, EqualityComparer<int>.Default), Is.True);
            Assert.That(data.EnumerationCount, Is.EqualTo(2));
        }

        [Test]
        public void ContainsWithComparerCallCounts()
        {
            Data<string> data = Data("a", "b", "c", "d");

            ThrowingStringEqualityComparer comparer = new ThrowingStringEqualityComparer();

            data.Contains("b", comparer);

            Assert.That(comparer.GetHashCodeCallCount, Is.EqualTo(0));
            Assert.That(comparer.EqualsCallCount, Is.EqualTo(2));
        }

        [Test]
        public void ContainsWithComparerNoMatchCallCounts()
        {
            Data<string> data = Data("a", "b", "c", "d");

            ThrowingStringEqualityComparer comparer = new ThrowingStringEqualityComparer();

            data.Contains("e", comparer);

            Assert.That(comparer.GetHashCodeCallCount, Is.EqualTo(0));
            Assert.That(comparer.EqualsCallCount, Is.EqualTo(4));
        }

        [Test]
        public void ContainsWithComparerValueArgumentNull()
        {
            Assert.DoesNotThrow(() => DummyData.Contains(null, EqualityComparer<object>.Default));
        }

        [Test]
        public void ContainsWithComparerComparerArgumentNull()
        {
            Assert.DoesNotThrow(() => DummyData.Contains(new object(), null));
        }

        [Test]
        public void ContainsWithComparerSourceArgumentNull()
        {
            Assert.Throws<ArgumentNullException>(() => NullData.Contains(new object(), EqualityComparer<object>.Default)).WithParameter("source");
        }

        #endregion ENDOF: Contains With Comparer
    }
}
