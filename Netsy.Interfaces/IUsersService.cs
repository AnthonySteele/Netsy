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
        /// User details completed event
        /// </summary>
        event EventHandler<EventArgs<Users, ResultStatus>> GetUserDetailsCompleted;
        
        /// <summary>
        /// Query for user details
        /// </summary>
        /// <param name="userId">the id of the user</param>
        /// <param name="detailLevel">the level of detail</param>
        /// <returns>the async state</returns>
        IAsyncResult GetUserDetails(int userId, DetailLevel detailLevel);
    }
}
