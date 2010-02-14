//-----------------------------------------------------------------------
// <copyright file="UsersService.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.Services
{
    using System;

    using Netsy.Cache;
    using Netsy.DataModel;
    using Netsy.Helpers;
    using Netsy.Interfaces;

    using Requests;

    /// <summary>
    /// Implementation of Etsy users service calls API 
    /// </summary>
    public class UsersService : IUsersService
    {
        /// <summary>
        /// the Etsy context data
        /// </summary>
        private readonly EtsyContext etsyContext;

        /// <summary>
        /// The data retriever
        /// </summary>
        private readonly IDataRetriever dataRetriever;

        /// <summary>
        /// Initializes a new instance of the UsersService class
        /// </summary>
        /// <param name="etsyContext">the etsy context to use</param>
        /// <param name="dataRetriever">the data retreiver to use</param>
        public UsersService(EtsyContext etsyContext, IDataRetriever dataRetriever)
        {
            this.etsyContext = etsyContext;
            this.dataRetriever = dataRetriever;
        }

        #region IEtsyUsers Members

        /// <summary>
        /// Event handler for when GetUserDetails completes
        /// </summary>
        public event EventHandler<ResultEventArgs<Users>> GetUserDetailsCompleted;

        /// <summary>
        /// Event handler for when GetUserByName completes
        /// </summary>
        public event EventHandler<ResultEventArgs<Users>> GetUsersByNameCompleted;

        /// <summary>
        /// Get the user details
        /// </summary>
        /// <param name="userId">the user id</param>
        /// <param name="detailLevel">the detail level</param>
        /// <returns>the async state</returns>
        public IAsyncResult GetUserDetails(int userId, DetailLevel detailLevel)
        {
            if (!RequestHelper.TestCallPrerequisites(this, this.GetUserDetailsCompleted, this.etsyContext))
            {
                return null;
            }

            UriBuilder uriBuilder = UriBuilder.Start(this.etsyContext, "users", userId)
                .DetailLevel(detailLevel);

            return this.dataRetriever.StartRetrieve(uriBuilder.Result(), this.GetUserDetailsCompleted);
        }

        /// <summary>
        /// Query for users by name
        /// </summary>
        /// <param name="searchName">the name to search for</param>
        /// <param name="offset">the searh results offset</param>
        /// <param name="limit">the search limit</param>
        /// <param name="detailLevel">the level of detail</param>
        /// <returns>the async state</returns>
        public IAsyncResult GetUsersByName(string searchName, int offset, int limit, DetailLevel detailLevel)
        {
            if (!RequestHelper.TestCallPrerequisites(this, this.GetUsersByNameCompleted, this.etsyContext))
            {
                return null;
            }

            UriBuilder uriBuilder = UriBuilder.Start(this.etsyContext, "users/keywords", searchName)
                .OffsetLimit(offset, limit)
                .DetailLevel(detailLevel);

            return this.dataRetriever.StartRetrieve(uriBuilder.Result(), this.GetUsersByNameCompleted);
        }

        #endregion
    }
}
