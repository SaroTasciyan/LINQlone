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

namespace LINQlone.Test.DataObjects
{
    public class Country
    {
        #region Properties

        public int Id { get; set; }
        public string Name { get; set; }
        public string Continent { get; set; }
        public int Population { get; set; }
        public float PercentageOfWorldPopulation { get; set; }

        #endregion ENDOF: Properties

        #region Overriden Methods

        public override string ToString()
        {
            return String.Format("Name: {0}, Continent: {1}, Population: {2}, Percentage: {3}%", Name, Continent, Population, PercentageOfWorldPopulation);
        }

        #endregion ENDOF: Overriden Methods
    }
}
