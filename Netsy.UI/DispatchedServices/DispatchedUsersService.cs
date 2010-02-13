//-----------------------------------------------------------------------
// <copyright file="DispatchedUsersService.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.UI.DispatchedServices
{
    using System;
    using System.Windows.Threading;

    using Netsy.DataModel;
    using Netsy.Helpers;
    using Netsy.Interfaces;

    /// <summary>
    /// User service wrapped to use a dispatcher 
    /// To put the results back on the Dispatcher's thread
    /// </summary>
    public class DispatchedUsersService : DispatchedService, IUsersService
    {
        /// <summary>
        /// The wrapped service
        /// </summary>
        private readonly IUsersService wrappedService;

        /// <summary>
        /// Initializes a new instance of the DispatchedUsersService class
        /// </summary>
        /// <param name="wrappedService">the wrapped service</param>
        /// <param name="dispatcher">the thread dispatcher</param>
        public DispatchedUsersService(IUsersService wrappedService, Dispatcher dispatcher) : base(dispatcher)
        {
            if (wrappedService == null)
            {
                throw new ArgumentNullException("wrappedService");
            }

            this.wrappedService = wrappedService;

            this.wrappedService.GetUserDetailsCompleted += (s, e) => this.DispatchEvent(this.GetUserDetailsCompleted, s, e);
            this.wrappedService.GetUsersByNameCompleted += (s, e) => this.DispatchEvent(this.GetUsersByNameCompleted, s, e);
        }

        /// <summary>
        /// User details by id completed event
        /// </summary>
        public event EventHandler<ResultEventArgs<Users>> GetUserDetailsCompleted;

        /// <summary>
        /// Users by name completed event
        /// </summary>
        public event EventHandler<ResultEventArgs<Users>> GetUsersByNameCompleted;

        /// <summary>
        /// Query for user details
        /// </summary>
        /// <param name="userId">the id of the user</param>
        /// <param name="detailLevel">the level of detail</param>
        /// <returns>the async state</returns>
        public IAsyncResult GetUserDetails(int userId, DetailLevel detailLevel)
        {
            return this.wrappedService.GetUserDetails(userId, detailLevel);
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
            return this.wrappedService.GetUsersByName(searchName, offset, limit, detailLevel);
        }
    }
}
