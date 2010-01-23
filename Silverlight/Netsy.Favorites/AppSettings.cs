//-----------------------------------------------------------------------
// <copyright file="AppSettings.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.Favorites
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Settings to read from the commandline
    /// </summary>
    internal class AppSettings
    {
        /// <summary>
        /// Initializes a new instance of the AppSettings class
        /// </summary>
        public AppSettings()
        {
            this.ColumnCount = Constants.DefaultColumnCount;
            this.ItemsPerPage = Constants.DefaultItemsPerPage; 
        }

        /// <summary>
        /// Gets the user to show 
        /// </summary>
        public string UserId { get; private set; }

        /// <summary>
        /// Gets the columns count 
        /// </summary>
        public int ColumnCount { get; private set; }

        /// <summary>
        /// Gets the items per page 
        /// </summary>
        public int ItemsPerPage { get; private set; }

        /// <summary>
        /// Read the commandline parameters
        /// </summary>
        /// <param name="initParams">the control params</param>
        public void ReadParams(IDictionary<string, string> initParams)
        {
            const string UserIdKey = "UserId";
            const string ColumnCountKey = "ColumnCount";
            const string ItemsPerPageKey = "ItemsPerPage";

            // user id is compulsory
            if (!initParams.ContainsKey(UserIdKey))
            {
                throw new ArgumentException("No User id found in Control params");
            }

            this.UserId = initParams[UserIdKey];

            // Column count is optional 
            if (initParams.ContainsKey(ColumnCountKey))
            {
                int columnCountRead;
                if (int.TryParse(initParams[ColumnCountKey], out columnCountRead))
                {
                    this.ColumnCount = columnCountRead;
                }
            }

            // Items per page is optional 
            if (initParams.ContainsKey(ItemsPerPageKey))
            {
                int itemsPerPageRead;
                if (int.TryParse(initParams[ItemsPerPageKey], out itemsPerPageRead))
                {
                    this.ColumnCount = itemsPerPageRead;
                }
            }
        }
    }
}
