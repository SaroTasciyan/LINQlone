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
    internal sealed class Parameter
    {
        #region Parameter Definitions

        internal static readonly Parameter Source = new Parameter("source");
        internal static readonly Parameter Index = new Parameter("index");
        internal static readonly Parameter Predicate = new Parameter("predicate");
        internal static readonly Parameter Selector = new Parameter("selector");
        internal static readonly Parameter CollectionSelector = new Parameter("collectionSelector");
        internal static readonly Parameter ResultSelector = new Parameter("resultSelector");
        internal static readonly Parameter Count = new Parameter("count");
        internal static readonly Parameter First = new Parameter("first");
        internal static readonly Parameter Second = new Parameter("second");
        internal static readonly Parameter KeySelector = new Parameter("keySelector");
        internal static readonly Parameter ElementSelector = new Parameter("elementSelector");
        internal static readonly Parameter Outer = new Parameter("outer");
        internal static readonly Parameter Inner = new Parameter("inner");
        internal static readonly Parameter OuterKeySelector = new Parameter("outerKeySelector");
        internal static readonly Parameter InnerKeySelector = new Parameter("innerKeySelector");
        internal static readonly Parameter Func = new Parameter("func");

        #endregion ENDOF: Parameter Definitions

        #region Fields

        private readonly string mName;

        #endregion ENDOF: Fields

        #region Properties

        public string Name
        {
            get { return mName; }
        }

        #endregion ENDOF: Properties

        #region Constructors

        private Parameter(string name)
        {
            mName = name;
        }

        #endregion ENDOF: Constructors
    }
}
