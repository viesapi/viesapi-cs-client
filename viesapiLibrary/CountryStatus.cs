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
using System.Runtime.InteropServices;

namespace VIESAPI
{
    #region interface

    /// <summary>
    /// Interface for EU member country status
    /// </summary>
    [Guid("F118626D-4604-44CE-B373-C1878F2D68B5")]
	[ComVisible(true)]
	public interface ICountryStatus
    {
		/// <summary>
		/// Country code (2-letters)
		/// </summary>
		[DispId(1)]
		string CountryCode { get; set; }

        /// <summary>
        /// Country status
        /// </summary>
        [DispId(2)]
		string Status { get; set; }

        [DispId(3)]
		string ToString();
    }

    #endregion

    #region implementation

    /// <summary>
    /// EU member country status
    /// </summary>
    [Guid("A7324D39-38FF-49B1-9661-7834B52F1076")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComVisible(true)]
	public class CountryStatus : ICountryStatus
    {
        public const string UNKNOWN = "Unknown";
        public const string AVAILABLE = "Available";
        public const string UNAVAILABLE = "Unavailable";
        
        /// <summary>
        /// Country code (2-letters)
        /// </summary>
        public string CountryCode { get; set; }

        /// <summary>
        /// Country status
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Create new object
        /// </summary>
        public CountryStatus()
		{
		}

		public override string ToString()
		{
			return "CountryStatus: [CountryCode = " + CountryCode
                + ", Status = " + Status
                + "]";
		}
	}

	#endregion
}
