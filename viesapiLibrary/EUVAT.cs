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

using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace VIESAPI
{
	#region interface

	/// <summary>
	/// Interface for EU VAT number verificator
	/// </summary>
	[Guid("F3D1DB81-1F29-48F4-99A6-39FD4ECEAEA2")]
	[ComVisible(true)]
	public interface IEUVAT
	{
		/// <summary>
		/// Normalizes form of the VAT number
		/// </summary>
		/// <param name="number">EU VAT number in any valid format</param>
		/// <returns>normalized VAT number</returns>
		[DispId(1)]
		string Normalize(string number);

		/// <summary>
		/// Checks if specified VAT number is valid
		/// </summary>
		/// <param name="number">input number</param>
		/// <returns>true if number is valid</returns>
		[DispId(2)]
		bool IsValid(string number);
	}

	#endregion

	#region implementation

	/// <summary>
	/// EU VAT number verificator
	/// </summary>
	[Guid("2727032C-61B0-4642-94CA-F6D3DF5DF2EB")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComVisible(true)]
	public class EUVAT : IEUVAT
    {
		private static Dictionary<string, string> map = new Dictionary<string, string>();

		static EUVAT()
		{
			map.Add("AT", @"ATU\d{8}$");
			map.Add("BE", @"BE0\d{9}$");
			map.Add("BG", @"BG\d{9,10}$");
			map.Add("CY", @"CY\d{8}[A-Z]{1}$");
			map.Add("CZ", @"CZ\d{8,10}$");
			map.Add("DE", @"DE\d{9}$");
			map.Add("DK", @"DK\d{8}$");
			map.Add("EE", @"EE\d{9}$");
			map.Add("EL", @"EL\d{9}$");
			map.Add("ES", @"ES[A-Z0-9]{9}$");
			map.Add("FI", @"FI\d{8}$");
			map.Add("FR", @"FR[A-Z0-9]{2}\d{9}$");
			map.Add("HR", @"HR\d{11}$");
			map.Add("HU", @"HU\d{8}$");
			map.Add("IE", @"IE[A-Z0-9]{8,9}$");
			map.Add("IT", @"IT\d{11}$");
			map.Add("LT", @"LT\d{9,12}$");
			map.Add("LU", @"LU\d{8}$");
			map.Add("LV", @"LV\d{11}$");
			map.Add("MT", @"MT\d{8}$");
			map.Add("NL", @"NL\d{9}B\d{2}$");
			map.Add("PL", @"PL\d{10}$");
			map.Add("PT", @"PT\d{9}$");
			map.Add("RO", @"RO\d{2,10}$");
			map.Add("SE", @"SE\d{12}$");
			map.Add("SI", @"SI\d{8}$");
			map.Add("SK", @"SK\d{10}$");
			map.Add("XI", @"XI[A-Z0-9]{5,12}$");
		}

		/// <summary>
		/// Normalizes form of the VAT number
		/// </summary>
		/// <param name="number">EU VAT number in any valid format</param>
		/// <returns>normalized VAT number</returns>
		[ComVisible(false)]
		public static string Normalize(string number)
        {
            if (number == null || number.Length == 0)
            {
                return null;
            }

            number = number.Replace("-", "");
            number = number.Replace(" ", "");
			number = number.Trim().ToUpper();

			Regex re = new Regex(@"^[A-Z]{2}[A-Z0-9]{2,12}$");

			if (!re.IsMatch(number))
			{
				return null;
			}

			return number;
        }

		/// <summary>
		/// Normalizes form of the VAT number
		/// </summary>
		/// <param name="number">EU VAT number in any valid format</param>
		/// <returns>normalized VAT number</returns>
		string IEUVAT.Normalize(string number)
		{
			return Normalize(number);
		}

		/// <summary>
		/// Checks if specified VAT number is valid
		/// </summary>
		/// <param name="number">input number</param>
		/// <returns>true if number is valid</returns>
		[ComVisible(false)]
		public static bool IsValid(string number)
        {
            if ((number = Normalize(number)) == null)
            {
                return false;
            }

            string cc = number.Substring(0, 2).ToUpper();
            string num = number.Substring(2).ToUpper();

			if (!map.ContainsKey(cc))
			{
				return false;
			}

			Regex re = new Regex(map[cc]);

			if (!re.IsMatch(number))
			{
				return false;
			}

			if (cc.Equals("PL"))
            {
                return NIP.IsValid(num);
            }

            return true;
		}

		/// <summary>
		/// Checks if specified VAT number is valid
		/// </summary>
		/// <param name="number">input number</param>
		/// <returns>true if number is valid</returns>
		bool IEUVAT.IsValid(string number)
		{
			return IsValid(number);
		}
	}

	#endregion
}
