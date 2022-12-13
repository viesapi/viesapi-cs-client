/**
 * Copyright 2022 NETCAT (www.netcat.pl)
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
 * @copyright 2022 NETCAT (www.netcat.pl)
 * @license http://www.apache.org/licenses/LICENSE-2.0
 */

using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using System.Xml.XPath;

namespace VIESAPI
{
	#region interface

	/// <summary>
	/// Interface for VIES API service client
	/// </summary>
	[Guid("106856E7-C1B3-458E-9117-5B0A5A4CC1B8")]
	[ComVisible(true)]
	public interface IVIESAPIClient
	{
		/// <summary>
		/// Get last error code
		/// </summary>
		[DispId(1)]
		int LastErrorCode { get; set; }

		/// <summary>
		/// Get last error message
		/// </summary>
		[DispId(2)]
		string LastError { get; set; }

		/// <summary>
		/// Set non default service URL
		/// </summary>
		[DispId(3)]
		string URL { get; set; }

		/// <summary>
		/// API key identifier
		/// </summary>
		[DispId(4)]
		string Id { get; set; }

		/// <summary>
		/// API key
		/// </summary>
		[DispId(5)]
		string Key { get; set; }

		/// <summary>
		/// Set application info
		/// </summary>
		[DispId(6)]
		string Application { get; set; }

		/// <summary>
		/// HTTP proxy settings
		/// </summary>
		[DispId(7)]
		IWebProxy Proxy { get; set; }

		/// <summary>
		/// Flag enabling the use of legacy SSL/TLS protocols
		/// </summary>
		[DispId(8)]
		bool LegacyProtocolsEnabled { get; set; }

		/// <summary>
		/// Get VIES data for specified number from EU VIES system
		/// </summary>
		/// <param name="euvat">EU VAT number with 2-letter country prefix</param>
		/// <returns>VIES data or null in case of error</returns>
		[DispId(9)]
		VIESData GetVIESData(string euvat);

		/// <summary>
		/// Get current account status
		/// </summary>
		/// <returns>account status or null in case of error</returns>
		[DispId(10)]
        AccountStatus GetAccountStatus();
    }

	#endregion

	#region implementation

	/// <summary>
	/// VIES API service client
	/// </summary>
	[Guid("C315D018-2276-4E3A-ADAC-348C11D5401A")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComVisible(true)]
	public class VIESAPIClient : IVIESAPIClient
	{
		public const string VERSION = "1.2.5";

		public const string PRODUCTION_URL = "https://viesapi.eu/api";
		public const string TEST_URL = "https://viesapi.eu/api-test";

		public const string TEST_ID = "test_id";
		public const string TEST_KEY = "test_key";

		/// <summary>
		/// Get last error code
		/// </summary>
		public int LastErrorCode { get; set; }

		/// <summary>
		/// Get last error message
		/// </summary>
		public string LastError { get; set; }

		/// <summary>
		/// Set non default service URL
		/// </summary>
		public string URL { get; set; }

		/// <summary>
		/// API key identifier
		/// </summary>
		public string Id { get; set; }

		/// <summary>
		/// API key
		/// </summary>
		public string Key { get; set; }

		/// <summary>
		/// Set application info
		/// </summary>
		public string Application { get; set; }

		/// <summary>
		/// HTTP proxy settings
		/// </summary>
		public IWebProxy Proxy { get; set; }

		/// <summary>
		/// Flag enabling the use of legacy SSL/TLS protocols
		/// </summary>
		public bool LegacyProtocolsEnabled { get; set; }

		private RandomNumberGenerator rng;

		/// <summary>
		/// Create new client object
		/// </summary>
		/// <param name="url">VIES API service URL address</param>
		/// <param name="id">API key identifier</param>
		/// <param name="key">API key</param>
		public VIESAPIClient(string url, string id, string key)
		{
			URL = url;
			Id = id;
			Key = key;

			Proxy = WebRequest.GetSystemWebProxy();
			
			Clear();

#if VIESAPI_COM
			LegacyProtocolsEnabled = true;
#else
			LegacyProtocolsEnabled = false;
#endif

			rng = new RNGCryptoServiceProvider();
		}

