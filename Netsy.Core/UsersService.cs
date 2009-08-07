//-----------------------------------------------------------------------
// <copyright file="UsersService.cs" company="AFS">
// Copyright (c) AFS. All rights reserved.
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.Core
{
    using System;

    using Netsy.DataModel;
    using Netsy.DataModel.UserData;
    using Netsy.Helpers;
    using Netsy.Interfaces;

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
        /// Initializes a new instance of the UsersService class
        /// </summary>
        /// <param name="etsyContext">the etsy context to use</param>
        public UsersService(EtsyContext etsyContext)
        {
            this.etsyContext = etsyContext;
        }

        #region IEtsyUsers Members

        /// <summary>
        /// Event handler for when GetUserDetails completes
        /// </summary>
        public event EventHandler<ResultEventArgs<Users>> GetUserDetailsCompleted;

        /// <summary>
        /// Event handler for when GetUserByName completes
        /// </summary>
        public event EventHandler<ResultEventArgs<Users>> GetUserByNameCompleted;

        /// <summary>
        /// Get the user details
        /// </summary>
        /// <param name="userId">the user id</param>
        /// <param name="detailLevel">the detail level</param>
        /// <returns>the async state</returns>
        public IAsyncResult GetUserDetails(int userId, DetailLevel detailLevel)
        {
            if (!ServiceHelper.TestCallPrerequisites(this, this.GetUserDetailsCompleted, this.etsyContext))
            {
                return null;
            }

            string url = this.etsyContext.BaseUrl + "users/" + userId + 
                "?api_key=" + this.etsyContext.ApiKey +
                "&detail_level=" + detailLevel.ToStringLower();

            return ServiceHelper.GenerateRequest(this, new Uri(url), this.GetUserDetailsCompleted);
        }

        /// <summary>
        /// Query for users by name
        /// </summary>
        /// <param name="searchName">the name to search for</param>
        /// <param name="offset">the searh results offset</param>
        /// <param name="limit">the search limit</param>
        /// <param name="detailLevel"></param>
        /// <param name="detailLevel">the level of detail</param>
        /// <returns>the async state</returns>
        public IAsyncResult GetUsersByName(string searchName, int offset, int limit, DetailLevel detailLevel)
        {
            if (!ServiceHelper.TestCallPrerequisites(this, this.GetUserByNameCompleted, this.etsyContext))
            {
                return null;
            }

            string url = this.etsyContext.BaseUrl +
                "users/keywords/" + searchName +
                "?api_key=" + this.etsyContext.ApiKey +
                "&offset=" + offset +
                "&limit=" + limit +
                "&detail_level=" + detailLevel.ToStringLower();

            return ServiceHelper.GenerateRequest(this, new Uri(url), this.GetUserByNameCompleted);
        }

        #endregion
    }
}
