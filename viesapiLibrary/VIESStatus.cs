/**
 * Copyright 2022-2025 NETCAT (www.netcat.pl)
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 * @author NETCAT <firma@netcat.pl>
 * @copyright 2022-2025 NETCAT (www.netcat.pl)
 * @license http://www.apache.org/licenses/LICENSE-2.0
 */

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace VIESAPI
{
    #region interface

    /// <summary>
    /// Interface for current VIES system status information
    /// </summary>
    [Guid("2FE255A8-8DFD-45B0-8B54-F25BAC3BF4BB")]
	[ComVisible(true)]
	public interface IVIESStatus
    {
		/// <summary>
		/// Unique response ID
		/// </summary>
		[DispId(1)]
		string UID { get; set; }

        /// <summary>
        /// Availability flag
        /// </summary>
        [DispId(2)]
		bool Available { get; set; }

        /// <summary>
        /// List of member countries
        /// </summary>
        [DispId(3)]
        CountryStatus[] Countries { get; set; }

        [DispId(4)]
		string ToString();
    }

    #endregion

    #region implementation

    /// <summary>
    /// Current VIES system status information
    /// </summary>
    [Guid("560EA72B-29DE-4E25-97F5-D01FB49059B1")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComVisible(true)]
	public class VIESStatus : IVIESStatus
    {
		/// <summary>
		/// Unique response ID
		/// </summary>
		public string UID { get; set; }

        /// <summary>
        /// Availability flag
        /// </summary>
        public bool Available { get; set; }

        /// <summary>
        /// List of member countries
        /// </summary>
        [ComVisible(false)]
        public List<CountryStatus> Countries { get; set; }

        /// <summary>
        /// List of member countries
        /// </summary>
        CountryStatus[] IVIESStatus.Countries
        {
            get { return Countries.ToArray(); }
            set { Countries = new List<CountryStatus>(value); }
        }

        /// <summary>
        /// Create new object
        /// </summary>
        public VIESStatus()
		{
            Countries = new List<CountryStatus>();
        }

		public override string ToString()
		{
			return "VIESStatus: [UID = " + UID
				+ ", Available = " + Available
                + ", Countries = [" + string.Join(", ", Countries.ConvertAll(e => Convert.ToString(e)).ToArray()) + "]"
                + "]";
		}
	}

	#endregion
}
