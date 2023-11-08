/**
 * Copyright 2022-2023 NETCAT (www.netcat.pl)
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
 * @copyright 2022-2023 NETCAT (www.netcat.pl)
 * @license http://www.apache.org/licenses/LICENSE-2.0
 */

using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace VIESAPI
{
    #region implementation

    /// <summary>
    /// Error codes
    /// </summary>
	[Guid("05A7B300-136E-45B2-9DCD-8626E638C7AD")]
    [ClassInterface(ClassInterfaceType.None)]
    [ComVisible(false)]
    public class Error
    {
        public const int NIP_EMPTY             = 1;
        public const int NIP_UNKNOWN           = 2;
        public const int GUS_LOGIN             = 3;
        public const int GUS_CAPTCHA           = 4;
        public const int GUS_SYNC              = 5;
        public const int NIP_UPDATE            = 6;
        public const int NIP_BAD               = 7;
        public const int CONTENT_SYNTAX        = 8;
        public const int NIP_NOT_ACTIVE        = 9;
        public const int INVALID_PATH          = 10;
        public const int EXCEPTION             = 11;
        public const int NO_PERMISSION         = 12;
        public const int GEN_INVOICES          = 13;
        public const int GEN_SPEC_INV          = 14;
        public const int SEND_INVOICE          = 15;
        public const int PREMIUM_FEATURE       = 16;
        public const int SEND_ANNOUNCEMENT     = 17;
        public const int INVOICE_PAYMENT       = 18;
        public const int REGON_BAD             = 19;
        public const int SEARCH_KEY_EMPTY      = 20;
        public const int KRS_BAD               = 21;
        public const int EUVAT_BAD             = 22;
        public const int VIES_SYNC             = 23;
        public const int CEIDG_SYNC            = 24;
        public const int RANDOM_NUMBER         = 25;
        public const int PLAN_FEATURE          = 26;
        public const int SEARCH_TYPE           = 27;
        public const int PPUMF_SYNC            = 28;
        public const int PPUMF_DIRECT          = 29;
        public const int NIP_FEATURE           = 30;
        public const int REGON_FEATURE         = 31;
        public const int KRS_FEATURE           = 32;
        public const int TEST_MODE             = 33;
        public const int ACTIVITY_CHECK        = 34;
        public const int ACCESS_DENIED         = 35;
        public const int MAINTENANCE           = 36;
        public const int BILLING_PLANS         = 37;
        public const int DOCUMENT_PDF          = 38;
        public const int EXPORT_PDF            = 39;
        public const int RANDOM_TYPE           = 40;
        public const int LEGAL_FORM            = 41;
        public const int GROUP_CHECKS          = 42;
        public const int CLIENT_COUNTERS       = 43;
        public const int URE_SYNC              = 44;
        public const int URE_DATA              = 45;
        public const int DKN_BAD               = 46;
        public const int SEND_REMAINDER        = 47;
        public const int EXPORT_JPK            = 48;
        public const int GEN_ORDER_INV         = 49;
        public const int SEND_EXPIRATION       = 50;
        public const int IBAN_SYNC             = 51;
        public const int ORDER_CANCEL          = 52;
        public const int WHITELIST_CHECK       = 53;
        public const int AUTH_TIMESTAMP        = 54;
        public const int AUTH_MAC              = 55;
        public const int IBAN_BAD              = 56;

        public const int DB_AUTH_IP            = 101;
        public const int DB_AUTH_KEY_STATUS    = 102;
        public const int DB_AUTH_KEY_VALUE     = 103;
        public const int DB_AUTH_OVER_PLAN     = 104;
        public const int DB_CLIENT_LOCKED      = 105;
        public const int DB_CLIENT_TYPE        = 106;
        public const int DB_CLIENT_NOT_PAID    = 107;
        public const int DB_AUTH_KEYID_VALUE   = 108;

        public const int CLI_CONNECT           = 201;
        public const int CLI_RESPONSE          = 202;
        public const int CLI_NUMBER            = 203;
        public const int CLI_NIP               = 204;
        public const int CLI_EUVAT             = 205;
        public const int CLI_EXCEPTION         = 206;
        public const int CLI_DATEFORMAT        = 207;
		public const int CLI_INPUT             = 208;

        private static readonly Dictionary<int, string> Codes = new Dictionary<int, string> {
            { CLI_CONNECT,     "Failed to connect to the VIES API service" },
            { CLI_RESPONSE,    "VIES API service response has invalid format" },
            { CLI_NUMBER,      "Invalid number type" },
            { CLI_NIP,         "NIP is invalid" },
            { CLI_EUVAT,       "EU VAT ID is invalid" },
            { CLI_EXCEPTION,   "Function generated an exception" },
            { CLI_DATEFORMAT,  "Date has an invalid format" },
			{ CLI_INPUT,       "Invalid input parameter" }
        };

        /// <summary>
        /// Get error message
        /// </summary>
        /// <param name="code">error code</param>
        /// <returns>error message</returns>
        public static string Message(int code)
        {
            if (code < CLI_CONNECT || code > CLI_INPUT)
            {
                return null;
            }

            return Codes[code];
        }
	}

	#endregion
}
