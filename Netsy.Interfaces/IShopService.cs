//-----------------------------------------------------------------------
// <copyright file="IShopService.cs" company="AFS">
// Copyright (c) AFS. All rights reserved.
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.Interfaces
{
    using System;

    using DataModel;
    using DataModel.ShopData;

    using Helpers;

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
        /// Search all active shops sorted alphabetically by user_name.
        /// </summary>
        /// <param name="searchName">the name to search for</param>
        /// <param name="offset">the search results offset</param>
        /// <param name="sortOrder">the sort order</param>
        /// <param name="limit">the search limit</param>
        /// <param name="detailLevel">the level of detail</param>
        /// <returns>the async state</returns>
        IAsyncResult GetShopsByName(string searchName, SortOrder sortOrder, int offset, int limit, DetailLevel detailLevel);
    }
}
