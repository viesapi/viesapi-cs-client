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
    [Guid("2C3B5C75-B9E6-4014-8C99-B24398413CF2")]
    [ComVisible(true)]
    public interface INameComponents
    {
        /// <summary>
        /// Trader name
        /// </summary>
        [DispId(1)]
        string Name { get; set; }

        /// <summary>
        /// Legal form
        /// </summary>
        [DispId(2)]
        string LegalForm { get; set; }

        /// <summary>
        /// Canonical legal form id
        /// </summary>
        [DispId(3)]
        LegalForm LegalFormCanonicalId { get; set; }

        /// <summary>
        /// Canonical legal form name
        /// </summary>
        [DispId(4)]
        string LegalFormCanonicalName { get; set; }

        [DispId(5)]
        string ToString();
    }

    #endregion

    #region implementation

    /// <summary>
    /// VIES data
    /// </summary>
    [Guid("17ABA34B-836F-4AB6-99C5-D48E7BE28D23")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComVisible(true)]
    public class NameComponents : INameComponents
    {
        /// <summary>
        /// Trader name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Legal form
        /// </summary>
        public string LegalForm { get; set; }

        /// <summary>
        /// Canonical legal form id
        /// </summary>
        public LegalForm LegalFormCanonicalId { get; set; }

        /// <summary>
        /// Canonical legal form name
        /// </summary>
        public string LegalFormCanonicalName { get; set; }

        /// <summary>
        /// Create new object
        /// </summary>
        public NameComponents()
        {
        }

        public override string ToString()
        {
            return "NameComponents: [Name = " + Name
                + ", LegalForm = " + LegalForm
                + ", LegalFormCanonicalId = " + LegalFormCanonicalId
                + ", LegalFormCanonicalName = " + LegalFormCanonicalName
                + "]";
        }
    }

    #endregion
}
