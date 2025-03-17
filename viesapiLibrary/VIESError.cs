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
	/// Interface for VIES error
	/// </summary>
	[Guid("39E4E016-5BAB-4786-99B2-F3712A9EF972")]
	[ComVisible(true)]
	public interface IVIESError
    {
		/// <summary>
		/// Unique response ID
		/// </summary>
		[DispId(1)]
		string UID { get; set; }

		/// <summary>
		/// Country code (2-letters)
		/// </summary>
		[DispId(2)]
		string CountryCode { get; set; }

		/// <summary>
		/// VAT number
		/// </summary>
		[DispId(3)]
		string VATNumber { get; set; }

		/// <summary>
		/// Error description
		/// </summary>
		[DispId(4)]
		string Error { get; set; }

        /// <summary>
        /// Check date time
        /// </summary>
        [DispId(5)]
        DateTime Date { get; set; }

        /// <summary>
        /// The source of returned information
        /// </summary>
        [DispId(6)]
        string Source { get; set; }

        [DispId(7)]
		string ToString();
    }

	#endregion

	#region implementation

	/// <summary>
	/// VIES error
	/// </summary>
	[Guid("93F81582-367B-4086-B60F-10EDFD27B007")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComVisible(true)]
	public class VIESError : IVIESError
	{
		/// <summary>
		/// Unique response ID
		/// </summary>
		public string UID { get; set; }

		/// <summary>
		/// Country code (2-letters)
		/// </summary>
		public string CountryCode { get; set; }

		/// <summary>
		/// VAT number
		/// </summary>
		public string VATNumber { get; set; }

		/// <summary>
		/// Error description
		/// </summary>
		public string Error { get; set; }

		/// <summary>
		/// Check date time
		/// </summary>
		[ComVisible(false)]
        public DateTime? Date { get; set; }

		/// <summary>
		/// Check date time
		/// </summary>
		DateTime IVIESError.Date
        {
            get { return Date ?? DateTime.MinValue; }
            set { Date = value; }
        }

		/// <summary>
		/// The source of returned information
		/// </summary>
		public string Source { get; set; }

        /// <summary>
        /// Create new object
        /// </summary>
        public VIESError()
		{
		}

		public override string ToString()
		{
			return "VIESError: [UID = " + UID
				+ ", CountryCode = " + CountryCode
				+ ", VATNumber = " + VATNumber
				+ ", Error = " + Error
                + ", Date = " + Date
                + ", Source = " + Source
                + "]";
		}
	}

	#endregion
}
