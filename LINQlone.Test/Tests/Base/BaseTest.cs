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

using NUnit.Framework;

using LINQlone.Test.DataObjects;

namespace LINQlone.Test
{
    //TODO #L! Solution ismi, Proje ismi, Assembly tanımları ve Unit testlerin ismi değişecek!

    [SetUICulture("en-US")] // # Setting UI culture to get stable exception messages (English)
    public class BaseTest
    {
        #region DataEnumerables

        private List<Data> mDataEnumerables;
        private List<Data> DataEnumerables
        {
            get
            {
                if (mDataEnumerables == null) { mDataEnumerables = new List<Data>(); }

                return mDataEnumerables;
            }
        }

        #endregion ENDOF: DataEnumerables

        #region Test Life-Cycle

        [TearDown]
        public void TearDown()
        {
            if (TestContext.CurrentContext.Result.Status != TestStatus.Passed) { return; }

            if (mDataEnumerables != null)
            {
                foreach (Data dataEnumerable in mDataEnumerables)
                {
                    if (dataEnumerable.IsEnumerated && !(dataEnumerable is ThrowingData))
                    {
                        Assert.That(dataEnumerable.IsDisposed, Is.True); // # Enumerator instance must be disposed
                    }
                }
            }

            mDataEnumerables = null;
        }

        #endregion ENDOF: Test Life-Cycle

        #region Data Properties

        protected IEnumerable<object> IntOverflowEnumerable
        {
            get
            {
                for (int i = 0; i < Int32.MaxValue; i++)
                {
                    yield return new object();
                }

                yield return new object(); // # Yielding 32 bit MaxValue + 1 element which will cause overflow when counted
            }
        }

        protected IEnumerable<object> LongOverflowEnumerable
        {
            get
            {
                for (long i = 0; i < Int64.MaxValue; i++)
                {
                    yield return new object();
                }

                yield return new object(); // # Yielding 64 bit MaxValue + 1 element which will cause overflow when counted
            }
        }

        protected Data<object> DummyData
        {
            get { return Data(new object(), String.Empty, 1, 2.5f, 'c'); }
        }

        protected Data<object> EmptyData
        {
            get { return Data<object>(); }
        }

        protected Data<object> NullData
        {
            get { return null; }
        }

        protected ListData<object> DummyListData
        {
            get { return ListData(new object(), String.Empty, 1, 2.5f, 'c'); }
        }

        protected ListData<object> EmptyListData
        {
            get { return ListData<object>(); }
        }

        #endregion ENDOF: Data Properties

        #region Data Builder Methods

        protected Data<T> Data<T>(params T[] source)
        {
            Assert.That(source, Is.Not.Null);

            Data<T> dataEnumerable = new Data<T>(source);
            DataEnumerables.Add((Data)dataEnumerable);

            return dataEnumerable;
        }

        protected ListData<T> ListData<T>(params T[] source)
        {
            Assert.That(source, Is.Not.Null);

            ListData<T> dataEnumerable = new ListData<T>(new List<T>(source));
            DataEnumerables.Add((Data)dataEnumerable);

            return dataEnumerable;
        }

        protected ThrowingData ThrowingData()
        {
            ThrowingData throwingData = new ThrowingData();
            DataEnumerables.Add((Data)throwingData);

            return throwingData;
        }
        
        protected LateThrowingData<object> LateThrowingData()
        {
            object[] data = new object[] { "Late", "Throwing", "Data" };
            LateThrowingData<object> dataEnumerable = new LateThrowingData<object>(data);
            DataEnumerables.Add((Data)dataEnumerable);

            return dataEnumerable;
        }

        #endregion ENDOF: Data Builder Methods
    }
}
