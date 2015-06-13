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

namespace LINQlone.Infrastructure
{
    internal class QuickSorter<T>
    {
        #region Fields

        protected T[] mData;
        protected IComparer<T> mComparer;

        #endregion ENDOF: Fields

        #region Constructors

        protected QuickSorter() { }

        internal QuickSorter(T[] data, IComparer<T> comparer)
        {
            mData = data;
            mComparer = comparer;
        }

        #endregion ENDOF: Constructors

        #region Methods

        internal virtual void Sort() { QuickSort(0, (mData.Length - 1)); }

        protected void QuickSort(int left, int right)
        {
            do
            {
                int currentLeft = left;
                int currentRight = right;
                T pivotValue = mData[currentLeft + ((currentRight - currentLeft) / 2)]; // # Selecting value in the middle of array as pivot

                do
                {
                    if (currentLeft < mData.Length && mComparer.Compare(mData[currentLeft], pivotValue) < 0) { currentLeft++; } // # Left cursor
                    else
                    {
                        while (currentRight >= 0 && mComparer.Compare(mData[currentRight], pivotValue) > 0) { currentRight--; } // # Right cursor

                        if (currentLeft > currentRight) { break; } // # Partitioning point
                        if (currentLeft < currentRight) { Swap(currentLeft, currentRight); }
                        
                        currentLeft++;
                        currentRight--;
                    }
                } while (currentLeft <= currentRight);

                if (currentRight - left <= right - currentLeft)
                {
                    if (left < currentRight) { QuickSort(left, currentRight); } // # Partition: Left
                    left = currentLeft;
                }
                else
                {
                    if (currentLeft < right) { QuickSort(currentLeft, right); } // # Partition: Right
                    right = currentRight;
                }
            }
            while (left < right);
        }

        private void Swap(int firstIndex, int secondIndex)
        {
            T temp = mData[firstIndex];
            mData[firstIndex] = mData[secondIndex];
            mData[secondIndex] = temp;
        }

        #endregion ENDOF: Methods
    }
}
