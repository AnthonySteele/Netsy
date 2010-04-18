//-----------------------------------------------------------------------
// <copyright file="AppSettings.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 
namespace Netsy.Shop
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Application settings for the Shop control
    /// </summary>
    internal class AppSettings  
    {
        /// <summary>
        /// Initializes a new instance of the AppSettings class
        /// </summary>
        public AppSettings()
        {
            this.ItemsPerPage = Constants.DefaultItemsPerPage;
        }

        /// <summary>
        /// Gets the items per page 
        /// </summary>
        public int ItemsPerPage { get; private set; }

        /// <summary>
        /// Gets the user to show 
        /// </summary>
        public string UserId { get; private set; }
        
        /// <summary>
        /// Read paramters from the host
        /// </summary>
        /// <param name="initParams">parameters from the host</param>
        public void ReadParams(IDictionary<string, string> initParams)
        {
            this.ReadUserId(initParams);
        }

        /// <summary>
        /// Read the user id from the params
        /// </summary>
        /// <param name="initParams">the params to read from</param>
        private void ReadUserId(IDictionary<string, string> initParams)
        {
            const string UserIdKey = "UserId";

            if (!initParams.ContainsKey(UserIdKey))
            {
                throw new ArgumentException("No User id found in Control params");
            }

            if (initParams.ContainsKey(UserIdKey))
            {
                this.UserId = initParams[UserIdKey];
            }
        }
    }
}