		/// <summary>
		/// Create new client object
		/// </summary>
		/// <param name="url">VIES API service URL address</param>
		/// <param name="id">API key identifier</param>
		/// <param name="key">API key</param>
		public VIESAPIClient(Uri url, string id, string key) : this(url.ToString(), id, key)
		{
		}

		/// <summary>
		/// Create new client object for production service
		/// </summary>
		/// <param name="id">API key identifier</param>
		/// <param name="key">API key</param>
		public VIESAPIClient(string id, string key) : this(PRODUCTION_URL, id, key)
		{
		}

		/// <summary>
		/// Create new client object for test service
		/// </summary>
		public VIESAPIClient() : this(TEST_URL, TEST_ID, TEST_KEY)
		{
		}

		/// <summary>
		/// Get VIES data for specified number from EU VIES system
		/// </summary>
		/// <param name="euvat">EU VAT number with 2-letter country prefix</param>
		/// <returns>VIES data or null in case of error</returns>
		public VIESData GetVIESData(string euvat)
		{
			try
			{
				// clear error
				Clear();

				// validate number and construct path
				string suffix = null;

				if ((suffix = GetPathSuffix(Number.EUVAT, euvat)) == null)
				{
					return null;
				}

				// prepare url
				Uri url = new Uri(URL + "/get/vies/" + suffix);

				// prepare request
				XPathDocument doc = Get(url);

				if (doc == null)
				{
					Set(Error.CLI_CONNECT);
					return null;
				}

				// parse response
				string code = GetString(doc, "/result/error/code", null);

				if (code != null)
				{
					Set(int.Parse(code), GetString(doc, "/result/error/description", null));
					return null;
				}

				VIESData vies = new VIESData();

				vies.UID = GetString(doc, "/result/vies/uid", null);

				vies.CountryCode = GetString(doc, "/result/vies/countryCode", null);
				vies.VATNumber = GetString(doc, "/result/vies/vatNumber", null);

				vies.Valid = GetString(doc, "/result/vies/valid", "false").Equals("true");

				vies.TraderName = GetString(doc, "/result/vies/traderName", null);
				vies.TraderCompanyType = GetString(doc, "/result/vies/traderCompanyType", null);
				vies.TraderAddress = GetString(doc, "/result/vies/traderAddress", null);

				vies.ID = GetString(doc, "/result/vies/id", null);
				vies.Date = GetDateTime(doc, "/result/vies/date");
				vies.Source = GetString(doc, "/result/vies/source", null);

				return vies;
			}
			catch (Exception e)
			{
				Set(Error.CLI_EXCEPTION, e.Message);
			}

			return null;
		}

