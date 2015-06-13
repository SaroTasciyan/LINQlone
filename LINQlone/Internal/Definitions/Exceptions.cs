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
            internal const string ARGUMENT_NULL = "Value cannot be null.";
            internal const string ARGUMENT_OUT_OF_RANGE = "Specified argument was out of the range of valid values.";
            internal const string NO_ELEMENTS = "Sequence contains no elements";
            internal const string NO_MATCHING_ELEMENTS = "Sequence contains no matching element";
            internal const string MORE_THAN_ONE_ELEMENTS = "Sequence contains more than one element";
            internal const string MORE_THAN_ONE_MATCHING_ELEMENTS = "Sequence contains more than one matching element";
            internal const string NOT_SUPPORTED = "Specified method is not supported.";
        }

        #endregion ENDOF: Messages        

        #region Methods

        internal static ArgumentNullException ArgumentNull(Parameter parameter)
        {
            return new ArgumentNullException(parameter.Name, Messages.ARGUMENT_NULL);
        }

        internal static ArgumentOutOfRangeException ArgumentOutOfRange(Parameter parameter)
        {
            return new ArgumentOutOfRangeException(parameter.Name, Messages.ARGUMENT_OUT_OF_RANGE);
        }

        internal static InvalidOperationException NoElements()
        {
            return new InvalidOperationException(Messages.NO_ELEMENTS);
        }

        internal static InvalidOperationException NoMatchingElements()
        {
            return new InvalidOperationException(Messages.NO_MATCHING_ELEMENTS);
        }

        internal static InvalidOperationException MoreThanOneElements()
        {
            return new InvalidOperationException(Messages.MORE_THAN_ONE_ELEMENTS);
        }

        internal static InvalidOperationException MoreThanOneMatchingElements()
        {
            return new InvalidOperationException(Messages.MORE_THAN_ONE_MATCHING_ELEMENTS);
        }

        internal static NotSupportedException NotSupported()
        {
            return new NotSupportedException(Messages.NOT_SUPPORTED);
        }

        #endregion ENDOF: Methods
    }
}
