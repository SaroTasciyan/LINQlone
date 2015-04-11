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

using System.Collections.Generic;

namespace System.Linq
{
    /// <summary>
    /// Represents a collection of objects that have a common key.
    /// </summary>
    /// <typeparam name="TKey">The type of the key of the System.Linq.IGrouping&lt;TKey,TElement&gt;.</typeparam>
    /// <typeparam name="TElement">The type of the values in the System.Linq.IGrouping&lt;TKey,TElement&gt;.</typeparam>
    public interface IGrouping<TKey, TElement> : IEnumerable<TElement>
    {
        /// <summary>
        /// Gets the key of the System.Linq.IGrouping&lt;TKey,TElement&gt;.
        /// </summary>
        /// <returns>The key of the System.Linq.IGrouping&lt;TKey,TElement&gt;.</returns>
        TKey Key { get; }
    }
}
