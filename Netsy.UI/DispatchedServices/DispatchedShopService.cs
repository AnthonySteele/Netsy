//-----------------------------------------------------------------------
// <copyright file="DispatchedShopService.cs" company="AFS">
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
    /// Shop service wrapped to use a dispatcher 
    /// To put the results back on the Dispatcher's thread
    /// </summary>
    public class DispatchedShopService : DispatchedService, IShopService
    {
        /// <summary>
        /// The wrapped service
        /// </summary>
        private readonly IShopService wrappedService;

        /// <summary>
        /// Initializes a new instance of the DispatchedShopService class
        /// </summary>
        /// <param name="wrappedService">the wrapped service</param>
        /// <param name="dispatcher">the thread dispatcher</param>
        public DispatchedShopService(IShopService wrappedService, Dispatcher dispatcher) 
            : base(dispatcher)
        {
            this.wrappedService = wrappedService;

            this.wrappedService.GetShopDetailsCompleted += (s, e) => this.DispatchEvent(this.GetShopDetailsCompleted, s, e);
            this.wrappedService.GetFeaturedSellersCompleted += (s, e) => this.DispatchEvent(this.GetFeaturedSellersCompleted, s, e);
            this.wrappedService.GetShopsByNameCompleted += (s, e) => this.DispatchEvent(this.GetShopsByNameCompleted, s, e);
            this.wrappedService.GetShopListingsCompleted += (s, e) => this.DispatchEvent(this.GetShopListingsCompleted, s, e);
            this.wrappedService.GetFeaturedDetailsCompleted += (s, e) => this.DispatchEvent(this.GetFeaturedDetailsCompleted, s, e);
        }

        /// <summary>
        /// Shop details by id completed event
        /// </summary>
        public event EventHandler<ResultEventArgs<Shops>> GetShopDetailsCompleted;

        /// <summary>
        /// Shop search by name completed event
        /// </summary>
        public event EventHandler<ResultEventArgs<Shops>> GetFeaturedSellersCompleted;

        /// <summary>
        /// Shop search by name completed event
        /// </summary>
        public event EventHandler<ResultEventArgs<Shops>> GetShopsByNameCompleted;

        /// <summary>
        /// Get shop listings completed event
        /// </summary>
        public event EventHandler<ResultEventArgs<Listings>> GetShopListingsCompleted;

        /// <summary>
        /// Get Featured details completed event
        /// </summary>
        public event EventHandler<ResultEventArgs<Listings>> GetFeaturedDetailsCompleted;

        /// <summary>
        /// Get the details of a seller's shop.
        /// </summary>
        /// <param name="userId">the id of the shop</param>
        /// <param name="detailLevel">the level of detail</param>
        /// <returns>the async state</returns>
        public IAsyncResult GetShopDetails(int userId, DetailLevel detailLevel)
        {
            return this.wrappedService.GetShopDetails(userId, detailLevel);
        }

        /// <summary>
        /// Get a list of all the featured sellers.
        /// </summary>
        /// <param name="offset">the search results offset</param>
        /// <param name="limit">the search limit</param>
        /// <param name="detailLevel">the level of detail</param>
        /// <returns>the async state</returns>
        public IAsyncResult GetFeaturedSellers(int offset, int limit, DetailLevel detailLevel)
        {
            return this.wrappedService.GetFeaturedSellers(offset, limit, detailLevel);
        }

        /// <summary>
        /// Get all the listings in a shop.
        /// </summary>
        /// <param name="userId">the user id</param>
        /// <param name="sortOn">field to sort on</param>
        /// <param name="sortOrder">sort ascending or descending</param>
        /// <param name="sectionId">shop section to show</param>
        /// <param name="offset">the search results offset</param>
        /// <param name="limit">the search limit</param>
        /// <param name="detailLevel">the level of detail</param>
        /// <returns>the async state</returns>
        public IAsyncResult GetShopListings(int userId, SortField sortOn, SortOrder sortOrder, int? sectionId, int offset, int limit, DetailLevel detailLevel)
        {
            return this.wrappedService.GetShopListings(userId, sortOn, sortOrder, sectionId, offset, limit, detailLevel);
        }

        /// <summary>
        /// Get all the listings in a shop.
        /// </summary>
        /// <param name="userName">the user name</param>
        /// <param name="sortOn">field to sort on</param>
        /// <param name="sortOrder">sort ascending or descending</param>
        /// <param name="sectionId">shop section to show</param>
        /// <param name="offset">the search results offset</param>
        /// <param name="limit">the search limit</param>
        /// <param name="detailLevel">the level of detail</param>
        /// <returns>the async state</returns>
        public IAsyncResult GetShopListings(string userName, SortField sortOn, SortOrder sortOrder, int? sectionId, int offset, int limit, DetailLevel detailLevel)
        {
            return this.wrappedService.GetShopListings(userName, sortOn, sortOrder, sectionId, offset, limit, detailLevel);
        }

        /// <summary>
        /// Get the expanded details on featured listings of a shop, ordered by highest ranked featured item.
        /// </summary>
        /// <param name="userName">the user name</param>
        /// <param name="detailLevel">the level of detail</param>
        /// <returns>the async state</returns>
        public IAsyncResult GetFeaturedDetails(string userName, DetailLevel detailLevel)
        {
            return this.wrappedService.GetFeaturedDetails(userName, detailLevel);
        }

        /// <summary>
        /// Get the expanded details on featured listings of a shop, ordered by highest ranked featured item.
        /// </summary>
        /// <param name="userId">the user id</param>
        /// <param name="detailLevel">the level of detail</param>
        /// <returns>the async state</returns>
        public IAsyncResult GetFeaturedDetails(int userId, DetailLevel detailLevel)
        {
            return this.wrappedService.GetFeaturedDetails(userId, detailLevel);
        }

        /// <summary>
        /// Search all active shops sorted alphabetically by user_name.
        /// </summary>
        /// <param name="searchName">the name to search for</param>
        /// <param name="sortOrder">the sort order</param>
        /// <param name="offset">the search results offset</param>
        /// <param name="limit">the search limit</param>
        /// <param name="detailLevel">the level of detail</param>
        /// <returns>the async state</returns>
        public IAsyncResult GetShopsByName(string searchName, SortOrder sortOrder, int offset, int limit, DetailLevel detailLevel)
        {
            return this.wrappedService.GetShopsByName(searchName, sortOrder, offset, limit, detailLevel);
        }
    }
}
