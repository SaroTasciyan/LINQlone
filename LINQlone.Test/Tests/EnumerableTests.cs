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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

using NUnit.Framework;

namespace LINQlone.Test
{
    [TestFixture]
    public class EnumerableTests
    {
        #region Enumerable

        [Test]
        public void AllOperatorsAreExtensionMethods()
        {
            MethodInfo[] methods = typeof(Enumerable).GetMethods(BindingFlags.Static | BindingFlags.Public);

            foreach (MethodInfo method in methods)
            {
                if (IsFirstParameterEnumerable(method))
                {
                    bool isExtensionMethod = Attribute.IsDefined(method, typeof(ExtensionAttribute));
                    Assert.That(isExtensionMethod, Is.True);
                }
            }
        }

        #endregion ENDOF: Enumerable

        #region Helper

        private bool IsFirstParameterEnumerable(MethodInfo method)
        {
            ParameterInfo[] parameters = method.GetParameters();
            if (parameters.Length == 0) { return false; }

            Type firstParameterType = parameters[0].ParameterType;

            return (firstParameterType == typeof(IEnumerable)
                || (firstParameterType.IsGenericType
                    && (firstParameterType.GetGenericTypeDefinition() == typeof(IEnumerable<>)
                        || firstParameterType.GetGenericTypeDefinition() == typeof(IOrderedEnumerable<>))));
        }

        #endregion ENDOF: Helper
    }
}