		/// <summary>
		/// Get current account status
		/// </summary>
		/// <returns>account status or null in case of error</returns>
		public AccountStatus GetAccountStatus()
		{
			try
			{
				// clear error
				Clear();

				// prepare url
				Uri url = new Uri(URL + "/check/account/status");

				// prepare request
				XPathDocument doc = Get(url);

				if (doc == null)
				{
					Set(Error.CLI_CONNECT);
					return null;
				}

				// parse response
				string code = GetString(doc, "/result/error/code", null);

				if (code != null)
				{
					Set(int.Parse(code), GetString(doc, "/result/error/description", null));
					return null;
				}

				AccountStatus status = new AccountStatus();

				status.UID = GetString(doc, "/result/account/uid", null);
				status.Type = GetString(doc, "/result/account/type", null);
				status.ValidTo = GetDateTime(doc, "/result/account/validTo");
				status.BillingPlanName = GetString(doc, "/result/account/billingPlan/name", null);

				status.SubscriptionPrice = decimal.Parse(GetString(doc, "/result/account/billingPlan/subscriptionPrice", "0"), CultureInfo.InvariantCulture);
				status.ItemPrice = decimal.Parse(GetString(doc, "/result/account/billingPlan/itemPrice", "0"), CultureInfo.InvariantCulture);
				status.ItemPriceStatus = decimal.Parse(GetString(doc, "/result/account/billingPlan/itemPriceCheckStatus", "0"), CultureInfo.InvariantCulture);

				status.Limit = int.Parse(GetString(doc, "/result/account/billingPlan/limit", "0"));
				status.RequestDelay = int.Parse(GetString(doc, "/result/account/billingPlan/requestDelay", "0"));
				status.DomainLimit = int.Parse(GetString(doc, "/result/account/billingPlan/domainLimit", "0"));
				status.OverPlanAllowed = GetString(doc, "/result/account/billingPlan/overplanAllowed", "false").Equals("true");
				status.ExcelAddIn = GetString(doc, "/result/account/billingPlan/excelAddin", "false").Equals("true");
				status.App = GetString(doc, "/result/account/billingPlan/app", "false").Equals("true");
				status.CLI = GetString(doc, "/result/account/billingPlan/cli", "false").Equals("true");
				status.Stats = GetString(doc, "/result/account/billingPlan/stats", "false").Equals("true");
				status.Monitor = GetString(doc, "/result/account/billingPlan/monitor", "false").Equals("true");

				status.FuncGetVIESData = GetString(doc, "/result/account/billingPlan/funcGetVIESData", "false").Equals("true");

				status.VIESDataCount = int.Parse(GetString(doc, "/result/account/requests/viesData", "0"));
				status.TotalCount = int.Parse(GetString(doc, "/result/account/requests/total", "0"));

				return status;
			}
			catch (Exception e)
			{
				Set(Error.CLI_EXCEPTION, e.Message);
			}

			return null;
		}

		/// <summary>
		/// Clear last error
		/// </summary>
		private void Clear()
		{
			LastErrorCode = 0;
			LastError = string.Empty;
		}

		/// <summary>
		/// Set last error information
		/// </summary>
		/// <param name="code">error code</param>
		/// <param name="err">error message</param>
		private void Set(int code, string err = null)
		{
			LastErrorCode = code;
			LastError = (string.IsNullOrEmpty(err) ? Error.Message(code) : err);
		}

		/// <summary>
		/// Perform HTTP GET
		/// </summary>
		/// <param name="url">request URL</param>
		/// <returns>response or null</returns>
		private XPathDocument Get(Uri url)
		{
			XPathDocument doc = null;

			try
			{
				if (!LegacyProtocolsEnabled)
				{
					// SecurityProtocolType:
					// Tls		192
					// Tls11	768
					// Tls12	3072
					// Tls13	12288
					try
					{
						ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072 | (SecurityProtocolType)12288;
					}
					catch (Exception e1)
					{
						// no tls13
						try
						{
							ServicePointManager.SecurityProtocol = (SecurityProtocolType)768 | (SecurityProtocolType)3072;
						}
						catch (Exception e2)
						{
							// no tls12
							try
							{
								ServicePointManager.SecurityProtocol = (SecurityProtocolType)768;
							}
							catch (Exception e3)
							{
								// no tls11
							}
						}
					}
				}

				using (WebClient wc = new WebClient())
				{
					wc.Proxy = Proxy;

					wc.Headers.Set("Authorization", GetAuthHeader("GET", url));
					wc.Headers.Set("User-Agent", GetAgentHeader());

					byte[] b = wc.DownloadData(url);

					doc = new XPathDocument(new MemoryStream(b));
				}
			}
			catch (Exception e)
			{
				Set(Error.CLI_EXCEPTION, e.Message);
			}

			return doc;
		}

