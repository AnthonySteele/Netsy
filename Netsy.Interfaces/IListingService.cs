//-----------------------------------------------------------------------
// <copyright file="IListingService.cs" company="AFS">
//  This source code is part of Netsy http://github.com/AnthonySteele/Netsy/
//  and is made available under the terms of the Microsoft Public License (Ms-PL)
//  http://www.opensource.org/licenses/ms-pl.html
// </copyright>
//----------------------------------------------------------------------- 

namespace Netsy.Interfaces
{
    using System;
    using System.Collections.Generic;

    using DataModel;
    using DataModel.ListingData;

    using Helpers;

    /// <summary>
    /// Interface to listing functions on the Etsy API
    /// </summary>
    public interface IListingService
    {
        /// <summary>
        /// GetListingDetails completed event
        /// </summary>
        event EventHandler<ResultEventArgs<Listings>> GetListingDetailsCompleted;

        /// <summary>
        /// GetAllListingscompleted event
        /// </summary>
        event EventHandler<ResultEventArgs<Listings>> GetAllListingsCompleted;

        /// <summary>
        /// GetListingsByCategory completed event
        /// </summary>
        event EventHandler<ResultEventArgs<Listings>> GetListingsByCategoryCompleted;

        /// <summary>
        /// GetListingsByColor completed event
        /// </summary>
        event EventHandler<ResultEventArgs<Listings>> GetListingsByColorCompleted;

        /// <summary>
        /// GetListingsByColorAndKeywords completed event
        /// </summary>
        event EventHandler<ResultEventArgs<Listings>> GetListingsByColorAndKeywordsCompleted;

        /// <summary>
        /// GetFrontFeaturedListings completed event
        /// </summary>
        event EventHandler<ResultEventArgs<Listings>> GetFrontFeaturedListingsCompleted;

        /// <summary>
        /// GetListingsByKeyword completed event
        /// </summary>
        event EventHandler<ResultEventArgs<Listings>> GetListingsByKeywordCompleted;

        /// <summary>
        /// GetListingsByMaterials completed event
        /// </summary>
        event EventHandler<ResultEventArgs<Listings>> GetListingsByMaterialsCompleted;

        /// <summary>
        /// GetListingsByTags completed event
        /// </summary>
        event EventHandler<ResultEventArgs<Listings>> GetListingsByTagsCompleted;

        /// <summary>
        /// Get the details of a listing.
        /// </summary>
        /// <param name="listingId"></param>
        /// <param name="detailLevel"></param>
        /// <returns></returns>
        IAsyncResult GetListingDetails(int listingId, DetailLevel detailLevel);

        /// <summary>
        /// Get all active listings on Etsy.
        /// </summary>
        /// <param name="sortOn"></param>
        /// <param name="sortOrder"></param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <param name="detailLevel"></param>
        /// <returns></returns>
        IAsyncResult GetAllListings(SortField sortOn, SortOrder sortOrder, int offset, int limit, DetailLevel detailLevel);
        
        /// <summary>
        /// Search for listings by category.
        /// </summary>
        /// <param name="category"></param>
        /// <param name="sortOrder"></param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <param name="detailLevel"></param>
        /// <returns></returns>
        IAsyncResult GetListingsByCategory(string category, SortOrder sortOrder, int offset, int limit, DetailLevel detailLevel);
        
        /// <summary>
        /// Search for listings by average color of primary image.
        /// </summary>
        /// <param name="color"></param>
        /// <param name="wiggle"></param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <param name="detailLevel"></param>
        /// <returns></returns>
        IAsyncResult GetListingsByColor(string color, int wiggle, int offset, int limit, DetailLevel detailLevel);
        
        /// <summary>
        /// Search for listings by keywords and average color of primary image.
        /// </summary>
        /// <param name="keywords"></param>
        /// <param name="color"></param>
        /// <param name="wiggle"></param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <param name="detailLevel"></param>
        /// <returns></returns>
        IAsyncResult GetListingsByColorAndKeywords(IEnumerable<string> keywords, string color, int wiggle, int offset, int limit, DetailLevel detailLevel);
        
        /// <summary>
        /// Get the featured listings on the front page for the current day.
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <param name="detailLevel"></param>
        /// <returns></returns>
        IAsyncResult GetFrontFeaturedListings(int offset, int limit, DetailLevel detailLevel);
        
        /// <summary>
        /// Search for listings by keyword.
        /// </summary>
        /// <param name="searchTerms"></param>
        /// <param name="sortOn"></param>
        /// <param name="sortOrder"></param>
        /// <param name="minPrince"></param>
        /// <param name="maxPrice"></param>
        /// <param name="searchDescription"></param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <param name="detailLevel"></param>
        /// <returns></returns>
        IAsyncResult GetListingsByKeyword(IEnumerable<string> searchTerms, SortField sortOn, SortOrder sortOrder, decimal minPrince, decimal maxPrice, bool searchDescription, int offset, int limit, DetailLevel detailLevel);
        
        /// <summary>
        /// Search for listings by materials used.
        /// </summary>
        /// <param name="materials"></param>
        /// <param name="sortOn"></param>
        /// <param name="sortOrder"></param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <param name="detailLevel"></param>
        /// <returns></returns>
        IAsyncResult GetListingsByMaterials(IEnumerable<string> materials, SortField sortOn, SortOrder sortOrder, int offset, int limit, DetailLevel detailLevel);
        
        /// <summary>
        /// Search for listings by tags.
        /// </summary>
        /// <param name="tags"></param>
        /// <param name="sortOn"></param>
        /// <param name="sortOrder"></param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <param name="detailLevel"></param>
        /// <returns></returns>
        IAsyncResult GetListingsByTags(IEnumerable<string> tags, SortField sortOn, SortOrder sortOrder, int offset, int limit, DetailLevel detailLevel);
    }
}
