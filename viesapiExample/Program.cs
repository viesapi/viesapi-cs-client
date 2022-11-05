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

namespace VIESAPI
{
	/// <summary>
	/// Example program
	/// </summary>
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				// Create client object and establish connection to the production system
				// id – API identifier
				// key – API key (keep it secret)
				// VIESAPIClient viesapi = new VIESAPIClient("id", "key");

				// Create client object and establish connection to the test system
				VIESAPIClient viesapi = new VIESAPIClient();

				string nip = "7171642051";
                string nip_eu = "PL" + nip;

				// Get current account status
				AccountStatus account = viesapi.GetAccountStatus();

				if (account != null)
				{
					Console.WriteLine(account);
				}
				else
				{
					Console.WriteLine("Error: " + viesapi.LastError + " (code: " + viesapi.LastErrorCode + ")");
				}

				// Get VIES data from VIES system
				VIESData vies = viesapi.GetVIESData(nip_eu);

                if (vies != null)
                {
                    Console.WriteLine(vies);
                }
                else
                {
					Console.WriteLine("Error: " + viesapi.LastError + " (code: " + viesapi.LastErrorCode + ")");
				}
			}
			catch (Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.StackTrace);
			}
		}
	}
}
