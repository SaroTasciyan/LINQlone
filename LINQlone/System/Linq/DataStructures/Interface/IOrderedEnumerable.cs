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
    /// Represents a sorted sequence.
    /// </summary>
    /// <typeparam name="TElement">The type of the elements of the sequence.</typeparam>
    public interface IOrderedEnumerable<TElement> : IEnumerable<TElement>
    {
        /// <summary>
        /// Performs a subsequent ordering on the elements of an System.Linq.IOrderedEnumerable&lt;TElement&gt; according to a key.
        /// </summary>
        /// <param name="keySelector">The System.Func&lt;T,TResult&gt; used to extract the key for each element.</param>
        /// <param name="comparer">The System.Collections.Generic.IComparer&lt;T&gt; used to compare keys for placement in the returned sequence.</param>
        /// <param name="descending">true to sort the elements in descending order; false to sort the elements in ascending order.</param>
        /// <typeparam name="TKey">The type of the key produced by keySelector.</typeparam>
        /// <returns>An System.Linq.IOrderedEnumerable&lt;TElement&gt; whose elements are sorted according to a key.</returns>
        IOrderedEnumerable<TElement> CreateOrderedEnumerable<TKey>(Func<TElement, TKey> keySelector, IComparer<TKey> comparer, bool descending);
    }
}
