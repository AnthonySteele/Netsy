//-----------------------------------------------------------------------
// <copyright file="IUsersService.cs" company="AFS">
// Copyright (c) AFS. All rights reserved.
// </copyright>
//----------------------------------------------------------------------- 
namespace Netsy.Interfaces
{
    using System;

    using Netsy.DataModel;
    using Netsy.DataModel.UserData;
    using Netsy.Helpers;

    /// <summary>
    /// Interface to Etsy Users API
    /// </summary>
    public interface IUsersService
    {
        /// <summary>
        /// User details by id completed event
        /// </summary>
        event EventHandler<ResultEventArgs<Users>> GetUserDetailsCompleted;

        /// <summary>
        /// Users by name completed event
        /// </summary>
        event EventHandler<ResultEventArgs<Users>> GetUserByNameCompleted;

        /// <summary>
        /// Query for user details
        /// </summary>
        /// <param name="userId">the id of the user</param>
        /// <param name="detailLevel">the level of detail</param>
        /// <returns>the async state</returns>
        IAsyncResult GetUserDetails(int userId, DetailLevel detailLevel);

        /// <summary>
        /// Query for users by name
        /// </summary>
        /// <param name="searchName">the name to search for</param>
        /// <param name="offset">the searh results offset</param>
        /// <param name="limit">the search limit</param>
        /// <param name="detailLevel"></param>
        /// <param name="detailLevel">the level of detail</param>
        /// <returns>the async state</returns>
        IAsyncResult GetUsersByName(string searchName, int offset, int limit, DetailLevel detailLevel);
    }
}
