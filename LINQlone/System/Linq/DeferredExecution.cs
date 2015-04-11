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

using System.Collections;
using System.Collections.Generic;

using LINQlone.Definitions;
using LINQlone.Infrastructure;

namespace System.Linq
{
    /// <summary>
    /// Provides a set of static (Shared in Visual Basic) methods for querying objects that implement System.Collections.Generic.IEnumerable&lt;T&gt;.
    /// </summary>
    public static partial class Enumerable
    {
        #region Extension Method Overloads

        #region Select Overloads
        
        /// <summary>
        /// Projects each element of a sequence into a new form.
        /// </summary>
        /// <param name="source">A sequence of values to invoke a transform function on.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TResult">The type of the value returned by selector.</typeparam>
        /// <returns>An System.Collections.Generic.IEnumerable&lt;T&gt; whose elements are the result of invoking the transform function on each element of source.</returns>
        /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
        public static IEnumerable<TResult> Select<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }
            if (selector == null) { throw Exceptions.ArgumentNull(Parameter.Selector); }

            return SelectYielder(source, selector);
        }

        /// <summary>
        /// Projects each element of a sequence into a new form by incorporating the element's index.
        /// </summary>
        /// <param name="source">A sequence of values to invoke a transform function on.</param>
        /// <param name="selector">A transform function to apply to each source element; the second parameter of the function represents the index of the source element.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TResult">The type of the value returned by selector.</typeparam>
        /// <returns>An System.Collections.Generic.IEnumerable&lt;T&gt; whose elements are the result of invoking the transform function on each element of source.</returns>
        /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
        public static IEnumerable<TResult> Select<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, int, TResult> selector)
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }
            if (selector == null) { throw Exceptions.ArgumentNull(Parameter.Selector); }

            return SelectYielder(source, selector);
        }

        #endregion ENDOF: Select Overloads

        #region SelectMany Overloads

        /// <summary>
        /// Projects each element of a sequence to an System.Collections.Generic.IEnumerable&lt;T&gt; and flattens the resulting sequences into one sequence.
        /// </summary>
        /// <param name="source">A sequence of values to project.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TResult">The type of the elements of the sequence returned by selector.</typeparam>
        /// <returns>An System.Collections.Generic.IEnumerable&lt;T&gt; whose elements are the result of invoking the one-to-many transform function on each element of the input sequence.</returns>
        /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
        public static IEnumerable<TResult> SelectMany<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, IEnumerable<TResult>> selector)
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }
            if (selector == null) { throw Exceptions.ArgumentNull(Parameter.Selector); }

            return SelectManyYielder(source, selector);
        }

        /// <summary>
        /// Projects each element of a sequence to an System.Collections.Generic.IEnumerable&lt;T&gt;, and flattens the resulting sequences into one sequence. The index of each source element is used in the projected form of that element.
        /// </summary>
        /// <param name="source">A sequence of values to project.</param>
        /// <param name="selector">A transform function to apply to each source element; the second parameter of the function represents the index of the source element.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TResult">The type of the elements of the sequence returned by selector.</typeparam>
        /// <returns>An System.Collections.Generic.IEnumerable&lt;T&gt; whose elements are the result of invoking the one-to-many transform function on each element of an input sequence.</returns>
        /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
        public static IEnumerable<TResult> SelectMany<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, int, IEnumerable<TResult>> selector)
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }
            if (selector == null) { throw Exceptions.ArgumentNull(Parameter.Selector); }

            return SelectManyYielder(source, selector);
        }

        /// <summary>
        /// Projects each element of a sequence to an System.Collections.Generic.IEnumerable&lt;T&gt;, flattens the resulting sequences into one sequence, and invokes a result selector function on each element therein.
        /// </summary>
        /// <param name="source">A sequence of values to project.</param>
        /// <param name="collectionSelector">A transform function to apply to each element of the input sequence.</param>
        /// <param name="resultSelector">A transform function to apply to each element of the intermediate sequence.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TCollection">The type of the intermediate elements collected by collectionSelector.</typeparam>
        /// <typeparam name="TResult">The type of the elements of the resulting sequence.</typeparam>
        /// <returns>An System.Collections.Generic.IEnumerable&lt;T&gt; whose elements are the result of invoking the one-to-many transform function collectionSelector on each element of source and then mapping each of those sequence elements and their corresponding source element to a result element.</returns>
        /// <exception cref="System.ArgumentNullException">source or collectionSelector or resultSelector is null.</exception>
        public static IEnumerable<TResult> SelectMany<TSource, TCollection, TResult>(this IEnumerable<TSource> source, Func<TSource, IEnumerable<TCollection>> collectionSelector, Func<TSource, TCollection, TResult> resultSelector)
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }
            if (collectionSelector == null) { throw Exceptions.ArgumentNull(Parameter.CollectionSelector); }
            if (resultSelector == null) { throw Exceptions.ArgumentNull(Parameter.ResultSelector); }

            return SelectManyYielder(source, collectionSelector, resultSelector);
        }

        /// <summary>
        /// Projects each element of a sequence to an System.Collections.Generic.IEnumerable&lt;T&gt;, flattens the resulting sequences into one sequence, and invokes a result selector function on each element therein. The index of each source element is used in the intermediate projected form of that element.
        /// </summary>
        /// <param name="source">A sequence of values to project.</param>
        /// <param name="collectionSelector">A transform function to apply to each source element; the second parameter of the function represents the index of the source element.</param>
        /// <param name="resultSelector">A transform function to apply to each element of the intermediate sequence.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TCollection">The type of the intermediate elements collected by collectionSelector.</typeparam>
        /// <typeparam name="TResult">The type of the elements of the resulting sequence.</typeparam>
        /// <returns>An System.Collections.Generic.IEnumerable&lt;T&gt; whose elements are the result of invoking the one-to-many transform function collectionSelector on each element of source and then mapping each of those sequence elements and their corresponding source element to a result element.</returns>
        /// <exception cref="System.ArgumentNullException">source or collectionSelector or resultSelector is null.</exception>
        public static IEnumerable<TResult> SelectMany<TSource, TCollection, TResult>(this IEnumerable<TSource> source, Func<TSource, int, IEnumerable<TCollection>> collectionSelector, Func<TSource, TCollection, TResult> resultSelector)
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }
            if (collectionSelector == null) { throw Exceptions.ArgumentNull(Parameter.CollectionSelector); }
            if (resultSelector == null) { throw Exceptions.ArgumentNull(Parameter.ResultSelector); }

            return SelectManyYielder(source, collectionSelector, resultSelector);
        }

        #endregion ENDOF: SelectMany Overloads

        #region Where Overloads

        /// <summary>
        /// Filters a sequence of values based on a predicate.
        /// </summary>
        /// <param name="source">An System.Collections.Generic.IEnumerable&lt;T&gt; to filter.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <returns>An System.Collections.Generic.IEnumerable&lt;T&gt; that contains elements from the input sequence that satisfy the condition.</returns>
        /// <exception cref="System.ArgumentNullException">source or predicate is null.</exception>
        public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }
            if (predicate == null) { throw Exceptions.ArgumentNull(Parameter.Predicate); }

            return WhereYielder(source, predicate);
        }

        /// <summary>
        /// Filters a sequence of values based on a predicate. Each element's index is used in the logic of the predicate function.
        /// </summary>
        /// <param name="source">An System.Collections.Generic.IEnumerable&lt;T&gt; to filter.</param>
        /// <param name="predicate">A function to test each source element for a condition; the second parameter of the function represents the index of the source element.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <returns>An System.Collections.Generic.IEnumerable&lt;T&gt; that contains elements from the input sequence that satisfy the condition.</returns>
        /// <exception cref="System.ArgumentNullException">source or predicate is null.</exception>
        public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source, Func<TSource, int, bool> predicate)
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }
            if (predicate == null) { throw Exceptions.ArgumentNull(Parameter.Predicate); }

            return WhereYielder(source, predicate);
        }

        #endregion ENDOF: Where Overloads

        #region OfType Overloads

        /// <summary>
        /// Filters the elements of an System.Collections.IEnumerable based on a specified type.
        /// </summary>
        /// <param name="source">The System.Collections.IEnumerable whose elements to filter.</param>
        /// <typeparam name="TResult">The type to filter the elements of the sequence on.</typeparam>
        /// <returns>An System.Collections.Generic.IEnumerable&lt;T&gt; that contains elements from the input sequence of type TResult.</returns>
        /// <exception cref="System.ArgumentNullException">source is null.</exception>
        public static IEnumerable<TResult> OfType<TResult>(this IEnumerable source)
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }

            return OfTypeYielder<TResult>(source);
        }

        #endregion ENDOF: OfType Overloads

        #region Cast Overloads

        /// <summary>
        /// Converts the elements of an System.Collections.IEnumerable to the specified type.
        /// </summary>
        /// <param name="source">The System.Collections.IEnumerable that contains the elements to be converted.</param>
        /// <typeparam name="TResult">The type to convert the elements of source to.</typeparam>
        /// <returns>An System.Collections.Generic.IEnumerable&lt;T&gt; that contains each element of the source sequence converted to the specified type.</returns>
        /// <exception cref="System.ArgumentNullException">source is null.</exception>
        /// <exception cref="System.InvalidCastException">An element in the sequence cannot be cast to type TResult.</exception>
        public static IEnumerable<TResult> Cast<TResult>(this IEnumerable source) // # Cast extensions, throws an exception if there is any record that can not be converted.
        {
            IEnumerable<TResult> enumerable = source as IEnumerable<TResult>;
            if (enumerable != null) { return enumerable; }

            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }

            return CastYielder<TResult>(source);
        }

        #endregion ENDOF: Cast Overloads

        #region Take Overloads

        /// <summary>
        /// Returns a specified number of contiguous elements from the start of a sequence.
        /// </summary>
        /// <param name="source">The sequence to return elements from.</param>
        /// <param name="count">The number of elements to return.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <returns>An System.Collections.Generic.IEnumerable&lt;T&gt; that contains the specified number of elements from the start of the input sequence.</returns>
        /// <exception cref="System.ArgumentNullException">source is null.</exception>
        public static IEnumerable<TSource> Take<TSource>(this IEnumerable<TSource> source, int count)
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }

            return TakeYielder(source, count);
        }

        #endregion ENDOF: Take Overloads

        #region Skip Overloads

        /// <summary>
        /// Bypasses a specified number of elements in a sequence and then returns the remaining elements.
        /// </summary>
        /// <param name="source">An System.Collections.Generic.IEnumerable&lt;T&gt; to return elements from.</param>
        /// <param name="count">The number of elements to skip before returning the remaining elements.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <returns>An System.Collections.Generic.IEnumerable&lt;T&gt; that contains the elements that occur after the specified index in the input sequence.</returns>
        /// <exception cref="System.ArgumentNullException">source is null.</exception>
        public static IEnumerable<TSource> Skip<TSource>(this IEnumerable<TSource> source, int count)
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }

            return SkipYielder(source, count);
        }

        #endregion ENDOF: Skip Overloads

        #region TakeWhile Overloads

        /// <summary>
        /// Returns elements from a sequence as long as a specified condition is true.
        /// </summary>
        /// <param name="source">A sequence to return elements from.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <returns>An System.Collections.Generic.IEnumerable&lt;T&gt; that contains the elements from the input sequence that occur before the element at which the test no longer passes.</returns>
        /// <exception cref="System.ArgumentNullException">source or predicate is null.</exception>
        public static IEnumerable<TSource> TakeWhile<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }
            if (predicate == null) { throw Exceptions.ArgumentNull(Parameter.Predicate); }

            return TakeWhileYielder(source, predicate);
        }

        /// <summary>
        /// Returns elements from a sequence as long as a specified condition is true. The element's index is used in the logic of the predicate function.
        /// </summary>
        /// <param name="source">The sequence to return elements from.</param>
        /// <param name="predicate">A function to test each source element for a condition; the second parameter of the function represents the index of the source element.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <returns>An System.Collections.Generic.IEnumerable&lt;T&gt; that contains elements from the input sequence that occur before the element at which the test no longer passes.</returns>
        /// <exception cref="System.ArgumentNullException">source or predicate is null.</exception>
        public static IEnumerable<TSource> TakeWhile<TSource>(this IEnumerable<TSource> source, Func<TSource, int, bool> predicate)
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }
            if (predicate == null) { throw Exceptions.ArgumentNull(Parameter.Predicate); }

            return TakeWhileYielder(source, predicate);
        }

        #endregion ENDOF: TakeWhile Overloads

        #region SkipWhile Overloads

        /// <summary>
        /// Bypasses elements in a sequence as long as a specified condition is true and then returns the remaining elements.
        /// </summary>
        /// <param name="source">An System.Collections.Generic.IEnumerable&lt;T&gt; to return elements from.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <returns>An System.Collections.Generic.IEnumerable&lt;T&gt; that contains the elements from the input sequence starting at the first element in the linear series that does not pass the test specified by predicate.</returns>
        /// <exception cref="System.ArgumentNullException">source or predicate is null.</exception>
        public static IEnumerable<TSource> SkipWhile<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }
            if (predicate == null) { throw Exceptions.ArgumentNull(Parameter.Predicate); }

            return SkipWhileYielder(source, predicate);
        }

        /// <summary>
        /// Bypasses elements in a sequence as long as a specified condition is true and then returns the remaining elements. The element's index is used in the logic of the predicate function.
        /// </summary>
        /// <param name="source">An System.Collections.Generic.IEnumerable&lt;T&gt; to return elements from.</param>
        /// <param name="predicate">A function to test each source element for a condition; the second parameter of the function represents the index of the source element.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <returns>An System.Collections.Generic.IEnumerable&lt;T&gt; that contains the elements from the input sequence starting at the first element in the linear series that does not pass the test specified by predicate.</returns>
        /// <exception cref="System.ArgumentNullException">source or predicate is null.</exception>
        public static IEnumerable<TSource> SkipWhile<TSource>(this IEnumerable<TSource> source, Func<TSource, int, bool> predicate)
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }
            if (predicate == null) { throw Exceptions.ArgumentNull(Parameter.Predicate); }

            return SkipWhileYielder(source, predicate);
        }

        #endregion ENDOF: SkipWhile Overloads

        #region DefaultIfEmpty Overloads

        /// <summary>
        /// Returns the elements of the specified sequence or the type parameter's default value in a singleton collection if the sequence is empty.
        /// </summary>
        /// <param name="source">The sequence to return a default value for if it is empty.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <returns>An System.Collections.Generic.IEnumerable&lt;T&gt; object that contains the default value for the TSource type if source is empty; otherwise, source.</returns>
        /// <exception cref="System.ArgumentNullException">source is null.</exception>
        public static IEnumerable<TSource> DefaultIfEmpty<TSource>(this IEnumerable<TSource> source)
        {
            return DefaultIfEmpty(source, default(TSource));
        }

        /// <summary>
        /// Returns the elements of the specified sequence or the specified value in a singleton collection if the sequence is empty.
        /// </summary>
        /// <param name="source">The sequence to return the specified value for if it is empty.</param>
        /// <param name="defaultValue">The value to return if the sequence is empty.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <returns>An System.Collections.Generic.IEnumerable&lt;T&gt; that contains defaultValue if source is empty; otherwise, source.</returns>
        public static IEnumerable<TSource> DefaultIfEmpty<TSource>(this IEnumerable<TSource> source, TSource defaultValue)
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); } // # Source ArgumentNullException is not documented, but it occurs when LINQ method is tested.

            return DefaultIfEmptyYielder(source, defaultValue);
        }

        #endregion ENDOF: DefaultIfEmpty Overloads

        #region AsEnumerable Overloads

        /// <summary>
        /// Returns the input typed as System.Collections.Generic.IEnumerable&lt;T&gt;.
        /// </summary>
        /// <param name="source">The sequence to type as System.Collections.Generic.IEnumerable&lt;T&gt;.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <returns>The input sequence typed as System.Collections.Generic.IEnumerable&lt;T&gt;.</returns>
        public static IEnumerable<TSource> AsEnumerable<TSource>(this IEnumerable<TSource> source)
        {
            return source;
        }

        #endregion ENDOF: AsEnumerable Overloads

        #region Repeat Overloads

        /// <summary>
        /// Generates a sequence that contains one repeated value.
        /// </summary>
        /// <param name="element">The value to be repeated.</param>
        /// <param name="count">The number of times to repeat the value in the generated sequence.</param>
        /// <typeparam name="TResult">The type of the value to be repeated in the result sequence.</typeparam>
        /// <returns>An System.Collections.Generic.IEnumerable&lt;T&gt; that contains a repeated value.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">count is less than 0.</exception>
        public static IEnumerable<TResult> Repeat<TResult>(TResult element, int count)
        {
            if (count < 0) { throw Exceptions.ArgumentOutOfRange(Parameter.Count); }

            return RepeatYielder(element, count);
        }

        #endregion ENDOF: Repeat Overloads

        #region Range Overloads

        /// <summary>
        /// Generates a sequence of integral numbers within a specified range.
        /// </summary>
        /// <param name="start">The value of the first integer in the sequence.</param>
        /// <param name="count">The number of sequential integers to generate.</param>
        /// <returns>An IEnumerable&lt;Int32&gt; in C# or IEnumerable(Of Int32) in Visual Basic that contains a range of sequential integral numbers.</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">count is less than 0.-or-start + count -1 is larger than System.Int32.MaxValue.</exception>
        public static IEnumerable<int> Range(int start, int count)
        {
            if (count < 0) { throw Exceptions.ArgumentOutOfRange(Parameter.Count); }

            // # Sum of start and count value -1 can exceed integer max value and cause an overflow. 
            // # Unchecked overflow causes the value evaluated to be negative and Int32.MaxValue comparison to be wrong.
            // # Checking if LONG range maximum value exceeds integer max value limit; ArgumentOutOfRangeException is thrown instead
            long rangeMaxValue = (long)start + (long)count - 1L; 
            if (rangeMaxValue > Int32.MaxValue) { throw Exceptions.ArgumentOutOfRange(Parameter.Count); }

            return RangeYielder(start, count);
        }

        #endregion ENDOF: Range Overloads

        #region Concat Overloads

        /// <summary>
        /// Concatenates two sequences.
        /// </summary>
        /// <param name="first">The first sequence to concatenate.</param>
        /// <param name="second">The sequence to concatenate to the first sequence.</param>
        /// <typeparam name="TSource">The type of the elements of the input sequences.</typeparam>
        /// <returns>An System.Collections.Generic.IEnumerable&lt;T&gt; that contains the concatenated elements of the two input sequences.</returns>
        /// <exception cref="System.ArgumentNullException">first or second is null.</exception>
        public static IEnumerable<TSource> Concat<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second)
        {
            if (first == null) { throw Exceptions.ArgumentNull(Parameter.First); }
            if (second == null) { throw Exceptions.ArgumentNull(Parameter.Second); }

            return ConcatYielder(first, second);
        }

        #endregion ENDOF: Concat Overloads

        #region Distinct Overloads

        /// <summary>
        /// Returns distinct elements from a sequence by using the default equality comparer to compare values.
        /// </summary>
        /// <param name="source">The sequence to remove duplicate elements from.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <returns>An System.Collections.Generic.IEnumerable&lt;T&gt; that contains distinct elements from the source sequence.</returns>
        /// <exception cref="System.ArgumentNullException">source is null.</exception>
        public static IEnumerable<TSource> Distinct<TSource>(this IEnumerable<TSource> source)
        {
            return Distinct(source, null);
        }

        /// <summary>
        /// Returns distinct elements from a sequence by using a specified System.Collections.Generic.IEqualityComparer&lt;T&gt; to compare values.
        /// </summary>
        /// <param name="source">The sequence to remove duplicate elements from.</param>
        /// <param name="comparer">An System.Collections.Generic.IEqualityComparer&lt;T&gt; to compare values.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <returns>An System.Collections.Generic.IEnumerable&lt;T&gt; that contains distinct elements from the source sequence.</returns>
        /// <exception cref="System.ArgumentNullException">source is null.</exception>
        public static IEnumerable<TSource> Distinct<TSource>(this IEnumerable<TSource> source, IEqualityComparer<TSource> comparer)
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }

            return DistinctYielder(source, comparer);
        }

        #endregion ENDOF: Distinct Overloads

        #region Union Overloads

        /// <summary>
        /// Produces the union of two sequences by using the default equality comparer.
        /// </summary>
        /// <param name="first">An System.Collections.Generic.IEnumerable&lt;T&gt; whose distinct elements form the first set for the union.</param>
        /// <param name="second">An System.Collections.Generic.IEnumerable&lt;T&gt; whose distinct elements form the second set for the union.</param>
        /// <typeparam name="TSource">The type of the elements of the input sequences.</typeparam>
        /// <returns>An System.Collections.Generic.IEnumerable&lt;T&gt; that contains the elements from both input sequences, excluding duplicates.</returns>
        /// <exception cref="System.ArgumentNullException">first or second is null.</exception>
        public static IEnumerable<TSource> Union<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second)
        {
            return Union(first, second, null);
        }

        /// <summary>
        /// Produces the set union of two sequences by using a specified System.Collections.Generic.IEqualityComparer&lt;T&gt;.
        /// </summary>
        /// <param name="first">An System.Collections.Generic.IEnumerable&lt;T&gt; whose distinct elements form the first set for the union.</param>
        /// <param name="second">An System.Collections.Generic.IEnumerable&lt;T&gt; whose distinct elements form the second set for the union.</param>
        /// <param name="comparer">The System.Collections.Generic.IEqualityComparer&lt;T&gt; to compare values.</param>
        /// <typeparam name="TSource">The type of the elements of the input sequences.</typeparam>
        /// <returns>An System.Collections.Generic.IEnumerable&lt;T&gt; that contains the elements from both input sequences, excluding duplicates.</returns>
        /// <exception cref="System.ArgumentNullException">first or second is null.</exception>
        public static IEnumerable<TSource> Union<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
        {
            if (first == null) { throw Exceptions.ArgumentNull(Parameter.First); }
            if (second == null) { throw Exceptions.ArgumentNull(Parameter.Second); }

            return UnionYielder(first, second, comparer);
        }

        #endregion ENDOF: Union Overloads

        #region Except Overloads

        /// <summary>
        /// Produces the set difference of two sequences by using the default equality comparer to compare values.
        /// </summary>
        /// <param name="first">An System.Collections.Generic.IEnumerable&lt;T&gt; whose elements that are not also in second will be returned.</param>
        /// <param name="second">An System.Collections.Generic.IEnumerable&lt;T&gt; whose elements that also occur in the first sequence will cause those elements to be removed from the returned sequence.</param>
        /// <typeparam name="TSource">The type of the elements of the input sequences.</typeparam>
        /// <returns>A sequence that contains the set difference of the elements of two sequences.</returns>
        /// <exception cref="System.ArgumentNullException">first or second is null.</exception>
        public static IEnumerable<TSource> Except<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second)
        {
            return Except(first, second, null);
        }

        /// <summary>
        /// Produces the set difference of two sequences by using the specified System.Collections.Generic.IEqualityComparer&lt;T&gt; to compare values.
        /// </summary>
        /// <param name="first">An System.Collections.Generic.IEnumerable&lt;T&gt; whose elements that are not also in second will be returned.</param>
        /// <param name="second">An System.Collections.Generic.IEnumerable&lt;T&gt; whose elements that also occur in the first sequence will cause those elements to be removed from the returned sequence.</param>
        /// <param name="comparer">An System.Collections.Generic.IEqualityComparer&lt;T&gt; to compare values.</param>
        /// <typeparam name="TSource">The type of the elements of the input sequences.</typeparam>
        /// <returns>A sequence that contains the set difference of the elements of two sequences.</returns>
        /// <exception cref="System.ArgumentNullException">first or second is null.</exception>
        public static IEnumerable<TSource> Except<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
        {
            if (first == null) { throw Exceptions.ArgumentNull(Parameter.First); }
            if (second == null) { throw Exceptions.ArgumentNull(Parameter.Second); }

            return ExceptYielder(first, second, comparer);
        }

        #endregion ENDOF: Except Overloads

        #region Intersect Overloads

        /// <summary>
        /// Produces the set intersection of two sequences by using the default equality comparer to compare values.
        /// </summary>
        /// <param name="first">An System.Collections.Generic.IEnumerable&lt;T&gt; whose distinct elements that also appear in second will be returned.</param>
        /// <param name="second">An System.Collections.Generic.IEnumerable&lt;T&gt; whose distinct elements that also appear in the first sequence will be returned.</param>
        /// <typeparam name="TSource">The type of the elements of the input sequences.</typeparam>
        /// <returns>A sequence that contains the elements that form the set intersection of two sequences.</returns>
        /// <exception cref="System.ArgumentNullException">first or second is null.</exception>
        public static IEnumerable<TSource> Intersect<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second)
        {
            return Intersect(first, second, null);
        }

        /// <summary>
        /// Produces the set intersection of two sequences by using the specified System.Collections.Generic.IEqualityComparer&lt;T&gt; to compare values.
        /// </summary>
        /// <param name="first">An System.Collections.Generic.IEnumerable&lt;T&gt; whose distinct elements that also appear in second will be returned.</param>
        /// <param name="second">An System.Collections.Generic.IEnumerable&lt;T&gt; whose distinct elements that also appear in the first sequence will be returned.</param>
        /// <param name="comparer">An System.Collections.Generic.IEqualityComparer&lt;T&gt; to compare values.</param>
        /// <typeparam name="TSource">The type of the elements of the input sequences.</typeparam>
        /// <returns>A sequence that contains the elements that form the set intersection of two sequences.</returns>
        /// <exception cref="System.ArgumentNullException">first or second is null.</exception>
        public static IEnumerable<TSource> Intersect<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
        {
            if (first == null) { throw Exceptions.ArgumentNull(Parameter.First); }
            if (second == null) { throw Exceptions.ArgumentNull(Parameter.Second); }

            return IntersectYielder(first, second, comparer);
        }

        #endregion ENDOF: Intersect Overloads

        #region Reverse Overloads

        /// <summary>
        /// Inverts the order of the elements in a sequence.
        /// </summary>
        /// <param name="source">A sequence of values to reverse.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <returns>A sequence whose elements correspond to those of the input sequence in reverse order.</returns>
        /// <exception cref="System.ArgumentNullException">source is null.</exception>
        public static IEnumerable<TSource> Reverse<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }

            return ReverseYielder(source);
        }

        #endregion ENDOF: Reverse Overloads

        #region Join Overloads

        /// <summary>
        /// Correlates the elements of two sequences based on matching keys. The default equality comparer is used to compare keys.
        /// </summary>
        /// <param name="outer">The first sequence to join.</param>
        /// <param name="inner">The sequence to join to the first sequence.</param>
        /// <param name="outerKeySelector">A function to extract the join key from each element of the first sequence.</param>
        /// <param name="innerKeySelector">A function to extract the join key from each element of the second sequence.</param>
        /// <param name="resultSelector">A function to create a result element from two matching elements.</param>
        /// <typeparam name="TOuter">The type of the elements of the first sequence.</typeparam>
        /// <typeparam name="TInner">The type of the elements of the second sequence.</typeparam>
        /// <typeparam name="TKey">The type of the keys returned by the key selector functions.</typeparam>
        /// <typeparam name="TResult">The type of the result elements.</typeparam>
        /// <returns>An System.Collections.Generic.IEnumerable&lt;T&gt; that has elements of type TResult that are obtained by performing an inner join on two sequences.</returns>
        /// <exception cref="System.ArgumentNullException">outer or inner or outerKeySelector or innerKeySelector or resultSelector is null.</exception>
        public static IEnumerable<TResult> Join<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector)
        {
            return Join(outer, inner, outerKeySelector, innerKeySelector, resultSelector, null);
        }

        /// <summary>
        /// Correlates the elements of two sequences based on matching keys. A specified System.Collections.Generic.IEqualityComparer&lt;T&gt; is used to compare keys.
        /// </summary>
        /// <param name="outer">The first sequence to join.</param>
        /// <param name="inner">The sequence to join to the first sequence.</param>
        /// <param name="outerKeySelector">A function to extract the join key from each element of the first sequence.</param>
        /// <param name="innerKeySelector">A function to extract the join key from each element of the second sequence.</param>
        /// <param name="resultSelector">A function to create a result element from two matching elements.</param>
        /// <param name="comparer">An System.Collections.Generic.IEqualityComparer&lt;T&gt; to hash and compare keys.</param>
        /// <typeparam name="TOuter">The type of the elements of the first sequence.</typeparam>
        /// <typeparam name="TInner">The type of the elements of the second sequence.</typeparam>
        /// <typeparam name="TKey">The type of the keys returned by the key selector functions.</typeparam>
        /// <typeparam name="TResult">The type of the result elements.</typeparam>
        /// <returns>An System.Collections.Generic.IEnumerable&lt;T&gt; that has elements of type TResult that are obtained by performing an inner join on two sequences.</returns>
        /// <exception cref="System.ArgumentNullException">outer or inner or outerKeySelector or innerKeySelector or resultSelector is null.</exception>
        public static IEnumerable<TResult> Join<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector, IEqualityComparer<TKey> comparer)
        {
            if (outer == null) { throw Exceptions.ArgumentNull(Parameter.Outer); }
            if (inner == null) { throw Exceptions.ArgumentNull(Parameter.Inner); }
            if (outerKeySelector == null) { throw Exceptions.ArgumentNull(Parameter.OuterKeySelector); }
            if (innerKeySelector == null) { throw Exceptions.ArgumentNull(Parameter.InnerKeySelector); }
            if (resultSelector == null) { throw Exceptions.ArgumentNull(Parameter.ResultSelector); }

            return JoinYielder(outer, inner, outerKeySelector, innerKeySelector, resultSelector, comparer);
        }

        #endregion ENDOF: Join Overloads

        #region GroupBy Overloads

        /// <summary>
        /// Groups the elements of a sequence according to a specified key selector function.
        /// </summary>
        /// <param name="source">An System.Collections.Generic.IEnumerable&lt;T&gt; whose elements to group.</param>
        /// <param name="keySelector">A function to extract the key for each element.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by keySelector.</typeparam>
        /// <returns>An IEnumerable&lt;IGrouping&lt;TKey, TSource&gt;&gt; in C# or IEnumerable(Of IGrouping(Of TKey, TSource)) in Visual Basic where each System.Linq.IGrouping&lt;TKey,TElement&gt; object contains a sequence of objects and a key.</returns>
        /// <exception cref="System.ArgumentNullException">source or keySelector is null.</exception>
        public static IEnumerable<IGrouping<TKey, TSource>> GroupBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            return GroupBy(source, keySelector, null);
        }

        /// <summary>
        /// Groups the elements of a sequence according to a specified key selector function and creates a result value from each group and its key.
        /// </summary>
        /// <param name="source">An System.Collections.Generic.IEnumerable&lt;T&gt; whose elements to group.</param>
        /// <param name="keySelector">A function to extract the key for each element.</param>
        /// <param name="resultSelector">A function to create a result value from each group.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by keySelector.</typeparam>
        /// <typeparam name="TResult">The type of the result value returned by resultSelector.</typeparam>
        /// <returns>A collection of elements of type TResult where each element represents a projection over a group and its key.</returns>
        public static IEnumerable<TResult> GroupBy<TSource, TKey, TResult>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TKey, IEnumerable<TSource>, TResult> resultSelector)
        {
            return GroupBy(source, keySelector, resultSelector, null);
        }

        /// <summary>
        /// Groups the elements of a sequence according to a specified key selector function and projects the elements for each group by using a specified function.
        /// </summary>
        /// <param name="source">An System.Collections.Generic.IEnumerable&lt;T&gt; whose elements to group.</param>
        /// <param name="keySelector">A function to extract the key for each element.</param>
        /// <param name="elementSelector">A function to map each source element to an element in the System.Linq.IGrouping&lt;TKey,TElement&gt;.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by keySelector.</typeparam>
        /// <typeparam name="TElement">The type of the elements in the System.Linq.IGrouping&lt;TKey,TElement&gt;.</typeparam>
        /// <returns>An IEnumerable&lt;IGrouping&lt;TKey, TElement&gt;&gt; in C# or IEnumerable(Of IGrouping(Of TKey, TElement)) in Visual Basic where each System.Linq.IGrouping&lt;TKey,TElement&gt; object contains a collection of objects of type TElement and a key.</returns>
        /// <exception cref="System.ArgumentNullException">source or keySelector or elementSelector is null.</exception>
        public static IEnumerable<IGrouping<TKey, TElement>> GroupBy<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
        {
            return GroupBy(source, keySelector, elementSelector, null);
        }

        /// <summary>
        /// Groups the elements of a sequence according to a specified key selector function and compares the keys by using a specified comparer.
        /// </summary>
        /// <param name="source">An System.Collections.Generic.IEnumerable&lt;T&gt; whose elements to group.</param>
        /// <param name="keySelector">A function to extract the key for each element.</param>
        /// <param name="comparer">An System.Collections.Generic.IEqualityComparer&lt;T&gt; to compare keys.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by keySelector.</typeparam>
        /// <returns>An IEnumerable&lt;IGrouping&lt;TKey, TSource&gt;&gt; in C# or IEnumerable(Of IGrouping(Of TKey, TSource)) in Visual Basic where each System.Linq.IGrouping&lt;TKey,TElement&gt; object contains a collection of objects and a key.</returns>
        /// <exception cref="System.ArgumentNullException">source or keySelector is null.</exception>
        public static IEnumerable<IGrouping<TKey, TSource>> GroupBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }
            if (keySelector == null) { throw Exceptions.ArgumentNull(Parameter.KeySelector); }

            return GroupByYielder(source, keySelector, comparer);
        }

        /// <summary>
        /// Groups the elements of a sequence according to a specified key selector function and creates a result value from each group and its key. The keys are compared by using a specified comparer.
        /// </summary>
        /// <param name="source">An System.Collections.Generic.IEnumerable&lt;T&gt; whose elements to group.</param>
        /// <param name="keySelector">A function to extract the key for each element.</param>
        /// <param name="resultSelector">A function to create a result value from each group.</param>
        /// <param name="comparer">An System.Collections.Generic.IEqualityComparer&lt;T&gt; to compare keys with.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by keySelector.</typeparam>
        /// <typeparam name="TResult">The type of the result value returned by resultSelector.</typeparam>
        /// <returns>A collection of elements of type TResult where each element represents a projection over a group and its key.</returns>
        public static IEnumerable<TResult> GroupBy<TSource, TKey, TResult>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TKey, IEnumerable<TSource>, TResult> resultSelector, IEqualityComparer<TKey> comparer)
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }
            if (keySelector == null) { throw Exceptions.ArgumentNull(Parameter.KeySelector); }
            if (resultSelector == null) { throw Exceptions.ArgumentNull(Parameter.ResultSelector); }

            return GroupByYielder(source, keySelector, resultSelector, comparer);
        }

        /// <summary>
        /// Groups the elements of a sequence according to a specified key selector function and creates a result value from each group and its key. The elements of each group are projected by using a specified function.
        /// </summary>
        /// <param name="source">An System.Collections.Generic.IEnumerable&lt;T&gt; whose elements to group.</param>
        /// <param name="keySelector">A function to extract the key for each element.</param>
        /// <param name="elementSelector">A function to map each source element to an element in an System.Linq.IGrouping&lt;TKey,TElement&gt;.</param>
        /// <param name="resultSelector">A function to create a result value from each group.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by keySelector.</typeparam>
        /// <typeparam name="TElement">The type of the elements in each System.Linq.IGrouping&lt;TKey,TElement&gt;.</typeparam>
        /// <typeparam name="TResult">The type of the result value returned by resultSelector.</typeparam>
        /// <returns>A collection of elements of type TResult where each element represents a projection over a group and its key.</returns>
        public static IEnumerable<TResult> GroupBy<TSource, TKey, TElement, TResult>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, Func<TKey, IEnumerable<TElement>, TResult> resultSelector)
        {
            return GroupBy(source, keySelector, elementSelector, resultSelector, null);
        }

        /// <summary>
        /// Groups the elements of a sequence according to a key selector function. The keys are compared by using a comparer and each group's elements are projected by using a specified function.
        /// </summary>
        /// <param name="source">An System.Collections.Generic.IEnumerable&lt;T&gt; whose elements to group.</param>
        /// <param name="keySelector">A function to extract the key for each element.</param>
        /// <param name="elementSelector">A function to map each source element to an element in an System.Linq.IGrouping&lt;TKey,TElement&gt;.</param>
        /// <param name="comparer">An System.Collections.Generic.IEqualityComparer&lt;T&gt; to compare keys.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by keySelector.</typeparam>
        /// <typeparam name="TElement">The type of the elements in the System.Linq.IGrouping&lt;TKey,TElement&gt;.</typeparam>
        /// <returns>An IEnumerable&lt;IGrouping&lt;TKey, TElement&gt;&gt; in C# or IEnumerable(Of IGrouping(Of TKey, TElement)) in Visual Basic where each System.Linq.IGrouping&lt;TKey,TElement&gt; object contains a collection of objects of type TElement and a key.</returns>
        /// <exception cref="System.ArgumentNullException">source or keySelector or elementSelector is null.</exception>
        public static IEnumerable<IGrouping<TKey, TElement>> GroupBy<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey> comparer)
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }
            if (keySelector == null) { throw Exceptions.ArgumentNull(Parameter.KeySelector); }
            if (elementSelector == null) { throw Exceptions.ArgumentNull(Parameter.ElementSelector); }

            return GroupByYielder(source, keySelector, elementSelector, comparer);
        }

        /// <summary>
        /// Groups the elements of a sequence according to a specified key selector function and creates a result value from each group and its key. Key values are compared by using a specified comparer, and the elements of each group are projected by using a specified function.
        /// </summary>
        /// <param name="source">An System.Collections.Generic.IEnumerable&lt;T&gt; whose elements to group.</param>
        /// <param name="keySelector">A function to extract the key for each element.</param>
        /// <param name="elementSelector">A function to map each source element to an element in an System.Linq.IGrouping&lt;TKey,TElement&gt;.</param>
        /// <param name="resultSelector">A function to create a result value from each group.</param>
        /// <param name="comparer">An System.Collections.Generic.IEqualityComparer&lt;T&gt; to compare keys with.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by keySelector.</typeparam>
        /// <typeparam name="TElement">The type of the elements in each System.Linq.IGrouping&lt;TKey,TElement&gt;.</typeparam>
        /// <typeparam name="TResult">The type of the result value returned by resultSelector.</typeparam>
        /// <returns>A collection of elements of type TResult where each element represents a projection over a group and its key.</returns>
        public static IEnumerable<TResult> GroupBy<TSource, TKey, TElement, TResult>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, Func<TKey, IEnumerable<TElement>, TResult> resultSelector, IEqualityComparer<TKey> comparer)
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }
            if (keySelector == null) { throw Exceptions.ArgumentNull(Parameter.KeySelector); }
            if (elementSelector == null) { throw Exceptions.ArgumentNull(Parameter.ElementSelector); }
            if (resultSelector == null) { throw Exceptions.ArgumentNull(Parameter.ResultSelector); }

            return GroupByYielder(source, keySelector, elementSelector, resultSelector, comparer);
        }

        #endregion ENDOF: GroupBy Overloads

        #region GroupJoin Overloads

        /// <summary>
        /// Correlates the elements of two sequences based on equality of keys and groups the results. The default equality comparer is used to compare keys.
        /// </summary>
        /// <param name="outer">The first sequence to join.</param>
        /// <param name="inner">The sequence to join to the first sequence.</param>
        /// <param name="outerKeySelector">A function to extract the join key from each element of the first sequence.</param>
        /// <param name="innerKeySelector">A function to extract the join key from each element of the second sequence.</param>
        /// <param name="resultSelector">A function to create a result element from an element from the first sequence and a collection of matching elements from the second sequence.</param>
        /// <typeparam name="TOuter">The type of the elements of the first sequence.</typeparam>
        /// <typeparam name="TInner">The type of the elements of the second sequence.</typeparam>
        /// <typeparam name="TKey">The type of the keys returned by the key selector functions.</typeparam>
        /// <typeparam name="TResult">The type of the result elements.</typeparam>
        /// <returns>An System.Collections.Generic.IEnumerable&lt;T&gt; that contains elements of type TResult that are obtained by performing a grouped join on two sequences.</returns>
        /// <exception cref="System.ArgumentNullException">outer or inner or outerKeySelector or innerKeySelector or resultSelector is null.</exception>
        public static IEnumerable<TResult> GroupJoin<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, IEnumerable<TInner>, TResult> resultSelector)
        {
            return GroupJoin(outer, inner, outerKeySelector, innerKeySelector, resultSelector, null);
        }

        /// <summary>
        /// Correlates the elements of two sequences based on key equality and groups the results. A specified System.Collections.Generic.IEqualityComparer&lt;T&gt; is used to compare keys.
        /// </summary>
        /// <param name="outer">The first sequence to join.</param>
        /// <param name="inner">The sequence to join to the first sequence.</param>
        /// <param name="outerKeySelector">A function to extract the join key from each element of the first sequence.</param>
        /// <param name="innerKeySelector">A function to extract the join key from each element of the second sequence.</param>
        /// <param name="resultSelector">A function to create a result element from an element from the first sequence and a collection of matching elements from the second sequence.</param>
        /// <param name="comparer">An System.Collections.Generic.IEqualityComparer&lt;T&gt; to hash and compare keys.</param>
        /// <typeparam name="TOuter">The type of the elements of the first sequence.</typeparam>
        /// <typeparam name="TInner">The type of the elements of the second sequence.</typeparam>
        /// <typeparam name="TKey">The type of the keys returned by the key selector functions.</typeparam>
        /// <typeparam name="TResult">The type of the result elements.</typeparam>
        /// <returns>An System.Collections.Generic.IEnumerable&lt;T&gt; that contains elements of type TResult that are obtained by performing a grouped join on two sequences.</returns>
        /// <exception cref="System.ArgumentNullException">outer or inner or outerKeySelector or innerKeySelector or resultSelector is null.</exception>
        public static IEnumerable<TResult> GroupJoin<TOuter, TInner, TKey, TResult>(this IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, IEnumerable<TInner>, TResult> resultSelector, IEqualityComparer<TKey> comparer)
        {
            if (outer == null) { throw Exceptions.ArgumentNull(Parameter.Outer); }
            if (inner == null) { throw Exceptions.ArgumentNull(Parameter.Inner); }
            if (outerKeySelector == null) { throw Exceptions.ArgumentNull(Parameter.OuterKeySelector); }
            if (innerKeySelector == null) { throw Exceptions.ArgumentNull(Parameter.InnerKeySelector); }
            if (resultSelector == null) { throw Exceptions.ArgumentNull(Parameter.ResultSelector); }

            return GroupJoinYielder(outer, inner, outerKeySelector, innerKeySelector, resultSelector, comparer);
        }

        #endregion ENDOF: GroupJoin Overloads

        #region OrderBy Overloads

        /// <summary>
        /// Sorts the elements of a sequence in ascending order according to a key.
        /// </summary>
        /// <param name="source">A sequence of values to order.</param>
        /// <param name="keySelector">A function to extract a key from an element.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by keySelector.</typeparam>
        /// <returns>An System.Linq.IOrderedEnumerable&lt;TElement&gt; whose elements are sorted according to a key.</returns>
        /// <exception cref="System.ArgumentNullException">source or keySelector is null.</exception>
        public static IOrderedEnumerable<TSource> OrderBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            return OrderBy(source, keySelector, null);
        }

        /// <summary>
        /// Sorts the elements of a sequence in ascending order by using a specified comparer.
        /// </summary>
        /// <param name="source">A sequence of values to order.</param>
        /// <param name="keySelector">A function to extract a key from an element.</param>
        /// <param name="comparer">An System.Collections.Generic.IComparer&lt;T&gt; to compare keys.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by keySelector.</typeparam>
        /// <returns>An System.Linq.IOrderedEnumerable&lt;TElement&gt; whose elements are sorted according to a key.</returns>
        /// <exception cref="System.ArgumentNullException">source or keySelector is null.</exception>
        public static IOrderedEnumerable<TSource> OrderBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }
            if (keySelector == null) { throw Exceptions.ArgumentNull(Parameter.KeySelector); }

            return new OrderedEnumerable<TSource, TKey>(source, keySelector, comparer, false);
        }

        #endregion ENDOF: OrderBy Overloads

        #region OrderByDescending Overloads

        /// <summary>
        /// Sorts the elements of a sequence in descending order according to a key.
        /// </summary>
        /// <param name="source">A sequence of values to order.</param>
        /// <param name="keySelector">A function to extract a key from an element.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by keySelector.</typeparam>
        /// <returns>An System.Linq.IOrderedEnumerable&lt;TElement&gt; whose elements are sorted in descending order according to a key.</returns>
        /// <exception cref="System.ArgumentNullException">source or keySelector is null.</exception>
        public static IOrderedEnumerable<TSource> OrderByDescending<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            return OrderByDescending(source, keySelector, null);
        }

        /// <summary>
        /// Sorts the elements of a sequence in descending order by using a specified comparer.
        /// </summary>
        /// <param name="source">A sequence of values to order.</param>
        /// <param name="keySelector">A function to extract a key from an element.</param>
        /// <param name="comparer">An System.Collections.Generic.IComparer&lt;T&gt; to compare keys.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by keySelector.</typeparam>
        /// <returns>An System.Linq.IOrderedEnumerable&lt;TElement&gt; whose elements are sorted in descending order according to a key.</returns>
        /// <exception cref="System.ArgumentNullException">source or keySelector is null.</exception>
        public static IOrderedEnumerable<TSource> OrderByDescending<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }
            if (keySelector == null) { throw Exceptions.ArgumentNull(Parameter.KeySelector); }

            return new OrderedEnumerable<TSource, TKey>(source, keySelector, comparer, true);
        }

        #endregion ENDOF: OrderByDescending Overloads

        #region ThenBy Overloads

        /// <summary>
        /// Performs a subsequent ordering of the elements in a sequence in ascending order according to a key.
        /// </summary>
        /// <param name="source">An System.Linq.IOrderedEnumerable&lt;TElement&gt; that contains elements to sort.</param>
        /// <param name="keySelector">A function to extract a key from each element.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by keySelector.</typeparam>
        /// <returns>An System.Linq.IOrderedEnumerable&lt;TElement&gt; whose elements are sorted according to a key.</returns>
        /// <exception cref="System.ArgumentNullException">source or keySelector is null.</exception>
        public static IOrderedEnumerable<TSource> ThenBy<TSource, TKey>(this IOrderedEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            return ThenBy(source, keySelector, null);
        }

        /// <summary>
        /// Performs a subsequent ordering of the elements in a sequence in ascending order by using a specified comparer.
        /// </summary>
        /// <param name="source">An System.Linq.IOrderedEnumerable&lt;TElement&gt; that contains elements to sort.</param>
        /// <param name="keySelector">A function to extract a key from each element.</param>
        /// <param name="comparer">An System.Collections.Generic.IComparer&lt;T&gt; to compare keys.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by keySelector.</typeparam>
        /// <returns>An System.Linq.IOrderedEnumerable&lt;TElement&gt; whose elements are sorted according to a key.</returns>
        /// <exception cref="System.ArgumentNullException">source or keySelector is null.</exception>
        public static IOrderedEnumerable<TSource> ThenBy<TSource, TKey>(this IOrderedEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }
            if (keySelector == null) { throw Exceptions.ArgumentNull(Parameter.KeySelector); }

            return source.CreateOrderedEnumerable(keySelector, comparer, false);
        }

        #endregion ENDOF: ThenBy Overloads

        #region ThenByDescending Overloads

        /// <summary>
        /// Performs a subsequent ordering of the elements in a sequence in descending order, according to a key.
        /// </summary>
        /// <param name="source">An System.Linq.IOrderedEnumerable&lt;TElement&gt; that contains elements to sort.</param>
        /// <param name="keySelector">A function to extract a key from each element.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by keySelector.</typeparam>
        /// <returns>An System.Linq.IOrderedEnumerable&lt;TElement&gt; whose elements are sorted in descending order according to a key.</returns>
        /// <exception cref="System.ArgumentNullException">source or keySelector is null.</exception>
        public static IOrderedEnumerable<TSource> ThenByDescending<TSource, TKey>(this IOrderedEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            return ThenByDescending(source, keySelector, null);
        }

        /// <summary>
        /// Performs a subsequent ordering of the elements in a sequence in descending order by using a specified comparer.
        /// </summary>
        /// <param name="source">An System.Linq.IOrderedEnumerable&lt;TElement&gt; that contains elements to sort.</param>
        /// <param name="keySelector">A function to extract a key from each element.</param>
        /// <param name="comparer">An System.Collections.Generic.IComparer&lt;T&gt; to compare keys.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by keySelector.</typeparam>
        /// <returns>An System.Linq.IOrderedEnumerable&lt;TElement&gt; whose elements are sorted in descending order according to a key.</returns>
        /// <exception cref="System.ArgumentNullException">source or keySelector is null.</exception>
        public static IOrderedEnumerable<TSource> ThenByDescending<TSource, TKey>(this IOrderedEnumerable<TSource> source, Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }
            if (keySelector == null) { throw Exceptions.ArgumentNull(Parameter.KeySelector); }

            return source.CreateOrderedEnumerable(keySelector, comparer, true);
        }

        #endregion ENDOF: ThenByDescending Overloads

        #endregion ENDOF: Extension Method Overloads

        #region Yielders

        #region Select Yielders

        private static IEnumerable<TResult> SelectYielder<TSource, TResult>(IEnumerable<TSource> source, Func<TSource, int, TResult> selector)
        {
            int index = 0;
            foreach (TSource item in source)
            {
                yield return selector(item, checked(index++));
            }
        }

        private static IEnumerable<TResult> SelectYielder<TSource, TResult>(IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            foreach (TSource item in source)
            {
                yield return selector(item);
            }
        }

        #endregion ENDOF: Select Yielders

        #region SelectMany Yielders

        private static IEnumerable<TResult> SelectManyYielder<TSource, TResult>(IEnumerable<TSource> source, Func<TSource, IEnumerable<TResult>> selector)
        {
            foreach (TSource sourceItem in source)
            {
                foreach (TResult resultItem in selector(sourceItem))
                {
                    yield return resultItem;
                }
            }
        }

        private static IEnumerable<TResult> SelectManyYielder<TSource, TResult>(IEnumerable<TSource> source, Func<TSource, int, IEnumerable<TResult>> selector)
        {
            int index = 0;
            foreach (TSource sourceItem in source)
            {
                foreach (TResult resultItem in selector(sourceItem, checked(index++)))
                {
                    yield return resultItem;
                }
            }
        }

        private static IEnumerable<TResult> SelectManyYielder<TSource, TCollection, TResult>(IEnumerable<TSource> source, Func<TSource, IEnumerable<TCollection>> collectionSelector, Func<TSource, TCollection, TResult> resultSelector)
        {
            foreach (TSource sourceItem in source)
            {
                foreach (TCollection collectionItem in collectionSelector(sourceItem))
                {
                    yield return resultSelector(sourceItem, collectionItem);
                }
            }
        }

        private static IEnumerable<TResult> SelectManyYielder<TSource, TCollection, TResult>(IEnumerable<TSource> source, Func<TSource, int, IEnumerable<TCollection>> collectionSelector, Func<TSource, TCollection, TResult> resultSelector)
        {
            int index = 0;
            foreach (TSource sourceItem in source)
            {
                foreach (TCollection collectionItem in collectionSelector(sourceItem, checked(index++)))
                {
                    yield return resultSelector(sourceItem, collectionItem);
                }
            }
        }

        #endregion ENDOF: SelectMany Yielders

        #region Where Yielders

        private static IEnumerable<TSource> WhereYielder<TSource>(IEnumerable<TSource> source, Func<TSource, int, bool> predicate)
        {
            int index = 0;
            foreach (TSource item in source)
            {
                if (predicate(item, checked(index++))) { yield return item; }
            }
        }

        private static IEnumerable<TSource> WhereYielder<TSource>(IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            foreach (TSource item in source)
            {
                if (predicate(item)) { yield return item; }
            }
        }

        #endregion ENDOF: Where Yielders

        #region OfType Yielders

        private static IEnumerable<TResult> OfTypeYielder<TResult>(IEnumerable source)
        {
            foreach (object item in source)
            {
                if (item is TResult) { yield return (TResult)item; } // # Only items that can be casted to TResult are yielded
            }
        }

        #endregion ENDOF: OfType Yielders

        #region Cast Yielders

        private static IEnumerable<TResult> CastYielder<TResult>(IEnumerable source)
        {
            foreach (object item in source)
            {
                yield return (TResult)item; // # Unlike OfType, InvalidCastException is thrown if item can not be casted to TResult
            }
        }

        #endregion ENDOF: Cast Yielders

        #region Take Yielders

        private static IEnumerable<TSource> TakeYielder<TSource>(IEnumerable<TSource> source, int count)
        {
            if (count > 0)
            {
                using (IEnumerator<TSource> enumerator = source.GetEnumerator())
                {
                    int iterationCount = 0;
                    while (++iterationCount <= count && enumerator.MoveNext())
                    {
                        yield return enumerator.Current;
                    }
                }
            }
        }

        #endregion ENDOF: Take Yielders

        #region Skip Yielders

        private static IEnumerable<TSource> SkipYielder<TSource>(IEnumerable<TSource> source, int count)
        {
            using (IEnumerator<TSource> enumerator = source.GetEnumerator())
            {
                int iterationCount = 0;
                while (++iterationCount <= count && enumerator.MoveNext()) { }

                while (enumerator.MoveNext()) { yield return enumerator.Current; }
            }
        }

        #endregion ENDOF: Skip Yielders

        #region TakeWhile Yielders

        private static IEnumerable<TSource> TakeWhileYielder<TSource>(IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            foreach (TSource item in source)
            {
                if (predicate(item)) { yield return item; }
                else { yield break; }
            }
        }

        private static IEnumerable<TSource> TakeWhileYielder<TSource>(IEnumerable<TSource> source, Func<TSource, int, bool> predicate)
        {
            int index = 0;
            foreach (TSource item in source)
            {
                if (predicate(item, checked(index++))) { yield return item; }
                else { yield break; }
            }
        }

        #endregion ENDOF: TakeWhile Yielders

        #region SkipWhile Yielders

        private static IEnumerable<TSource> SkipWhileYielder<TSource>(IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            bool skipComplete = false;
            foreach (TSource item in source)
            {
                if (!skipComplete && !predicate(item)) { skipComplete = true; }
                if (skipComplete) { yield return item; }
            }
        }

        private static IEnumerable<TSource> SkipWhileYielder<TSource>(IEnumerable<TSource> source, Func<TSource, int, bool> predicate)
        {
            int index = 0;
            bool skipComplete = false;
            foreach (TSource item in source)
            {
                if (!skipComplete && !predicate(item, checked(index++))) { skipComplete = true; }
                if (skipComplete) { yield return item; }
            }
        }

        #endregion ENDOF: SkipWhile Yielders

        #region DefaultIfEmpty Yielders

        private static IEnumerable<TSource> DefaultIfEmptyYielder<TSource>(IEnumerable<TSource> source, TSource defaultValue)
        {
            using (IEnumerator<TSource> enumerator = source.GetEnumerator())
            {
                if (!enumerator.MoveNext()) { yield return defaultValue; }
                else
                {
                    do
                    {
                        yield return enumerator.Current;
                    } while (enumerator.MoveNext());
                }
            }
        }

        #endregion ENDOF: DefaultIfEmpty Yielders

        #region Repeat Yielders

        private static IEnumerable<TResult> RepeatYielder<TResult>(TResult element, int count)
        {
            for (int i = 0; i < count; i++)
            {
                yield return element;
            }
        }

        #endregion ENDOF: Repeat Yielders

        #region Range Yielders

        private static IEnumerable<int> RangeYielder(int start, int count)
        {
            for (int i = 0; i < count; i++)
            {
                yield return start++;
            }
        }

        #endregion ENDOF: Range Yielders

        #region Concat Yielders

        private static IEnumerable<TSource> ConcatYielder<TSource>(IEnumerable<TSource> first, IEnumerable<TSource> second)
        {
            foreach (TSource item in first) { yield return item; }

            foreach (TSource item in second) { yield return item; }
        }

        #endregion ENDOF: Concat Yielders

        #region Distinct Yielders

        private static IEnumerable<TSource> DistinctYielder<TSource>(IEnumerable<TSource> source, IEqualityComparer<TSource> comparer)
        {
            UniqueCollection<TSource> distinctCollection = new UniqueCollection<TSource>(comparer);

            foreach (TSource item in source)
            {
                if (distinctCollection.TryAdd(item)) { yield return item; }
            }
        }

        #endregion ENDOF: Distinct Yielders

        #region Union Yielders

        private static IEnumerable<TSource> UnionYielder<TSource>(IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
        {
            UniqueCollection<TSource> uniqueCollection = new UniqueCollection<TSource>(comparer);

            foreach (TSource item in first)
            {
                if (uniqueCollection.TryAdd(item)) { yield return item; }
            }

            foreach (TSource item in second)
            {
                if (uniqueCollection.TryAdd(item)) { yield return item; }
            }
        }

        #endregion ENDOF: Union Yielders

        #region Except Yielders

        private static IEnumerable<TSource> ExceptYielder<TSource>(IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
        {
            UniqueCollection<TSource> uniqueCollection = new UniqueCollection<TSource>(second, comparer); // # Buffering second enumerable unique elements (Non-Streaming part of the flow)

            foreach (TSource item in first)
            {
                if (uniqueCollection.TryAdd(item)) { yield return item; }
            }
        }

        #endregion ENDOF: Except Yielders

        #region Intersect Yielders

        private static IEnumerable<TSource> IntersectYielder<TSource>(IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
        {
            UniqueCollection<TSource> uniqueCollection = new UniqueCollection<TSource>(second, comparer);

            foreach (TSource item in first)
            {
                if (uniqueCollection.Remove(item)) { yield return item; }
            }
        }

        #endregion ENDOF: Intersect Yielders

        #region Reverse Yielders

        private static IEnumerable<TSource> ReverseYielder<TSource>(IEnumerable<TSource> source)
        {
            Stack<TSource> stack = new Stack<TSource>(source);

            foreach (TSource item in stack) // # Items are being poped in REVERSE order, due to LIFO structure of stack
            {
                yield return item;
            }
        }

        #endregion ENDOF: Reverse Yielders

        #region Join Yielders

        private static IEnumerable<TResult> JoinYielder<TOuter, TInner, TKey, TResult>(IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, TInner, TResult> resultSelector, IEqualityComparer<TKey> comparer)
        {
            ILookup<TKey, TInner> innerLookup = inner.ToLookup(innerKeySelector, comparer);

            TKey key;
            foreach (TOuter outerItem in outer)
            {
                key = outerKeySelector(outerItem);

                if (key != null) // # Altho it is missing in the documentation (http://msdn.microsoft.com/en-us/library/bb534675.aspx), Join does not support null keys
                {
                    foreach (TInner innerItem in innerLookup[key])
                    {
                        yield return resultSelector(outerItem, innerItem);
                    }
                }
            }
        }

        #endregion ENDOF: Join Yielders

        #region GroupBy Yielders

        private static IEnumerable<IGrouping<TKey, TSource>> GroupByYielder<TSource, TKey>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
        {
            ILookup<TKey, TSource> lookup = source.ToLookup(keySelector, comparer);

            foreach (IGrouping<TKey, TSource> item in lookup)
            {
                yield return item;
            }
        }

        private static IEnumerable<TResult> GroupByYielder<TSource, TKey, TResult>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TKey, IEnumerable<TSource>, TResult> resultSelector, IEqualityComparer<TKey> comparer)
        {
            ILookup<TKey, TSource> lookup = source.ToLookup(keySelector, comparer);

            foreach (IGrouping<TKey, TSource> item in lookup)
            {
                yield return resultSelector(item.Key, item);
            }
        }

        private static IEnumerable<IGrouping<TKey, TElement>> GroupByYielder<TSource, TKey, TElement>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey> comparer)
        {
            ILookup<TKey, TElement> lookup = source.ToLookup(keySelector, elementSelector, comparer);

            foreach (IGrouping<TKey, TElement> item in lookup)
            {
                yield return item;
            }
        }

        private static IEnumerable<TResult> GroupByYielder<TSource, TKey, TElement, TResult>(IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, Func<TKey, IEnumerable<TElement>, TResult> resultSelector, IEqualityComparer<TKey> comparer)
        {
            ILookup<TKey, TElement> lookup = source.ToLookup(keySelector, elementSelector, comparer);

            foreach (IGrouping<TKey, TElement> item in lookup)
            {
                yield return resultSelector(item.Key, item);
            }
        }

        #endregion ENDOF: GroupBy Yielders

        #region GroupJoin Yielders
       
        private static IEnumerable<TResult> GroupJoinYielder<TOuter, TInner, TKey, TResult>(IEnumerable<TOuter> outer, IEnumerable<TInner> inner, Func<TOuter, TKey> outerKeySelector, Func<TInner, TKey> innerKeySelector, Func<TOuter, IEnumerable<TInner>, TResult> resultSelector, IEqualityComparer<TKey> comparer)
        {
            ILookup<TKey, TInner> innerLookup = inner.ToLookup(innerKeySelector, comparer);

            TKey key;
            foreach (TOuter outerItem in outer)
            {
                key = outerKeySelector(outerItem);

                if (key == null) { yield return resultSelector(outerItem, new TInner[0]); } // # Unlike Join; GroupJoin DOES support null keys, returning empty enumerable instead of matching values
                else
                {
                    yield return resultSelector(outerItem, innerLookup[key]);
                }
            }
        }

        #endregion ENDOF: GroupJoin Yielders

        #endregion ENDOF: Yielders
    }
}
