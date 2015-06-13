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
using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;

namespace LINQlone.Test
{
    [TestFixture]
    public class GroupingTests : BaseTest
    {
        #region Type

        [Test]
        public void GroupingImplementsGenericIList()
        {
            IEnumerable<IGrouping<int, int>> groups = Data(1, 2, 3).GroupBy(x => x);

            using (IEnumerator<IGrouping<int, int>> groupingEnumerator = groups.GetEnumerator())
            {
                groupingEnumerator.MoveNext();
                Assert.That(groupingEnumerator.Current, Is.AssignableTo<IList<int>>());
            }
        }

        #endregion ENDOF: Type

        #region IndexOf

        [Test]
        public void GroupingIndexOf()
        {
            IEnumerable<IGrouping<bool, int>> groups = Data(1, 2, 3).GroupBy(x => x % 2 == 0);

            using (IEnumerator<IGrouping<bool, int>> groupingEnumerator = groups.GetEnumerator())
            {
                groupingEnumerator.MoveNext();

                IList<int> elements = (IList<int>)groupingEnumerator.Current;
                Assert.That(elements.IndexOf(1), Is.EqualTo(0));
            }
        }

        [Test]
        public void GroupingIndexOfNoMatch()
        {
            IEnumerable<IGrouping<bool, int>> groups = Data(1, 2, 3).GroupBy(x => x % 2 == 0);

            using (IEnumerator<IGrouping<bool, int>> groupingEnumerator = groups.GetEnumerator())
            {
                groupingEnumerator.MoveNext();

                IList<int> elements = (IList<int>)groupingEnumerator.Current;
                Assert.That(elements.IndexOf(4), Is.EqualTo(-1));
            }
        }

        #endregion ENDOF: IndexOf

        #region Insert

        [Test]
        public void GroupingInsert()
        {
            IEnumerable<IGrouping<int, int>> groups = Data(1, 2, 3).GroupBy(x => x);

            using (IEnumerator<IGrouping<int, int>> groupingEnumerator = groups.GetEnumerator())
            {
                groupingEnumerator.MoveNext();
                Assert.Throws<NotSupportedException>(() => ((IList<int>)groupingEnumerator.Current).Insert(0, 0)).WithNotSupportedMessage();
            }
        }

        #endregion ENDOF: Insert

        #region RemoveAt

        [Test]
        public void GroupingRemoveAt()
        {
            IEnumerable<IGrouping<int, int>> groups = Data(1, 2, 3).GroupBy(x => x);

            using (IEnumerator<IGrouping<int, int>> groupingEnumerator = groups.GetEnumerator())
            {
                groupingEnumerator.MoveNext();
                Assert.Throws<NotSupportedException>(() => ((IList<int>)groupingEnumerator.Current).RemoveAt(0)).WithNotSupportedMessage();
            }
        }

        #endregion ENDOF: RemoveAt

        #region Indexer

        [Test]
        public void GroupingIndexer()
        {
            IEnumerable<IGrouping<int, int>> groups = Data(1, 2, 3).GroupBy(x => x);

            using (IEnumerator<IGrouping<int, int>> groupingEnumerator = groups.GetEnumerator())
            {
                groupingEnumerator.MoveNext();

                int element = ((IList<int>)groupingEnumerator.Current)[0];
                Assert.That(element, Is.EqualTo(1));
            }
        }

        [Test]
        public void GroupingIndexerNegativeIndex()
        {
            IEnumerable<IGrouping<int, int>> groups = Data(1, 2, 3).GroupBy(x => x);

            using (IEnumerator<IGrouping<int, int>> groupingEnumerator = groups.GetEnumerator())
            {
                int dontCare;

                groupingEnumerator.MoveNext();
                Assert.Throws<ArgumentOutOfRangeException>(() => dontCare = ((IList<int>)groupingEnumerator.Current)[-1]).WithParameter("index");
            }
        }

        [Test]
        public void GroupingIndexerOutOfBoundsIndex()
        {
            IEnumerable<IGrouping<int, int>> groups = Data(1, 2, 3).GroupBy(x => x);

            using (IEnumerator<IGrouping<int, int>> groupingEnumerator = groups.GetEnumerator())
            {
                int dontCare;

                groupingEnumerator.MoveNext();
                Assert.Throws<ArgumentOutOfRangeException>(() => dontCare = ((IList<int>)groupingEnumerator.Current)[3]).WithParameter("index");
            }
        }

        [Test]
        public void GroupingIndexerSetter()
        {
            IEnumerable<IGrouping<int, int>> groups = Data(1, 2, 3).GroupBy(x => x);

            using (IEnumerator<IGrouping<int, int>> groupingEnumerator = groups.GetEnumerator())
            {
                groupingEnumerator.MoveNext();
                Assert.Throws<NotSupportedException>(() => ((IList<int>)groupingEnumerator.Current)[0] = 0).WithNotSupportedMessage();
            }
        }

