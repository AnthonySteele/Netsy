//-----------------------------------------------------------------------
// <copyright file="IShopService.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.Interfaces
{
    using System;

    using Netsy.DataModel;
    using Netsy.DataModel.ListingData;
    using Netsy.DataModel.ShopData;
    using Netsy.Helpers;

    /// <summary>
    /// Interface to Etsy Shop API
    /// </summary>
    public interface IShopService
    {
        /// <summary>
        /// Shop details by id completed event
        /// </summary>
        event EventHandler<ResultEventArgs<Shops>> GetShopDetailsCompleted;

        /// <summary>
        /// Shop search by name completed event
        /// </summary>
        event EventHandler<ResultEventArgs<Shops>> GetFeaturedSellersCompleted;

        /// <summary>
        /// Shop search by name completed event
        /// </summary>
        event EventHandler<ResultEventArgs<Shops>> GetShopsByNameCompleted;

        /// <summary>
        /// Get shop listings completed event
        /// </summary>
        event EventHandler<ResultEventArgs<Listings>> GetShopListingsCompleted;

        /// <summary>
        /// Get Featured details completed event
        /// </summary>
        event EventHandler<ResultEventArgs<Listings>> GetFeaturedDetailsCompleted;

        /// <summary>
        /// Get the details of a seller's shop.
        /// </summary>
        /// <param name="userId">the id of the shop</param>
        /// <param name="detailLevel">the level of detail</param>
        /// <returns>the async state</returns>
        IAsyncResult GetShopDetails(int userId, DetailLevel detailLevel);

        /// <summary>
        /// Get a list of all the featured sellers.
        /// </summary>
        /// <param name="offset">the search results offset</param>
        /// <param name="limit">the search limit</param>
        /// <param name="detailLevel">the level of detail</param>
        /// <returns>the async state</returns>
        IAsyncResult GetFeaturedSellers(int offset, int limit, DetailLevel detailLevel);

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
        IAsyncResult GetShopListings(int userId, SortField sortOn, SortOrder sortOrder, int? sectionId, int offset, int limit, DetailLevel detailLevel);
        
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
        IAsyncResult GetShopListings(string userName, SortField sortOn, SortOrder sortOrder, int? sectionId, int offset, int limit, DetailLevel detailLevel);

        /// <summary>
        /// Get the expanded details on featured listings of a shop, ordered by highest ranked featured item.
        /// </summary>
        /// <param name="userName">the user name</param>
        /// <param name="detailLevel">the level of detail</param>
        /// <returns>the async state</returns>
        IAsyncResult GetFeaturedDetails(string userName, DetailLevel detailLevel);

        /// <summary>
        /// Get the expanded details on featured listings of a shop, ordered by highest ranked featured item.
        /// </summary>
        /// <param name="userId">the user id</param>
        /// <param name="detailLevel">the level of detail</param>
        /// <returns>the async state</returns>
        IAsyncResult GetFeaturedDetails(int userId, DetailLevel detailLevel);

        /// <summary>
        /// Search all active shops sorted alphabetically by user_name.
        /// </summary>
        /// <param name="searchName">the name to search for</param>
        /// <param name="sortOrder">the sort order</param>
        /// <param name="offset">the search results offset</param>
        /// <param name="limit">the search limit</param>
        /// <param name="detailLevel">the level of detail</param>
        /// <returns>the async state</returns>
        IAsyncResult GetShopsByName(string searchName, SortOrder sortOrder, int offset, int limit, DetailLevel detailLevel);
    }
}