		/// <summary>
		/// Create user agent header
		/// </summary>
		/// <returns>user agent information</returns>
		private string GetAgentHeader()
		{
			return (string.IsNullOrEmpty(Application) ? "" : Application + " ") + "VIESAPIClient/" + VERSION + " .NET/" + Environment.Version;
		}

		/// <summary>
		/// Create authorization header
		/// </summary>
		/// <param name="method">HTTP method name</param>
		/// <param name="uri">target URL address</param>
		/// <returns>authorization information</returns>
		private string GetAuthHeader(string method, Uri uri)
		{
			TimeSpan ts = DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc));

			string nonce = GetRandom(8);

			string str = string.Format("{0}\n{1}\n{2}\n{3}\n{4}\n{5}\n\n",
				Convert.ToInt32(ts.TotalSeconds),
				nonce,
				method,
				uri.PathAndQuery,
				uri.Host,
				uri.Port);

			string mac = GetMac(str);

			return string.Format("MAC id=\"{0}\", ts=\"{1}\", nonce=\"{2}\", mac=\"{3}\"", Id, Convert.ToInt32(ts.TotalSeconds), nonce, mac);
		}

		/// <summary>
		/// Get random hex string
		/// </summary>
		/// <param name="length">lenght of string</param>
		/// <returns>random hex string</returns>
		private string GetRandom(int length)
		{
			byte[] b = new byte[length / 2];

			rng.GetBytes(b);

			return BitConverter.ToString(b).Replace("-", "").ToLower();
		}

		/// <summary>
		/// Calculates HMAC256 from input string
		/// </summary>
		/// <param name="str">input string</param>
		/// <returns>HMAC256 as Base64 string or null</returns>
		private string GetMac(string str)
		{
			HashAlgorithm ha = new HMACSHA256(Encoding.ASCII.GetBytes(Key));

			return Convert.ToBase64String(ha.ComputeHash(Encoding.ASCII.GetBytes(str)));
		}

		/// <summary>
		/// Get XML node value as string
		/// </summary>
		/// <param name="doc">XML document</param>
		/// <param name="path">XPath expression selecting the value</param>
		/// <param name="def">default value used when node does not exist</param>
		/// <returns>node value</returns>
		private string GetString(XPathDocument doc, string path, string def)
		{
			try
			{
				XPathNavigator xpn = doc.CreateNavigator();

				string val = xpn.SelectSingleNode(path).Value;

				if (val != null)
				{
					return val;
				}
			}
			catch (Exception)
			{
			}

			return def;
		}

		/// <summary>
		/// Get XML node value as local date time
		/// </summary>
		/// <param name="doc">XML document</param>
		/// <param name="path">XPath expression selecting the value</param>
		/// <returns>local date time or null</returns>
		private DateTime? GetDateTime(XPathDocument doc, string path)
		{
			try
			{
				string val = GetString(doc, path, null);

				if (val != null)
				{
					return XmlConvert.ToDateTime(val, XmlDateTimeSerializationMode.Local);
				}
			}
			catch (Exception)
			{
			}

			return null;
		}

		/// <summary>
		/// Get path suffix
		/// </summary>
		/// <param name="type">number type</param>
		/// <param name="number">number value</param>
		/// <returns>path fragment or null</returns>
		private string GetPathSuffix(Number type, string number)
        {
            string path = "";

            if (type.Equals(Number.NIP))
            {
                if (!NIP.IsValid(number))
                {
					Set(Error.CLI_NIP);
                    return null;
                }

                path = "nip/" + NIP.Normalize(number);
            }
            else if (type.Equals(Number.EUVAT))
            {
                if (!EUVAT.IsValid(number))
                {
					Set(Error.CLI_EUVAT);
					return null;
                }

                path = "euvat/" + EUVAT.Normalize(number);
            }
            else
            {
				Set(Error.CLI_NUMBER);
				return null;
            }

            return path;
        }
    }

	#endregion
}