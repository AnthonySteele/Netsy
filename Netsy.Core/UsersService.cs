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
    /// Implementation of Etsy users
    /// </summary>
    public class UsersService : IUsersService
    {
        /// <summary>
        /// the API to use for authentication
        /// </summary>
        private readonly string apiKey;

        /// <summary>
        /// Initializes a new instance of the UsersService class
        /// </summary>
        /// <param name="apiKey">the API key to use</param>
        public UsersService(string apiKey)
        {
            this.apiKey = apiKey;
        }

        #region IEtsyUsers Members

        /// <summary>
        /// Event handler for when GetUserDetails completes
        /// </summary>
        public event EventHandler<EventArgs<ResultData<Users>>> GetUserDetailsCompleted;

        /// <summary>
        /// Get the user details
        /// </summary>
        /// <param name="userId">the user id</param>
        /// <param name="detailLevel">the detail level</param>
        /// <returns>the async state</returns>
        public IAsyncResult GetUserDetails(int userId, DetailLevel detailLevel)
        {
            if (string.IsNullOrEmpty(this.apiKey))
            {
                this.SendError("No Api key", null);
                return null;
            }

            string url = Constants.BaseUrl + "users/" + userId + 
                "?api_key=" + this.apiKey + 
                "&detail_level=" + detailLevel.ToString().ToLower(CultureInfo.InvariantCulture);
            Uri uri = new Uri(url);

            WebRequest request = WebRequest.Create(uri);
            IAsyncResult result = request.BeginGetResponse(this.GetUserDetailsCompletedCallback, request);

            return result;
        }

        #endregion

        /// <summary>
        /// GetUserDetails completed Callback
        /// </summary>
        /// <param name="asyncResult">the result of the operation</param>
        private void GetUserDetailsCompletedCallback(IAsyncResult asyncResult)
        {
            WebRequest request = (WebRequest)asyncResult.AsyncState;

            HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(asyncResult);
            Stream responseStream = response.GetResponseStream();
            StreamReader streamReader = new StreamReader(responseStream);

            string resultString = streamReader.ReadToEnd();
            streamReader.Close();
            response.Close();

            Users users = resultString.Deserialize<Users>();

            this.SendResult(new ResultData<Users>
                {
                    Success = true,
                    Data = users
                });
        }

        /// <summary>
        /// Send an error message
        /// </summary>
        /// <param name="errorMessage">the error message</param>
        /// <param name="ex">the exception</param>
        private void SendError(string errorMessage, Exception ex)
        {
            this.SendResult(new ResultData<Users>
            {
                ErrorMessage = errorMessage,
                Exception = ex
            });            
        }

        /// <summary>
        /// Send the result message
        /// </summary>
        /// <param name="result">tjhe result message to send</param>
        private void SendResult(ResultData<Users> result)
        {
          if (this.GetUserDetailsCompleted != null)
          {
              this.GetUserDetailsCompleted(this, new EventArgs<ResultData<Users>>(result));
          }
        }
    }
}
