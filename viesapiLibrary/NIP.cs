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

using System;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace VIESAPI
{
	#region interface

	/// <summary>
	/// Interface for NIP (polish VAT number) verificator
	/// </summary>
	[Guid("012B1958-9F09-47C1-A722-FEEE58653D07")]
	[ComVisible(true)]
	public interface INIP
    {
		/// <summary>
		/// Normalizes form of the NIP number
		/// </summary>
		/// <param name="nip">NIP number in any valid format</param>
		/// <returns>normalized NIP number</returns>
		[DispId(1)]
		string Normalize(string nip);

		/// <summary>
		/// Checks if specified NIP is valid
		/// </summary>
		/// <param name="nip">input number</param>
		/// <returns>true if number is valud</returns>
		[DispId(2)]
		bool IsValid(string nip);
    }

	#endregion

	#region implementation

	/// <summary>
	/// NIP (polish VAT number) verificator
	/// </summary>
	[Guid("64E1F812-102C-407C-97F2-C43D2E687694")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComVisible(true)]
	public class NIP : INIP
	{
		/// <summary>
		/// Normalizes form of the NIP number
		/// </summary>
		/// <param name="nip">NIP number in any valid format</param>
		/// <returns>normalized NIP number</returns>
		[ComVisible(false)]
		public static string Normalize(string nip)
		{
			if (nip == null || nip.Length == 0)
			{
				return null;
			}

			nip = nip.Replace("-", "");
			nip = nip.Trim();

			Regex re = new Regex(@"^[0-9]{10}$");

			if (!re.IsMatch(nip))
			{
				return null;
			}

			return nip;
		}

		/// <summary>
		/// Normalizes form of the NIP number
		/// </summary>
		/// <param name="nip">NIP number in any valid format</param>
		/// <returns>normalized NIP number</returns>
		string INIP.Normalize(string nip)
		{
			return Normalize(nip);
		}

		/// <summary>
		/// Checks if specified NIP is valid
		/// </summary>
		/// <param name="nip">input number</param>
		/// <returns>true if number is valud</returns>
		[ComVisible(false)]
		public static bool IsValid(string nip)
		{
			if ((nip = Normalize(nip)) == null)
			{
				return false;
			}

			int[] w = {
				6, 5, 7, 2, 3, 4, 5, 6, 7
			};

			int sum = 0;

			for (int i = 0; i < w.Length; i++)
			{
				sum += Convert.ToInt32("" + nip[i], 10) * w[i];
			}

			sum %= 11;

			if (sum != Convert.ToInt32("" + nip[9], 10))
			{
				return false;
			}

			return true;
		}

		/// <summary>
		/// Checks if specified NIP is valid
		/// </summary>
		/// <param name="nip">input number</param>
		/// <returns>true if number is valud</returns>
		bool INIP.IsValid(string nip)
		{
			return IsValid(nip);
		}
	}

	#endregion
}
