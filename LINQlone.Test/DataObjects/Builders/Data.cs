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
using System.Collections;
using System.Collections.Generic;

using NUnit.Framework;

namespace LINQlone.Test.DataObjects
{
    #region Data

    public abstract class Data // # Non-generic base type is required in order to store data type unaware of generic type argument
    {
        #region Fields

        protected bool mIsEnumerated;
        protected bool mIsDisposed;
        protected int mEnumerationCount;

        #endregion ENDOF: Fields

        #region Constructors

        protected Data() { }

        #endregion ENDOF: Constructors

        #region Properties

        public bool IsEnumerated
        {
            get { return mIsEnumerated; }
        }

        public bool IsDisposed
        {
            get { return mIsDisposed; }
        }

        public int EnumerationCount
        {
            get { return mEnumerationCount; }
        }

        #endregion ENDOF: Properties
    }

    #endregion ENDOF: Data

    #region Data<T>

    public class Data<T> : Data, IEnumerable<T>
    {
        #region DataEnumerator

        private class DataEnumerator : IEnumerator<T>
        {
            #region Delegates

            public delegate void OnDisposed(int enumerationCount);
            public delegate void OnEnumerationEnded();

            #endregion ENDOF: Delegates

            #region Fields

            private IEnumerator<T> mEnumerator;
            private OnDisposed mOnDisposed;
            private OnEnumerationEnded mOnEnumerationEnded;
            
            private int mEnumerationCount;

            #endregion ENDOF: Fields

            #region Properties

            public int EnumerationCount
            {
                get { return mEnumerationCount; }
            }

            #endregion ENDOF: Properties

            #region Constructors

            internal DataEnumerator(IEnumerable<T> source, OnDisposed onDisposed, OnEnumerationEnded onEnumerationEnded)
            {
                if (source == null) { throw new ArgumentNullException("source", "source can not be null!"); }

                mEnumerator = source.GetEnumerator();
                mOnDisposed = onDisposed;
                mOnEnumerationEnded = onEnumerationEnded;
            }

            #endregion ENDOF: Constructors

            #region IEnumerator<T> Members

            public T Current
            {
                get 
                {
                    return mEnumerator.Current;
                }
            }

            #endregion

            #region IDisposable Members

            public void Dispose()
            {
                IEnumerator<T> tempEnumerator = mEnumerator;
                mEnumerator = null;

                if (tempEnumerator != null)
                {
                    tempEnumerator.Dispose();

                    if (mOnEnumerationEnded != null)
                    {
                        mOnEnumerationEnded = null;
                    }

                    if (mOnDisposed != null) 
                    { 
                        mOnDisposed.Invoke(mEnumerationCount);
                        mOnDisposed = null;
                    }
                }
            }

            #endregion

            #region IEnumerator Members

            object IEnumerator.Current
            {
                get { return Current; }
            }

            public bool MoveNext()
            {
                mEnumerationCount++;
                bool hasNext = mEnumerator.MoveNext();

                if (!hasNext && mOnEnumerationEnded != null)
                {
                    mOnEnumerationEnded.Invoke();
                }
                
                return hasNext;
            }

            public void Reset()
            {
                mEnumerator.Reset();
            }

            #endregion
        }

        #endregion ENDOF: DataEnumerator

        #region Fields

        private readonly IEnumerable<T> mSource;

        #endregion ENDOF: Fields

        #region Properties

        protected IEnumerable<T> Source
        {
            get { return mSource; }
        }

        #endregion ENDOF: Properties

        #region Constructors

        public Data(IEnumerable<T> source)
        {
            mSource = source;
        }

        #endregion ENDOF: Constructors

        #region IEnumerable<T> Members

        public IEnumerator<T> GetEnumerator()
        {
            Assert.That(mIsEnumerated, Is.False); // # GetEnumerator() must not be called for the second time
            mIsEnumerated = true;

            return new DataEnumerator(mSource, Disposed, EnumerationEnded);
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Methods

        private void Disposed(int enumerationCount)
        {
            mIsDisposed = true;
            mEnumerationCount = enumerationCount;
        }

        protected virtual void EnumerationEnded()
        {
        }

        #endregion ENDOF: Methods
    }

    #endregion ENDOF: Data<T>
}
