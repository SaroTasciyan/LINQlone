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

namespace LINQlone.Test.DataObjects
{
    public class ListData<T> : Data<T>, IList<T>
    {
        #region Properties

        private IList<T> ListSource
        {
            get { return (IList<T>)Source; }
        }

        #endregion ENDOF: Properties

        #region Constructors

        public ListData(IList<T> source) : base(source) 
        {
        }

        #endregion ENDOF: Constructors

        #region IList<T> Members

        public int IndexOf(T item)
        {
            return ListSource.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            ListSource.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            ListSource.RemoveAt(index);
        }

        public T this[int index]
        {
            get { return ListSource[index]; }
            set { ListSource[index] = value; }
        }

        #endregion

        #region ICollection<T> Members

        public void Add(T item)
        {
            ListSource.Add(item);
        }

        public void Clear()
        {
            ListSource.Clear();
        }

        public bool Contains(T item)
        {
            return ListSource.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            ListSource.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return ListSource.Count; }
        }

        public bool IsReadOnly
        {
            get { return ListSource.IsReadOnly; }
        }

        public bool Remove(T item)
        {
            return ListSource.Remove(item);
        }

        #endregion
    }
}
