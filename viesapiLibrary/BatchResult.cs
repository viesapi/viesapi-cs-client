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
using System.Runtime.InteropServices;

namespace VIESAPI
{
	#region interface

	/// <summary>
	/// Interface for batch result
	/// </summary>
	[Guid("99C1127F-68B6-4C53-A38E-E6F25C8CE254")]
	[ComVisible(true)]
	public interface IBatchResult
    {
		/// <summary>
		/// Valid VIES results
		/// </summary>
		[DispId(1)]
		VIESData[] Numbers { get; set; }

		/// <summary>
		/// Failed VIES results
		/// </summary>
		[DispId(2)]
		VIESError[] Errors { get; set; }

        [DispId(3)]
		string ToString();
    }

	#endregion

	#region implementation

	/// <summary>
	/// Batch result
	/// </summary>
	[Guid("4722CC8F-B6EB-4B38-9466-6C2B8B2169ED")]
	[ClassInterface(ClassInterfaceType.None)]
	[ComVisible(true)]
	public class BatchResult : IBatchResult
	{
        /// <summary>
        /// Valid VIES results
        /// </summary>
        [ComVisible(false)]
        public List<VIESData> Numbers { get; set; }

        /// <summary>
        /// Valid VIES results
        /// </summary>
        VIESData[] IBatchResult.Numbers
        {
            get { return Numbers.ToArray(); }
            set { Numbers = new List<VIESData>(value); }
        }

        /// <summary>
        /// Failed VIES results
        /// </summary>
        [ComVisible(false)]
        public List<VIESError> Errors { get; set; }

        /// <summary>
        /// Failed VIES results
        /// </summary>
        VIESError[] IBatchResult.Errors
        {
            get { return Errors.ToArray(); }
            set { Errors = new List<VIESError>(value); }
        }

        /// <summary>
        /// Create new object
        /// </summary>
        public BatchResult()
        {
            Numbers = new List<VIESData>();
            Errors = new List<VIESError>();
        }

        public override string ToString()
		{
			return "BatchResult: [Numbers = [" + string.Join(", ", Numbers.ConvertAll(e => Convert.ToString(e)).ToArray()) + "]"
                + ", Errors = [" + string.Join(", ", Errors.ConvertAll(e => Convert.ToString(e)).ToArray()) + "]"
                + "]";
		}
	}

	#endregion
}