        #endregion ENDOF: Indexer

        #region Add

        [Test]
        public void GroupingAdd()
        {
            IEnumerable<IGrouping<int, int>> groups = Data(1, 2, 3).GroupBy(x => x);

            using (IEnumerator<IGrouping<int, int>> groupingEnumerator = groups.GetEnumerator())
            {
                groupingEnumerator.MoveNext();
                Assert.Throws<NotSupportedException>(() => ((ICollection<int>)groupingEnumerator.Current).Add(0)).WithNotSupportedMessage();
            }
        }

        #endregion ENDOF: Add

        #region Clear

        [Test]
        public void GroupingClear()
        {
            IEnumerable<IGrouping<int, int>> groups = Data(1, 2, 3).GroupBy(x => x);

            using (IEnumerator<IGrouping<int, int>> groupingEnumerator = groups.GetEnumerator())
            {
                groupingEnumerator.MoveNext();
                Assert.Throws<NotSupportedException>(() => ((ICollection<int>)groupingEnumerator.Current).Clear()).WithNotSupportedMessage();
            }
        }

        #endregion ENDOF: Clear

        #region Contains

        [Test]
        public void GroupingContains()
        {
            IEnumerable<IGrouping<bool, int>> groups = Data(1, 2, 3).GroupBy(x => x % 2 == 0);

            using (IEnumerator<IGrouping<bool, int>> groupingEnumerator = groups.GetEnumerator())
            {
                groupingEnumerator.MoveNext();

                ICollection<int> elements = (ICollection<int>)groupingEnumerator.Current;
                Assert.That(elements.Contains(1), Is.True);
            }
        }

        [Test]
        public void GroupingContainsNoMatch()
        {
            IEnumerable<IGrouping<bool, int>> groups = Data(1, 2, 3).GroupBy(x => x % 2 == 0);

            using (IEnumerator<IGrouping<bool, int>> groupingEnumerator = groups.GetEnumerator())
            {
                groupingEnumerator.MoveNext();

                ICollection<int> elements = (ICollection<int>)groupingEnumerator.Current;
                Assert.That(elements.Contains(4), Is.False);
            }
        }

        #endregion ENDOF: Contains

        #region CopyTo

        [Test]
        public void GroupingCopyTo()
        {
            IEnumerable<IGrouping<bool, int>> groups = Data(1, 2, 3).GroupBy(x => x % 2 == 0);

            using (IEnumerator<IGrouping<bool, int>> groupingEnumerator = groups.GetEnumerator())
            {
                groupingEnumerator.MoveNext();

                int[] elementsArray = new int[2];
                ICollection<int> elements = (ICollection<int>)groupingEnumerator.Current;
                elements.CopyTo(elementsArray, 0);

                elementsArray.AssertEqual(1, 3);
            }
        }

        #endregion ENDOF: CopyTo

        #region Count

        [Test]
        public void GroupingCount()
        {
            IEnumerable<IGrouping<bool, int>> groups = Data(1, 2, 3).GroupBy(x => x % 2 == 0);

            using (IEnumerator<IGrouping<bool, int>> groupingEnumerator = groups.GetEnumerator())
            {
                groupingEnumerator.MoveNext();

                ICollection<int> elements = (ICollection<int>)groupingEnumerator.Current;
                Assert.That(elements.Count, Is.EqualTo(2));
            }
        }

        #endregion ENDOF: Count

        #region IsReadOnly

        [Test]
        public void GroupingIsReadOnly()
        {
            IEnumerable<IGrouping<bool, int>> groups = Data(1, 2, 3).GroupBy(x => x % 2 == 0);

            using (IEnumerator<IGrouping<bool, int>> groupingEnumerator = groups.GetEnumerator())
            {
                groupingEnumerator.MoveNext();

                ICollection<int> elements = (ICollection<int>)groupingEnumerator.Current;
                Assert.That(elements.IsReadOnly, Is.True);
            }
        }

        #endregion ENDOF: IsReadOnly

        #region Remove

        [Test]
        public void GroupingRemove()
        {
            IEnumerable<IGrouping<int, int>> groups = Data(1, 2, 3).GroupBy(x => x);

            using (IEnumerator<IGrouping<int, int>> groupingEnumerator = groups.GetEnumerator())
            {
                groupingEnumerator.MoveNext();
                Assert.Throws<NotSupportedException>(() => ((ICollection<int>)groupingEnumerator.Current).Remove(1)).WithNotSupportedMessage();
            }
        }

        #endregion ENDOF: Remove
    }
}
