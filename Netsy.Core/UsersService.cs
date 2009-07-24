//-----------------------------------------------------------------------
// <copyright file="UsersService.cs" company="AFS">
// Copyright (c) AFS. All rights reserved.
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.Core
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Net;

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
        /// the API to use for authentication
        /// </summary>
        private readonly string ApiKey;

        /// <summary>
        /// Initializes a new instance of the UsersService class
        /// </summary>
        /// <param name="apiKey">the API key to use</param>
        public UsersService(string apiKey)
        {
            this.ApiKey = apiKey;
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
            if (string.IsNullOrEmpty(this.ApiKey))
            {
                ResultEventArgs<Users> errorResult = new ResultEventArgs<Users>(null, new ResultStatus("No Api key", null));
                ServiceHelper.TestSendEvent(this.GetUserDetailsCompleted, this, errorResult);
                return null;
            }

            string url = Constants.BaseUrl + "users/" + userId + 
                "?api_key=" + this.ApiKey +
                "&detail_level=" + detailLevel.ToStringLower();

            return ServiceHelper.GenerateRequest(new Uri(url), 
                s =>
                {
                    Users users = s.Deserialize<Users>();
                    ResultEventArgs<Users> sucessResult = new ResultEventArgs<Users>(users, new ResultStatus(true));
                    ServiceHelper.TestSendEvent(this.GetUserDetailsCompleted, this, sucessResult);
                });
            
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
            if (string.IsNullOrEmpty(this.ApiKey))
            {
                ResultEventArgs<Users> errorResult = new ResultEventArgs<Users>(null, new ResultStatus("No Api key", null));
                ServiceHelper.TestSendEvent(this.GetUserByNameCompleted, this, errorResult);
                return null;
            }

            string url = Constants.BaseUrl +
                "users/keywords/" + searchName +
                "?api_key=" + this.ApiKey +
                "&offset=" + offset +
                "&limit=" + limit +
                "&detail_level=" + detailLevel.ToStringLower();

            return ServiceHelper.GenerateRequest(new Uri(url),
                    s =>
                    {
                        Users users = s.Deserialize<Users>();
                        ResultEventArgs<Users> sucessResult = new ResultEventArgs<Users>(users, new ResultStatus(true));
                        ServiceHelper.TestSendEvent(this.GetUserByNameCompleted, this, sucessResult);
                    });            
        }

        #endregion
    }
}
