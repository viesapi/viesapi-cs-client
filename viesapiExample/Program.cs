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
using System.Collections.Generic;
using System.Threading;

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

                string eu_vat = "PL7171642051";

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
				VIESData vies = viesapi.GetVIESData(eu_vat);

                if (vies != null)
                {
                    Console.WriteLine(vies);
                }
                else
                {
					Console.WriteLine("Error: " + viesapi.LastError + " (code: " + viesapi.LastErrorCode + ")");
				}

                // Get VIES data with parsed trader address from VIES system
                VIESData vies_parsed = viesapi.GetVIESDataParsed(eu_vat);

                if (vies_parsed != null)
                {
                    Console.WriteLine(vies_parsed);
                }
                else
                {
                    Console.WriteLine("Error: " + viesapi.LastError + " (code: " + viesapi.LastErrorCode + ")");
                }

                // Upload batch of VAT numbers and get their current VAT statuses and traders data
                List<string> numbers = new List<string>
                {
                    eu_vat,
                    "DK56314210",
                    "CZ7710043187"
                };

                string token = viesapi.GetVIESDataAsync(numbers);

                if (token != null)
                {
                    Console.WriteLine("Batch token: " + token);
                }
                else
                {
                    Console.WriteLine("Error: " + viesapi.LastError + " (code: " + viesapi.LastErrorCode + ")");
                    return;
                }

                // Check batch result and download data (at production it usually takes 2-3 min for result to be ready)
                BatchResult result;

                while ((result = viesapi.GetVIESDataAsyncResult(token)) == null)
                {
                    if (viesapi.LastErrorCode != Error.BATCH_PROCESSING)
                    {
                        Console.WriteLine("Error: " + viesapi.LastError + " (code: " + viesapi.LastErrorCode + ")");
                        return;
                    }

                    Console.WriteLine("Batch is still processing, waiting...");
                    Thread.Sleep(10000);
                }

                // Batch result is ready
                Console.WriteLine(result);
            }
            catch (Exception e)
			{
				System.Diagnostics.Debug.WriteLine(e.StackTrace);
			}
		}
	}
}
