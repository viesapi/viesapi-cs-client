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
    [Guid("4FDD229E-FF0E-4A78-9C82-F7060385E487")]
    [ComVisible(true)]
    public interface IAddressComponents
    {
        /// <summary>
        /// Country name
        /// </summary>
        [DispId(1)]
        string Country { get; set; }

        /// <summary>
        /// Postal code
        /// </summary>
        [DispId(2)]
        string PostalCode { get; set; }

        /// <summary>
        /// City or locality
        /// </summary>
        [DispId(3)]
        string City { get; set; }

        /// <summary>
        /// Street name
        /// </summary>
        [DispId(4)]
        string Street { get; set; }

        /// <summary>
        /// Street number
        /// </summary>
        [DispId(5)]
        string StreetNumber { get; set; }

        /// <summary>
        /// House number
        /// </summary>
        [DispId(6)]
        string HouseNumber { get; set; }

        [DispId(7)]
        string ToString();
    }

    #endregion

    #region implementation

    /// <summary>
    /// VIES data
    /// </summary>
    [Guid("CFA66CC9-C327-4796-81EA-DA98FDA68888")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComVisible(true)]
    public class AddressComponents : IAddressComponents
    {
        /// <summary>
        /// Country name
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Postal code
        /// </summary>
        public string PostalCode { get; set; }

        /// <summary>
        /// City or locality
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Street name
        /// </summary>
        public string Street { get; set; }

        /// <summary>
        /// Street number
        /// </summary>
        public string StreetNumber { get; set; }

        /// <summary>
        /// House number
        /// </summary>
        public string HouseNumber { get; set; }

        /// <summary>
        /// Create new object
        /// </summary>
        public AddressComponents()
        {
        }

        public override string ToString()
        {
            return "AddressComponents: [Country = " + Country
                + ", PostalCode = " + PostalCode
                + ", City = " + City
                + ", Street = " + Street
                + ", StreetNumber = " + StreetNumber
                + ", HouseNumber = " + HouseNumber
                + "]";
        }
    }

    #endregion
}
