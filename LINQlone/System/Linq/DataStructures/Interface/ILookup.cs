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
    /// Defines an indexer, size property, and Boolean search method for data structures that map keys to System.Collections.Generic.IEnumerable&lt;T&gt; sequences of values.
    /// </summary>
    /// <typeparam name="TKey">The type of the keys in the System.Linq.ILookup&lt;TKey,TElement&gt;.</typeparam>
    /// <typeparam name="TElement">The type of the elements in the System.Collections.Generic.IEnumerable&lt;T&gt; sequences that make up the values in the System.Linq.ILookup&lt;TKey,TElement&gt;.</typeparam>
    public interface ILookup<TKey, TElement> : IEnumerable<IGrouping<TKey, TElement>>
    {
        /// <summary>
        /// Gets the number of key/value collection pairs in the System.Linq.ILookup&lt;TKey,TElement&gt;.
        /// </summary>
        /// <returns>The number of key/value collection pairs in the System.Linq.ILookup&lt;TKey,TElement&gt;.</returns>
        int Count { get; }

        /// <summary>
        /// Gets the System.Collections.Generic.IEnumerable&lt;T&gt; sequence of values indexed by a specified key.
        /// </summary>
        /// <param name="key">The key of the desired sequence of values.</param>
        /// <returns>The System.Collections.Generic.IEnumerable&lt;T&gt; sequence of values indexed by the specified key.</returns>
        IEnumerable<TElement> this[TKey key] { get; }

        /// <summary>
        /// Determines whether a specified key exists in the System.Linq.ILookup&lt;TKey,TElement&gt;.
        /// </summary>
        /// <param name="key">The key to search for in the System.Linq.ILookup&lt;TKey,TElement&gt;.</param>
        /// <returns>true if key is in the System.Linq.ILookup&lt;TKey,TElement&gt;; otherwise, false.</returns>
        bool Contains(TKey key);
    }
}
