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
	/// Interface for account status information
	/// </summary>
	[Guid("4D2AEDB5-6B3D-442A-8CF0-01D64BA39C6A")]
	[ComVisible(true)]
	public interface IAccountStatus
	{
		/// <summary>
		/// Unique response ID
		/// </summary>
		[DispId(1)]
		string UID { get; set; }

		/// <summary>
		/// Account type
		/// </summary>
		[DispId(2)]
		string Type { get; set; }

		/// <summary>
		/// Account validity date (only for pre-paid accounts)
		/// </summary>
		[DispId(3)]
		DateTime ValidTo { get; set; }

		/// <summary>
		/// Billing plan name
		/// </summary>
		[DispId(4)]
		string BillingPlanName { get; set; }

		/// <summary>
		/// Monthly subscription net price
		/// </summary>
		[DispId(5)]
		decimal SubscriptionPrice { get; set; }

		/// <summary>
		/// Single query cost off-plan (only for standard plans)
		/// </summary>
		[DispId(6)]
		decimal ItemPrice { get; set; }

		/// <summary>
		/// Net price of a single query for an individual plan
		/// </summary>
		[DispId(7)]
		decimal ItemPriceStatus { get; set; }

        /// <summary>
        /// Net price of a single query for an individual plan
        /// </summary>
        [DispId(8)]
        decimal ItemPriceParsed { get; set; }
        
		/// <summary>
        /// Maximum number of queries in the plan
        /// </summary>
        [DispId(9)]
		int Limit { get; set; }

		/// <summary>
		/// The minimum time interval between queries
		/// </summary>
		[DispId(10)]
		int RequestDelay { get; set; }

		/// <summary>
		/// Maximum number of domains (API keys)
		/// </summary>
		[DispId(11)]
		int DomainLimit { get; set; }

		/// <summary>
		/// Ability to exceed the maximum number of queries in the plan
		/// </summary>
		[DispId(12)]
		bool OverPlanAllowed { get; set; }

		/// <summary>
		/// Access to MS Excel add-in
		/// </summary>
		[DispId(13)]
		bool ExcelAddIn { get; set; }

		/// <summary>
		/// Access to VIES Checker App application
		/// </summary>
		[DispId(14)]
		bool App { get; set; }

		/// <summary>
		/// Access to VIES Checker CLI/CMD command line application
		/// </summary>
		[DispId(15)]
		bool CLI { get; set; }

		/// <summary>
		/// Access to the statistics of the queries made
		/// </summary>
		[DispId(16)]
		bool Stats { get; set; }

		/// <summary>
		/// Access to monitoring the status of entities
		/// </summary>
		[DispId(17)]
		bool Monitor { get; set; }

		/// <summary>
		/// Access to entity status checking functions in the VIES system
		/// </summary>
		[DispId(18)]
		bool FuncGetVIESData { get; set; }

        /// <summary>
        /// Access to entity status checking functions in the VIES system returning parsed data
        /// </summary>
        [DispId(19)]
        bool FuncGetVIESDataParsed { get; set; }
        
		/// <summary>
        /// Number of queries to the VIES system performed in the current month
        /// </summary>
        [DispId(20)]
		int VIESDataCount { get; set; }

        /// <summary>
        /// Number of queries to the VIES system returning parsed data performed in the current month
        /// </summary>
        [DispId(21)]
        int VIESDataParsedCount { get; set; }
        
		/// <summary>
        /// Total number of queries performed in the current month
        /// </summary>
        [DispId(22)]
		int TotalCount { get; set; }

		[DispId(23)]
		string ToString();
	}

	#endregion

	#region implementation

	/// <summary>
	/// Account status information
	/// </summary>
	[Guid("02B194C5-7008-43C0-ACCF-C760B47E366A")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComVisible(true)]
	public class AccountStatus : IAccountStatus
	{
		/// <summary>
		/// Unique response ID
		/// </summary>
		public string UID { get; set; }

		/// <summary>
		/// Account type
		/// </summary>
		public string Type { get; set; }

		/// <summary>
		/// Account validity date (only for pre-paid accounts)
		/// </summary>
		[ComVisible(false)]
		public DateTime? ValidTo { get; set; }

		/// <summary>
		/// Account validity date (only for pre-paid accounts)
		/// </summary>
		DateTime IAccountStatus.ValidTo
		{
			get { return ValidTo ?? DateTime.MinValue; }
			set { ValidTo = value; }
		}

		/// <summary>
		/// Billing plan name
		/// </summary>
		public string BillingPlanName { get; set; }

		/// <summary>
		/// Monthly subscription net price
		/// </summary>
		public decimal SubscriptionPrice { get; set; }

		/// <summary>
		/// Single query cost off-plan (only for standard plans)
		/// </summary>
		public decimal ItemPrice { get; set; }

		/// <summary>
		/// Net price of a single query for an individual plan
		/// </summary>
		public decimal ItemPriceStatus { get; set; }

        /// <summary>
        /// Net price of a single query for an individual plan
        /// </summary>
        public decimal ItemPriceParsed { get; set; }
        
		/// <summary>
        /// Maximum number of queries in the plan
        /// </summary>
        public int Limit { get; set; }

		/// <summary>
		/// The minimum time interval between queries
		/// </summary>
		public int RequestDelay { get; set; }

		/// <summary>
		/// Maximum number of domains (API keys)
		/// </summary>
		public int DomainLimit { get; set; }

		/// <summary>
		/// Ability to exceed the maximum number of queries in the plan
		/// </summary>
		public bool OverPlanAllowed { get; set; }

		/// <summary>
		/// Access to MS Excel add-in
		/// </summary>
		public bool ExcelAddIn { get; set; }

		/// <summary>
		/// Access to VIES Checker App application
		/// </summary>
		public bool App { get; set; }

		/// <summary>
		/// Access to VIES Checker CLI/CMD command line application
		/// </summary>
		public bool CLI { get; set; }

		/// <summary>
		/// Access to the statistics of the queries made
		/// </summary>
		public bool Stats { get; set; }

		/// <summary>
		/// Access to monitoring the status of entities
		/// </summary>
		public bool Monitor { get; set; }

		/// <summary>
		/// Access to entity status checking functions in the VIES system
		/// </summary>
		public bool FuncGetVIESData { get; set; }

        /// <summary>
        /// Access to entity status checking functions in the VIES system returning parsed data
        /// </summary>
        public bool FuncGetVIESDataParsed { get; set; }
        
		/// <summary>
        /// Number of queries to the VIES system performed in the current month
        /// </summary>
        public int VIESDataCount { get; set; }

        /// <summary>
        /// Number of queries to the VIES system returning parsed data performed in the current month
        /// </summary>
        public int VIESDataParsedCount { get; set; }
        
		/// <summary>
        /// Total number of queries performed in the current month
        /// </summary>
        public int TotalCount { get; set; }

		/// <summary>
		/// Create a new object
		/// </summary>
		public AccountStatus()
		{
		}

		public override string ToString()
        {
            return "AccountStatus: [UID = " + UID
				+ ", Type = " + Type
				+ ", ValidTo = " + ValidTo
				+ ", BillingPlanName = " + BillingPlanName

				+ ", SubscriptionPrice = " + SubscriptionPrice
				+ ", ItemPrice = " + ItemPrice
				+ ", ItemPriceStatus = " + ItemPriceStatus
                + ", ItemPriceParsed = " + ItemPriceParsed

                + ", Limit = " + Limit
				+ ", RequestDelay = " + RequestDelay
				+ ", DomainLimit = " + DomainLimit

				+ ", OverPlanAllowed = " + OverPlanAllowed
				+ ", ExcelAddIn = " + ExcelAddIn
				+ ", App = " + App
				+ ", CLI = " + CLI
				+ ", Stats = " + Stats
				+ ", Monitor = " + Monitor
				
				+ ", FuncGetVIESData = " + FuncGetVIESData
                + ", FuncGetVIESDataParsed = " + FuncGetVIESDataParsed

                + ", VIESDataCount = " + VIESDataCount
                + ", VIESDataParsedCount = " + VIESDataParsedCount
                + ", TotalCount = " + TotalCount
				+ "]";
        }
    }

	#endregion
}