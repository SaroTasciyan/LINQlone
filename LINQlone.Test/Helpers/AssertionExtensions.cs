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

#undef PROFILE_DATA

using System;
using System.Collections.Generic;

using NUnit.Framework;

using LINQlone.Test.DataObjects;

namespace LINQlone.Test
{
    public static class AssertionExtensions
    {
        #region Execution Assertions

        // # Enumerable is not enumerated for streaming deferred execution
        // # Non-streaming (buffered) parts of execution should not be subject to deferred execution assertion
        public static void AssertDeferredExecution<T>(this Data<T> data)
        {
            Assert.That(data.IsEnumerated, Is.False); 
        }

        #endregion ENDOF: Execution Assertions

        #region Data Assertions

        public static void AssertEqual<T>(this IEnumerable<T> actual, params T[] expected)
        {
            List<T> actualList = new List<T>(actual); // # Filling into a list, ensuring iteration of enumerable

            Assert.AreEqual(expected.Length, actualList.Count);

            for (int i = 0; i < actualList.Count; i++)
            {
                #if PROFILE_DATA
                Console.WriteLine("Expected: {0}, Actual: {1}", actualList[i], expected[i]);
                #endif

                Assert.That(actualList[i], Is.EqualTo(expected[i]));
            }
        }

        public static void AssertSame<T>(this IEnumerable<T> actual, params T[] expected)
        {
            List<T> actualList = new List<T>(actual);

            Assert.AreEqual(expected.Length, actualList.Count);

            for (int i = 0; i < actualList.Count; i++)
            {
                #if PROFILE_DATA
                Console.WriteLine("Expected: {0}, Actual: {1}", actualList[i], expected[i]);
                #endif

                Assert.That(actualList[i], Is.SameAs(expected[i]));
            }
        }

        public static void AssertEmpty<T>(this IEnumerable<T> actual)
        {
            actual.AssertEqual();
        }

        public static void AssertNotEmpty<T>(this IEnumerable<T> actual)
        {
            using (IEnumerator<T> enumerator = actual.GetEnumerator())
            {
                Assert.That(enumerator.MoveNext(), Is.True);
            }
        }

        #endregion ENDOF: Data Assertions

        #region Exception Assertions

        public static ArgumentNullException WithParameter(this ArgumentNullException exception, string parameterName)
        {
            string expectedMessage = String.Format("Value cannot be null.\r\nParameter name: {0}", parameterName);

            Assert.That(exception.Message, Is.EqualTo(expectedMessage));

            return exception;
        }
        
        public static ArgumentOutOfRangeException WithParameter(this ArgumentOutOfRangeException exception, string parameterName)
        {
            string expectedMessage = String.Format("Specified argument was out of the range of valid values.\r\nParameter name: {0}", parameterName);

            Assert.That(exception.Message, Is.EqualTo(expectedMessage));

            return exception;
        }

        public static InvalidOperationException WithMessageNoElements(this InvalidOperationException exception)
        {
            Assert.That(exception.Message, Is.EqualTo("Sequence contains no elements"));

            return exception;
        }

        public static InvalidOperationException WithMessageNoMatchingElement(this InvalidOperationException exception)
        {
            Assert.That(exception.Message, Is.EqualTo("Sequence contains no matching element"));

            return exception;
        }

        public static InvalidOperationException WithMessageMoreThanOneElement(this InvalidOperationException exception)
        {
            Assert.That(exception.Message, Is.EqualTo("Sequence contains more than one element"));

            return exception;
        }

        public static InvalidOperationException WithMessageMoreThanOneMatchingElement(this InvalidOperationException exception)
        {
            Assert.That(exception.Message, Is.EqualTo("Sequence contains more than one matching element"));

            return exception;
        }

        public static NotSupportedException WithNotSupportedMessage(this NotSupportedException exception)
        {
            Assert.That(exception.Message, Is.EqualTo("Specified method is not supported."));

            return exception;
        }

        #endregion ENDOF: Exception Assertions
    }
}
