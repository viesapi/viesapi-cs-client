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
	/// Interface for VIES data
	/// </summary>
	[Guid("0778A12B-8191-4251-8951-497531C97959")]
	[ComVisible(true)]
	public interface IVIESData
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
		/// Activity flag
		/// </summary>
		[DispId(4)]
		bool Valid { get; set; }

		/// <summary>
		/// Trader name
		/// </summary>
		[DispId(5)]
		string TraderName { get; set; }

        /// <summary>
        /// Trader name components
        /// </summary>
        [DispId(6)]
        NameComponents TraderNameComponents { get; set; }

        /// <summary>
        /// Trader company type
        /// </summary>
        [DispId(7)]
		string TraderCompanyType { get; set; }

		/// <summary>
		/// Trader address
		/// </summary>
		[DispId(8)]
		string TraderAddress { get; set; }

        /// <summary>
        /// Trader address components
        /// </summary>
        [DispId(9)]
        AddressComponents TraderAddressComponents { get; set; }

        /// <summary>
        /// Request ID from EU VIES system
        /// </summary>
        [DispId(10)]
        string ID { get; set; }

        /// <summary>
        /// Check date time
        /// </summary>
        [DispId(11)]
        DateTime Date { get; set; }

        /// <summary>
        /// The source of returned information
        /// </summary>
        [DispId(12)]
        string Source { get; set; }

        [DispId(13)]
		string ToString();
    }

	#endregion

	#region implementation

	/// <summary>
	/// VIES data
	/// </summary>
	[Guid("56C0179B-F116-4AE5-AC32-8F2A1EB38942")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComVisible(true)]
	public class VIESData : IVIESData
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
		/// Activity flag
		/// </summary>
		public bool Valid { get; set; }

		/// <summary>
		/// Trader name
		/// </summary>
		public string TraderName { get; set; }

        /// <summary>
        /// Trader name components
        /// </summary>
        public NameComponents TraderNameComponents { get; set; }
        
		/// <summary>
        /// Trader company type
        /// </summary>
        public string TraderCompanyType { get; set; }

		/// <summary>
		/// Trader address
		/// </summary>
		public string TraderAddress { get; set; }

        /// <summary>
        /// Trader address components
        /// </summary>
        public AddressComponents TraderAddressComponents { get; set; }
        
		/// <summary>
        /// Request ID from EU VIES system
        /// </summary>
        public string ID { get; set; }

		/// <summary>
		/// Check date time
		/// </summary>
		[ComVisible(false)]
        public DateTime? Date { get; set; }

		/// <summary>
		/// Check date time
		/// </summary>
		DateTime IVIESData.Date
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
        public VIESData()
		{
		}

		public override string ToString()
		{
			return "VIESData: [UID = " + UID
				+ ", CountryCode = " + CountryCode
				+ ", VATNumber = " + VATNumber
				+ ", Valid = " + Valid
				+ ", TraderName = " + TraderName
                + ", TraderNameComponents = " + TraderNameComponents
                + ", TraderCompanyType = " + TraderCompanyType
				+ ", TraderAddress = " + TraderAddress
				+ ", TraderAddressComponents = " + TraderAddressComponents
                + ", ID = " + ID
                + ", Date = " + Date
                + ", Source = " + Source
                + "]";
		}
	}

	#endregion
}
