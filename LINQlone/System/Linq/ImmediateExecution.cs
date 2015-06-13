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

using LINQlone.Definitions;
using LINQlone.Infrastructure;

namespace System.Linq
{
    public static partial class Enumerable
    {
        #region ToList Overloads

        /// <summary>
        /// Creates a System.Collections.Generic.List&lt;T&gt; from an System.Collections.Generic.IEnumerable&lt;T&gt;.
        /// </summary>
        /// <param name="source">The System.Collections.Generic.IEnumerable&lt;T&gt; to create a System.Collections.Generic.List&lt;T&gt; from.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <returns>A System.Collections.Generic.List&lt;T&gt; that contains elements from the input sequence.</returns>
        /// <exception cref="System.ArgumentNullException">source is null.</exception>
        public static List<TSource> ToList<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }

            return new List<TSource>(source);
        }

        #endregion ENDOF: ToList Overloads

        #region First Overloads

        /// <summary>
        /// Returns the first element of a sequence.
        /// </summary>
        /// <param name="source">The System.Collections.Generic.IEnumerable&lt;T&gt; to return the first element of.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <returns>The first element in the specified sequence.</returns>
        /// <exception cref="System.ArgumentNullException">source is null.</exception>
        /// <exception cref="System.InvalidOperationException">The source sequence is empty.</exception>
        public static TSource First<TSource>(this IEnumerable<TSource> source) 
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }

            IList<TSource> list = source as IList<TSource>;
            if (list != null)
            {
                if (list.Count > 0) { return list[0]; }
            }
            else
            {
                foreach (TSource item in source)
                {
                    return item;
                }
            }

            throw Exceptions.NoElements(); // # Source had no elements, operation is invalid.
        }

        /// <summary>
        /// Returns the first element in a sequence that satisfies a specified condition.
        /// </summary>
        /// <param name="source">An System.Collections.Generic.IEnumerable&lt;T&gt; to return an element from.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <returns>The first element in the sequence that passes the test in the specified predicate function.</returns>
        /// <exception cref="System.ArgumentNullException">source or predicate is null.</exception>
        /// <exception cref="System.InvalidOperationException">No element satisfies the condition in predicate.-or-The source sequence is empty.</exception>
        public static TSource First<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }
            if (predicate == null) { throw Exceptions.ArgumentNull(Parameter.Predicate); }

            foreach (TSource item in source)
            {
                if (predicate(item)) { return item; }
            }

            throw Exceptions.NoMatchingElements(); // # Source had no elements matching given predicate, operation is invalid.
        }

        #endregion ENDOF : First Overloads

        #region FirstOrDefault Overloads

        /// <summary>
        /// Returns the first element of a sequence, or a default value if the sequence contains no elements.
        /// </summary>
        /// <param name="source">The System.Collections.Generic.IEnumerable&lt;T&gt; to return the first element of.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <returns>default(TSource) if source is empty; otherwise, the first element in source.</returns>
        /// <exception cref="System.ArgumentNullException">source is null.</exception>
        public static TSource FirstOrDefault<TSource>(this IEnumerable<TSource> source) 
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }

            IList<TSource> list = source as IList<TSource>;
            if (list != null)
            {
                if (list.Count > 0) { return list[0]; }
            }
            else
            {
                foreach (TSource item in source)
                {
                    return item;
                }
            }

            return default(TSource);
        }

        /// <summary>
        /// Returns the first element of the sequence that satisfies a condition or a default value if no such element is found.
        /// </summary>
        /// <param name="source">An System.Collections.Generic.IEnumerable&lt;T&gt; to return an element from.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <returns>default(TSource) if source is empty or if no element passes the test specified by predicate; otherwise, the first element in source that passes the test specified by predicate.</returns>
        /// <exception cref="System.ArgumentNullException">source or predicate is null.</exception>
        public static TSource FirstOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate) 
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }
            if (predicate == null) { throw Exceptions.ArgumentNull(Parameter.Predicate); }

            foreach (TSource item in source)
            {
                if (predicate(item)) { return item; }
            }

            return default(TSource);
        }
        
        #endregion ENDOF: ToList Overloads

        #region ElementAt Overloads

        /// <summary>
        /// Returns the element at a specified index in a sequence.
        /// </summary>
        /// <param name="source">An System.Collections.Generic.IEnumerable&lt;T&gt; to return an element from.</param>
        /// <param name="index">The zero-based index of the element to retrieve.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <returns>The element at the specified position in the source sequence.</returns>
        /// <exception cref="System.ArgumentNullException">source is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">index is less than 0 or greater than or equal to the number of elements in source.</exception>
        public static TSource ElementAt<TSource>(this IEnumerable<TSource> source, int index) 
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }

            IList<TSource> list = source as IList<TSource>;
            if (list != null) { return list[index]; }

            if (index >= 0)
            {
                int iterationCount = 0;
                foreach (TSource item in source)
                {
                    if (index == iterationCount++) { return item; }
                }
            }

            throw Exceptions.ArgumentOutOfRange(Parameter.Index); // # No elements were returned, index argument is out of range
        }

        #endregion ENDOF: ElementAt Overloads

        #region ElementAtOrDefault Overloads

        /// <summary>
        /// Returns the element at a specified index in a sequence or a default value if the index is out of range.
        /// </summary>
        /// <param name="source">An System.Collections.Generic.IEnumerable&lt;T&gt; to return an element from.</param>
        /// <param name="index">The zero-based index of the element to retrieve.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <returns>default(TSource) if the index is outside the bounds of the source sequence; otherwise, the element at the specified position in the source sequence.</returns>
        /// <exception cref="System.ArgumentNullException">source is null.</exception>
        public static TSource ElementAtOrDefault<TSource>(this IEnumerable<TSource> source, int index)
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }

            if (index >= 0)
            {
                IList<TSource> list = source as IList<TSource>;
                if (list != null)
                {
                    if (index < list.Count) { return list[index]; }
                }
                else
                {
                    int iterationCount = 0;
                    foreach (TSource item in source)
                    {
                        if (index == iterationCount++) { return item; }
                    }
                }
            }

            return default(TSource);
        }

        #endregion ENDOF : ElementAtOrDefault Overloads

        #region Single Overloads

        /// <summary>
        /// Returns the only element of a sequence, and throws an exception if there is not exactly one element in the sequence.
        /// </summary>
        /// <param name="source">An System.Collections.Generic.IEnumerable&lt;T&gt; to return the single element of.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <returns>The single element of the input sequence.</returns>
        /// <exception cref="System.ArgumentNullException">source is null.</exception>
        /// <exception cref="System.InvalidOperationException">The input sequence contains more than one element.-or-The input sequence is empty.</exception>
        public static TSource Single<TSource>(this IEnumerable<TSource> source) 
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }

            IList<TSource> list = source as IList<TSource>;
            if (list != null)
            {
                if (list.Count == 0) { throw Exceptions.NoElements(); }
                if (list.Count == 1) { return list[0]; }
            }
            else
            {
                using (IEnumerator<TSource> enumerator = source.GetEnumerator())
                {
                    if (!enumerator.MoveNext()) { throw Exceptions.NoElements(); }

                    TSource item = enumerator.Current;
                    if (!enumerator.MoveNext()) { return item; }
                }
            }

            throw Exceptions.MoreThanOneElements();
        }

        /// <summary>
        /// Returns the only element of a sequence that satisfies a specified condition, and throws an exception if more than one such element exists.
        /// </summary>
        /// <param name="source">An System.Collections.Generic.IEnumerable&lt;T&gt; to return a single element from.</param>
        /// <param name="predicate">A function to test an element for a condition.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <returns>The single element of the input sequence that satisfies a condition.</returns>
        /// <exception cref="System.ArgumentNullException">source or predicate is null.</exception>
        /// <exception cref="System.InvalidOperationException">No element satisfies the condition in predicate.-or-More than one element satisfies the condition in predicate.-or-The source sequence is empty.</exception>
        public static TSource Single<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate) 
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }
            if (predicate == null) { throw Exceptions.ArgumentNull(Parameter.Predicate); }

            TSource output = default(TSource);
            long matchingCount = 0;
            foreach (TSource item in source)
            {
                if (predicate(item))
                {
                    checked { matchingCount++; }
                    
                    output = item;
                }
            }

            if (matchingCount == 0) { throw Exceptions.NoMatchingElements(); }
            else if (matchingCount == 1) { return output; }
            else { throw Exceptions.MoreThanOneMatchingElements(); }
        }

        #endregion ENDOF : Single Overloads

        #region SingleOrDefault Overloads

        /// <summary>
        /// Returns the only element of a sequence, or a default value if the sequence is empty; this method throws an exception if there is more than one element in the sequence.
        /// </summary>
        /// <param name="source">An System.Collections.Generic.IEnumerable&lt;T&gt; to return the single element of.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <returns>The single element of the input sequence, or default(TSource) if the sequence contains no elements.</returns>
        /// <exception cref="System.ArgumentNullException">source is null.</exception>
        /// <exception cref="System.InvalidOperationException">The input sequence contains more than one element.</exception>
        public static TSource SingleOrDefault<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }

            IList<TSource> list = source as IList<TSource>;
            if (list != null)
            {
                if (list.Count == 0) { return default(TSource); }
                if (list.Count == 1) { return list[0]; }
            }
            else
            {
                using (IEnumerator<TSource> enumerator = source.GetEnumerator())
                {
                    if (!enumerator.MoveNext()) { return default(TSource); }

                    TSource item = enumerator.Current;
                    if (!enumerator.MoveNext()) { return item; }
                }
            }

            throw Exceptions.MoreThanOneElements();
        }

        /// <summary>
        /// Returns the only element of a sequence that satisfies a specified condition or a default value if no such element exists; this method throws an exception if more than one element satisfies the condition.
        /// </summary>
        /// <param name="source">An System.Collections.Generic.IEnumerable&lt;T&gt; to return a single element from.</param>
        /// <param name="predicate">A function to test an element for a condition.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <returns>The single element of the input sequence that satisfies the condition, or default(TSource) if no such element is found.</returns>
        /// <exception cref="System.ArgumentNullException">source or predicate is null.</exception>
        /// <exception cref="System.InvalidOperationException">More than one element satisfies the condition in predicate.</exception>
        public static TSource SingleOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }
            if (predicate == null) { throw Exceptions.ArgumentNull(Parameter.Predicate); }

            TSource output = default(TSource);
            long matchingCount = 0;
            foreach (TSource item in source)
            {
                if (predicate(item))
                {
                    checked { matchingCount++; }
                    
                    output = item;
                }                
            }

            if (matchingCount < 2) { return output; }
            else { throw Exceptions.MoreThanOneMatchingElements(); }
        }

        #endregion ENDOF : SingleOrDefault Overloads

        #region Any Overloads
        
        /// <summary>
        /// Determines whether a sequence contains any elements.
        /// </summary>
        /// <param name="source">The System.Collections.Generic.IEnumerable&lt;T&gt; to check for emptiness.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <returns>true if the source sequence contains any elements; otherwise, false.</returns>
        /// <exception cref="System.ArgumentNullException">source is null.</exception>
        public static bool Any<TSource>(this IEnumerable<TSource> source) 
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }

            using (IEnumerator<TSource> enumerator = source.GetEnumerator())
            {
                return enumerator.MoveNext();
            }
        }

        /// <summary>
        /// Determines whether any element of a sequence satisfies a condition.
        /// </summary>
        /// <param name="source">An System.Collections.Generic.IEnumerable&lt;T&gt; whose elements to apply the predicate to.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <returns>true if any elements in the source sequence pass the test in the specified predicate; otherwise, false.</returns>
        /// <exception cref="System.ArgumentNullException">source or predicate is null.</exception>
        public static bool Any<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }
            if (predicate == null) { throw Exceptions.ArgumentNull(Parameter.Predicate); }
            
            foreach (TSource item in source)
            {
                if (predicate(item)) // ~ Predicate met
                {
                    return true;
                }
            }

            return false;
        }

        #endregion ENDOF : Any Overloads

        #region Contains Overloads

        /// <summary>
        /// Determines whether a sequence contains a specified element by using the default equality comparer.
        /// </summary>
        /// <param name="source">A sequence in which to locate a value.</param>
        /// <param name="value">The value to locate in the sequence.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <returns>true if the source sequence contains an element that has the specified value; otherwise, false.</returns>
        /// <exception cref="System.ArgumentNullException">source is null.</exception>
        public static bool Contains<TSource>(this IEnumerable<TSource> source, TSource value) 
        {
            ICollection<TSource> collection = source as ICollection<TSource>;
            if (collection != null) { return collection.Contains(value); }

            return Contains(source, value, null);
        }

        /// <summary>
        /// Determines whether a sequence contains a specified element by using a specified System.Collections.Generic.IEqualityComparer&lt;T&gt;.
        /// </summary>
        /// <param name="source">A sequence in which to locate a value.</param>
        /// <param name="value">The value to locate in the sequence.</param>
        /// <param name="comparer">An equality comparer to compare values.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <returns>true if the source sequence contains an element that has the specified value; otherwise, false.</returns>
        /// <exception cref="System.ArgumentNullException">source is null.</exception>
        public static bool Contains<TSource>(this IEnumerable<TSource> source, TSource value, IEqualityComparer<TSource> comparer) 
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }

            comparer = comparer ?? EqualityComparer<TSource>.Default;
            foreach (TSource item in source)
            {
                if (comparer.Equals(item, value))
                {
                    return true;
                }
            }

            return false;
        }

        #endregion ENDOF : Contains Overloads

        #region Count Overloads

        /// <summary>
        /// Returns the number of elements in a sequence.
        /// </summary>
        /// <param name="source">A sequence that contains elements to be counted.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <returns>The number of elements in the input sequence.</returns>
        /// <exception cref="System.ArgumentNullException">source is null.</exception>
        /// <exception cref="System.OverflowException">The number of elements in source is larger than System.Int32.MaxValue.</exception>
        public static int Count<TSource>(this IEnumerable<TSource> source) 
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }

            // ~ Optimization for ICollection<T>; number of elements are known
            ICollection<TSource> genericCollection;
            if ((genericCollection = source as ICollection<TSource>) != null) { return genericCollection.Count; }
            
            int elementCount = 0;
            using (IEnumerator<TSource> enumerator = source.GetEnumerator())
            {
                while (enumerator.MoveNext()) 
                {
                    checked { elementCount++; }
                }
            }

            return elementCount;
        }

        /// <summary>
        /// Returns a number that represents how many elements in the specified sequence satisfy a condition.
        /// </summary>
        /// <param name="source">A sequence that contains elements to be tested and counted.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <returns>A number that represents how many elements in the sequence satisfy the condition in the predicate function.</returns>
        /// <exception cref="System.ArgumentNullException">source or predicate is null.</exception>
        /// <exception cref="System.OverflowException">The number of elements in source is larger than System.Int32.MaxValue.</exception>
        public static int Count<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }
            if (predicate == null) { throw Exceptions.ArgumentNull(Parameter.Predicate); }

            int elementCount = 0;
            foreach (TSource item in source)
            {
                if (predicate(item))
                {
                    checked { elementCount++; }
                }
            }

            return elementCount;
        }

        #endregion ENDOF : Count Overloads

        #region LongCount Overloads

        /// <summary>
        /// Returns an System.Int64 that represents the total number of elements in a sequence.
        /// </summary>
        /// <param name="source">An System.Collections.Generic.IEnumerable&lt;T&gt; that contains the elements to be counted.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <returns>The number of elements in the source sequence.</returns>
        /// <exception cref="System.ArgumentNullException">source is null.</exception>
        /// <exception cref="System.OverflowException">The number of elements exceeds System.Int64.MaxValue.</exception>
        public static long LongCount<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }

            long elementCount = 0;
            using (IEnumerator<TSource> enumerator = source.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    checked { elementCount++; }
                }
            }

            return elementCount;
        }

        /// <summary>
        /// Returns an System.Int64 that represents how many elements in a sequence satisfy a condition.
        /// </summary>
        /// <param name="source">An System.Collections.Generic.IEnumerable&lt;T&gt; that contains the elements to be counted.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <returns>A number that represents how many elements in the sequence satisfy the condition in the predicate function.</returns>
        /// <exception cref="System.ArgumentNullException">source or predicate is null.</exception>
        /// <exception cref="System.OverflowException">The number of matching elements exceeds System.Int64.MaxValue.</exception>
        public static long LongCount<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }
            if (predicate == null) { throw Exceptions.ArgumentNull(Parameter.Predicate); }

            long elementCount = 0;
            foreach (TSource item in source)
            {
                if (predicate(item))
                {
                    checked { elementCount++; }
                }
            }

            return elementCount;
        }

        #endregion ENDOF : LongCount Overloads

        #region All Overloads

        /// <summary>
        /// Determines whether all elements of a sequence satisfy a condition.
        /// </summary>
        /// <param name="source">An System.Collections.Generic.IEnumerable&lt;T&gt; that contains the elements to apply the predicate to.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <returns>true if every element of the source sequence passes the test in the specified predicate, or if the sequence is empty; otherwise, false.</returns>
        /// <exception cref="System.ArgumentNullException">source or predicate is null.</exception>
        public static bool All<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate) 
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }
            if (predicate == null) { throw Exceptions.ArgumentNull(Parameter.Predicate); }

            foreach (TSource item in source)
            {
                if (!predicate(item))
                {
                    return false;
                }
            }

            return true;
        }

        #endregion ENDOF : All Overloads

        #region Last Overloads

        /// <summary>
        /// Returns the last element of a sequence.
        /// </summary>
        /// <param name="source">An System.Collections.Generic.IEnumerable&lt;T&gt; to return the last element of.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <returns>The value at the last position in the source sequence.</returns>
        /// <exception cref="System.ArgumentNullException">source is null.</exception>
        /// <exception cref="System.InvalidOperationException">The source sequence is empty.</exception>
        public static TSource Last<TSource>(this IEnumerable<TSource> source) 
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }

            IList<TSource> list = source as IList<TSource>;
            if (list != null) 
            {
                if (list.Count > 0) { return list[list.Count - 1]; }
            }
            else
            {
                using (IEnumerator<TSource> enumerator = source.GetEnumerator())
                {
                    if (enumerator.MoveNext())
                    {
                        TSource lastItem;

                        do
                        {
                            lastItem = enumerator.Current;
                        } while (enumerator.MoveNext());

                        return lastItem;
                    }
                }
            }

            throw Exceptions.NoElements();
        }

        /// <summary>
        /// Returns the last element of a sequence that satisfies a specified condition.
        /// </summary>
        /// <param name="source">An System.Collections.Generic.IEnumerable&lt;T&gt; to return an element from.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <returns>The last element in the sequence that passes the test in the specified predicate function.</returns>
        /// <exception cref="System.ArgumentNullException">source or predicate is null.</exception>
        /// <exception cref="System.InvalidOperationException">No element satisfies the condition in predicate.-or-The source sequence is empty.</exception>
        public static TSource Last<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }
            if (predicate == null) { throw Exceptions.ArgumentNull(Parameter.Predicate); }

            TSource lastItem = default(TSource);
            bool isLastItemSet = false;

            foreach (TSource item in source)
            {
                if (predicate(item))
                {
                    lastItem = item;
                    isLastItemSet = true;
                }
            }

            if (isLastItemSet) { return lastItem; }

            throw Exceptions.NoMatchingElements();
        }

        #endregion ENDOF : Last Overloads

        #region LastOrDefault Overloads

        /// <summary>
        /// Returns the last element of a sequence, or a default value if the sequence contains no elements.
        /// </summary>
        /// <param name="source">An System.Collections.Generic.IEnumerable&lt;T&gt; to return the last element of.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <returns>default(TSource) if the source sequence is empty; otherwise, the last element in the System.Collections.Generic.IEnumerable&lt;T&gt;.</returns>
        /// <exception cref="System.ArgumentNullException">source is null.</exception>
        public static TSource LastOrDefault<TSource>(this IEnumerable<TSource> source) 
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }

            TSource lastItem = default(TSource);

            IList<TSource> list = source as IList<TSource>;
            if (list != null) 
            {
                if (list.Count > 0) { return list[list.Count - 1]; }
            }
            else
            {
                foreach (TSource item in source)
                {
                    lastItem = item;
                }
            }
            
            return lastItem;
        }

        /// <summary>
        /// Returns the last element of a sequence that satisfies a condition or a default value if no such element is found.
        /// </summary>
        /// <param name="source">An System.Collections.Generic.IEnumerable&lt;T&gt; to return an element from.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <returns>default(TSource) if the sequence is empty or if no elements pass the test in the predicate function; otherwise, the last element that passes the test in the predicate function.</returns>
        /// <exception cref="System.ArgumentNullException">source or predicate is null.</exception>
        public static TSource LastOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate) 
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }
            if (predicate == null) { throw Exceptions.ArgumentNull(Parameter.Predicate); }

            TSource lastItem = default(TSource);
            foreach (TSource item in source)
            {
                if (predicate(item)) { lastItem = item; }
            }

            return lastItem;
        }

        #endregion ENDOF : LastOrDefault Overloads

        #region ToArray Overloads

        /// <summary>
        /// Creates an array from a System.Collections.Generic.IEnumerable&lt;T&gt;.
        /// </summary>
        /// <param name="source">An System.Collections.Generic.IEnumerable&lt;T&gt; to create an array from.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <returns>An array that contains the elements from the input sequence.</returns>
        /// <exception cref="System.ArgumentNullException">source is null.</exception>
        public static TSource[] ToArray<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }

            return new Buffer<TSource>(source).ToArray();
        }

        #endregion ENDOF : ToArray Overloads

        #region SequenceEqual Overloads

        /// <summary>
        /// Determines whether two sequences are equal by comparing the elements by using the default equality comparer for their type.
        /// </summary>
        /// <param name="first">An System.Collections.Generic.IEnumerable&lt;T&gt; to compare to second.</param>
        /// <param name="second">An System.Collections.Generic.IEnumerable&lt;T&gt; to compare to the first sequence.</param>
        /// <typeparam name="TSource">The type of the elements of the input sequences.</typeparam>
        /// <returns>true if the two source sequences are of equal length and their corresponding elements are equal according to the default equality comparer for their type; otherwise, false.</returns>
        /// <exception cref="System.ArgumentNullException">first or second is null.</exception>
        public static bool SequenceEqual<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second) 
        {
            return SequenceEqual(first, second, null);
        }

        /// <summary>
        /// Determines whether two sequences are equal by comparing their elements by using a specified System.Collections.Generic.IEqualityComparer&lt;T&gt;.
        /// </summary>
        /// <param name="first">An System.Collections.Generic.IEnumerable&lt;T&gt; to compare to second.</param>
        /// <param name="second">An System.Collections.Generic.IEnumerable&lt;T&gt; to compare to the first sequence.</param>
        /// <param name="comparer">An System.Collections.Generic.IEqualityComparer&lt;T&gt; to use to compare elements.</param>
        /// <typeparam name="TSource">The type of the elements of the input sequences.</typeparam>
        /// <returns>true if the two source sequences are of equal length and their corresponding elements compare equal according to comparer; otherwise, false.</returns>
        /// <exception cref="System.ArgumentNullException">first or second is null.</exception>
        public static bool SequenceEqual<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
        {
            if (first == null) { throw Exceptions.ArgumentNull(Parameter.First); }
            if (second == null) { throw Exceptions.ArgumentNull(Parameter.Second); }

            if (comparer == null) { comparer = EqualityComparer<TSource>.Default; }

            using (IEnumerator<TSource> firstEnumerator = first.GetEnumerator())
            using (IEnumerator<TSource> secondEnumerator = second.GetEnumerator())
            {
                bool firstHasNext = firstEnumerator.MoveNext();
                bool secondHasNext = secondEnumerator.MoveNext();

                while (firstHasNext || secondHasNext)
                {
                    if (firstHasNext != secondHasNext || !comparer.Equals(firstEnumerator.Current, secondEnumerator.Current))
                    {
                        return false;
                    }

                    firstHasNext = firstEnumerator.MoveNext();
                    secondHasNext = secondEnumerator.MoveNext();
                } 
            }

            return true;        
        }

        #endregion ENDOF : SequenceEqual Overloads

        #region ToLookup Overloads

        /// <summary>
        /// Creates a System.Linq.Lookup&lt;TKey,TElement&gt; from an System.Collections.Generic.IEnumerable&lt;T&gt; according to a specified key selector function.
        /// </summary>
        /// <param name="source">The System.Collections.Generic.IEnumerable&lt;T&gt; to create a System.Linq.Lookup&lt;TKey,TElement&gt; from.</param>
        /// <param name="keySelector">A function to extract a key from each element.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by keySelector.</typeparam>
        /// <returns>A System.Linq.Lookup&lt;TKey,TElement&gt; that contains keys and values.</returns>
        /// <exception cref="System.ArgumentNullException">source or keySelector is null.</exception>
        public static ILookup<TKey, TSource> ToLookup<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            return ToLookup(source, keySelector, null);
        }

        /// <summary>
        /// Creates a System.Linq.Lookup&lt;TKey,TElement&gt; from an System.Collections.Generic.IEnumerable&lt;T&gt; according to specified key selector and element selector functions.
        /// </summary>
        /// <param name="source">The System.Collections.Generic.IEnumerable&lt;T&gt; to create a System.Linq.Lookup&lt;TKey,TElement&gt; from.</param>
        /// <param name="keySelector">A function to extract a key from each element.</param>
        /// <param name="elementSelector">A transform function to produce a result element value from each element.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by keySelector.</typeparam>
        /// <typeparam name="TElement">The type of the value returned by elementSelector.</typeparam>
        /// <returns>A System.Linq.Lookup&lt;TKey,TElement&gt; that contains values of type TElement selected from the input sequence.</returns>
        /// <exception cref="System.ArgumentNullException">source or keySelector or elementSelector is null.</exception>
        public static ILookup<TKey, TElement> ToLookup<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
        {
            return ToLookup(source, keySelector, elementSelector, null);
        }

        /// <summary>
        /// Creates a System.Linq.Lookup&lt;TKey,TElement&gt; from an System.Collections.Generic.IEnumerable&lt;T&gt; according to a specified key selector function and key comparer.
        /// </summary>
        /// <param name="source">The System.Collections.Generic.IEnumerable&lt;T&gt; to create a System.Linq.Lookup&lt;TKey,TElement&gt; from.</param>
        /// <param name="keySelector">A function to extract a key from each element.</param>
        /// <param name="comparer">An System.Collections.Generic.IEqualityComparer&lt;T&gt; to compare keys.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by keySelector.</typeparam>
        /// <returns>A System.Linq.Lookup&lt;TKey,TElement&gt; that contains keys and values.</returns>
        /// <exception cref="System.ArgumentNullException">source or keySelector is null.</exception>
        public static ILookup<TKey, TSource> ToLookup<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }
            if (keySelector == null) { throw Exceptions.ArgumentNull(Parameter.KeySelector); }

            Lookup<TKey, TSource> lookup = new Lookup<TKey, TSource>(comparer);

            foreach (TSource item in source) { lookup.Add(keySelector(item), item); }

            return lookup;
        }

        /// <summary>
        /// Creates a System.Linq.Lookup&lt;TKey,TElement&gt; from an System.Collections.Generic.IEnumerable&lt;T&gt; according to a specified key selector function, a comparer and an element selector function.
        /// </summary>
        /// <param name="source">The System.Collections.Generic.IEnumerable&lt;T&gt; to create a System.Linq.Lookup&lt;TKey,TElement&gt; from.</param>
        /// <param name="keySelector">A function to extract a key from each element.</param>
        /// <param name="elementSelector">A transform function to produce a result element value from each element.</param>
        /// <param name="comparer">An System.Collections.Generic.IEqualityComparer&lt;T&gt; to compare keys.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by keySelector.</typeparam>
        /// <typeparam name="TElement">The type of the value returned by elementSelector.</typeparam>
        /// <returns>A System.Linq.Lookup&lt;TKey,TElement&gt; that contains values of type TElement selected from the input sequence.</returns>
        /// <exception cref="System.ArgumentNullException">source or keySelector or elementSelector is null.</exception>
        public static ILookup<TKey, TElement> ToLookup<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey> comparer)
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }
            if (keySelector == null) { throw Exceptions.ArgumentNull(Parameter.KeySelector); }
            if (elementSelector == null) { throw Exceptions.ArgumentNull(Parameter.ElementSelector); }

            Lookup<TKey, TElement> lookup = new Lookup<TKey, TElement>(comparer);

            foreach (TSource item in source) { lookup.Add(keySelector(item), elementSelector(item)); }

            return lookup;
        }

        #endregion ENDOF: ToLookup Overloads

        #region Sum Overloads

        /// <summary>
        /// Computes the sum of a sequence of System.Int32 values.
        /// </summary>
        /// <param name="source">A sequence of System.Int32 values to calculate the sum of.</param>
        /// <returns>The sum of the values in the sequence.</returns>
        /// <exception cref="System.ArgumentNullException">source is null.</exception>
        /// <exception cref="System.OverflowException">The sum is larger than System.Int32.MaxValue.</exception>
        public static int Sum(this IEnumerable<int> source)
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }
            int sum = 0;

            foreach (int item in source) 
            { 
                checked { sum += item; } 
            }

            return sum;
        }

        /// <summary>
        /// Computes the sum of a sequence of System.Single values.
        /// </summary>
        /// <param name="source">A sequence of System.Single values to calculate the sum of.</param>
        /// <returns>The sum of the values in the sequence.</returns>
        /// <exception cref="System.ArgumentNullException">source is null.</exception>
        public static float Sum(this IEnumerable<float> source)
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }
            double sum = 0;

            foreach (float item in source) 
            { 
                sum += item; // ~ Unchecked; Result will be evaluted to Infinity instead of overflow
            }

            return (float)sum;
        }

        /// <summary>
        /// Computes the sum of a sequence of System.Double values.
        /// </summary>
        /// <param name="source">A sequence of System.Double values to calculate the sum of.</param>
        /// <returns>The sum of the values in the sequence.</returns>
        /// <exception cref="System.ArgumentNullException">source is null.</exception>
        public static double Sum(this IEnumerable<double> source)
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }
            double sum = 0;

            foreach (double item in source) 
            {
                sum += item;
            }

            return sum;
        }

        /// <summary>
        /// Computes the sum of a sequence of System.Int64 values.
        /// </summary>
        /// <param name="source">A sequence of System.Int64 values to calculate the sum of.</param>
        /// <returns>The sum of the values in the sequence.</returns>
        /// <exception cref="System.ArgumentNullException">source is null.</exception>
        /// <exception cref="System.OverflowException">The sum is larger than System.Int64.MaxValue.</exception>
        public static long Sum(this IEnumerable<long> source)
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }
            long sum = 0;

            foreach (long item in source) 
            { 
                checked { sum += item; } 
            }

            return sum;
        }

        /// <summary>
        /// Computes the sum of a sequence of System.Decimal values.
        /// </summary>
        /// <param name="source">A sequence of System.Decimal values to calculate the sum of.</param>
        /// <returns>The sum of the values in the sequence.</returns>
        /// <exception cref="System.ArgumentNullException">source is null.</exception>
        /// <exception cref="System.OverflowException">The sum is larger than System.Decimal.MaxValue.</exception>
        public static decimal Sum(this IEnumerable<decimal> source)
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }
            decimal sum = 0;

            foreach (decimal item in source) 
            { 
                checked { sum += item; } 
            }

            return sum;
        }

        /// <summary>
        /// Computes the sum of a sequence of nullable System.Int32 values.
        /// </summary>
        /// <param name="source">A sequence of nullable System.Int32 values to calculate the sum of.</param>
        /// <returns>The sum of the values in the sequence.</returns>
        /// <exception cref="System.ArgumentNullException">source is null.</exception>
        /// <exception cref="System.OverflowException">The sum is larger than System.Int32.MaxValue.</exception>
        public static int? Sum(this IEnumerable<int?> source)
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }
            int sum = 0;

            foreach (int? item in source) 
            { 
                if (item.HasValue) { checked { sum += item.Value; } } 
            }

            return sum;
        }

        /// <summary>
        /// Computes the sum of a sequence of nullable System.Single values.
        /// </summary>
        /// <param name="source">A sequence of nullable System.Single values to calculate the sum of.</param>
        /// <returns>The sum of the values in the sequence.</returns>
        /// <exception cref="System.ArgumentNullException">source is null.</exception>
        public static float? Sum(this IEnumerable<float?> source)
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }
            double sum = 0;

            foreach (float? item in source) 
            {
                if (item.HasValue) { sum += item.Value; }
            }

            return (float)sum;
        }

        /// <summary>
        /// Computes the sum of a sequence of nullable System.Double values.
        /// </summary>
        /// <param name="source">A sequence of nullable System.Double values to calculate the sum of.</param>
        /// <returns>The sum of the values in the sequence.</returns>
        /// <exception cref="System.ArgumentNullException">source is null.</exception>
        public static double? Sum(this IEnumerable<double?> source)
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }
            double sum = 0;

            foreach (double? item in source) 
            {
                if (item.HasValue) { sum += item.Value; }
            }

            return sum;
        }

        /// <summary>
        /// Computes the sum of a sequence of nullable System.Int64 values.
        /// </summary>
        /// <param name="source">A sequence of nullable System.Int64 values to calculate the sum of.</param>
        /// <returns>The sum of the values in the sequence.</returns>
        /// <exception cref="System.ArgumentNullException">source is null.</exception>
        /// <exception cref="System.OverflowException">The sum is larger than System.Int64.MaxValue.</exception>
        public static long? Sum(this IEnumerable<long?> source)
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }
            long sum = 0;

            foreach (long? item in source) 
            { 
                if (item.HasValue) { checked { sum += item.Value; } } 
            }

            return sum;
        }

        /// <summary>
        /// Computes the sum of a sequence of nullable System.Decimal values.
        /// </summary>
        /// <param name="source">A sequence of nullable System.Decimal values to calculate the sum of.</param>
        /// <returns>The sum of the values in the sequence.</returns>
        /// <exception cref="System.ArgumentNullException">source is null.</exception>
        /// <exception cref="System.OverflowException">The sum is larger than System.Decimal.MaxValue.</exception>
        public static decimal? Sum(this IEnumerable<decimal?> source)
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }
            decimal sum = 0;

            foreach (decimal? item in source) 
            { 
                if (item.HasValue) { checked { sum += item.Value; } } 
            }

            return sum;
        }

        /// <summary>
        /// Computes the sum of the sequence of nullable System.Decimal values that are obtained by invoking a transform function on each element of the input sequence.
        /// </summary>
        /// <param name="source">A sequence of values that are used to calculate a sum.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <returns>The sum of the projected values.</returns>
        /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
        /// <exception cref="System.OverflowException">The sum is larger than System.Decimal.MaxValue.</exception>
        public static decimal? Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal?> selector)
        {
            return Sum(source.Select(selector));
        }

        /// <summary>
        /// Computes the sum of the sequence of System.Decimal values that are obtained by invoking a transform function on each element of the input sequence.
        /// </summary>
        /// <param name="source">A sequence of values that are used to calculate a sum.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <returns>The sum of the projected values.</returns>
        /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
        /// <exception cref="System.OverflowException">The sum is larger than System.Decimal.MaxValue.</exception>
        public static decimal Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal> selector)
        {
            return Sum(source.Select(selector));
        }

        /// <summary>
        /// Computes the sum of the sequence of nullable System.Double values that are obtained by invoking a transform function on each element of the input sequence.
        /// </summary>
        /// <param name="source">A sequence of values that are used to calculate a sum.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <returns>The sum of the projected values.</returns>
        /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
        public static double? Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, double?> selector)
        {
            return Sum(source.Select(selector));
        }

        /// <summary>
        /// Computes the sum of the sequence of System.Double values that are obtained by invoking a transform function on each element of the input sequence.
        /// </summary>
        /// <param name="source">A sequence of values that are used to calculate a sum.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <returns>The sum of the projected values.</returns>
        /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
        public static double Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, double> selector)
        {
            return Sum(source.Select(selector));
        }

        /// <summary>
        /// Computes the sum of the sequence of nullable System.Single values that are obtained by invoking a transform function on each element of the input sequence.
        /// </summary>
        /// <param name="source">A sequence of values that are used to calculate a sum.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <returns>The sum of the projected values.</returns>
        /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
        public static float? Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, float?> selector)
        {
            return Sum(source.Select(selector));
        }

        /// <summary>
        /// Computes the sum of the sequence of System.Single values that are obtained by invoking a transform function on each element of the input sequence.
        /// </summary>
        /// <param name="source">A sequence of values that are used to calculate a sum.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <returns>The sum of the projected values.</returns>
        /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
        public static float Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, float> selector)
        {
            return Sum(source.Select(selector));
        }

        /// <summary>
        /// Computes the sum of the sequence of nullable System.Int32 values that are obtained by invoking a transform function on each element of the input sequence.
        /// </summary>
        /// <param name="source">A sequence of values that are used to calculate a sum.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <returns>The sum of the projected values.</returns>
        /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
        /// <exception cref="System.OverflowException">The sum is larger than System.Int32.MaxValue.</exception>
        public static int? Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, int?> selector)
        {
            return Sum(source.Select(selector));
        }

        /// <summary>
        /// Computes the sum of the sequence of System.Int32 values that are obtained by invoking a transform function on each element of the input sequence.
        /// </summary>
        /// <param name="source">A sequence of values that are used to calculate a sum.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <returns>The sum of the projected values.</returns>
        /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
        /// <exception cref="System.OverflowException">The sum is larger than System.Int32.MaxValue.</exception>
        public static int Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector) 
        {
            return Sum(source.Select(selector));
        }

        /// <summary>
        /// Computes the sum of the sequence of nullable System.Int64 values that are obtained by invoking a transform function on each element of the input sequence.
        /// </summary>
        /// <param name="source">A sequence of values that are used to calculate a sum.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <returns>The sum of the projected values.</returns>
        /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
        /// <exception cref="System.OverflowException">The sum is larger than System.Int64.MaxValue.</exception>
        public static long? Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, long?> selector)
        {
            return Sum(source.Select(selector));
        }

        /// <summary>
        /// Computes the sum of the sequence of System.Int64 values that are obtained by invoking a transform function on each element of the input sequence.
        /// </summary>
        /// <param name="source">A sequence of values that are used to calculate a sum.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <returns>The sum of the projected values.</returns>
        /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
        /// <exception cref="System.OverflowException">The sum is larger than System.Int64.MaxValue.</exception>
        public static long Sum<TSource>(this IEnumerable<TSource> source, Func<TSource, long> selector)
        {
            return Sum(source.Select(selector));
        }

        #endregion Sum Overloads

        #region Max Overloads

        /// <summary>
        /// Returns the maximum value in a sequence of System.Int32 values.
        /// </summary>
        /// <param name="source">A sequence of System.Int32 values to determine the maximum value of.</param>
        /// <returns>The maximum value in the sequence.</returns>
        /// <exception cref="System.ArgumentNullException">source is null.</exception>
        /// <exception cref="System.InvalidOperationException">source contains no elements.</exception>
        public static int Max(this IEnumerable<int> source)
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }
            using (IEnumerator<int> enumerator = source.GetEnumerator())
            {
                bool has = enumerator.MoveNext();

                if (!has) { throw Exceptions.NoElements(); } // ~ Max does not support empty source

                int max = enumerator.Current;

                while (enumerator.MoveNext())
                {
                    if (enumerator.Current > max) { max = enumerator.Current; }
                }

                return max;
            }
        }

        /// <summary>
        /// Returns the maximum value in a sequence of System.Single values.
        /// </summary>
        /// <param name="source">A sequence of System.Single values to determine the maximum value of.</param>
        /// <returns>The maximum value in the sequence.</returns>
        /// <exception cref="System.ArgumentNullException">source is null.</exception>
        /// <exception cref="System.InvalidOperationException">source contains no elements.</exception>
        public static float Max(this IEnumerable<float> source)
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }
            using (IEnumerator<float> enumerator = source.GetEnumerator())
            {
                bool has = enumerator.MoveNext();
                
                if (!has) { throw Exceptions.NoElements(); }

                float max = enumerator.Current;

                while (enumerator.MoveNext())
                {
                    if (float.IsNaN(enumerator.Current)) { continue; }
                    if (enumerator.Current > (double)max || float.IsNaN(max)) { max = enumerator.Current; }
                }

                return max;
            }
        }

        /// <summary>
        /// Returns the maximum value in a sequence of System.Double values.
        /// </summary>
        /// <param name="source">A sequence of System.Double values to determine the maximum value of.</param>
        /// <returns>The maximum value in the sequence.</returns>
        /// <exception cref="System.ArgumentNullException">source is null.</exception>
        /// <exception cref="System.InvalidOperationException">source contains no elements.</exception>
        public static double Max(this IEnumerable<double> source)
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }
            using (IEnumerator<double> enumerator = source.GetEnumerator())
            {
                bool has = enumerator.MoveNext();

                if (!has) { throw Exceptions.NoElements(); }

                double max = enumerator.Current;

                while (enumerator.MoveNext())
                {
                    if (double.IsNaN(enumerator.Current)) { continue; }
                    if (enumerator.Current > max || double.IsNaN(max)) { max = enumerator.Current; }
                }

                return max;
            }
        }

        /// <summary>
        /// Returns the maximum value in a sequence of System.Int64 values.
        /// </summary>
        /// <param name="source">A sequence of System.Int64 values to determine the maximum value of.</param>
        /// <returns>The maximum value in the sequence.</returns>
        /// <exception cref="System.ArgumentNullException">source is null.</exception>
        /// <exception cref="System.InvalidOperationException">source contains no elements.</exception>
        public static long Max(this IEnumerable<long> source)
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }
            using (IEnumerator<long> enumerator = source.GetEnumerator())
            {
                bool has = enumerator.MoveNext();

                if (!has) { throw Exceptions.NoElements(); }

                long max = enumerator.Current;

                while (enumerator.MoveNext())
                {
                    if (enumerator.Current > max) { max = enumerator.Current; }
                }

                return max;
            }
        }

        /// <summary>
        /// Returns the maximum value in a sequence of System.Decimal values.
        /// </summary>
        /// <param name="source">A sequence of System.Decimal values to determine the maximum value of.</param>
        /// <returns>The maximum value in the sequence.</returns>
        /// <exception cref="System.ArgumentNullException">source is null.</exception>
        /// <exception cref="System.InvalidOperationException">source contains no elements.</exception>
        public static decimal Max(this IEnumerable<decimal> source)
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }
            using (IEnumerator<decimal> enumerator = source.GetEnumerator())
            {
                bool has = enumerator.MoveNext();

                if (!has) { throw Exceptions.NoElements(); }

                decimal max = enumerator.Current;

                while (enumerator.MoveNext())
                {
                    if (enumerator.Current > max) { max = enumerator.Current; }
                }

                return max;
            }
        }

        /// <summary>
        /// Returns the maximum value in a generic sequence.
        /// </summary>
        /// <param name="source">A sequence of values to determine the maximum value of.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <returns>The maximum value in the sequence.</returns>
        /// <exception cref="System.ArgumentNullException">source is null.</exception>
        public static TSource Max<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }
            
            Comparer<TSource> comparer = Comparer<TSource>.Default;
            TSource max = default(TSource);

            if (max == null) // ~ Reference type
            {
                foreach (TSource item in source)
                {
                    if (item == null) { continue; }
                    if (max == null || comparer.Compare(item, max) > 0) { max = item; }
                }
            }
            else
            {
                using (IEnumerator<TSource> enumerator = source.GetEnumerator())
                {
                    bool has = enumerator.MoveNext();
                    
                    if (!has) { throw Exceptions.NoElements(); }

                    max = enumerator.Current;

                    while (enumerator.MoveNext())
                    {
                        if (comparer.Compare(enumerator.Current, max) > 0) { max = enumerator.Current; }
                    }
                }
            }

            return max;
        }

        /// <summary>
        /// Returns the maximum value in a sequence of nullable System.Int32 values.
        /// </summary>
        /// <param name="source">A sequence of nullable System.Int32 values to determine the maximum value of.</param>
        /// <returns>A value of type Nullable&lt;Int32&gt; in C# or Nullable(Of Int32) in Visual Basic that corresponds to the maximum value in the sequence.</returns>
        /// <exception cref="System.ArgumentNullException">source is null.</exception>
        public static int? Max(this IEnumerable<int?> source)
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }
            
            int? maximum = new int?();
            using (IEnumerator<int?> enumerator = source.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    int? item = enumerator.Current;
                    if (item.HasValue)
                    {
                        maximum = item;
                        break;
                    }
                }

                if (maximum.HasValue)
                {
                    while (enumerator.MoveNext())
                    {
                        int? item = enumerator.Current;
                        if (item.HasValue && item.Value > maximum.Value) { maximum = item; }
                    }
                }
            }

            return maximum;
        }

        /// <summary>
        /// Returns the maximum value in a sequence of nullable System.Single values.
        /// </summary>
        /// <param name="source">A sequence of nullable System.Single values to determine the maximum value of.</param>
        /// <returns>A value of type Nullable&lt;Single&gt; in C# or Nullable(Of Single) in Visual Basic that corresponds to the maximum value in the sequence.</returns>
        /// <exception cref="System.ArgumentNullException">source is null.</exception>
        public static float? Max(this IEnumerable<float?> source)
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }
            
            float? maximum = new float?();
            using (IEnumerator<float?> enumerator = source.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    float? item = enumerator.Current;
                    if (item.HasValue)
                    {
                        maximum = item;
                        break;
                    }
                }

                if (maximum.HasValue)
                {
                    while (enumerator.MoveNext())
                    {
                        float? item = enumerator.Current;
                        if (item.HasValue && !float.IsNaN(item.Value))
                        {
                            if (item.Value > (double)maximum.Value || float.IsNaN(maximum.Value)) { maximum = item; }
                        }
                    }
                }
            }

            return maximum;
        }

        /// <summary>
        /// Returns the maximum value in a sequence of nullable System.Double values.
        /// </summary>
        /// <param name="source">A sequence of nullable System.Double values to determine the maximum value of.</param>
        /// <returns>A value of type Nullable&lt;Double&gt; in C# or Nullable(Of Double) in Visual Basic that corresponds to the maximum value in the sequence.</returns>
        /// <exception cref="System.ArgumentNullException">source is null.</exception>
        public static double? Max(this IEnumerable<double?> source)
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }

            double? maximum = new double?();
            using (IEnumerator<double?> enumerator = source.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    double? item = enumerator.Current;
                    if (item.HasValue)
                    {
                        maximum = item;
                        break;
                    }
                }

                if (maximum.HasValue)
                {
                    while (enumerator.MoveNext())
                    {
                        double? item = enumerator.Current;
                        if (item.HasValue && !double.IsNaN(item.Value))
                        {
                            if (item.Value > maximum.Value || double.IsNaN(maximum.Value)) { maximum = item; }
                        }
                    }
                }
            }

            return maximum;
        }

        /// <summary>
        /// Returns the maximum value in a sequence of nullable System.Int64 values.
        /// </summary>
        /// <param name="source">A sequence of nullable System.Int64 values to determine the maximum value of.</param>
        /// <returns>A value of type Nullable&lt;Int64&gt; in C# or Nullable(Of Int64) in Visual Basic that corresponds to the maximum value in the sequence.</returns>
        /// <exception cref="System.ArgumentNullException">source is null.</exception>
        public static long? Max(this IEnumerable<long?> source)
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }

            long? maximum = new long?();
            using (IEnumerator<long?> enumerator = source.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    long? item = enumerator.Current;
                    if (item.HasValue)
                    {
                        maximum = item;
                        break;
                    }
                }

                if (maximum.HasValue)
                {
                    while (enumerator.MoveNext())
                    {
                        long? item = enumerator.Current;
                        if (item.HasValue && item.Value > maximum.Value) { maximum = item; }
                    }
                }
            }

            return maximum;
        }

        /// <summary>
        /// Returns the maximum value in a sequence of nullable System.Decimal values.
        /// </summary>
        /// <param name="source">A sequence of nullable System.Decimal values to determine the maximum value of.</param>
        /// <returns>A value of type Nullable&lt;Decimal&gt; in C# or Nullable(Of Decimal) in Visual Basic that corresponds to the maximum value in the sequence.</returns>
        /// <exception cref="System.ArgumentNullException">source is null.</exception>
        public static decimal? Max(this IEnumerable<decimal?> source)
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }

            decimal? maximum = new decimal?();
            using (IEnumerator<decimal?> enumerator = source.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    decimal? item = enumerator.Current;
                    if (item.HasValue)
                    {
                        maximum = item;
                        break;
                    }
                }

                if (maximum.HasValue)
                {
                    while (enumerator.MoveNext())
                    {
                        decimal? item = enumerator.Current;
                        if (item.HasValue && item.Value > maximum.Value) { maximum = item; }
                    }
                }
            }

            return maximum;
        }

        /// <summary>
        /// Invokes a transform function on each element of a sequence and returns the maximum System.Int32 value.
        /// </summary>
        /// <param name="source">A sequence of values to determine the maximum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <returns>The maximum value in the sequence.</returns>
        /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
        /// <exception cref="System.InvalidOperationException">source contains no elements.</exception>
        public static int Max<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector)
        {
            return Max(source.Select(selector));
        }

        /// <summary>
        /// Invokes a transform function on each element of a sequence and returns the maximum System.Int64 value.
        /// </summary>
        /// <param name="source">A sequence of values to determine the maximum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <returns>The maximum value in the sequence.</returns>
        /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
        /// <exception cref="System.InvalidOperationException">source contains no elements.</exception>
        public static long Max<TSource>(this IEnumerable<TSource> source, Func<TSource, long> selector)
        {
            return Max(source.Select(selector));
        }

        /// <summary>
        /// Invokes a transform function on each element of a sequence and returns the maximum System.Single value.
        /// </summary>
        /// <param name="source">A sequence of values to determine the maximum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <returns>The maximum value in the sequence.</returns>
        /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
        /// <exception cref="System.InvalidOperationException">source contains no elements.</exception>
        public static float Max<TSource>(this IEnumerable<TSource> source, Func<TSource, float> selector)
        {
            return Max(source.Select(selector));
        }

        /// <summary>
        /// Invokes a transform function on each element of a sequence and returns the maximum System.Double value.
        /// </summary>
        /// <param name="source">A sequence of values to determine the maximum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <returns>The maximum value in the sequence.</returns>
        /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
        /// <exception cref="System.InvalidOperationException">source contains no elements.</exception>
        public static double Max<TSource>(this IEnumerable<TSource> source, Func<TSource, double> selector)
        {
            return Max(source.Select(selector));
        }

        /// <summary>
        /// Invokes a transform function on each element of a sequence and returns the maximum System.Decimal value.
        /// </summary>
        /// <param name="source">A sequence of values to determine the maximum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <returns>The maximum value in the sequence.</returns>
        /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
        /// <exception cref="System.InvalidOperationException">source contains no elements.</exception>
        public static decimal Max<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal> selector)
        {
            return Max(source.Select(selector));
        }

        /// <summary>
        /// Invokes a transform function on each element of a sequence and returns the maximum nullable System.Int32 value.
        /// </summary>
        /// <param name="source">A sequence of values to determine the maximum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <returns>The value of type Nullable&lt;Int32&gt; in C# or Nullable(Of Int32) in Visual Basic that corresponds to the maximum value in the sequence.</returns>
        /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
        public static int? Max<TSource>(this IEnumerable<TSource> source, Func<TSource, int?> selector)
        {
            return Max(source.Select(selector));
        }

        /// <summary>
        /// Invokes a transform function on each element of a sequence and returns the maximum nullable System.Int64 value.
        /// </summary>
        /// <param name="source">A sequence of values to determine the maximum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <returns>The value of type Nullable&lt;Int64&gt; in C# or Nullable(Of Int64) in Visual Basic that corresponds to the maximum value in the sequence.</returns>
        /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
        public static long? Max<TSource>(this IEnumerable<TSource> source, Func<TSource, long?> selector)
        {
            return Max(source.Select(selector));
        }

        /// <summary>
        /// Invokes a transform function on each element of a sequence and returns the maximum nullable System.Single value.
        /// </summary>
        /// <param name="source">A sequence of values to determine the maximum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <returns>The value of type Nullable&lt;Single&gt; in C# or Nullable(Of Single) in Visual Basic that corresponds to the maximum value in the sequence.</returns>
        /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
        public static float? Max<TSource>(this IEnumerable<TSource> source, Func<TSource, float?> selector)
        {
            return Max(source.Select(selector));
        }

        /// <summary>
        /// Invokes a transform function on each element of a sequence and returns the maximum nullable System.Double value.
        /// </summary>
        /// <param name="source">A sequence of values to determine the maximum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <returns>The value of type Nullable&lt;Double&gt; in C# or Nullable(Of Double) in Visual Basic that corresponds to the maximum value in the sequence.</returns>
        /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
        public static double? Max<TSource>(this IEnumerable<TSource> source, Func<TSource, double?> selector)
        {
            return Max(source.Select(selector));
        }

        /// <summary>
        /// Invokes a transform function on each element of a sequence and returns the maximum nullable System.Decimal value.
        /// </summary>
        /// <param name="source">A sequence of values to determine the maximum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <returns>The value of type Nullable&lt;Decimal&gt; in C# or Nullable(Of Decimal) in Visual Basic that corresponds to the maximum value in the sequence.</returns>
        /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
        public static decimal? Max<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal?> selector)
        {
            return Max(source.Select(selector));
        }

        /// <summary>
        /// Invokes a transform function on each element of a generic sequence and returns the maximum resulting value.
        /// </summary>
        /// <param name="source">A sequence of values to determine the maximum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TResult">The type of the value returned by selector.</typeparam>
        /// <returns>The maximum value in the sequence.</returns>
        /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
        public static TResult Max<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            return Max(source.Select(selector));
        }

        #endregion Max Overloads

        #region Min Overloads

        /// <summary>
        /// Returns the minimum value in a sequence of System.Int32 values.
        /// </summary>
        /// <param name="source">A sequence of System.Int32 values to determine the minimum value of.</param>
        /// <returns>The minimum value in the sequence.</returns>
        /// <exception cref="System.ArgumentNullException">source is null.</exception>
        /// <exception cref="System.InvalidOperationException">source contains no elements.</exception>
        public static int Min(this IEnumerable<int> source)
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }
            using (IEnumerator<int> enumerator = source.GetEnumerator())
            {
                bool has = enumerator.MoveNext();

                if (!has) { throw Exceptions.NoElements(); }
                
                int min = enumerator.Current;

                while (enumerator.MoveNext())
                {
                    if (enumerator.Current < min) { min = enumerator.Current; }
                }

                return min;
            }
        }

        /// <summary>
        /// Returns the minimum value in a sequence of System.Single values.
        /// </summary>
        /// <param name="source">A sequence of System.Single values to determine the minimum value of.</param>
        /// <returns>The minimum value in the sequence.</returns>
        /// <exception cref="System.ArgumentNullException">source is null.</exception>
        /// <exception cref="System.InvalidOperationException">source contains no elements.</exception>
        public static float Min(this IEnumerable<float> source)
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }
            using (IEnumerator<float> enumerator = source.GetEnumerator())
            {
                bool has = enumerator.MoveNext();

                if (!has) { throw Exceptions.NoElements(); }
                
                float minimum = enumerator.Current;
                if (float.IsNaN(minimum)) { return minimum; }

                while (enumerator.MoveNext())
                {
                    if (float.IsNaN(enumerator.Current)) 
                    {
                        minimum = enumerator.Current;
                        break;
                    }
                    
                    if (enumerator.Current < (double)minimum) { minimum = enumerator.Current; }
                }

                return minimum;
            }
        }

        /// <summary>
        /// Returns the minimum value in a sequence of System.Double values.
        /// </summary>
        /// <param name="source">A sequence of System.Double values to determine the minimum value of.</param>
        /// <returns>The minimum value in the sequence.</returns>
        /// <exception cref="System.ArgumentNullException">source is null.</exception>
        /// <exception cref="System.InvalidOperationException">source contains no elements.</exception>
        public static double Min(this IEnumerable<double> source)
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }
            using (IEnumerator<double> enumerator = source.GetEnumerator())
            {
                bool has = enumerator.MoveNext();

                if (!has) { throw Exceptions.NoElements(); }
                
                double minimum = enumerator.Current;
                if (double.IsNaN(minimum)) { return minimum; }

                while (enumerator.MoveNext())
                {
                    if (double.IsNaN(enumerator.Current)) 
                    {
                        minimum = enumerator.Current;
                        break;
                    }
                    else if (enumerator.Current < minimum) { minimum = enumerator.Current; }
                }

                return minimum;
            }
        }

        /// <summary>
        /// Returns the minimum value in a sequence of System.Int64 values.
        /// </summary>
        /// <param name="source">A sequence of System.Int64 values to determine the minimum value of.</param>
        /// <returns>The minimum value in the sequence.</returns>
        /// <exception cref="System.ArgumentNullException">source is null.</exception>
        /// <exception cref="System.InvalidOperationException">source contains no elements.</exception>
        public static long Min(this IEnumerable<long> source)
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }
            using (IEnumerator<long> enumerator = source.GetEnumerator())
            {
                bool has = enumerator.MoveNext();

                if (!has) { throw Exceptions.NoElements(); }
                
                long min = enumerator.Current;

                while (enumerator.MoveNext())
                {
                    if (enumerator.Current < min) { min = enumerator.Current; }
                }

                return min;
            }
        }

        /// <summary>
        /// Returns the minimum value in a sequence of System.Decimal values.
        /// </summary>
        /// <param name="source">A sequence of System.Decimal values to determine the minimum value of.</param>
        /// <returns>The minimum value in the sequence.</returns>
        /// <exception cref="System.ArgumentNullException">source is null.</exception>
        /// <exception cref="System.InvalidOperationException">source contains no elements.</exception>
        public static decimal Min(this IEnumerable<decimal> source)
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }
            using (IEnumerator<decimal> enumerator = source.GetEnumerator())
            {
                bool has = enumerator.MoveNext();

                if (!has) { throw Exceptions.NoElements(); }
                
                decimal min = enumerator.Current;

                while (enumerator.MoveNext())
                {
                    if (enumerator.Current < min) { min = enumerator.Current; }
                }

                return min;
            }
        }

        /// <summary>
        /// Returns the minimum value in a generic sequence.
        /// </summary>
        /// <param name="source">A sequence of values to determine the minimum value of.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <returns>The minimum value in the sequence.</returns>
        /// <exception cref="System.ArgumentNullException">source is null.</exception>
        public static TSource Min<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }

            Comparer<TSource> comparer = Comparer<TSource>.Default;
            TSource min = default(TSource);

            if (min == null)
            {
                foreach (TSource item in source)
                {
                    if (item == null) { continue; }
                    if (min == null || comparer.Compare(item, min) < 0) { min = item; }
                }
            }
            else
            {
                using (IEnumerator<TSource> enumerator = source.GetEnumerator())
                {
                    bool has = enumerator.MoveNext();

                    if (!has) { throw Exceptions.NoElements(); }

                    min = enumerator.Current;

                    while (enumerator.MoveNext())
                    {
                        if (comparer.Compare(enumerator.Current, min) < 0) { min = enumerator.Current; }
                    }
                }
            }

            return min;
        }

        /// <summary>
        /// Returns the minimum value in a sequence of nullable System.Int32 values.
        /// </summary>
        /// <param name="source">A sequence of nullable System.Int32 values to determine the minimum value of.</param>
        /// <returns>A value of type Nullable&lt;Int32&gt; in C# or Nullable(Of Int32) in Visual Basic that corresponds to the minimum value in the sequence.</returns>
        /// <exception cref="System.ArgumentNullException">source is null.</exception>
        public static int? Min(this IEnumerable<int?> source)
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }
            
            int? minimum = new int?();
            using (IEnumerator<int?> enumerator = source.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    int? item = enumerator.Current;
                    if (item.HasValue)
                    {
                        minimum = item;
                        break;
                    }
                }

                if (minimum.HasValue)
                {
                    while (enumerator.MoveNext())
                    {
                        int? item = enumerator.Current;
                        if (item.HasValue && item.Value < minimum.Value) { minimum = item; }
                    }
                }
            }

            return minimum;
        }

        /// <summary>
        /// Returns the minimum value in a sequence of nullable System.Single values.
        /// </summary>
        /// <param name="source">A sequence of nullable System.Single values to determine the minimum value of.</param>
        /// <returns>A value of type Nullable&lt;Single&gt; in C# or Nullable(Of Single) in Visual Basic that corresponds to the minimum value in the sequence.</returns>
        /// <exception cref="System.ArgumentNullException">source is null.</exception>
        public static float? Min(this IEnumerable<float?> source)
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }

            float? minimum = new float?();
            using (IEnumerator<float?> enumerator = source.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    float? item = enumerator.Current;
                    if (item.HasValue)
                    {
                        minimum = item;
                        break;
                    }
                }

                if (minimum.HasValue)
                {
                    while (enumerator.MoveNext())
                    {
                        float? item = enumerator.Current;
                        if (item.HasValue)
                        {
                            if (float.IsNaN(item.Value)) { return item; }
                            if (item.Value < (double)minimum.Value) { minimum = item; }
                        }
                    }
                }
            }

            return minimum;
        }

        /// <summary>
        /// Returns the minimum value in a sequence of nullable System.Double values.
        /// </summary>
        /// <param name="source">A sequence of nullable System.Double values to determine the minimum value of.</param>
        /// <returns>A value of type Nullable&lt;Double&gt; in C# or Nullable(Of Double) in Visual Basic that corresponds to the minimum value in the sequence.</returns>
        /// <exception cref="System.ArgumentNullException">source is null.</exception>
        public static double? Min(this IEnumerable<double?> source)
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }
            
            double? minimum = new double?();
            using (IEnumerator<double?> enumerator = source.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    double? item = enumerator.Current;
                    if (item.HasValue)
                    {
                        minimum = item;
                        break;
                    }
                }

                if (minimum.HasValue)
                {
                    while (enumerator.MoveNext())
                    {
                        double? item = enumerator.Current;
                        if (item.HasValue)
                        {
                            if (double.IsNaN(item.Value)) { return item; }
                            if (item.Value < minimum.Value) { minimum = item; }
                        }
                    }
                }
            }

            return minimum;
        }

        /// <summary>
        /// Returns the minimum value in a sequence of nullable System.Int64 values.
        /// </summary>
        /// <param name="source">A sequence of nullable System.Int64 values to determine the minimum value of.</param>
        /// <returns>A value of type Nullable&lt;Int64&gt; in C# or Nullable(Of Int64) in Visual Basic that corresponds to the minimum value in the sequence.</returns>
        /// <exception cref="System.ArgumentNullException">source is null.</exception>
        public static long? Min(this IEnumerable<long?> source)
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }

            long? minimum = new long?();
            using (IEnumerator<long?> enumerator = source.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    long? item = enumerator.Current;
                    if (item.HasValue)
                    {
                        minimum = item;
                        break;
                    }
                }

                if (minimum.HasValue)
                {
                    while (enumerator.MoveNext())
                    {
                        long? item = enumerator.Current;
                        if (item.HasValue && item.Value < minimum.Value) { minimum = item; }
                    }
                }
            }

            return minimum;
        }

        /// <summary>
        /// Returns the minimum value in a sequence of nullable System.Decimal values.
        /// </summary>
        /// <param name="source">A sequence of nullable System.Decimal values to determine the minimum value of.</param>
        /// <returns>A value of type Nullable&lt;Decimal&gt; in C# or Nullable(Of Decimal) in Visual Basic that corresponds to the minimum value in the sequence.</returns>
        /// <exception cref="System.ArgumentNullException">source is null.</exception>
        public static decimal? Min(this IEnumerable<decimal?> source)
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }

            decimal? minimum = new decimal?();
            using (IEnumerator<decimal?> enumerator = source.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    decimal? item = enumerator.Current;
                    if (item.HasValue)
                    {
                        minimum = item;
                        break;
                    }
                }

                if (minimum.HasValue)
                {
                    while (enumerator.MoveNext())
                    {
                        decimal? item = enumerator.Current;
                        if (item.HasValue && item.Value < minimum.Value) { minimum = item; }
                    }
                }
            }

            return minimum;
        }

        /// <summary>
        /// Invokes a transform function on each element of a sequence and returns the minimum System.Int32 value.
        /// </summary>
        /// <param name="source">A sequence of values to determine the minimum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <returns>The minimum value in the sequence.</returns>
        /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
        /// <exception cref="System.InvalidOperationException">source contains no elements.</exception>
        public static int Min<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector)
        {
            return Min(source.Select(selector));
        }

        /// <summary>
        /// Invokes a transform function on each element of a sequence and returns the minimum System.Int64 value.
        /// </summary>
        /// <param name="source">A sequence of values to determine the minimum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <returns>The minimum value in the sequence.</returns>
        /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
        /// <exception cref="System.InvalidOperationException">source contains no elements.</exception>
        public static long Min<TSource>(this IEnumerable<TSource> source, Func<TSource, long> selector)
        {
            return Min(source.Select(selector));
        }

        /// <summary>
        /// Invokes a transform function on each element of a sequence and returns the minimum System.Single value.
        /// </summary>
        /// <param name="source">A sequence of values to determine the minimum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <returns>The minimum value in the sequence.</returns>
        /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
        /// <exception cref="System.InvalidOperationException">source contains no elements.</exception>
        public static float Min<TSource>(this IEnumerable<TSource> source, Func<TSource, float> selector)
        {
            return Min(source.Select(selector));
        }

        /// <summary>
        /// Invokes a transform function on each element of a sequence and returns the minimum System.Double value.
        /// </summary>
        /// <param name="source">A sequence of values to determine the minimum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <returns>The minimum value in the sequence.</returns>
        /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
        /// <exception cref="System.InvalidOperationException">source contains no elements.</exception>
        public static double Min<TSource>(this IEnumerable<TSource> source, Func<TSource, double> selector)
        {
            return Min(source.Select(selector));
        }

        /// <summary>
        /// Invokes a transform function on each element of a sequence and returns the minimum System.Decimal value.
        /// </summary>
        /// <param name="source">A sequence of values to determine the minimum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <returns>The minimum value in the sequence.</returns>
        /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
        /// <exception cref="System.InvalidOperationException">source contains no elements.</exception>
        public static decimal Min<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal> selector)
        {
            return Min(source.Select(selector));
        }

        /// <summary>
        /// Invokes a transform function on each element of a sequence and returns the minimum nullable System.Int32 value.
        /// </summary>
        /// <param name="source">A sequence of values to determine the minimum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <returns>The value of type Nullable&lt;Int32&gt; in C# or Nullable(Of Int32) in Visual Basic that corresponds to the minimum value in the sequence.</returns>
        /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
        public static int? Min<TSource>(this IEnumerable<TSource> source, Func<TSource, int?> selector)
        {
            return Min(source.Select(selector));
        }

        /// <summary>
        /// Invokes a transform function on each element of a sequence and returns the minimum nullable System.Int64 value.
        /// </summary>
        /// <param name="source">A sequence of values to determine the minimum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <returns>The value of type Nullable&lt;Int64&gt; in C# or Nullable(Of Int64) in Visual Basic that corresponds to the minimum value in the sequence.</returns>
        /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
        public static long? Min<TSource>(this IEnumerable<TSource> source, Func<TSource, long?> selector)
        {
            return Min(source.Select(selector));
        }

        /// <summary>
        /// Invokes a transform function on each element of a sequence and returns the minimum nullable System.Single value.
        /// </summary>
        /// <param name="source">A sequence of values to determine the minimum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <returns>The value of type Nullable&lt;Single&gt; in C# or Nullable(Of Single) in Visual Basic that corresponds to the minimum value in the sequence.</returns>
        /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
        public static float? Min<TSource>(this IEnumerable<TSource> source, Func<TSource, float?> selector)
        {
            return Min(source.Select(selector));
        }

        /// <summary>
        /// Invokes a transform function on each element of a sequence and returns the minimum nullable System.Double value.
        /// </summary>
        /// <param name="source">A sequence of values to determine the minimum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <returns>The value of type Nullable&lt;Double&gt; in C# or Nullable(Of Double) in Visual Basic that corresponds to the minimum value in the sequence.</returns>
        /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
        public static double? Min<TSource>(this IEnumerable<TSource> source, Func<TSource, double?> selector)
        {
            return Min(source.Select(selector));
        }

        /// <summary>
        /// Invokes a transform function on each element of a sequence and returns the minimum nullable System.Decimal value.
        /// </summary>
        /// <param name="source">A sequence of values to determine the minimum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <returns>The value of type Nullable&lt;Decimal&gt; in C# or Nullable(Of Decimal) in Visual Basic that corresponds to the minimum value in the sequence.</returns>
        /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
        public static decimal? Min<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal?> selector)
        {
            return Min(source.Select(selector));
        }

        /// <summary>
        /// Invokes a transform function on each element of a generic sequence and returns the minimum resulting value.
        /// </summary>
        /// <param name="source">A sequence of values to determine the minimum value of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TResult">The type of the value returned by selector.</typeparam>
        /// <returns>The minimum value in the sequence.</returns>
        /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
        public static TResult Min<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            return Min(source.Select(selector));
        }

        #endregion Min Overloads

        #region Average Overloads

        /// <summary>
        /// Computes the average of a sequence of System.Int32 values.
        /// </summary>
        /// <param name="source">A sequence of System.Int32 values to calculate the average of.</param>
        /// <returns>The average of the sequence of values.</returns>
        /// <exception cref="System.ArgumentNullException">source is null.</exception>
        /// <exception cref="System.InvalidOperationException">source contains no elements.</exception>
        /// <exception cref="System.OverflowException">The sum of the elements in the sequence is larger than System.Int64.MaxValue.</exception>
        public static double Average(this IEnumerable<int> source)
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }
            
            long sum = 0L;
            long count = 0L;

            foreach (int item in source)
            {
                checked
                {
                    sum += item;
                    count++;
                }
            }

            if (count == 0)
            {
                throw Exceptions.NoElements();
            }

            return (double)sum / count;
        }

        /// <summary>
        /// Computes the average of a sequence of System.Int64 values.
        /// </summary>
        /// <param name="source">A sequence of System.Int64 values to calculate the average of.</param>
        /// <returns>The average of the sequence of values.</returns>
        /// <exception cref="System.ArgumentNullException">source is null.</exception>
        /// <exception cref="System.InvalidOperationException">source contains no elements.</exception>
        /// <exception cref="System.OverflowException">The sum of the elements in the sequence is larger than System.Int64.MaxValue.</exception>
        public static double Average(this IEnumerable<long> source)
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }
            
            long sum = 0L;
            long count = 0L;

            foreach (long item in source)
            {
                checked
                {
                    sum += item;
                    count++;
                }
            }

            if (count == 0)
            {
                throw Exceptions.NoElements();
            }

            return (double)sum / count;
        }

        /// <summary>
        /// Computes the average of a sequence of System.Single values.
        /// </summary>
        /// <param name="source">A sequence of System.Single values to calculate the average of.</param>
        /// <returns>The average of the sequence of values.</returns>
        /// <exception cref="System.ArgumentNullException">source is null.</exception>
        /// <exception cref="System.InvalidOperationException">source contains no elements.</exception>
        public static float Average(this IEnumerable<float> source)
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }
            
            double sum = 0D;
            long count = 0L;

            foreach (float item in source)
            {
                checked
                {
                    sum += item;
                    count++;
                }
            }

            if (count == 0)
            {
                throw Exceptions.NoElements();
            }

            return (float)(sum / count);
        }

        /// <summary>
        /// Computes the average of a sequence of System.Double values.
        /// </summary>
        /// <param name="source">A sequence of System.Double values to calculate the average of.</param>
        /// <returns>The average of the sequence of values.</returns>
        /// <exception cref="System.ArgumentNullException">source is null.</exception>
        /// <exception cref="System.InvalidOperationException">source contains no elements.</exception>
        public static double Average(this IEnumerable<double> source)
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }
            
            double sum = 0D;
            long count = 0L;

            foreach (double item in source)
            {
                checked
                {
                    sum += item;
                    count++;
                }
            }

            if (count == 0)
            {
                throw Exceptions.NoElements();
            }

            return sum / count;
        }

        /// <summary>
        /// Computes the average of a sequence of System.Decimal values.
        /// </summary>
        /// <param name="source">A sequence of System.Decimal values to calculate the average of.</param>
        /// <returns>The average of the sequence of values.</returns>
        /// <exception cref="System.ArgumentNullException">source is null.</exception>
        /// <exception cref="System.InvalidOperationException">source contains no elements.</exception>
        /// <exception cref="System.OverflowException">The sum of the elements in the sequence is larger than System.Decimal.MaxValue.</exception>
        public static decimal Average(this IEnumerable<decimal> source)
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }
            
            decimal sum = 0M;
            long count = 0L;

            foreach (decimal item in source)
            {
                checked
                {
                    sum += item;
                    count++;
                }
            }

            if (count == 0)
            {
                throw Exceptions.NoElements();
            }

            return sum / count;
        }

        /// <summary>
        /// Computes the average of a sequence of nullable System.Int32 values.
        /// </summary>
        /// <param name="source">A sequence of nullable System.Int32 values to calculate the average of.</param>
        /// <returns>The average of the sequence of values, or null if the source sequence is empty or contains only values that are null.</returns>
        /// <exception cref="System.ArgumentNullException">source is null.</exception>
        /// <exception cref="System.OverflowException">The sum of the elements in the sequence is larger than System.Int64.MaxValue.</exception>
        public static double? Average(this IEnumerable<int?> source)
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }

            long sum = 0L;
            long count = 0L;

            foreach (int? item in source)
            {
                if (item.HasValue)
                {
                    checked
                    {
                        sum += item.Value;
                        count++;
                    }
                }
            }

            if (count == 0) { return new double?(); }
            
            return (double)sum / count;
        }

        /// <summary>
        /// Computes the average of a sequence of nullable System.Int64 values.
        /// </summary>
        /// <param name="source">A sequence of nullable System.Int64 values to calculate the average of.</param>
        /// <returns>The average of the sequence of values, or null if the source sequence is empty or contains only values that are null.</returns>
        /// <exception cref="System.ArgumentNullException">source is null.</exception>
        /// <exception cref="System.OverflowException">The sum of the elements in the sequence is larger than System.Int64.MaxValue.</exception>
        public static double? Average(this IEnumerable<long?> source)
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }
            
            long sum = 0L;
            long count = 0L;

            foreach (long? item in source)
            {
                if (item.HasValue)
                {
                    checked
                    {
                        sum += item.Value;
                        count++;
                    }
                }
            }

            if (count == 0) { return new long?(); }

            return (double)sum / count;
        }

        /// <summary>
        /// Computes the average of a sequence of nullable System.Single values.
        /// </summary>
        /// <param name="source">A sequence of nullable System.Single values to calculate the average of.</param>
        /// <returns>The average of the sequence of values, or null if the source sequence is empty or contains only values that are null.</returns>
        /// <exception cref="System.ArgumentNullException">source is null.</exception>
        public static float? Average(this IEnumerable<float?> source)
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }
            
            double sum = 0D;
            long count = 0L;

            foreach (float? item in source)
            {
                if (item.HasValue) 
                {
                    checked
                    {
                        sum += item.Value;
                        count++;
                    }
                }
            }

            if (count == 0) { return new float?(); }

            return (float)(sum / count);
        }

        /// <summary>
        /// Computes the average of a sequence of nullable System.Double values.
        /// </summary>
        /// <param name="source">A sequence of nullable System.Double values to calculate the average of.</param>
        /// <returns>The average of the sequence of values, or null if the source sequence is empty or contains only values that are null.</returns>
        /// <exception cref="System.ArgumentNullException">source is null.</exception>
        public static double? Average(this IEnumerable<double?> source)
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }
            
            double sum = 0D;
            long count = 0L;

            foreach (double? item in source)
            {
                if (item.HasValue)
                {
                    checked 
                    {
                        sum += item.Value;
                        count++;
                    }
                }
            }

            if (count == 0) { return new double?(); }

            return sum / count;
        }

        /// <summary>
        /// Computes the average of a sequence of nullable System.Decimal values.
        /// </summary>
        /// <param name="source">A sequence of nullable System.Decimal values to calculate the average of.</param>
        /// <returns>The average of the sequence of values, or null if the source sequence is empty or contains only values that are null.</returns>
        /// <exception cref="System.ArgumentNullException">source is null.</exception>
        /// <exception cref="System.OverflowException">The sum of the elements in the sequence is larger than System.Decimal.MaxValue.</exception>
        public static decimal? Average(this IEnumerable<decimal?> source)
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }
            
            decimal sum = 0M;
            long count = 0L;

            foreach (decimal? item in source)
            {
                if (item.HasValue)
                {
                    checked
                    {
                        sum += item.Value;
                        count++;
                    }
                }
            }

            if (count == 0) { return new decimal?(); }

            return sum / count;
        }

        /// <summary>
        /// Computes the average of a sequence of nullable System.Int32 values that are obtained by invoking a transform function on each element of the input sequence.
        /// </summary>
        /// <param name="source">A sequence of values to calculate the average of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <returns>The average of the sequence of values, or null if the source sequence is empty or contains only values that are null.</returns>
        /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
        /// <exception cref="System.OverflowException">The sum of the elements in the sequence is larger than System.Int64.MaxValue.</exception>
        public static double? Average<TSource>(this IEnumerable<TSource> source, Func<TSource, int?> selector)
        {
            return Average(source.Select(selector));
        }

        /// <summary>
        /// Computes the average of a sequence of System.Int32 values that are obtained by invoking a transform function on each element of the input sequence.
        /// </summary>
        /// <param name="source">A sequence of values to calculate the average of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <returns>The average of the sequence of values.</returns>
        /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
        /// <exception cref="System.InvalidOperationException">source contains no elements.</exception>
        /// <exception cref="System.OverflowException">The sum of the elements in the sequence is larger than System.Int64.MaxValue.</exception>
        public static double Average<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector)
        {
            return Average(source.Select(selector));
        }

        /// <summary>
        /// Computes the average of a sequence of nullable System.Int64 values that are obtained by invoking a transform function on each element of the input sequence.
        /// </summary>
        /// <param name="source">A sequence of values to calculate the average of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <returns>The average of the sequence of values, or null if the source sequence is empty or contains only values that are null.</returns>
        public static double? Average<TSource>(this IEnumerable<TSource> source, Func<TSource, long?> selector)
        {
            return Average(source.Select(selector));
        }

        /// <summary>
        /// Computes the average of a sequence of System.Int64 values that are obtained by invoking a transform function on each element of the input sequence.
        /// </summary>
        /// <param name="source">A sequence of values to calculate the average of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <returns>The average of the sequence of values.</returns>
        /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
        /// <exception cref="System.InvalidOperationException">source contains no elements.</exception>
        /// <exception cref="System.OverflowException">The sum of the elements in the sequence is larger than System.Int64.MaxValue.</exception>
        public static double Average<TSource>(this IEnumerable<TSource> source, Func<TSource, long> selector)
        {
            return Average(source.Select(selector));
        }

        /// <summary>
        /// Computes the average of a sequence of nullable System.Single values that are obtained by invoking a transform function on each element of the input sequence.
        /// </summary>
        /// <param name="source">A sequence of values to calculate the average of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <returns>The average of the sequence of values, or null if the source sequence is empty or contains only values that are null.</returns>
        /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
        public static float? Average<TSource>(this IEnumerable<TSource> source, Func<TSource, float?> selector)
        {
            return Average(source.Select(selector));
        }

        /// <summary>
        /// Computes the average of a sequence of System.Single values that are obtained by invoking a transform function on each element of the input sequence.
        /// </summary>
        /// <param name="source">A sequence of values to calculate the average of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <returns>The average of the sequence of values.</returns>
        /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
        /// <exception cref="System.InvalidOperationException">source contains no elements.</exception>
        public static float Average<TSource>(this IEnumerable<TSource> source, Func<TSource, float> selector)
        {
            return Average(source.Select(selector));
        }

        /// <summary>
        /// Computes the average of a sequence of nullable System.Decimal values that are obtained by invoking a transform function on each element of the input sequence.
        /// </summary>
        /// <param name="source">A sequence of values to calculate the average of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <returns>The average of the sequence of values, or null if the source sequence is empty or contains only values that are null.</returns>
        /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
        /// <exception cref="System.OverflowException">The sum of the elements in the sequence is larger than System.Decimal.MaxValue.</exception>
        public static decimal? Average<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal?> selector)
        {
            return Average(source.Select(selector));
        }

        /// <summary>
        /// Computes the average of a sequence of System.Decimal values that are obtained by invoking a transform function on each element of the input sequence.
        /// </summary>
        /// <param name="source">A sequence of values that are used to calculate an average.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <returns>The average of the sequence of values.</returns>
        /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
        /// <exception cref="System.InvalidOperationException">source contains no elements.</exception>
        /// <exception cref="System.OverflowException">The sum of the elements in the sequence is larger than System.Decimal.MaxValue.</exception>
        public static decimal Average<TSource>(this IEnumerable<TSource> source, Func<TSource, decimal> selector)
        {
            return Average(source.Select(selector));
        }

        /// <summary>
        /// Computes the average of a sequence of nullable System.Double values that are obtained by invoking a transform function on each element of the input sequence.
        /// </summary>
        /// <param name="source">A sequence of values to calculate the average of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <returns>The average of the sequence of values, or null if the source sequence is empty or contains only values that are null.</returns>
        /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
        public static double? Average<TSource>(this IEnumerable<TSource> source, Func<TSource, double?> selector)
        {
            return Average(source.Select(selector));
        }

        /// <summary>
        /// Computes the average of a sequence of System.Double values that are obtained by invoking a transform function on each element of the input sequence.
        /// </summary>
        /// <param name="source">A sequence of values to calculate the average of.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <returns>The average of the sequence of values.</returns>
        /// <exception cref="System.ArgumentNullException">source or selector is null.</exception>
        /// <exception cref="System.InvalidOperationException">source contains no elements.</exception>
        public static double Average<TSource>(this IEnumerable<TSource> source, Func<TSource, double> selector)
        {
            return Average(source.Select(selector));
        }

        #endregion ENDOF: Average Overloads

        #region Empty Overloads

        /// <summary>
        /// Returns an empty System.Collections.Generic.IEnumerable&lt;T&gt; that has the specified type argument.
        /// </summary>
        /// <typeparam name="TResult">The type to assign to the type parameter of the returned generic System.Collections.Generic.IEnumerable&lt;T&gt;.</typeparam>
        /// <returns>An empty System.Collections.Generic.IEnumerable&lt;T&gt; whose type argument is TResult.</returns>
        public static IEnumerable<TResult> Empty<TResult>()
        {
            return EmptyEnumerable<TResult>.Instance; // # Returning singleton empty enumerable instance of given type
        }

        #endregion ENDOF: Empty Overloads

        #region ToDictionary Overloads

        /// <summary>
        /// Creates a System.Collections.Generic.Dictionary&lt;TKey,TValue&gt; from an System.Collections.Generic.IEnumerable&lt;T&gt; according to a specified key selector function.
        /// </summary>
        /// <param name="source">An System.Collections.Generic.IEnumerable&lt;T&gt; to create a System.Collections.Generic.Dictionary&lt;TKey,TValue&gt; from.</param>
        /// <param name="keySelector">A function to extract a key from each element.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by keySelector.</typeparam>
        /// <returns>A System.Collections.Generic.Dictionary&lt;TKey,TValue&gt; that contains keys and values.</returns>
        /// <exception cref="System.ArgumentNullException">source or keySelector is null.-or-keySelector produces a key that is null.</exception>
        /// <exception cref="System.ArgumentException">keySelector produces duplicate keys for two elements.</exception>
        public static Dictionary<TKey, TSource> ToDictionary<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            return ToDictionary(source, keySelector, null);
        }

        /// <summary>
        /// Creates a System.Collections.Generic.Dictionary&lt;TKey,TValue&gt; from an System.Collections.Generic.IEnumerable&lt;T&gt; according to specified key selector and element selector functions.
        /// </summary>
        /// <param name="source">An System.Collections.Generic.IEnumerable&lt;T&gt; to create a System.Collections.Generic.Dictionary&lt;TKey,TValue&gt; from.</param>
        /// <param name="keySelector">A function to extract a key from each element.</param>
        /// <param name="elementSelector">A transform function to produce a result element value from each element.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by keySelector.</typeparam>
        /// <typeparam name="TElement">The type of the value returned by elementSelector.</typeparam>
        /// <returns>A System.Collections.Generic.Dictionary&lt;TKey,TValue&gt; that contains values of type TElement selected from the input sequence.</returns>
        /// <exception cref="System.ArgumentNullException">source or keySelector or elementSelector is null.-or-keySelector produces a key that is null.</exception>
        /// <exception cref="System.ArgumentException">keySelector produces duplicate keys for two elements.</exception>
        public static Dictionary<TKey, TElement> ToDictionary<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector)
        {
            return ToDictionary(source, keySelector, elementSelector, null);
        }

        /// <summary>
        /// Creates a System.Collections.Generic.Dictionary&lt;TKey,TValue&gt; from an System.Collections.Generic.IEnumerable&lt;T&gt; according to a specified key selector function and key comparer.
        /// </summary>
        /// <param name="source">An System.Collections.Generic.IEnumerable&lt;T&gt; to create a System.Collections.Generic.Dictionary&lt;TKey,TValue&gt; from.</param>
        /// <param name="keySelector">A function to extract a key from each element.</param>
        /// <param name="comparer">An System.Collections.Generic.IEqualityComparer&lt;T&gt; to compare keys.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TKey">The type of the keys returned by keySelector.</typeparam>
        /// <returns>A System.Collections.Generic.Dictionary&lt;TKey,TValue&gt; that contains keys and values.</returns>
        /// <exception cref="System.ArgumentNullException">source or keySelector is null.-or-keySelector produces a key that is null.</exception>
        /// <exception cref="System.ArgumentException">keySelector produces duplicate keys for two elements.</exception>
        public static Dictionary<TKey, TSource> ToDictionary<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }
            if (keySelector == null) { throw Exceptions.ArgumentNull(Parameter.KeySelector); }

            Dictionary<TKey, TSource> dictionary = new Dictionary<TKey, TSource>(comparer ?? EqualityComparer<TKey>.Default);

            foreach (TSource item in source)
            {
                dictionary.Add(keySelector(item), item);
            }

            return dictionary;
        }

        /// <summary>
        /// Creates a System.Collections.Generic.Dictionary&lt;TKey,TValue&gt; from an System.Collections.Generic.IEnumerable&lt;T&gt; according to a specified key selector function, a comparer, and an element selector function.
        /// </summary>
        /// <param name="source">An System.Collections.Generic.IEnumerable&lt;T&gt; to create a System.Collections.Generic.Dictionary&lt;TKey,TValue&gt; from.</param>
        /// <param name="keySelector">A function to extract a key from each element.</param>
        /// <param name="elementSelector">A transform function to produce a result element value from each element.</param>
        /// <param name="comparer">An System.Collections.Generic.IEqualityComparer&lt;T&gt; to compare keys.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by keySelector.</typeparam>
        /// <typeparam name="TElement">The type of the value returned by elementSelector.</typeparam>
        /// <returns>A System.Collections.Generic.Dictionary&lt;TKey,TValue&gt; that contains values of type TElement selected from the input sequence.</returns>
        /// <exception cref="System.ArgumentNullException">source or keySelector or elementSelector is null.-or-keySelector produces a key that is null.</exception>
        /// <exception cref="System.ArgumentException">keySelector produces duplicate keys for two elements.</exception>
        public static Dictionary<TKey, TElement> ToDictionary<TSource, TKey, TElement>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, Func<TSource, TElement> elementSelector, IEqualityComparer<TKey> comparer)
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }
            if (keySelector == null) { throw Exceptions.ArgumentNull(Parameter.KeySelector); }
            if (elementSelector == null) { throw Exceptions.ArgumentNull(Parameter.ElementSelector); }

            Dictionary<TKey, TElement> dictionary = new Dictionary<TKey, TElement>(comparer ?? EqualityComparer<TKey>.Default);

            foreach (TSource item in source)
            {
                dictionary.Add(keySelector(item), elementSelector(item));
            }

            return dictionary;
        }

        #endregion ENDOF:  ToDictionary Overloads

        #region Aggregate Overloads
        
        /// <summary>
        /// Applies an accumulator function over a sequence.
        /// </summary>
        /// <param name="source">An System.Collections.Generic.IEnumerable&lt;T&gt; to aggregate over.</param>
        /// <param name="func">An accumulator function to be invoked on each element.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <returns>The final accumulator value.</returns>
        /// <exception cref="System.ArgumentNullException">source or func is null.</exception>
        /// <exception cref="System.InvalidOperationException">source contains no elements.</exception>
        public static TSource Aggregate<TSource>(this IEnumerable<TSource> source, Func<TSource, TSource, TSource> func)
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }
            if (func == null) { throw Exceptions.ArgumentNull(Parameter.Func); }

            using (IEnumerator<TSource> enumerator = source.GetEnumerator())
            {
                if (!enumerator.MoveNext()) { throw Exceptions.NoElements(); } // # Skipping first element if not empty

                TSource item = enumerator.Current;
                while (enumerator.MoveNext())
                {
                    item = func(item, enumerator.Current);
                }

                return item;
            }
        }

        /// <summary>
        /// Applies an accumulator function over a sequence. The specified seed value is used as the initial accumulator value.
        /// </summary>
        /// <param name="source">An System.Collections.Generic.IEnumerable&lt;T&gt; to aggregate over.</param>
        /// <param name="seed">The initial accumulator value.</param>
        /// <param name="func">An accumulator function to be invoked on each element.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TAccumulate">The type of the accumulator value.</typeparam>
        /// <returns>The final accumulator value.</returns>
        /// <exception cref="System.ArgumentNullException">source or func is null.</exception>
        public static TAccumulate Aggregate<TSource, TAccumulate>(this IEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> func)
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }
            if (func == null) { throw Exceptions.ArgumentNull(Parameter.Func); }

            TAccumulate accumulateValue = seed;
            foreach (TSource item in source)
            {
                accumulateValue = func(accumulateValue, item);
            }

            return accumulateValue;
        }

        /// <summary>
        /// Applies an accumulator function over a sequence. The specified seed value is used as the initial accumulator value, and the specified function is used to select the result value.
        /// </summary>
        /// <param name="source">An System.Collections.Generic.IEnumerable&lt;T&gt; to aggregate over.</param>
        /// <param name="seed">The initial accumulator value.</param>
        /// <param name="func">An accumulator function to be invoked on each element.</param>
        /// <param name="resultSelector">A function to transform the final accumulator value into the result value.</param>
        /// <typeparam name="TSource">The type of the elements of source.</typeparam>
        /// <typeparam name="TAccumulate">The type of the accumulator value.</typeparam>
        /// <typeparam name="TResult">The type of the resulting value.</typeparam>
        /// <returns>The transformed final accumulator value.</returns>
        /// <exception cref="System.ArgumentNullException">source or func or resultSelector is null.</exception>
        public static TResult Aggregate<TSource, TAccumulate, TResult>(this IEnumerable<TSource> source, TAccumulate seed, Func<TAccumulate, TSource, TAccumulate> func, Func<TAccumulate, TResult> resultSelector)
        {
            if (source == null) { throw Exceptions.ArgumentNull(Parameter.Source); }
            if (func == null) { throw Exceptions.ArgumentNull(Parameter.Func); }
            if (resultSelector == null) { throw Exceptions.ArgumentNull(Parameter.ResultSelector); }

            TAccumulate accumulateValue = seed;
            foreach (TSource item in source)
            {
                accumulateValue = func(accumulateValue, item);
            }

            return resultSelector(accumulateValue);
        }

        #endregion ENDOF: Aggregate Overloads
    }
}
