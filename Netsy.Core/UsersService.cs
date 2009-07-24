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
    /// todo: now there are two service calls, Many more to come. 
    /// todo: They are variations on a theme, so refactor common stuff out. 
    /// todo: use template methods and lambdas?
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
                this.SendUserDetailsResult(null, new ResultStatus("No Api key", null));
                return null;
            }

            string url = Constants.BaseUrl + "users/" + userId + 
                "?api_key=" + this.ApiKey + 
                "&detail_level=" + detailLevel.ToString().ToLower(CultureInfo.InvariantCulture);
            Uri uri = new Uri(url);

            WebRequest request = WebRequest.Create(uri);

            var completed = ServiceHelper.RequestCompletedCallback(
                s =>
                {
                    Users users = s.Deserialize<Users>();
                    this.SendUserDetailsResult(users, new ResultStatus(true));
                });

            IAsyncResult result = request.BeginGetResponse(completed, request);

            return result;
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
                this.SendUsersByNameResult(null, new ResultStatus("No Api key", null));            
                return null;
            }

            string url = Constants.BaseUrl +
                "users/keywords/" + searchName +
                "?api_key=" + this.ApiKey +
                "&offset=" + offset +
                "&limit=" + limit +
                "&detail_level=" + detailLevel.ToString().ToLower(CultureInfo.InvariantCulture);
            Uri uri = new Uri(url);

            WebRequest request = WebRequest.Create(uri);

            var completed = ServiceHelper.RequestCompletedCallback(
                    s =>
                    {
                        Users users = s.Deserialize<Users>();
                        this.SendUsersByNameResult(users, new ResultStatus(true));
                    });

            IAsyncResult result = request.BeginGetResponse(completed, request);

            return result;
        }

        #endregion

        /// <summary>
        /// Send the result message
        /// </summary>
        /// <param name="users">the users read</param>
        /// <param name="status">the status of the call</param>
        private void SendUserDetailsResult(Users users, ResultStatus status)
        {
          if (this.GetUserDetailsCompleted != null)
          {
              this.GetUserDetailsCompleted(this, new ResultEventArgs<Users>(users, status));
          }
        }

        /// <summary>
        /// Send the result message
        /// </summary>
        /// <param name="users">the users read</param>
        /// <param name="status">the status of the call</param>
        private void SendUsersByNameResult(Users users, ResultStatus status)
        {
            if (this.GetUserByNameCompleted != null)
            {
                this.GetUserByNameCompleted(this, new ResultEventArgs<Users>(users, status));
            }
        }
    }
}
