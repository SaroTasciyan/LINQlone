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

namespace LINQlone.Definitions
{
    internal static class Exceptions
    {
        #region Messages

        private static class Messages
        {
            internal const string ArgumentNull = "Value cannot be null.";
            internal const string ArgumentOutOfRange = "Specified argument was out of the range of valid values.";
            internal const string NoElements = "Sequence contains no elements";
            internal const string NoMatchingElements = "Sequence contains no matching element";
            internal const string MoreThanOneElements = "Sequence contains more than one element";
            internal const string MoreThanOneMatchingElements = "Sequence contains more than one matching element";
            internal const string NotSupported = "Specified method is not supported.";
        }

        #endregion ENDOF: Messages        

        #region Methods

        internal static ArgumentNullException ArgumentNull(Parameter parameter)
        {
            return new ArgumentNullException(parameter.Name, Messages.ArgumentNull);
        }

        internal static ArgumentOutOfRangeException ArgumentOutOfRange(Parameter parameter)
        {
            return new ArgumentOutOfRangeException(parameter.Name, Messages.ArgumentOutOfRange);
        }

        internal static InvalidOperationException NoElements()
        {
            return new InvalidOperationException(Messages.NoElements);
        }

        internal static InvalidOperationException NoMatchingElements()
        {
            return new InvalidOperationException(Messages.NoMatchingElements);
        }

        internal static InvalidOperationException MoreThanOneElements()
        {
            return new InvalidOperationException(Messages.MoreThanOneElements);
        }

        internal static InvalidOperationException MoreThanOneMatchingElements()
        {
            return new InvalidOperationException(Messages.MoreThanOneMatchingElements);
        }

        internal static NotSupportedException NotSupported()
        {
            return new NotSupportedException(Messages.NotSupported);
        }

        #endregion ENDOF: Methods
    }
}
